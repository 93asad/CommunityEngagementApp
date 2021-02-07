using System;

namespace CommunityEngagementApp.Fragments
{
    public abstract class BaseFragment : AndroidX.Fragment.App.Fragment
    {
        public interface IBaseDelegeate
        {
            void SetToolbarTitle(string title);
        }

        public IBaseDelegeate Delegate { get; set; }
        protected abstract string Title { get; }

        public BaseFragment()
        { }

        public BaseFragment(IntPtr intPtr,
            Android.Runtime.JniHandleOwnership transfer) : base(intPtr, transfer)
        { }

        public override void OnDetach()
        {
            base.OnDetach();
            Delegate = null;
        }

        public override void OnResume()
        {
            base.OnResume();
            Delegate?.SetToolbarTitle(Title);
        }

        public string GetTag()
            => GetType().Name.ToString();

        public static string GetTag<T>() where T : BaseFragment
            => typeof(T).Name.ToString();
    }
}
