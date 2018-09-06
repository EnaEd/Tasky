using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using TestToDo1.Core.IRepository;
using TestToDo1.Droid.Services;
using System.IO;
using TestToDo1.Core.ViewModels;

namespace TestToDo1.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public const string DATABASENAME = "MyDatabase4.db";
        public Setup(Context applicationContext) : base(applicationContext)
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
    }
}
