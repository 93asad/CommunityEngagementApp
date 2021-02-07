
using System;
using Android.Content;
using Android.OS;
using Android.Views;

namespace CommunityEngagementApp.Fragments
{
    public class PostsFragment : BaseFragment
    {
        public interface IDelegate : IBaseDelegeate
        {
            void OnCommentButtonClicked(Guid postGuid);
        }

        public new IDelegate Delegate
        {
            get => base.Delegate as IDelegate;
            set => base.Delegate = value;
        }

        protected override string Title => "Posts";

        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           
        }

        public override void OnViewCreated(Android.Views.View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

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
            => inflater.Inflate(Resource.Layout.fragment_posts, container, false);

        
    }
}
