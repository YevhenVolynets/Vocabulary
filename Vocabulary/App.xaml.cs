using Prism.Ioc;
using Prism.Unity;
using Unity;

namespace Vocabulary
{
    public partial class App : PrismApplication
    {
        public static IUnityContainer UnityContainer { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            UnityContainer = Container.GetContainer();
            UnityContainer.AddExtension(new ForceActivation());
        }

        protected override void OnInitialized()
        {
            
        }
    }
}