using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ConfirmPlaylistDifficulty.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        private bool enable = true;
        public virtual bool Enable
        {
            get => this.enable;
            set
            {
                if (this.enable != value)
                {
                    this.enable = value;
                    if (value)
                    {
                        DataModel.RefreshButtonColor();
                    }
                    else
                    {
                        DataModel.ChangeStartButtonColor(toWarningColor: false);
                    }
                }

            }
        }
    }
}