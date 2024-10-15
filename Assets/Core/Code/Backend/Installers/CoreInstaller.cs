using UnityEngine;
using Zenject;

public class CoreInstaller : Installer<CoreInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IMoodService>().To<MoodService>().AsSingle();
        Container.Bind<IRoomService>().To<RoomService>().AsSingle();
    }
}