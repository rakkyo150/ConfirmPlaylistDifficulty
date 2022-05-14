using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ConfirmPlaylistDifficulty.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        private bool changeColor = true;
        public virtual bool ChangeColor
        {
            get => this.changeColor;
            set
            {
                if (this.changeColor != value)
                {
                    this.changeColor = value;
                    if (value)
                    {
                        DataModel.RefreshPlayButton();
                    }
                    else
                    {
                        DataModel.ChangePlayButtonColor(toWarning: false);
                    }
                }

            }
        }

        private bool changeText = true;
        public bool ChangeText
        {
            get => this.changeText;
            set
            {
                this.changeText = value;
                if (value)
                {
                    DataModel.RefreshPlayButton();
                }
                else
                {
                    DataModel.ChangePlayButtonText(toWarning: false);
                }
            }
        }

        public string WarnPlayButtonText { get; set; } = "(>_<)";
        public string NormalPlayButtonText { get; set; } = "(⁎ᵕᴗᵕ⁎)";

        private bool cantClick = true;
        public bool CantClick
        {
            get => this.cantClick;
            set
            {
                this.cantClick = value;
                if (value)
                {
                    DataModel.RefreshPlayButton();
                }
                else
                {
                    DataModel.ChangePlayButtonInteractable(toWarning: false);
                    
                    // ButtonのinteractableをfalseにしているとImageViewを取得できず色を変更出来ないので
                    if (changeColor)
                    {
                        DataModel.RefreshPlayButton();
                    }
                    else
                    {
                        DataModel.ChangePlayButtonColor(toWarning: false);
                    }
                }
            }
        }
    }
}