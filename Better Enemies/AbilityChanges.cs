using Base.AI;
using Base.AI.Defs;
using Base.Core;
using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects;
using Base.Entities.Effects.ApplicationConditions;
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
            CoPoison();
            CoShred();
            FireWard();
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
            AddAttackBoostStatusDef source2Status = Repo.GetAllDefs<AddAttackBoostStatusDef>().FirstOrDefault(p => p.name.Equals("CorruptionAttack_StatusDef"));
            ApplyStatusAbilityDef CoPoison = Helper.CreateDefFromClone(
                source2,
                "798E3E83-4830-48C3-9FB0-6B74AA0D3104",
                skillName2);
            CoPoison.ViewElementDef = Helper.CreateDefFromClone(
                source2.ViewElementDef,
               "17C828C2-942B-4D65-8FB8-3FC2A8BA6559",
               skillName2);
            CoPoison.StatusDef = Helper.CreateDefFromClone(
            source2.StatusDef,
               "B63E13A8-E5DE-428C-B217-665B09FE3203",
               "CoPoison_StatusDef");                    

            AddAttackBoostStatusDef CoPoisonStatusDef = (AddAttackBoostStatusDef)CoPoison.StatusDef;

            CoPoisonStatusDef.Visuals = Helper.CreateDefFromClone(
            source2Status.Visuals,
               "10686939-87A4-4681-B574-F17D9EB0B47A",
               "E_Visuals [CoPoison_StatusDef]");

            CoPoisonStatusDef.DamageKeywordPairs = new DamageKeywordPair[]
            {
                new DamageKeywordPair()
                {
                    DamageKeywordDef = Shared.SharedDamageKeywords.PoisonousKeyword,
                    Value = 10,
                },
            };

            CoPoisonStatusDef.Visuals.DisplayName1 = new LocalizedTextBind("Poison Attack", true);
            CoPoisonStatusDef.Visuals.Description = new LocalizedTextBind("Successful attacks deal +10 Poison", true);
            CoPoison.ViewElementDef.DisplayName1 = new LocalizedTextBind("CoPoison", true);
            CoPoison.ViewElementDef.Description = new LocalizedTextBind("<b>All Pandorans in battle gain +10 Poison Damage.</b>", true);
        }
        public static void CoShred()
        {
            string skillName2 = "Acheron_CoShred_AbilityDef";
            ApplyStatusAbilityDef source2 = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(p => p.name.Equals("Acheron_CoCorruption_AbilityDef"));   
            AddAttackBoostStatusDef source2Status = Repo.GetAllDefs<AddAttackBoostStatusDef>().FirstOrDefault(p => p.name.Equals("CorruptionAttack_StatusDef"));
            ApplyStatusAbilityDef CoShred = Helper.CreateDefFromClone(
                source2,
                "B96E05F2-B05A-471B-88BD-6E07D2DAADB5",
                skillName2);
            CoShred.ViewElementDef = Helper.CreateDefFromClone(
                source2.ViewElementDef,
               "B6C913D7-397B-41E8-9F23-69F0D7541AE3",
               "E_ViewElement [Aura_OrichalcumStack2_AbilityDef]");
            CoShred.StatusDef = Helper.CreateDefFromClone(
            source2.StatusDef,
               "5E05F92B-6B4C-4932-BCF9-A923F10768A9",
               "CoShred_StatusDef");

            AddAttackBoostStatusDef CoShredStatusDef = (AddAttackBoostStatusDef)CoShred.StatusDef;

            CoShredStatusDef.Visuals = Helper.CreateDefFromClone(
            source2Status.Visuals,
               "F3B15FDE-65A4-41A0-BAA0-A9A38F9064A4",
               "E_Visuals [CoShred_StatusDef]");

            //CoShred.StatusDef = CoShredStatusDef;
            CoShredStatusDef.DamageKeywordPairs = new DamageKeywordPair[]
            {
                new DamageKeywordPair()
                {
                    DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword,
                    Value = 3,
                },
            };

            CoShredStatusDef.Visuals.DisplayName1 = new LocalizedTextBind("Shred Attack", true);
            CoShredStatusDef.Visuals.Description = new LocalizedTextBind("Successful attacks deal +3 Shred", true);
            CoShred.ViewElementDef.DisplayName1 = new LocalizedTextBind("CoShred", true);
            CoShred.ViewElementDef.Description = new LocalizedTextBind("<b>All Pandorans in battle gain +3 Shred Damage.</b>", true);
        }
        public static void FireWard()
        {
            string skillName2 = "FireWard2_AbilityDef";
            ApplyStatusAbilityDef source2 = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(p => p.name.Equals("PsychicWard_AbilityDef"));
            DamageMultiplierStatusDef source2Status = Repo.GetAllDefs<DamageMultiplierStatusDef>().FirstOrDefault(p => p.name.Equals("PsychicWard_StatusDef"));
            ApplyStatusAbilityDef FireWard = Helper.CreateDefFromClone(
                source2,
                "17C4BBDA-0E61-4C9A-A6CF-CAFC7D918916",
                skillName2);
            FireWard.ViewElementDef = Helper.CreateDefFromClone(
                source2.ViewElementDef,
               "0170F091-D4BC-474D-AB6D-4F6C4B18831E",
               skillName2);
            FireWard.StatusDef = Helper.CreateDefFromClone(
                source2.StatusDef,
               "8389563A-F44B-4FA3-BA15-05A8A62C4B59",
               skillName2);
            FireWard.CharacterProgressionData = Helper.CreateDefFromClone(
                source2.CharacterProgressionData,
               "EAD0E990-0E79-4E99-9891-CFECCCE15D25",
               skillName2);

            DamageMultiplierStatusDef FireWardStatus = (DamageMultiplierStatusDef)FireWard.StatusDef;

            FireWardStatus.Visuals = Helper.CreateDefFromClone(
            source2Status.Visuals,
               "7FA26FDE-07AA-43C6-B1E4-43EABC603829",
               "FireWard2_ViewElementDef");

            FireWard.CharacterProgressionData = null;
            FireWard.TargetingDataDef.Origin.Range = 99f;
            
            FireWardStatus.ApplicationConditions = null;
            FireWardStatus.Multiplier = 0.5f;            

            FireWardStatus.DamageTypeDefs = new DamageTypeBaseEffectDef[]
            {
                Repo.GetAllDefs<DamageTypeBaseEffectDef>().FirstOrDefault(p => p.name.Equals("Fire_DamageTypeBaseEffectDef"))
            };

            FireWardStatus.Visuals.DisplayName1 = new LocalizedTextBind("Fire Ward", true);
            FireWardStatus.Visuals.Description = new LocalizedTextBind("Fire Resistantance.", true);
            FireWard.ViewElementDef.DisplayName1 = new LocalizedTextBind("FIRE WARD", true);
            FireWard.ViewElementDef.Description = new LocalizedTextBind("<b>All Pandorans in battle gain Fire resistance.</b>", true);
        }
    }
}
