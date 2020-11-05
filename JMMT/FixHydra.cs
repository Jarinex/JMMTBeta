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


}






