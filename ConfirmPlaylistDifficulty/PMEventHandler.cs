using BeatSaberPlaylistsLib.Types;
using ConfirmPlaylistDifficulty.Configuration;
using UnityEngine;

namespace ConfirmPlaylistDifficulty
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class PMEventHandler : MonoBehaviour
    {
        public static PMEventHandler Instance { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            DontDestroyOnLoad(this);
            Instance = this;
        }

        public void OnEnable()
        {
            PlaylistManager.Utilities.Events.playlistSongSelected += PlaylistSongSelected;
        }


        public void OnDisable()
        {
            PlaylistManager.Utilities.Events.playlistSongSelected -= PlaylistSongSelected;
        }

        public void PlaylistSongSelected(IPlaylistSong ps)
        {
            DataModel.playlistSong = ps;
            if (PluginConfig.Instance.ChangeColor || PluginConfig.Instance.ChangeText)
            {
                DataModel.RefreshPlayButton();
            }
        }
    }
}
