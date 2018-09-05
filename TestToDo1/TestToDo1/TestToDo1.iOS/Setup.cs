using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using System.IO;
using TestToDo1.Core.IRepository;
using TestToDo1.Core.ViewModels;
using TestToDo1.iOS.Services;
using UIKit;

namespace TestToDo1.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(IMvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public Setup(IMvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.RegisterSingleton<IUserRepository>(() => new UserRepository());
            Mvx.RegisterSingleton<IItemRepository>(() => new ItemRepository());

            string filePath = Path.Combine(System.Environment.GetFolderPath(
                                           System.Environment.SpecialFolder.Personal), "ToDoUser.txt");
            if (File.Exists(filePath))
            {
                string user = File.ReadAllText(Path.Combine(System.Environment.GetFolderPath(
                                           System.Environment.SpecialFolder.Personal), "ToDoUser.txt"));

                string[] tempUser = user.Split('`');
                string userLogin = tempUser[0];
                string userPassword = tempUser[1];

                SignViewModel.UserCurrent = Mvx.Resolve<IUserRepository>().GetUserByData(userLogin, userPassword);
            }
            return new Core.App();
        }
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            return new MvxSidebarPresenter((MvxApplicationDelegate)ApplicationDelegate, Window);
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint(MvxPanelEnum.Center));
        }
    }
}
