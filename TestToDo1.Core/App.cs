using MvvmCross.Platform.IoC;
using TestToDo1.Core.ViewModels;

namespace TestToDo1.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
           
            RegisterAppStart(new AppStart());
        }
    }
}
