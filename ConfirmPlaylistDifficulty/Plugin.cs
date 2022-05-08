using BS_Utils.Utilities;
using ConfirmPlaylistDifficulty.Configuration;
using ConfirmPlaylistDifficulty.Installers;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Config = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace ConfirmPlaylistDifficulty
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public const string HarmonyId = "com.github.rakkyo150.ComfirmPlaylistDifficulty";

        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        private Harmony _harmony;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger,Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            _harmony = new Harmony(HarmonyId);
            PluginConfig.Instance = conf.Generated<PluginConfig>();
            zenjector.Install<MenuInstaller>(Location.Menu);
            Log.Info("ConfirmPlaylistDifficulty initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
            _harmony.UnpatchSelf();
        }
    }
}
