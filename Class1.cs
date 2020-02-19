using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using BS;
using HarmonyLib;
using Unity;
using UnityEngine;

namespace InstantSlowMo
{
    [HarmonyPatch(typeof(PlayerControl))]
    [HarmonyPatch("SlowMotion")]
    public class SlomoPatch
    {
        public static bool Prefix() //Maybe should be a Postfix?
        {
            Entry.一二三AresSlomoMethod三二一();
            return false; // Prevent original from running
        }
    }
    public class Entry : BS.LevelModule
    {
        
        public static bool 一二三AresSlomoMethod三二一() {

            SoundPlayer theCoolFX = new SoundPlayer(Properties.Resources.slowmotionFX);
            theCoolFX.Play();
            theCoolFX.Dispose();

            if (Time.timeScale == 1f)
            {
                GameManager.SetTimeScale(Mathf.Max(Catalog.current.gameData.slowMotionScale, 0.10f));
                return true;
            }
            else {
                GameManager.SetTimeScale(1f);
                return false;
            }
        }

        public override void OnLevelLoaded(LevelDefinition levelDefinition)
        {
            new Harmony("com.BS.AresButReallyMemeManInstaSlowMo").PatchAll();
            base.OnLevelLoaded(levelDefinition);
        }
    }
}
