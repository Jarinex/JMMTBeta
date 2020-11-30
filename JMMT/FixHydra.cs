using HarmonyLib;
using JMMT.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using ModMaker;
using Kingmaker.PubSubSystem;
using Kingmaker;
using Kingmaker.Localization;

namespace JMMT.JMMT
{
    class FixHydra : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHydraBP = "3b8ffcb67319ef94d88e1ed4c5a89b71";
        static readonly string copyHydraBP = "c170726da63c4b20a18c585623e28e98";
        static readonly string areaToReplaceBPIn = "c6b7bd4e20012b54cbe5643a815d20a3";

        static BlueprintUnit hydra;

        public static void Fix()
        {
            hydra = library.CopyAndAdd<BlueprintUnit>(originHydraBP, "JMMTHydra", copyHydraBP); //copies, not a deep copy
            hydra.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            hydra.LocalizedName.String = Helpers.CreateString("JMMTHydraName", "Big Dipshit"); //Changes the name of the custom hydra
            hydra.AddFacts = new BlueprintUnitFact[] //creates a new instance of AddFacts so the original hydra doesn't change, some things you can use new for, some things you need CreateInstance like above
            {
                library.Get<BlueprintBuff>("00402bae4442a854081264e498e7a833"), //displacement
                library.Get<BlueprintUnitFact>("f7f31752d838ff7429dbdbbd38e55a9d") //naturalArmor36
            };
        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }

        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHydraBP))
                    {
                        Game.Instance.EntityCreator.SpawnUnit(hydra, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeBigBossEarly : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originBigBossBP = "ad4f4735eab16be44a835da799d24faf";
        static readonly string copyBigBossBP = "0be00c3f311f494bbf57e89698a9acb7";
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

        static BlueprintUnit bigboss;

        public static void Fix()
        {
         Main.Mod.Debug("Fixing Big Dumbass");
            bigboss = library.CopyAndAdd<BlueprintUnit>(originBigBossBP, "JMMTBigBossEarly", copyBigBossBP); //copies, not a deep copy
            bigboss.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            bigboss.LocalizedName.String = Helpers.CreateString("JMMTBigBossEarlyName", "Big Dumbass"); //Changes the name of the custom hydra

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                Main.Mod.Debug("Found the dumbass area");
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originBigBossBP))
                    {
                        Main.Mod.Debug("Replacing little dumbass for Big Dumbass");
                        Game.Instance.EntityCreator.SpawnUnit(bigboss, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {
            
        }
    }

    class ChangePrisonDevourer : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originDevourerBP = "741f1f72663260c4fa6350a8829c843e";
        static readonly string copyDevourerBP = "f8ddbfda51484c449d05b40a330b0a2c";
        static readonly string areaToReplaceBPIn = "b407f0ee83587a24a8cd37ed7695c848";

        static BlueprintUnit Devourer;

        public static void Fix()
        {
            Main.Mod.Debug("Fixing Devourer");
            Devourer = library.CopyAndAdd<BlueprintUnit>(originDevourerBP, "JMMTDevourer", copyDevourerBP); //copies, not a deep copy
            Devourer.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            Devourer.LocalizedName.String = Helpers.CreateString("JMMTDevourerName", "Stinky"); //Changes the name of the custom hydra

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                Main.Mod.Debug("Found the dumbass area");
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originDevourerBP))
                    {
                        Main.Mod.Debug("Replacing little dumbass for Big Dumbass");
                        Game.Instance.EntityCreator.SpawnUnit(Devourer, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }


    class ChangeRiverBladeTransmuter : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originTransmuterBP = "52e641a20d9feb34bab4d0e27f785d34";
        static readonly string copyTransmuterBP = "788603e0561b435eacdcaf510efb78cb";
        static readonly string areaToReplaceBPIn = "a5b1798c2c0ceca4cb5c05af009bc54c";

        static BlueprintUnit Transmuter;

        public static void Fix()
        {
            Main.Mod.Debug("Fixing Devourer");
            Transmuter = library.CopyAndAdd<BlueprintUnit>(originTransmuterBP, "JMMTTransmuter", copyTransmuterBP); //copies, not a deep copy
            Transmuter.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            Transmuter.LocalizedName.String = Helpers.CreateString("JMMTTransmuter", "Weakling"); //Changes the name of the custom hydra

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                Main.Mod.Debug("Found the dumbass area");
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originTransmuterBP))
                    {
                        Main.Mod.Debug("Replacing little dumbass for Big Dumbass");
                        Game.Instance.EntityCreator.SpawnUnit(Transmuter, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }


    class ChangeOakSkeletonChamp : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originoakSkeletonMeleeBP = "f2917899f6976fc4ebc16724a592d0b7";
        static readonly string copyoakSkeletonMeleeBP = "5e90667157fa497398e7e925992f9cdc";
        static readonly string areaToReplaceBPIn = "ed22f08d587c8e04a91f48b86e1cc787";

        static BlueprintUnit originoakSkeletonMelee;

        public static void Fix()
        {
            Main.Mod.Debug("Fixing BarrowSkeletonMelee");
            originoakSkeletonMelee = library.CopyAndAdd<BlueprintUnit>(originoakSkeletonMeleeBP, "JMMToriginoakSkeletonMelee", copyoakSkeletonMeleeBP); //copies, not a deep copy
            originoakSkeletonMelee.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            originoakSkeletonMelee.LocalizedName.String = Helpers.CreateString("JMMToriginoakSkeletonMelee", "Skeletal Champion");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                Main.Mod.Debug("Found the dumbass area");
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originoakSkeletonMeleeBP))
                    {
                        Main.Mod.Debug("Replacing little dumbass for Big Dumbass");
                        Game.Instance.EntityCreator.SpawnUnit(originoakSkeletonMelee, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRangedBrevoyBad : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRangedBrevoyBadBP = "39955d0e5dbd30c4bb09abea5d8171c7";
        static readonly string copyRangedBrevoyBadBP = "78f72baaadb14364860f296642c63d2b";
        static readonly string areaToReplaceBPIn = "609170613556500419cc6e051db1dc0a";

        static BlueprintUnit RangedBrevoyBad;

        public static void Fix()
        {
            Main.Mod.Debug("Fixing Brevoy Druid");
            RangedBrevoyBad = library.CopyAndAdd<BlueprintUnit>(originRangedBrevoyBadBP, "JMMTRangedBrevoyBad", copyRangedBrevoyBadBP); //copies, not a deep copy
            RangedBrevoyBad.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RangedBrevoyBad.LocalizedName.String = Helpers.CreateString("JMMTRangedBrevoyBad", "I suck at modding");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                Main.Mod.Debug("Found the dumbass area");
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRangedBrevoyBadBP))
                    {
                        Main.Mod.Debug("Replacing little dumbass for Big Dumbass");
                        Game.Instance.EntityCreator.SpawnUnit(RangedBrevoyBad, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeSpectres : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originSpectreBP = "2f91d7337b60e3b4b9b137198a8c8745";
        static readonly string copySpectreBP = "b08095a9ede34f23aa6d829254fe14c5";
        static readonly string areaToReplaceBPIn = "4777ca570ca58c248a679ec3e9d9335d";

        static BlueprintUnit Spectre;

        public static void Fix()
        {
            Spectre = library.CopyAndAdd<BlueprintUnit>(originSpectreBP, "JMMTSpectre", copySpectreBP); //copies, not a deep copy
            Spectre.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            Spectre.LocalizedName.String = Helpers.CreateString("JMMTSpectre", "Vengeful Spectre");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originSpectreBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(Spectre, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeGoblinSentry : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originGoblinSentryBP = "dc7565e90a235be4f9e89b9376e5178d";
        static readonly string copyGoblinSentryBP = "8f0f0b0bfaae479e9a70860e33be4961";
        static readonly string areaToReplaceBPIn = "4b90066eae221c747a01997fef7eab0f";

        static BlueprintUnit GoblinSentry;

        public static void Fix()
        {
            GoblinSentry = library.CopyAndAdd<BlueprintUnit>(originGoblinSentryBP, "JMMTGoblinSentry", copyGoblinSentryBP); //copies, not a deep copy
            GoblinSentry.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            GoblinSentry.LocalizedName.String = Helpers.CreateString("JMMTGoblinSentry", "Goblin Horsereaper");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originGoblinSentryBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(GoblinSentry, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeGoblinSlyEye : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originGoblinSlyEyeBP = "d6064481c5e38604eb930a27a6b4ab0e";
        static readonly string copyGoblinSlyEyeBP = "8df02cfd6b0b4e9998c1dc1031dbaa34";
        static readonly string areaToReplaceBPIn = "4b90066eae221c747a01997fef7eab0f";

        static BlueprintUnit GoblinSlyEye;

        public static void Fix()
        {
            GoblinSlyEye = library.CopyAndAdd<BlueprintUnit>(originGoblinSlyEyeBP, "JMMTGoblinSlyEye", copyGoblinSlyEyeBP); //copies, not a deep copy
            GoblinSlyEye.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            GoblinSlyEye.LocalizedName.String = Helpers.CreateString("JMMTGoblinSlyEye", "Goblin EagleEye");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originGoblinSlyEyeBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(GoblinSlyEye, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeKoboldSentinels : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originKoboldSentinelsBP = "601c0bdc5c5c8fb4eb42c4a89b006db2";
        static readonly string copyKoboldSentinelsBP = "9c5720dd841249eaa2627bca7326cd40";
        static readonly string areaToReplaceBPIn = "c3fb59125861b474098095ed6b396d9d";

        static BlueprintUnit KoboldSentinels;

        public static void Fix()
        {
            KoboldSentinels = library.CopyAndAdd<BlueprintUnit>(originKoboldSentinelsBP, "JMMTKoboldSentinels", copyKoboldSentinelsBP); //copies, not a deep copy
            KoboldSentinels.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            KoboldSentinels.LocalizedName.String = Helpers.CreateString("JMMTKoboldSentinels", "Kobold Blade");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originKoboldSentinelsBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(KoboldSentinels, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeKoboldArchers : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originKoboldArchersBP = "8b23f76ebdd79a3468af56832a22f82e";
        static readonly string copyKoboldArchersBP = "bd58fd5e18aa48d891045e42fe5f2afc";
        static readonly string areaToReplaceBPIn = "8ad40376ad0a1a4499102c0755103a04";

        static BlueprintUnit KoboldArchers;

        public static void Fix()
        {
            KoboldArchers = library.CopyAndAdd<BlueprintUnit>(originKoboldArchersBP, "JMMTKoboldArchers", copyKoboldArchersBP); //copies, not a deep copy
            KoboldArchers.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            KoboldArchers.LocalizedName.String = Helpers.CreateString("JMMTKoboldArchers", "Kobold Skirmisher");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originKoboldArchersBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(KoboldArchers, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeKoboldEvoker : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originKoboldEvokerBP = "16e4671000f326943b5348e42846f320";
        static readonly string copyKoboldEvokerBP = "7c2e6cbfc0644d1c80a13c6f4d8aa294";
        static readonly string areaToReplaceBPIn = "8ad40376ad0a1a4499102c0755103a04";

        static BlueprintUnit KoboldEvoker;

        public static void Fix()
        {
            KoboldEvoker = library.CopyAndAdd<BlueprintUnit>(originKoboldEvokerBP, "JMMTKoboldEvoker", copyKoboldEvokerBP); //copies, not a deep copy
            KoboldEvoker.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            KoboldEvoker.LocalizedName.String = Helpers.CreateString("JMMTKoboldEvoker", "Kobold Dragon Shaman");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originKoboldEvokerBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(KoboldEvoker, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }


    class ChangeRuinedWatchBanditTransmuter : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRWTransmuterBP = "52e641a20d9feb34bab4d0e27f785d34";
        static readonly string copyRWTransmuterBP = "865cf4ef10f04168a340eaf879f72a15";
        static readonly string areaToReplaceBPIn = "973dacf5a7642a04183f0f10539ce093";

        static BlueprintUnit RWTransmuter;

        public static void Fix()
        {
            RWTransmuter = library.CopyAndAdd<BlueprintUnit>(originRWTransmuterBP, "JMMTRWTransmuter ", copyRWTransmuterBP); //copies, not a deep copy
            RWTransmuter.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RWTransmuter.LocalizedName.String = Helpers.CreateString("JMMTRWTransmuter ", "Bandit Transmuter");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRWTransmuterBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(RWTransmuter, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRuinedWatchBanditRogueMelee : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRWBanditRogueMeleeBP = "cdbdcfe136c81a4459ec886f030499b4";
        static readonly string copyRWBanditRogueMeleeBP = "8c4e5cdd770c4add814b2c0e3a765811";
        static readonly string areaToReplaceBPIn = "973dacf5a7642a04183f0f10539ce093";

        static BlueprintUnit RWBanditRogueMelee;

        public static void Fix()
        {
            RWBanditRogueMelee = library.CopyAndAdd<BlueprintUnit>(originRWBanditRogueMeleeBP, "JMMTRWBanditRogueMelee", copyRWBanditRogueMeleeBP); //copies, not a deep copy
            RWBanditRogueMelee.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RWBanditRogueMelee.LocalizedName.String = Helpers.CreateString("JMMTRWBanditRogueMelee", "Bandit");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRWBanditRogueMeleeBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(RWBanditRogueMelee, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRuinedWatchBanditRogueRanged : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRWBanditRogueRangedBP = "0693a983a7e460c4881b2aea51637999";
        static readonly string copyRWBanditRogueRangedBP = "d53d5ec6942848d1bf0a4b778ba08a83";
        static readonly string areaToReplaceBPIn = "973dacf5a7642a04183f0f10539ce093";

        static BlueprintUnit RWBanditRogueRanged;

        public static void Fix()
        {
            RWBanditRogueRanged = library.CopyAndAdd<BlueprintUnit>(originRWBanditRogueRangedBP, "JMMTRWBanditRogueRanged", copyRWBanditRogueRangedBP); //copies, not a deep copy
            RWBanditRogueRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RWBanditRogueRanged.LocalizedName.String = Helpers.CreateString("JMMTRWBanditRogueRanged", "Bandit");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRWBanditRogueRangedBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(RWBanditRogueRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRuinedWatchBanditFighterlevel4 : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRWBanditFighterlevel4BP = "435e3d847f566e5479acd4de2642ba31";
        static readonly string copyRWBanditFighterlevel4BP = "057aaf8db6bc4bc2bf963bbe1a11e316";
        static readonly string areaToReplaceBPIn = "973dacf5a7642a04183f0f10539ce093";

        static BlueprintUnit RWBanditFighterlevel4;

        public static void Fix()
        {
            RWBanditFighterlevel4 = library.CopyAndAdd<BlueprintUnit>(originRWBanditFighterlevel4BP, "JMMTRWBanditFighterlevel4", copyRWBanditFighterlevel4BP); //copies, not a deep copy
            RWBanditFighterlevel4.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RWBanditFighterlevel4.LocalizedName.String = Helpers.CreateString("JMMTRWBanditFighterlevel4", "Bandit");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRWBanditFighterlevel4BP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(RWBanditFighterlevel4, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRuinedWatchBanditFighterRanged : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRWBanditFighterRangedBP = "96b5042d7d758534f85ffb5bf6dcffce";
        static readonly string copyRWBanditFighterRangedBP = "b6eda239cb1d48d5935e9472a2a926a7";
        static readonly string areaToReplaceBPIn = "973dacf5a7642a04183f0f10539ce093";

        static BlueprintUnit RWBanditFighterRanged;

        public static void Fix()
        {
            RWBanditFighterRanged = library.CopyAndAdd<BlueprintUnit>(originRWBanditFighterRangedBP, "JMMTRWBanditFighterRanged", copyRWBanditFighterRangedBP); //copies, not a deep copy
            RWBanditFighterRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RWBanditFighterRanged.LocalizedName.String = Helpers.CreateString("JMMTRWBanditFighterRanged", "Bandit");

        }

        public void HandleModDisable()
        {
            EventBus.Subscribe(this); //removes the event
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this); //Adds the onAreaScenesLoaded event
        }



        public void OnAreaLoadingComplete() //once the area is loaded, check for the original hydra
        {
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {

                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRWBanditFighterRangedBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(RWBanditFighterRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }



}






