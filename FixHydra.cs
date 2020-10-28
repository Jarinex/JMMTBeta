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
                    }
                }
            }
        
        
            if (Game.Instance.CurrentlyLoadedArea.AssetGuid.Equals(areaToReplaceBPIn))
            {
                Main.Mod.Debug("Found the dumbass area");
                foreach (var unit in Game.Instance.State.Units)
                {
                    if (unit.Blueprint.AssetGuidThreadSafe.Equals(originBigBossBP))
                    {
                        Main.Mod.Debug("Replacing little dumbass for Big Dumbass");
                    }
                }
            }
        }

        public void OnAreaScenesLoaded() //unused
        {

        }
    }
}
