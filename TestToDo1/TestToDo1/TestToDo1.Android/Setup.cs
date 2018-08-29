using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using TestToDo1.Core.IRepository;
using TestToDo1.Droid.Services;

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
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
