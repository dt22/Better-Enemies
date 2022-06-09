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
        public static void CoPoison()
        {
            string skillName2 = "Acheron_CoPoison_AbilityDef";
            ApplyStatusAbilityDef source2 = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(p => p.name.Equals("Acheron_CoCorruption_AbilityDef"));
            ApplyStatusAbilityDef CoPoison = Helper.CreateDefFromClone(
                source2,
                "41da7c7f-277c-4fd7-bed2-4e44a90a82a0",
                skillName2);
            CoPoison.ViewElementDef = Helper.CreateDefFromClone(
                source2.ViewElementDef,
               "51981712-5036-427c-950c-4cb017d42fce",
               skillName2);
            CoPoison.StatusDef = Helper.CreateDefFromClone(
                source2.StatusDef,
               "1dd0c460-a017-48a3-8e77-43a7684b0655",
               skillName2);

            AddAttackBoostStatusDef CoPoisonStatus = (AddAttackBoostStatusDef)CoPoison.StatusDef;
            CoPoisonStatus.DamageKeywordPairs[0].DamageKeywordDef = Shared.SharedDamageKeywords.PoisonousKeyword;
            CoPoisonStatus.DamageKeywordPairs[0].Value = 10f;
            CoPoison.ViewElementDef.DisplayName1 = new LocalizedTextBind("CoPoison", true);
            CoPoison.ViewElementDef.Description = new LocalizedTextBind("<b>All Pandorans in battle gain +10 Poison Damage.</b>", true);
        }
        public static void CoShred()
        {
            string skillName2 = "Acheron_CoShred_AbilityDef";
            ApplyStatusAbilityDef source2 = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(p => p.name.Equals("Acheron_CoCorruption_AbilityDef"));
            ApplyStatusAbilityDef CoShred = Helper.CreateDefFromClone(
                source2,
                "e862d864-eb59-4a93-bafa-700672e6ae69",
                skillName2);
            CoShred.ViewElementDef = Helper.CreateDefFromClone(
                source2.ViewElementDef,
               "2c1547ce-ad5b-44ef-9e03-56d6327a884a",
               skillName2);
            CoShred.StatusDef = Helper.CreateDefFromClone(
                source2.StatusDef,
               "e115d4c7-44af-4b30-a537-f7aa880a29db",
               skillName2);

            AddAttackBoostStatusDef CoShredStatus = (AddAttackBoostStatusDef)CoShred.StatusDef;
            CoShredStatus.DamageKeywordPairs[0].DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword;
            CoShredStatus.DamageKeywordPairs[0].Value = 3f;
            CoShred.ViewElementDef.DisplayName1 = new LocalizedTextBind("CoShred", true);
            CoShred.ViewElementDef.Description = new LocalizedTextBind("<b>All Pandorans in battle gain +3 Shred Damage.</b>", true);
        }
    }
}
