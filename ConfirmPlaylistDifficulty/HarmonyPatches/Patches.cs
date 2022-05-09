using ConfirmPlaylistDifficulty.Configuration;
using HarmonyLib;
using HMUI;
using IPA.Utilities;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// See https://github.com/pardeike/Harmony/wiki for a full reference on Harmony.
/// </summary>
namespace ConfirmPlaylistDifficulty.HarmonyPatches
{
    /// <summary>
    /// This patches ClassToPatch.MethodToPatch(Parameter1Type arg1, Parameter2Type arg2)
    /// </summary>
    [HarmonyPatch(typeof(StandardLevelDetailView), nameof(StandardLevelDetailView.RefreshContent))]
    public class Patches
    {
        /// <summary>
        /// This code is run after the original code in MethodToPatch is run.
        /// </summary>
        /// <param name="__instance">The instance of ClassToPatch</param>
        /// <param name="arg1">The Parameter1Type arg1 that was passed to MethodToPatch</param>
        /// <param name="____privateFieldInClassToPatch">Reference to the private field in ClassToPatch named '_privateFieldInClassToPatch', 
        ///     added three _ to the beginning to reference it in the patch. Adding ref means we can change it.</param>
        static void Postfix(IBeatmapLevel ____level, IDifficultyBeatmap ____selectedDifficultyBeatmap, LevelParamsPanel ____levelParamsPanel, StandardLevelDetailView __instance)
        {
            DataModel._actionButton = __instance.actionButton;

            foreach (var bg in DataModel._actionButton.GetComponentsInChildren<ImageView>())
            {
                if (bg.name == "BG" && DataModel.defaultColor == new Color(0f, 0f, 0f, 0f))
                {
                    DataModel.defaultColor = bg.color;
                }
            }

            DataModel.difficultyBeatmap = ____selectedDifficultyBeatmap;
            Enum.TryParse(DataModel.difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.serializedName, out DataModel.characteristic);

            SelectLevelCategoryViewController selectLevelCategoryViewController = Resources.FindObjectsOfTypeAll<SelectLevelCategoryViewController>().First();

            if (PluginConfig.Instance.Enable)
            {
                if (selectLevelCategoryViewController.selectedLevelCategory != SelectLevelCategoryViewController.LevelCategory.CustomSongs)
                {
                    if (DataModel.defaultColor != null && DataModel._actionButton != null)
                    {
                        foreach (var bg in DataModel._actionButton.GetComponentsInChildren<ImageView>())
                        {
                            if (bg.name == "BG")
                            {
                                if (bg.color == Color.red)
                                {
                                    bg.color = DataModel.defaultColor;
                                    bg.SetField("_gradient", true);
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataModel.RefreshButtonColor();
                }
            }
        }
    }
}