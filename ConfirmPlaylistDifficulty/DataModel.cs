using System.Linq;
using BeatSaberPlaylistsLib.Types;
using HMUI;
using IPA.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ConfirmPlaylistDifficulty
{
    internal static class DataModel
    {
        internal static Color defaultColor;
        internal static Button _actionButton;
        internal static IDifficultyBeatmap difficultyBeatmap;
        internal static IPlaylistSong playlistSong = null;
        internal static SelectLevelCategoryViewController.LevelCategory selectedLevelCategory;

        public static void RefreshButtonColor()
        {
            var isTargetLevelSelected = playlistSong?.levelID == difficultyBeatmap?.level?.levelID;

            // プレイリストの難易度を選択していれば true
            var selectedCharasteristic = difficultyBeatmap?.parentDifficultyBeatmapSet?.beatmapCharacteristic?.serializedName;

            var isTargetDifficultySelected =
                selectedCharasteristic != null &&
                playlistSong
                    ?.Difficulties
                    ?.Any(x => x.Characteristic == selectedCharasteristic && x.BeatmapDifficulty == difficultyBeatmap?.difficulty)
                    == true;

            var shouldWarn = isTargetLevelSelected && !isTargetDifficultySelected;
            ChangeStartButtonColor(toWarningColor: shouldWarn);
        }

        public static void ChangeStartButtonColor(bool toWarningColor)
        {
            if (DataModel._actionButton?.IsDestroyed() != false)
            {
                Plugin.Log.Warn($"Action button is destroyed. Skipping start button color change.");
                return;
            }

            if (DataModel._actionButton.gameObject == null)
            {
                Plugin.Log.Warn($"DataModel._actionButton is null. Skipping start button color change.");
                return;
            }

            var bg = DataModel._actionButton.GetComponentsInChildren<ImageView>()
                ?.SingleOrDefault(x => x.name == "BG");

            if (bg == null)
            {
                Plugin.Log.Warn("Failed to find BG ImageView.");
                return;
            }

            if (toWarningColor)
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
