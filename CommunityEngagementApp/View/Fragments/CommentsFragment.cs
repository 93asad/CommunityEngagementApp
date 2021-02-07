using System;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Lifecycle;
using AndroidX.RecyclerView.Widget;
using CommunityEngagementApp.View.Adapters;
using CommunityEngagementApp.ViewModel;
using Google.Android.Material.FloatingActionButton;

namespace CommunityEngagementApp.Fragments
{
    public class CommentsFragment : BaseFragment
    {
        public interface IDelegate : IBaseDelegeate
        {
        }

        public new IDelegate Delegate
        {
            get => base.Delegate as IDelegate;
            set => base.Delegate = value;
        }

        Guid? _postGuid;

        public CommentsFragment()
        {
        }

        public CommentsFragment(Guid postGuid)
        {
            _postGuid = postGuid;
        }

        protected override string Title => "Comments";

        CommentsFragmentViewModel _viewModel;
        RecyclerView _recyclerView;
        CommentsAdapter _commentsAdapter;
        FloatingActionButton _addButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _viewModel = (CommentsFragmentViewModel)new ViewModelProvider(
                Activity, new CommentsFragmentViewModelFactory(App.DataManager))
                .Get(Java.Lang.Class.FromType(typeof(CommentsFragmentViewModel)));

            if (_viewModel.PostGuid != _postGuid && _postGuid != null)
            {
                _viewModel.PostGuid = _postGuid;
                _viewModel.Comments = null;
            }
        }

        public override void OnViewCreated(Android.Views.View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.comments);
            _addButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab); 

            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            _recyclerView.SetItemAnimator(new DefaultItemAnimator());

            _commentsAdapter = new CommentsAdapter();
            _recyclerView.SetAdapter(_commentsAdapter);

            Task.Run(SetCommentsAsync);
        }

        public override void OnResume()
        {
            base.OnResume();
            _addButton.Click += OnAddButtonClicked;
        }

        public override void OnPause()
        {
            base.OnPause();
            _addButton.Click -= OnAddButtonClicked;
        }

        void OnAddButtonClicked(object sender, EventArgs e)
        {
            var inputDialog = new AlertDialog.Builder(Activity);
            EditText userInput = new EditText(Activity);

            inputDialog.SetTitle(GetString(Resource.String.add_comment_title));
            inputDialog.SetView(userInput);
            inputDialog.SetPositiveButton(
                GetString(Android.Resource.String.Ok),
                (see, ess) =>
                {
                    if (userInput.Text != string.Empty && userInput.Text != "0")
                    {
                        OnCommentInputClosed(userInput.Text);
                    }
                });

            inputDialog.Show();
        }

        void OnCommentInputClosed(string text)
        {
            if (_viewModel.AddCommentAsync(text))
            {
                Activity.RunOnUiThread(delegate
                {
                    _commentsAdapter.SetComments(_viewModel.Comments);
                    _commentsAdapter.NotifyItemInserted(0);
                });
            }
        }

        async Task SetCommentsAsync()
        {
            if (_viewModel.Comments == null || _viewModel.Comments.Count == 0)
            {
                _viewModel.Comments = await _viewModel.FetchCommentsAsync();
            }

            if (_viewModel.Comments != null && _viewModel.Comments.Count > 0)
            {
                Activity.RunOnUiThread(delegate
                {
                    _commentsAdapter.SetComments(_viewModel.Comments);
                    _commentsAdapter.NotifyDataSetChanged();
                });
            }
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);

            if (context is IDelegate postsFragmentDelegate)
            {
                Delegate = postsFragmentDelegate;
            }
        }

        public override Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            => inflater.Inflate(Resource.Layout.fragment_comments, container, false);
    }
}
