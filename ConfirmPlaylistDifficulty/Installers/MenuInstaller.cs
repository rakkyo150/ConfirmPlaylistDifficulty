using ConfirmPlaylistDifficulty.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiraUtil;
using Zenject;

namespace ConfirmPlaylistDifficulty.Installers
{
    internal class MenuInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettingViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<PMEventHandler>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
