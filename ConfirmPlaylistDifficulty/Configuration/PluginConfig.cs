using IPA.Config.Stores;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ConfirmPlaylistDifficulty.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        private bool enable = true;
        public virtual bool Enable
        {
            get { return enable; }
            set
            {
                // DataModel.defaultColor != nullやDataModel._actionButton != nullとなることはないはずだけど一応
                if (value == false && DataModel.defaultColor != null && DataModel._actionButton != null)
                {
                    DataModel.ChangeStartButtonColor(false);
                }
                else if (value == true && DataModel.defaultColor != null && DataModel._actionButton != null)
                {
                    DataModel.RefreshButtonColor();
                }

                enable = value;
            }
        }
    }
}