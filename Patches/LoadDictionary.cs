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
                JMMT.ChangeBigBossEarly.Fix();
                JMMT.ChangePrisonDevourer.Fix();
                JMMT.ChangeRiverBladeTransmuter.Fix();
                JMMT.ChangeOakSkeletonChamp.Fix();
                JMMT.ChangeRangedBrevoyBad.Fix();
                JMMT.ChangeSpectres.Fix();
                JMMT.ChangeGoblinSentry.Fix();
                JMMT.ChangeGoblinSlyEye.Fix();
                JMMT.ChangeKoboldSentinels.Fix();
                JMMT.ChangeKoboldArchers.Fix();
                JMMT.ChangeKoboldEvoker.Fix();
                JMMT.ChangeRuinedWatchBanditTransmuter.Fix();
                JMMT.ChangeRuinedWatchBanditRogueMelee.Fix();
                JMMT.ChangeRuinedWatchBanditRogueRanged.Fix();
                JMMT.ChangeRuinedWatchBanditFighterlevel4.Fix();
                JMMT.ChangeRuinedWatchBanditFighterRanged.Fix();
                JMMT.ChangeLoneHouseBanditFighterlevel4.Fix();
                JMMT.ChangeLoneHouseBanditFighterRanged.Fix();
                JMMT.ChangeLoneHouseBanditConjurer.Fix();
                
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
