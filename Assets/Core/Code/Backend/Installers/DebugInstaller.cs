using UnityEngine;
using Zenject;

public class DebugInstaller : Installer<DebugInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ITestService>().To<TestService>().AsSingle();
    }
}