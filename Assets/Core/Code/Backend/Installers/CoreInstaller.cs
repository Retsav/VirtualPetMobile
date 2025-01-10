using UnityEngine;
using Zenject;

public class CoreInstaller : Installer<CoreInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IMoodService>().To<MoodService>().AsSingle();
        Container.Bind<IRoomService>().To<RoomService>().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IPetService>().To<PetService>().AsSingle();
        Container.Bind<IMinigameService>().To<MinigameService>().AsSingle();
        Container.Bind<ISceneManagmentService>().To<SceneManagmentService>().AsSingle();
        Container.Bind<IPopupService>().To<PopupService>().AsSingle();
    }
}