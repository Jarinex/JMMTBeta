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
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

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
                        var newHydra = Game.Instance.EntityCreator.SpawnUnit(hydra, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newHydra.GroupId = unit.GroupId;
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
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

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
            Transmuter.LocalizedName.String = Helpers.CreateString("JMMTTransmuter", "River Blade Wizard"); //Changes the name of the custom hydra

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
                        var newTransmuter = Game.Instance.EntityCreator.SpawnUnit(Transmuter, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newTransmuter.GroupId = unit.GroupId;
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
                        var newOakSkelly = Game.Instance.EntityCreator.SpawnUnit(originoakSkeletonMelee, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newOakSkelly.GroupId = unit.GroupId;
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

                        var newSpectre = Game.Instance.EntityCreator.SpawnUnit(Spectre, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newSpectre.GroupId = unit.GroupId;
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

                        var newGoblinSentry = Game.Instance.EntityCreator.SpawnUnit(GoblinSentry, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newGoblinSentry.GroupId = unit.GroupId;
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

                        var newGoblinSlyEye = Game.Instance.EntityCreator.SpawnUnit(GoblinSlyEye, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newGoblinSlyEye.GroupId = unit.GroupId;
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

                        var newKoboldSentinels = Game.Instance.EntityCreator.SpawnUnit(KoboldSentinels, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newKoboldSentinels.GroupId = unit.GroupId;
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

                        var newKoboldArchers = Game.Instance.EntityCreator.SpawnUnit(KoboldArchers, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newKoboldArchers.GroupId = unit.GroupId;
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

                        var newKoboldEvoker = Game.Instance.EntityCreator.SpawnUnit(KoboldEvoker, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newKoboldEvoker.GroupId = unit.GroupId;
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

                        var newRWTransmuter = Game.Instance.EntityCreator.SpawnUnit(RWTransmuter, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRWTransmuter.GroupId = unit.GroupId;
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

                        var newRWBanditRogueMelee = Game.Instance.EntityCreator.SpawnUnit(RWBanditRogueMelee, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRWBanditRogueMelee.GroupId = unit.GroupId;
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

                        var newRWBanditRogueRanged = Game.Instance.EntityCreator.SpawnUnit(RWBanditRogueRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRWBanditRogueRanged.GroupId = unit.GroupId;
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

                        var newRWBanditFighterlevel4 = Game.Instance.EntityCreator.SpawnUnit(RWBanditFighterlevel4, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRWBanditFighterlevel4.GroupId = unit.GroupId;
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

                        var newRWBanditFighterRanged = Game.Instance.EntityCreator.SpawnUnit(RWBanditFighterRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRWBanditFighterRanged.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeLoneHouseBanditFighterlevel4 : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originLHBanditFighterlevel4BP = "f9945eeae56066c48ad81afaceeca960";
        static readonly string copyLHBanditFighterlevel4BP = "c85030f5bd794e9e8878d364edc624e6";
        static readonly string areaToReplaceBPIn = "3f191b1420761a240bfea4516bbf5477";

        static BlueprintUnit LHBanditFighterlevel4;

        public static void Fix()
        {
            LHBanditFighterlevel4 = library.CopyAndAdd<BlueprintUnit>(originLHBanditFighterlevel4BP, "JMMTLHBanditFighterlevel4", copyLHBanditFighterlevel4BP); //copies, not a deep copy
            LHBanditFighterlevel4.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            LHBanditFighterlevel4.LocalizedName.String = Helpers.CreateString("JMMTLHBanditFighterlevel4", "Bandit");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originLHBanditFighterlevel4BP))
                    {

                        var newLHBanditFighterlevel4 = Game.Instance.EntityCreator.SpawnUnit(LHBanditFighterlevel4, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newLHBanditFighterlevel4.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeLoneHouseBanditFighterRanged : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originLHBanditFighterRangedBP = "2ca6af051fe573a42a737be900c8ba5d";
        static readonly string copyLHBanditFighterRangedBP = "cba50ae6db564bd5b128199e1833be3e";
        static readonly string areaToReplaceBPIn = "3f191b1420761a240bfea4516bbf5477";

        static BlueprintUnit LHBanditFighterRanged;

        public static void Fix()
        {
            LHBanditFighterRanged = library.CopyAndAdd<BlueprintUnit>(originLHBanditFighterRangedBP, "JMMTLHBanditFighterRanged", copyLHBanditFighterRangedBP); //copies, not a deep copy
            LHBanditFighterRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            LHBanditFighterRanged.LocalizedName.String = Helpers.CreateString("JMMTLHBanditFighterRanged", "Bandit Archer");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originLHBanditFighterRangedBP))
                    {

                        var newLHBanditFighterRanged = Game.Instance.EntityCreator.SpawnUnit(LHBanditFighterRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newLHBanditFighterRanged.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeLoneHouseBanditConjurer : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originLHBanditConjurerBP = "24088cc8784b8a0429316452bbb233b5";
        static readonly string copyLHBanditConjurerBP = "16a4df6a38a54280b4819703d255136b";
        static readonly string areaToReplaceBPIn = "3f191b1420761a240bfea4516bbf5477";

        static BlueprintUnit LHBanditConjurer;

        public static void Fix()
        {
            LHBanditConjurer = library.CopyAndAdd<BlueprintUnit>(originLHBanditConjurerBP, "JMMTLHBanditConjurer", copyLHBanditConjurerBP); //copies, not a deep copy
            LHBanditConjurer.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            LHBanditConjurer.LocalizedName.String = Helpers.CreateString("JMMTLHBanditConjurer", "Bandit Conjurer");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originLHBanditConjurerBP))
                    {

                        var newLHBanditConjurer = Game.Instance.EntityCreator.SpawnUnit(LHBanditConjurer, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newLHBanditConjurer.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeHuntingGroundHydra : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHGHydraBP = "101c479b6cd567b4497ce4283c6d5b3b";
        static readonly string copyHGHydraBP = "96ee2234876249df872a4fae05cac478";
        static readonly string areaToReplaceBPIn = "584940f8293b75c4e9f3fdabeeababd6";

        static BlueprintUnit HGHydra;

        public static void Fix()
        {
            HGHydra = library.CopyAndAdd<BlueprintUnit>(originHGHydraBP, "JMMTHGHydra", copyHGHydraBP); //copies, not a deep copy
            HGHydra.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            HGHydra.LocalizedName.String = Helpers.CreateString("JMMTHGHydra", "Primal Cyrohydra");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHGHydraBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(HGHydra, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeSilverstepFrogs : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originSilverFrogBP = "30080a8d8ae40bb43aca496b11b74c6b";
        static readonly string copySilverFrogBP = "70266f804573468285cea8ae27437a8e";
        static readonly string areaToReplaceBPIn = "34c828a6acd7dfd4ca8c7db689dece7d";

        static BlueprintUnit SilverFrog;

        public static void Fix()
        {
            SilverFrog = library.CopyAndAdd<BlueprintUnit>(originSilverFrogBP, "JMMTSilverFrog", copySilverFrogBP); //copies, not a deep copy
            SilverFrog.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            SilverFrog.LocalizedName.String = Helpers.CreateString("JMMTSilverFrog", "Greater Giant Poisonous Frog");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originSilverFrogBP))
                    {

                        var newSilverFrog = Game.Instance.EntityCreator.SpawnUnit(SilverFrog, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newSilverFrog.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeSilverstepSmallTatzyl : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originSilverSmallTatzylBP = "4dd913232eaf3894890b2bfaabcd8282";
        static readonly string copySilverSmallTatzylBP = "b1027467a67844e2914ab07f109035da";
        static readonly string areaToReplaceBPIn = "34c828a6acd7dfd4ca8c7db689dece7d";

        static BlueprintUnit SilverSmallTatzyl;

        public static void Fix()
        {
            SilverSmallTatzyl = library.CopyAndAdd<BlueprintUnit>(originSilverSmallTatzylBP, "JMMTSilverSmallTatzyl", copySilverSmallTatzylBP); //copies, not a deep copy
            SilverSmallTatzyl.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            SilverSmallTatzyl.LocalizedName.String = Helpers.CreateString("JMMTSilverSmallTatzyl", "Quickling Tatzylwyrm");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originSilverSmallTatzylBP))
                    {

                        var newSilverSmallTatzyl = Game.Instance.EntityCreator.SpawnUnit(SilverSmallTatzyl, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newSilverSmallTatzyl.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeCapitalFerociousHydra : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originCapitalHydraBP = "e135463f804adb541b402bae3f657af4";
        static readonly string copyCapitalHydraBP = "76706df8414046b59951e407666779f6";
        static readonly string areaToReplaceBPIn = "bf0bc94da0e88024b87b297635bc3bc7";

        static BlueprintUnit CapitalHydra;

        public static void Fix()
        {
            CapitalHydra = library.CopyAndAdd<BlueprintUnit>(originCapitalHydraBP, "JMMTCapitalHydra", copyCapitalHydraBP); //copies, not a deep copy
            CapitalHydra.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            CapitalHydra.LocalizedName.String = Helpers.CreateString("JMMTCapitalHydra", "Primal Pyrohydra");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originCapitalHydraBP))
                    {

                        var newCapitalHydra = Game.Instance.EntityCreator.SpawnUnit(CapitalHydra, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newCapitalHydra.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeHollowEyesArcher : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHollowEyesArcherBP = "afd39d3c17df206448c91db3fda6c6ed";
        static readonly string copyHollowEyesArcherBP = "e19ab93e73984e0e9e65d7b06d766e5b";
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

        static BlueprintUnit HollowEyesArcher;

        public static void Fix()
        {
            HollowEyesArcher = library.CopyAndAdd<BlueprintUnit>(originHollowEyesArcherBP, "JMMTHollowEyesArcher", copyHollowEyesArcherBP); //copies, not a deep copy
            HollowEyesArcher.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            HollowEyesArcher.LocalizedName.String = Helpers.CreateString("JMMTCapitalHydra", "Bandit");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHollowEyesArcherBP))
                    {

                        var newHollowEyesArcher = Game.Instance.EntityCreator.SpawnUnit(HollowEyesArcher, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newHollowEyesArcher.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeHollowMeleeFighterA : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHollowMeleeFighterABP = "179944f7553cafa468c6f548effbbf71";
        static readonly string copyHollowMeleeFighterABP = "dc6e7077127a4326aa7d63cd04523161";
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

        static BlueprintUnit HollowMeleeFighterA;

        public static void Fix()
        {
            HollowMeleeFighterA = library.CopyAndAdd<BlueprintUnit>(originHollowMeleeFighterABP, "JMMTHollowMeleeFighterA", copyHollowMeleeFighterABP); //copies, not a deep copy
            HollowMeleeFighterA.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            HollowMeleeFighterA.LocalizedName.String = Helpers.CreateString("JMMTHollowMeleeFighterA", "Bandit");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHollowMeleeFighterABP))
                    {

                        var newHollowMeleeFighterA = Game.Instance.EntityCreator.SpawnUnit(HollowMeleeFighterA, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newHollowMeleeFighterA.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeHollowMeleeFighterB : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHollowMeleeFighterBBP = "341a00b2bbf4be9498049c62e6cd7456";
        static readonly string copyHollowMeleeFighterBBP = "19e9d35272a947be8a0e2a3ac648d033";
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

        static BlueprintUnit HollowMeleeFighterB;

        public static void Fix()
        {
            HollowMeleeFighterB = library.CopyAndAdd<BlueprintUnit>(originHollowMeleeFighterBBP, "JMMTHollowMeleeFighterB", copyHollowMeleeFighterBBP); //copies, not a deep copy
            HollowMeleeFighterB.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            HollowMeleeFighterB.LocalizedName.String = Helpers.CreateString("JMMTHollowMeleeFighterB", "Bandit");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHollowMeleeFighterBBP))
                    {

                        var newHollowMeleeFighterB = Game.Instance.EntityCreator.SpawnUnit(HollowMeleeFighterB, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newHollowMeleeFighterB.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeHollowMeleeBrawler : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHollowMeleeBrawlerBP = "23cde53a19471f943a94fbde18a61c0b";
        static readonly string copyHollowMeleeBrawlerBP = "5099b31194dc484d8c2774ab949f4875";
        static readonly string areaToReplaceBPIn = "63f0934aa847a88408da0b04d933669d";

        static BlueprintUnit HollowMeleeBrawler;

        public static void Fix()
        {
            HollowMeleeBrawler = library.CopyAndAdd<BlueprintUnit>(originHollowMeleeBrawlerBP, "JMMTHollowMeleeBrawler", copyHollowMeleeBrawlerBP); //copies, not a deep copy
            HollowMeleeBrawler.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            HollowMeleeBrawler.LocalizedName.String = Helpers.CreateString("JMMTHollowMeleeBrawler", "Bandit Brawler");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHollowMeleeBrawlerBP))
                    {

                        var newHollowMeleeBrawler = Game.Instance.EntityCreator.SpawnUnit(HollowMeleeBrawler, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newHollowMeleeBrawler.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeClericofGorumArmag : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originClericofGorumBP = "4602809f9d59cc24a815d304715771c7";
        static readonly string copyClericofGorumBP = "05e04c93beb54609bec87d6521f08ea9";
        static readonly string areaToReplaceBPIn = "f0e41714d8f2bc14bb604bc0d4cfe40d";

        static BlueprintUnit ClericofGorum;

        public static void Fix()
        {
            ClericofGorum = library.CopyAndAdd<BlueprintUnit>(originClericofGorumBP, "JMMTClericofGorum", copyClericofGorumBP); //copies, not a deep copy
            ClericofGorum.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            ClericofGorum.LocalizedName.String = Helpers.CreateString("JMMTClericofGorum", "Cleric of Gorum");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originClericofGorumBP))
                    {

                        var newClericofGorum = Game.Instance.EntityCreator.SpawnUnit(ClericofGorum, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newClericofGorum.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeAdvancedDweomercat : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originDweomercatBP = "729464361d706f8429e4f4ea9b4f952c";
        static readonly string copyDweomercatBP = "7fd6f944a42e444fbc9157996b460729";
        static readonly string areaToReplaceBPIn = "4e4c968262e025e478ec6911e61aa705";

        static BlueprintUnit Dweomercat;

        public static void Fix()
        {
            Dweomercat = library.CopyAndAdd<BlueprintUnit>(originDweomercatBP, "JMMTDryad", copyDweomercatBP); //copies, not a deep copy
            Dweomercat.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            Dweomercat.LocalizedName.String = Helpers.CreateString("JMMTDryad", "Adept Dryad");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originDweomercatBP))
                    {

                        var newDryad = Game.Instance.EntityCreator.SpawnUnit(Dweomercat, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newDryad.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }


    class ChangeGhostMageArmag : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originGhostMageArmagBP = "cb8ff0a7ba777ba4291dabb6008767be";
        static readonly string copyGhostMageArmagBP = "4868d06edcd240f58e94d96beb2421ad";
        static readonly string areaToReplaceBPIn = "4777ca570ca58c248a679ec3e9d9335d";

        static BlueprintUnit GhostMageArmag;

        public static void Fix()
        {
            GhostMageArmag = library.CopyAndAdd<BlueprintUnit>(originGhostMageArmagBP, "JMMTGhostMageArmag", copyGhostMageArmagBP); //copies, not a deep copy
            GhostMageArmag.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            GhostMageArmag.LocalizedName.String = Helpers.CreateString("JMMTGhostMageArmag", "Venegeful Spectral Mage");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originGhostMageArmagBP))
                    {

                        var newGhostMage = Game.Instance.EntityCreator.SpawnUnit(GhostMageArmag, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newGhostMage.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeInquisitorArmagFirst : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originInquisitorArmagABP = "5b7fc5f74b0195e42ba17f1d7e21a3c9";
        static readonly string copyInquisitorArmagABP = "00b8c096d00e49ada6c5125a241b8f01";
        static readonly string areaToReplaceBPIn = "4777ca570ca58c248a679ec3e9d9335d";

        static BlueprintUnit InquisitorArmagA;

        public static void Fix()
        {
            InquisitorArmagA = library.CopyAndAdd<BlueprintUnit>(originInquisitorArmagABP, "JMMTInquisitorArmagA", copyInquisitorArmagABP); //copies, not a deep copy
            InquisitorArmagA.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            InquisitorArmagA.LocalizedName.String = Helpers.CreateString("JMMTInquisitorArmagA", "Inquisitor of Armag");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originInquisitorArmagABP))
                    {

                        var newInquisitorArmagA = Game.Instance.EntityCreator.SpawnUnit(InquisitorArmagA, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newInquisitorArmagA.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeInquisitorArmagSecond : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originInquisitorArmagBBP = "55f39411dc3c9ef43aa61c2d7fe3bfc9";
        static readonly string copyInquisitorArmagBBP = "f4329022dd224823a5e245e407c7023c";
        static readonly string areaToReplaceBPIn = "4777ca570ca58c248a679ec3e9d9335d";

        static BlueprintUnit InquisitorArmagB;

        public static void Fix()
        {
            InquisitorArmagB = library.CopyAndAdd<BlueprintUnit>(originInquisitorArmagBBP, "JMMTInquisitorArmagB", copyInquisitorArmagBBP); //copies, not a deep copy
            InquisitorArmagB.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            InquisitorArmagB.LocalizedName.String = Helpers.CreateString("JMMTInquisitorArmagB", "Inquisitor of Armag");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originInquisitorArmagBBP))
                    {

                        var newInquisitorArmagB = Game.Instance.EntityCreator.SpawnUnit(InquisitorArmagB, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newInquisitorArmagB.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeHollowNightmareArchers : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originHollowNightmareArcherBP = "9928642aa0612434bbb23f478dbbf988";
        static readonly string copyHollowNightmareArcherBP = "0518c22039c4495288dd46de248a9f96";
        static readonly string areaToReplaceBPIn = "295bafa1ddd480c47b21594f140a3aba";

        static BlueprintUnit HollowNightmareArcher;

        public static void Fix()
        {
            HollowNightmareArcher = library.CopyAndAdd<BlueprintUnit>(originHollowNightmareArcherBP, "JMMTHollowNightmareArcher", copyHollowNightmareArcherBP); //copies, not a deep copy
            HollowNightmareArcher.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            HollowNightmareArcher.LocalizedName.String = Helpers.CreateString("JMMTHollowNightmareArcher", "Nightmare Skeletal Champion Archer");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originHollowNightmareArcherBP))
                    {

                        var newHollowArcher = Game.Instance.EntityCreator.SpawnUnit(HollowNightmareArcher, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newHollowArcher.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeGhostMageArmagTwo : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originGhostMageArmagTwoBP = "a24e752e4748bd548936020938effee1";
        static readonly string copyGhostMageArmagTwoBP = "f582475c66864a009f6c07029c350a2c";
        static readonly string areaToReplaceBPIn = "4777ca570ca58c248a679ec3e9d9335d";

        static BlueprintUnit GhostMageArmagTwo;

        public static void Fix()
        {
            GhostMageArmagTwo = library.CopyAndAdd<BlueprintUnit>(originGhostMageArmagTwoBP, "JMMTGhostMageArmagTwo", copyGhostMageArmagTwoBP); //copies, not a deep copy
            GhostMageArmagTwo.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            GhostMageArmagTwo.LocalizedName.String = Helpers.CreateString("JMMTGhostMageArmagTwo", "Venegeful Spectral Mage");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originGhostMageArmagTwoBP))
                    {

                        var newGhostMageB = Game.Instance.EntityCreator.SpawnUnit(GhostMageArmagTwo, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newGhostMageB.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRiverBladeBard : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRiverBladeBardBP = "3f2672c1a9877244cab3caf16e221507";
        static readonly string copyRiverBladeBardBP = "c68f8282016f417c896e8536759ccc45";
        static readonly string areaToReplaceBPIn = "a5b1798c2c0ceca4cb5c05af009bc54c";

        static BlueprintUnit RiverBladeBard;

        public static void Fix()
        {
            RiverBladeBard = library.CopyAndAdd<BlueprintUnit>(originRiverBladeBardBP, "JMMTRiverBladeBard", copyRiverBladeBardBP); //copies, not a deep copy
            RiverBladeBard.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RiverBladeBard.LocalizedName.String = Helpers.CreateString("JMMTRiverBladeBard", "River Blade Bard");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRiverBladeBardBP))
                    {

                        var newRiverBladeBard = Game.Instance.EntityCreator.SpawnUnit(RiverBladeBard, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRiverBladeBard.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRiverBladeRogueMelee : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRiverBladeRogueMeleeBP = "a305c49dc73fe9d419003d5469b50f93";
        static readonly string copyRiverBladeRogueMeleeBP = "5aec1df0170f45d49ea41cccb7acd529";
        static readonly string areaToReplaceBPIn = "a5b1798c2c0ceca4cb5c05af009bc54c";

        static BlueprintUnit RiverBladeRogueMelee;

        public static void Fix()
        {
            RiverBladeRogueMelee = library.CopyAndAdd<BlueprintUnit>(originRiverBladeRogueMeleeBP, "JMMTRiverBladeRogueMelee", copyRiverBladeRogueMeleeBP); //copies, not a deep copy
            RiverBladeRogueMelee.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RiverBladeRogueMelee.LocalizedName.String = Helpers.CreateString("JMMTRiverBladeRogueMelee", "River Blade Bandit");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRiverBladeRogueMeleeBP))
                    {

                        var newRiverBladeRogueMelee = Game.Instance.EntityCreator.SpawnUnit(RiverBladeRogueMelee, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRiverBladeRogueMelee.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRiverBladeFighterRanged : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRiverBladeFighterRangedBP = "02459ebf6d89e8845933a6565f3bc2e3";
        static readonly string copyRiverBladeFighterRangedBP = "7bc81dc0338f4ff0a3096387c346857a";
        static readonly string areaToReplaceBPIn = "a5b1798c2c0ceca4cb5c05af009bc54c";

        static BlueprintUnit RiverBladeFighterRanged;

        public static void Fix()
        {
            RiverBladeFighterRanged = library.CopyAndAdd<BlueprintUnit>(originRiverBladeFighterRangedBP, "JMMTRiverBladeFighterRanged", copyRiverBladeFighterRangedBP); //copies, not a deep copy
            RiverBladeFighterRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RiverBladeFighterRanged.LocalizedName.String = Helpers.CreateString("JMMTRiverBladeFighterRanged", "River Blade Archer");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRiverBladeFighterRangedBP))
                    {

                        var newRiverBladeFighterRanged = Game.Instance.EntityCreator.SpawnUnit(RiverBladeFighterRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRiverBladeFighterRanged.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRiverBladeRogueRanged : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRiverBladeRogueRangedBP = "1af93c19b55cb1f4b8a3cd643f5cf0a3";
        static readonly string copyRiverBladeRogueRangedBP = "8fdb3247227544ef935d158e7293009a";
        static readonly string areaToReplaceBPIn = "a5b1798c2c0ceca4cb5c05af009bc54c";

        static BlueprintUnit RiverBladeRogueRanged;

        public static void Fix()
        {
            RiverBladeRogueRanged = library.CopyAndAdd<BlueprintUnit>(originRiverBladeRogueRangedBP, "JMMTRiverBladeRogueRanged", copyRiverBladeRogueRangedBP); //copies, not a deep copy
            RiverBladeRogueRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RiverBladeRogueRanged.LocalizedName.String = Helpers.CreateString("JMMTRiverBladeRogueRanged", "River Blade Archer");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRiverBladeRogueRangedBP))
                    {

                        var newRiverBladeRogueRanged = Game.Instance.EntityCreator.SpawnUnit(RiverBladeRogueRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRiverBladeRogueRanged.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeRiverBladeCleric : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originRiverBladeClericBP = "5890cea7c5a05264ab8533ca458b4212";
        static readonly string copyRiverBladeClericBP = "621a1133ee3c4eeca4548a7df5068cb2";
        static readonly string areaToReplaceBPIn = "a5b1798c2c0ceca4cb5c05af009bc54c";

        static BlueprintUnit RiverBladeCleric;

        public static void Fix()
        {
            RiverBladeCleric = library.CopyAndAdd<BlueprintUnit>(originRiverBladeClericBP, "JMMTRiverBladeCleric", copyRiverBladeClericBP); //copies, not a deep copy
            RiverBladeCleric.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            RiverBladeCleric.LocalizedName.String = Helpers.CreateString("JMMTRiverBladeCleric", "River Blade Cleric");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originRiverBladeClericBP))
                    {

                        var newRiverBladeCleric = Game.Instance.EntityCreator.SpawnUnit(RiverBladeCleric, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newRiverBladeCleric.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeLittletownFighterRanged : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originLittletownFighterRangedBP = "02459ebf6d89e8845933a6565f3bc2e3";
        static readonly string copyLittletownFighterRangedBP = "a530916c6dab409b9354a11eed1e4862";
        static readonly string areaToReplaceBPIn = "f2c0665786c0fc142b8d6668e31fb3e1";

        static BlueprintUnit LittletownFighterRanged;

        public static void Fix()
        {
            LittletownFighterRanged = library.CopyAndAdd<BlueprintUnit>(originLittletownFighterRangedBP, "JMMTLittletownFighterRanged", copyLittletownFighterRangedBP); //copies, not a deep copy
            LittletownFighterRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            LittletownFighterRanged.LocalizedName.String = Helpers.CreateString("JMMTLittletownFighterRanged", "River Blade Archer");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originLittletownFighterRangedBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(LittletownFighterRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeLittletownFighterMelee : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originLittletownFighterMeleeBP = "341a00b2bbf4be9498049c62e6cd7456";
        static readonly string copyLittletownFighterMeleeBP = "1e000c3e08c54cbbb931c6908e0995fd";
        static readonly string areaToReplaceBPIn = "f2c0665786c0fc142b8d6668e31fb3e1";

        static BlueprintUnit LittletownFighterMelee;

        public static void Fix()
        {
            LittletownFighterMelee = library.CopyAndAdd<BlueprintUnit>(originLittletownFighterMeleeBP, "JMMTLittletownFighterMelee", copyLittletownFighterMeleeBP); //copies, not a deep copy
            LittletownFighterMelee.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            LittletownFighterMelee.LocalizedName.String = Helpers.CreateString("JMMTLittletownFighterMelee", "River Blade Fighter");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originLittletownFighterMeleeBP))
                    {

                        Game.Instance.EntityCreator.SpawnUnit(LittletownFighterMelee, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }


    class ChangeWhiteroseRanged: IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originWhiteroseRangedBP = "052db94288d9e804b9fd70723030ce0e";
        static readonly string copyWhiteroseRangedBP = "b640e0571bfd4b7ea87f13e8462a3777";
        static readonly string areaToReplaceBPIn = "5ec616b225074e246a6f64b3554819fb";

        static BlueprintUnit WhiteroseRanged;

        public static void Fix()
        {
            WhiteroseRanged = library.CopyAndAdd<BlueprintUnit>(originWhiteroseRangedBP, "JMMTWhiteroseRanged", copyWhiteroseRangedBP); //copies, not a deep copy
            WhiteroseRanged.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            WhiteroseRanged.LocalizedName.String = Helpers.CreateString("JMMTWhiteroseRanged", "Pitax Priest");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originWhiteroseRangedBP))
                    {

                        var newWhiteroseRanged = Game.Instance.EntityCreator.SpawnUnit(WhiteroseRanged, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newWhiteroseRanged.GroupId = unit.GroupId;
                        unit.Destroy();
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }

    class ChangeWereratIrovetti : IModEventHandler, IAreaLoadingStagesHandler //ModMaker event interface for handling mod enable/disable, IArea is for the onAreaLoad methods
    {
        static LibraryScriptableObject library => Main.Library;

        public int Priority => 200; //don't really worry about this.  Mainly helpful with menu creation. 

        static readonly string originWereratIrovettiBP = "8a1f2d257fdc8e44e882ca4952f82ff7";
        static readonly string copyWereratIrovettiBP = "c88710b7a72d48ca97c200fbab642249";
        static readonly string areaToReplaceBPIn = "bf9dbc2998849ee40bbdba9cb40a7d4c";

        static BlueprintUnit WereratIrovetti;

        public static void Fix()
        {
            WereratIrovetti = library.CopyAndAdd<BlueprintUnit>(originWereratIrovettiBP, "JMMTWereratIrovetti", copyWereratIrovettiBP); //copies, not a deep copy
            WereratIrovetti.LocalizedName = ScriptableObject.CreateInstance<SharedStringAsset>(); //Creates a new instance so the original hydra's name doesn't change too.
            WereratIrovetti.LocalizedName.String = Helpers.CreateString("JMMTWereratIrovetti", "Wererat");

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
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originWereratIrovettiBP))
                    {

                        var newWereratIrovetti = Game.Instance.EntityCreator.SpawnUnit(WereratIrovetti, unit.Position, Quaternion.LookRotation(unit.OrientationDirection), Game.Instance.CurrentScene.MainState);
                        newWereratIrovetti.GroupId = unit.GroupId;
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






