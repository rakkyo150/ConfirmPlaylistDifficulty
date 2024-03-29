﻿using BeatSaberPlaylistsLib.Types;
using ConfirmPlaylistDifficulty.Configuration;
using HMUI;
using IPA.Utilities;
using System.Linq;
using TMPro;
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
        internal static string actionButtonText;
        internal static TextMeshProUGUI _actionButtonText;
        internal static string defaultText;

        internal static void RefreshPlayButton()
        {
            bool shouldWarn;

            // プレイリストの難易度指定が無い場合
            if (playlistSong?.Difficulties == null)
            {
                shouldWarn = false;
            }
            else
            {
                var isTargetLevelSelected = playlistSong?.levelID == difficultyBeatmap?.level?.levelID;

                var selectedCharasteristic = difficultyBeatmap?.parentDifficultyBeatmapSet?.beatmapCharacteristic?.serializedName;

                var isTargetDifficultySelected =
                    selectedCharasteristic != null &&
                    playlistSong
                        ?.Difficulties
                        ?.Any(x => x.Characteristic == selectedCharasteristic && x.BeatmapDifficulty == difficultyBeatmap?.difficulty)
                        == true;

                shouldWarn = isTargetLevelSelected && !isTargetDifficultySelected;
            }

            if (PluginConfig.Instance.CantClick)
            {
                // 注意：ButtonのinteractableをfalseにしているとImageViewを取得できず色を変更出来なくなる
                ChangePlayButtonInteractable(toWarning: shouldWarn);
            }
            if (PluginConfig.Instance.ChangeColor)
            {
                ChangePlayButtonColor(toWarning: shouldWarn);
            }
            if (PluginConfig.Instance.ChangeText)
            {
                ChangePlayButtonText(toWarning: shouldWarn);
            }
        }

        internal static void ChangePlayButtonColor(bool toWarning)
        {
            // いきなりプレイリストの曲を選ぶ場合、_actionButtonが生焼けなのにPlaylistManagerのイベントが発火してしまう
            if (DataModel._actionButton?.IsDestroyed() != false)
            {
                Plugin.Log.Warn($"Action button is destroyed. Skip starting button change.");
                return;
            }

            if (DataModel._actionButton?.gameObject == null)
            {
                Plugin.Log.Warn($"DataModel._actionButton is null. Skip starting button change.");
                return;
            }

            var bg = DataModel._actionButton?.GetComponentsInChildren<ImageView>()
                ?.SingleOrDefault(x => x.name == "BG");

            if (bg == null)
            {
                Plugin.Log.Warn("Failed to find BG ImageView.");
                return;
            }

            if (toWarning)
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

        internal static void ChangePlayButtonText(bool toWarning)
        {
            if (DataModel._actionButtonText?.IsDestroyed() != false)
            {
                Plugin.Log.Warn($"Action button text is destroyed. Skip starting button text change.");
                return;
            }

            if (DataModel.defaultText == null)
            {
                Plugin.Log.Warn($"DataModel.defaultText is null. Skip starting button text change.");
                return;
            }

            if (PluginConfig.Instance.ChangeText)
            {
                if (toWarning) DataModel._actionButtonText.text = PluginConfig.Instance.WarnPlayButtonText;
                else DataModel._actionButtonText.text = PluginConfig.Instance.NormalPlayButtonText;
            }
            else
            {
                DataModel._actionButtonText.text = DataModel.defaultText;
            }
        }

        internal static void ChangePlayButtonInteractable(bool toWarning)
        {
            if (DataModel._actionButton?.IsDestroyed() != false)
            {
                Plugin.Log.Warn($"Action button is destroyed. Skip starting button change.");
                return;
            }

            if (DataModel._actionButton?.gameObject == null)
            {
                Plugin.Log.Warn($"DataModel._actionButton is null. Skip starting button change.");
                return;
            }

            if (toWarning) DataModel._actionButton.interactable = false;
            else DataModel._actionButton.interactable = true;
        }
    }
}
