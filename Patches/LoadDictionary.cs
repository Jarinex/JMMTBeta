/* Remove this file form your project if you are not going use the EAHelpers.cs*/
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using JMMT.Utilities;
using System;
using static JMMT.Utilities.SettingsWrapper;


namespace JMMT.Patches
{
    [HarmonyLib.HarmonyPatch(typeof(LibraryScriptableObject), "LoadDictionary")]
    [HarmonyLib.HarmonyPatch(typeof(LibraryScriptableObject), "LoadDictionary", new Type[0])]
    static class LibraryScriptableObject_LoadDictionary_Patch
    {
        static void Postfix(LibraryScriptableObject __instance)
        {
            var self = __instance;
            if (Main.Library != null) return;
            Main.Library = self;
            try
            {
#if DEBUG
                bool allow_guid_generation = true;
                
#else
                bool allow_guid_generation = false; //no guids should be ever generated in release
#endif
                Helpers.GuidStorage.load(Properties.Resources.blueprints, allow_guid_generation);
                JMMT.FixHydra.Fix(); //Load fix
                JMMT.ChangeBigBossEarly.Fix(); //Keep, removes enemies
                JMMT.ChangePrisonDevourer.Fix();//Not being used
                JMMT.ChangeOakSkeletonChamp.Fix();
                JMMT.ChangeRangedBrevoyBad.Fix();
                JMMT.ChangeSpectres.Fix();
                JMMT.ChangeGoblinSentry.Fix();
                JMMT.ChangeGoblinSlyEye.Fix();
                JMMT.ChangeKoboldSentinels.Fix();//used in a couple areas, might be able to change?
                JMMT.ChangeKoboldArchers.Fix();//used once, might be able to change?
                JMMT.ChangeKoboldEvoker.Fix(); //used once, might be able to change?
                JMMT.ChangeRuinedWatchBanditTransmuter.Fix();
                JMMT.ChangeRuinedWatchBanditRogueMelee.Fix();
                JMMT.ChangeRuinedWatchBanditRogueRanged.Fix();
                JMMT.ChangeRuinedWatchBanditFighterlevel4.Fix();
                JMMT.ChangeRuinedWatchBanditFighterRanged.Fix();
                JMMT.ChangeLoneHouseBanditFighterlevel4.Fix();
                JMMT.ChangeLoneHouseBanditFighterRanged.Fix();
                JMMT.ChangeLoneHouseBanditConjurer.Fix();
                JMMT.ChangeHuntingGroundHydra.Fix();
                JMMT.ChangeSilverstepFrogs.Fix();
                JMMT.ChangeSilverstepSmallTatzyl.Fix();
                JMMT.ChangeCapitalFerociousHydra.Fix();
                JMMT.ChangeHollowEyesArcher.Fix();
                JMMT.ChangeHollowMeleeFighterA.Fix();
                JMMT.ChangeHollowMeleeFighterB.Fix();
                JMMT.ChangeHollowMeleeBrawler.Fix();
                JMMT.ChangeClericofGorumArmag.Fix();
                JMMT.ChangeAdvancedDweomercat.Fix();
                JMMT.ChangeGhostMageArmag.Fix();
                JMMT.ChangeInquisitorArmagFirst.Fix();
                JMMT.ChangeInquisitorArmagSecond.Fix();
                JMMT.ChangeHollowNightmareArchers.Fix();
                JMMT.ChangeGhostMageArmagTwo.Fix();
                JMMT.ChangeRiverBladeBard.Fix();
                JMMT.ChangeRiverBladeRogueMelee.Fix();
                JMMT.ChangeRiverBladeFighterRanged.Fix();
                JMMT.ChangeRiverBladeRogueRanged.Fix();
                JMMT.ChangeRiverBladeTransmuter.Fix();
                JMMT.ChangeRiverBladeCleric.Fix();
                JMMT.ChangeWhiteroseRanged.Fix();
                JMMT.ChangeWereratIrovetti.Fix();
                JMMT.CopyVeteranTroll.Fix();
                JMMT.CopyAdvancedErinyes.Fix();
                JMMT.CopyWrigglingWorm.Fix();
                JMMT.CopyHATEOTTreant.Fix();
                JMMT.CopyAlphaWorg.Fix();
                JMMT.ChangeThanadaemonMelee.Fix();
                JMMT.ChangeThanadaemonArcher.Fix();
                JMMT.ChangeAstraSoul.Fix();
                JMMT.ChangeDryad.Fix();
                JMMT.ChangeTreant.Fix();
                

#if DEBUG
                string guid_file_name = $@"{ModPath}blueprints.txt";
                Helpers.GuidStorage.dump(guid_file_name);
#endif
                Helpers.GuidStorage.dump($@"{ModPath}loaded_blueprints.txt");

                
            }
            catch (Exception ex)
            {
                Main.Mod.Error(ex);
            }
        }
    }
}
