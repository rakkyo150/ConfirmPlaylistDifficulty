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
            // playlistSongのキャッシュがある場合
            if (DataModel.playlistSong != null && DataModel.difficultyBeatmap!=null && DataModel.characteristic!=null)
            {                   
                if(DataModel.playlistSong.levelID == DataModel.difficultyBeatmap.level.levelID)
                {
                    // プレイリストの難易度指定が無い場合
                    if (DataModel.playlistSong.Difficulties == null)
                    {
                        ChangeStartButtonColor(false);
                        return;
                    }
                    // プレイリストの難易度指定がある場合
                    else
                    {
                        foreach (var difficulty in DataModel.playlistSong.Difficulties)
                        {
                            // プレイリストの難易度を選択している場合
                            if (difficulty.Characteristic == DataModel.characteristic.ToString()
                                && difficulty.BeatmapDifficulty == DataModel.difficultyBeatmap.difficulty)
                            {
                                ChangeStartButtonColor(false);
                                return;
                            }
                        }

                        // プレイリストの難易度を選択していない場合
                        ChangeStartButtonColor(true);
                        return;
                    }
                }

                ChangeStartButtonColor(false);
            }
        }

        public static void ChangeStartButtonColor(bool red)
        {
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
