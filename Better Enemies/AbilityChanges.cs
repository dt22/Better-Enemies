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
        public static void Change_Abilities()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();

            ApplyDamageEffectAbilityDef StomperLegs = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("StomperLegs_Stomp_AbilityDef"));
            ShootAbilityDef guardianBeam = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef"));

            StomperLegs.TargetingDataDef.Origin.TargetFriendlies = false;

            /*
            string skillName2 = "BE_Guardian_Beam_ShootAbilityDef";
            ShootAbilityDef source2 = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(p => p.name.Equals("Guardian_Beam_ShootAbilityDef"));
            ShootAbilityDef BEGB = Clone.CreateDefFromClone(
                source2,
                "64ba51e9-c67b-4e5e-ad61-315e7f796ffa",
                skillName2);
            BEGB.ViewElementDef = Clone.CreateDefFromClone(
                source2.ViewElementDef,
               "20f5659c-890a-4f29-9968-07ea67b04c6b",
               skillName2);
            */

            guardianBeam.UsesPerTurn = 1;
            guardianBeam.TrackWithCamera = false;
            guardianBeam.ShownModeToTrack = PhoenixPoint.Tactical.Levels.KnownState.Revealed;
            ShootAbilitySceneViewDef guardianBeamSVE = (ShootAbilitySceneViewDef)guardianBeam.SceneViewElementDef;
            guardianBeamSVE.HoverMarkerInvalidTarget = PhoenixPoint.Tactical.View.GroundMarkerType.AttackConeNoTarget;
            guardianBeamSVE.LineToCursorInvalidTarget = PhoenixPoint.Tactical.View.GroundMarkerType.AttackLineNoTarget;
            guardianBeam.SceneViewElementDef.HoverMarker = PhoenixPoint.Tactical.View.GroundMarkerType.AttackCone;
            guardianBeam.TargetingDataDef = Repo.GetAllDefs<TacticalTargetingDataDef>().FirstOrDefault(a => a.name.Equals("E_TargetingData [Queen_StartPreparing_AbilityDef]"));
        }
    }
}
