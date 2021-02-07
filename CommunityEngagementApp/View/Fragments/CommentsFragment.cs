using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

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

        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           
        }

        public override void OnViewCreated(Android.Views.View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
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
