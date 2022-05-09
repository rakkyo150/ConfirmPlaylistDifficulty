using BeatSaberMarkupLanguage;
using BS_Utils.Utilities;
using IPA.Utilities;
using PlaylistManager.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using PlaylistManager.Utilities;
using Zenject;
using BeatSaberPlaylistsLib.Types;
using TMPro;
using HMUI;
using ConfirmPlaylistDifficulty.Configuration;

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

        public void Start()
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
            if (PluginConfig.Instance.Enable)
            {
                DataModel.RefreshButtonColor();
            }            
        }
    }
}
