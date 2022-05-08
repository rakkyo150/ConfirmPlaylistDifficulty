using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using ConfirmPlaylistDifficulty.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace ConfirmPlaylistDifficulty.Views
{
    [HotReload]
    internal class SettingViewController : BSMLResourceViewController, IInitializable
    {
        public override string ResourceName => "ConfirmPlaylistDifficulty.Views.SettingView.bsml";

        [UIValue("enable")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }

        protected override void OnDestroy()
        {
            GameplaySetup.instance.RemoveTab("Confirm Playlist Difficulty");
            base.OnDestroy();
        }


        public void Initialize()
        {
            Plugin.Log.Debug(ResourceName);
            GameplaySetup.instance.AddTab("Confirm Playlist Difficulty",ResourceName,this);
        }
    }
}
