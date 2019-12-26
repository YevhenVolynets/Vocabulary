using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Vocabulary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Vocabulary.Common.Extensions
{
    public static class NavigationExtensions
    {
        public static void RegisterTypeForViewModelNavigation<TView, TViewModel>(this IContainerRegistry container) where TView : Page where TViewModel : BaseViewModel
        {
            var viewType = typeof(TView);
            ViewModelLocationProvider.Register(viewType.ToString(), typeof(TViewModel));
            container.RegisterForNavigation<TView>(typeof(TViewModel).Name);
        }

        public static async Task<INavigationResult> NavigateAsync<TViewModel>(this INavigationService navigationService, NavigationParameters parameters = null, bool? useModalNavigation = null, bool reset = false, bool animated = true)
        {
            var url = typeof(TViewModel).Name;
            if (reset)
                url = $"/{url}";
            return await navigationService.NavigateAsync(url, parameters, useModalNavigation, animated);
        }

        static string CreateNavigationUrl(bool reset = false, params Type[] types)
        {
            var url = string.Join("/", types.Select(t => t.Name));
            if (reset)
                url = $"/{url}";
            return url;
        }

        public static async Task<INavigationResult> NavigateAsync(this INavigationService navigationService, bool reset = false, NavigationParameters parameters = null, params Type[] types)
        {
            var url = CreateNavigationUrl(reset, types);
            return await navigationService.NavigateAsync(url, parameters, animated: reset);
        }

        public static async Task ResetWithNavigationRoot<TViewModel>(
             this INavigationService navigationService,
             INavigationParameters parameters = null,
             bool animated = true)
        {
            var resetNavigationString = $"/{typeof(NavigationPage).Name}/{typeof(TViewModel).Name}";
            await navigationService.NavigateAsync(
             new Uri(
              resetNavigationString,
              UriKind.Absolute),
             parameters,
             animated);
        }

        public static async Task<INavigationResult> BackToPage<T>(
            this INavigationService navigationService,
            bool animated = true)
        {
            var url = navigationService.GetNavigationUriPath();
            var vms = url.Remove(0, 1).Split('/');
            var index = vms.IndexOf(typeof(T).Name);
            var goBackCount = vms.Length - 1 - index;
            var urlBuilder = new StringBuilder();
            for (int i = 0; i < goBackCount; i++)
                urlBuilder.Append("../");

            return await navigationService.NavigateAsync(urlBuilder.ToString(), animated: animated);
        }

        public static async Task<INavigationResult> ChangeTopPage(
            this INavigationService navigationService,
            Type viewModel,
            INavigationParameters parameters = null,
            bool? useModalNavigation = null,
            bool animated = true)
        {
            return await navigationService.NavigateAsync($"../{viewModel.Name}", parameters, useModalNavigation, animated);
        }

        public static async Task<INavigationResult> ChangeTopPage<T>(
            this INavigationService navigationService,
            INavigationParameters parameters = null,
            bool? useModalNavigation = null,
            bool animated = true) where T : BindableBase
        {
            return await navigationService.ChangeTopPage(typeof(T), parameters, useModalNavigation, animated);
        }
    }
}
