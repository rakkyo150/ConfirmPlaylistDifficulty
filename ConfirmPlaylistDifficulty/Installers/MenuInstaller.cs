using ConfirmPlaylistDifficulty.Views;
using Zenject;

namespace ConfirmPlaylistDifficulty.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettingViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<PMEventHandler>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
