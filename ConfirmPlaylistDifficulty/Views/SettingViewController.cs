using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using ConfirmPlaylistDifficulty.Configuration;
using Zenject;

namespace ConfirmPlaylistDifficulty.Views
{
    [HotReload]
    internal class SettingViewController : BSMLAutomaticViewController, IInitializable
    {
        public string ResourceName => "ConfirmPlaylistDifficulty.Views.SettingView.bsml";

        [UIValue("changeColor")]
        public bool ChangeColor
        {
            get => PluginConfig.Instance.ChangeColor;
            set => PluginConfig.Instance.ChangeColor = value;
        }

        [UIValue("changeText")]
        public bool ChangeText
        {
            get => PluginConfig.Instance.ChangeText;
            set => PluginConfig.Instance.ChangeText = value;
        }

        [UIValue("cantClick")]
        public bool CantClick
        {
            get => PluginConfig.Instance.CantClick;
            set => PluginConfig.Instance.CantClick = value;
        }

        protected override void OnDestroy()
        {
            GameplaySetup.instance.RemoveTab("Confirm Playlist Difficulty");
            base.OnDestroy();
        }


        public void Initialize()
        {
            GameplaySetup.instance.AddTab("Confirm Playlist Difficulty", ResourceName, this);
        }
    }
}
