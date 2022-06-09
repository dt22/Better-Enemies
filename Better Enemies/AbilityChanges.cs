using Base.AI;
using Base.AI.Defs;
using Base.Core;
using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects;
using Base.Entities.Statuses;
using Base.Levels;
using Base.UI;
using Base.Utils.Maths;
using Harmony;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Geoscape.Entities.DifficultySystem;
using PhoenixPoint.Geoscape.Entities.Research;
using PhoenixPoint.Geoscape.Events.Eventus;
using PhoenixPoint.Tactical;
using PhoenixPoint.Tactical.AI;
using PhoenixPoint.Tactical.AI.Actions;
using PhoenixPoint.Tactical.AI.Considerations;
using PhoenixPoint.Tactical.AI.TargetGenerators;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Animations;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Effects;
using PhoenixPoint.Tactical.Entities.Effects.DamageTypes;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using PhoenixPoint.Tactical.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Better_Enemies
{
    internal class AbilityChanges
    {
        private static readonly DefRepository Repo = MyMod.Repo;
        private static readonly SharedData Shared = MyMod.Shared;
        public static void Change_Abilities()
        {
            Clone_GuardianBeam();
        }
        public static void Clone_GuardianBeam()
        {
            //ShootAbilityDef guardianBeam = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef"));
            string skillName2 = "BE_Guardian_Beam_ShootAbilityDef";
            ShootAbilityDef source2 = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(p => p.name.Equals("Guardian_Beam_ShootAbilityDef"));
            ShootAbilityDef BEGB = Helper.CreateDefFromClone(
                source2,
                "cfc8f607-2dac-40e3-bdfb-842f7e1ce71c",
                skillName2);
            BEGB.SceneViewElementDef = Helper.CreateDefFromClone(
                source2.SceneViewElementDef,
               "0bdef0ee-7070-4d21-972e-b2d1f07710ae",
               skillName2);
            BEGB.TargetingDataDef = Helper.CreateDefFromClone(
                source2.TargetingDataDef,
               "be53f499-9627-44b3-9cd8-87410b51f008",
               skillName2);


            BEGB.UsesPerTurn = 1;
            BEGB.TrackWithCamera = false;
            BEGB.ShownModeToTrack = PhoenixPoint.Tactical.Levels.KnownState.Revealed;
            ShootAbilitySceneViewDef guardianBeamSVE = (ShootAbilitySceneViewDef)BEGB.SceneViewElementDef;
            guardianBeamSVE.HoverMarkerInvalidTarget = PhoenixPoint.Tactical.View.GroundMarkerType.AttackConeNoTarget;
            guardianBeamSVE.LineToCursorInvalidTarget = PhoenixPoint.Tactical.View.GroundMarkerType.AttackLineNoTarget;
            guardianBeamSVE.HoverMarker = PhoenixPoint.Tactical.View.GroundMarkerType.AttackCone;
            BEGB.TargetingDataDef = Repo.GetAllDefs<TacticalTargetingDataDef>().FirstOrDefault(a => a.name.Equals("E_TargetingData [Queen_GunsFire_ShootAbilityDef]"));
        }
    }
}
