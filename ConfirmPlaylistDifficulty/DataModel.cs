using BeatSaberPlaylistsLib.Types;
using HMUI;
using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ConfirmPlaylistDifficulty
{
    internal static class DataModel
    {
        internal static Color defaultColor;
        internal static Button _actionButton;
        internal static IDifficultyBeatmap difficultyBeatmap;
        internal static SongDetailsCache.Structs.MapCharacteristic characteristic;
        internal static IPlaylistSong playlistSong = null;

        public static void RefreshButtonColor()
        {
            bool red = true;

            if (DataModel.playlistSong != null)
            {
                if (DataModel.playlistSong.Difficulties != null)
                {
                    foreach (var difficulty in DataModel.playlistSong.Difficulties)
                    {
                        Plugin.Log.Debug("PlaylistSongSelected : " + difficulty.Characteristic);
                        Plugin.Log.Debug("PlaylistSongSelected : " + difficulty.BeatmapDifficulty.ToString());

                        Plugin.Log.Debug("Harmony : " + DataModel.characteristic.ToString());
                        Plugin.Log.Debug("Harmony : " + DataModel.difficultyBeatmap.difficulty.ToString());

                        if (difficulty.Characteristic == DataModel.characteristic.ToString() && difficulty.BeatmapDifficulty == DataModel.difficultyBeatmap.difficulty)
                        {
                            red = false;
                            break;
                        }
                    }
                    ChangeStartButtonColor(red);
                }
                else
                {
                    ChangeStartButtonColor(false);
                }
            }
        }

        public static void ChangeStartButtonColor(bool red)
        {
            // StandardLevelDetailView standardLevelDetailView = Resources.FindObjectsOfTypeAll<StandardLevelDetailView>().FirstOrDefault<StandardLevelDetailView>();
            foreach (var bg in DataModel._actionButton.GetComponentsInChildren<ImageView>())
            {
                if (bg.name == "BG")
                {
                    if (red)
                    {
                        bg.color = Color.red;
                        bg.SetField("_gradient", false);
                    }
                    else
                    {
                        bg.color = DataModel.defaultColor;
                        bg.SetField("_gradient", true);
                    }
                }
            }
        }
    }
}
