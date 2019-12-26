using System.Diagnostics;
using Prism.Mvvm;
using Prism.Navigation;

namespace Vocabulary.ViewModels
{
    public class BaseViewModel : BindableBase, INavigatedAware, IInitialize, IDestructible
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Debug.WriteLine(this.GetType());
        }

        protected INavigationService NavigationService { get; }

        public virtual void Initialize(INavigationParameters parameters)
        {
            //method is called when VM is initializing but after ctr
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            //method is called when the page disappears
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            //method is called when the page appears
        }

        public virtual void Destroy()
        {
            //method is called when the page is deleted from the navigation stack
        }
    }
}
