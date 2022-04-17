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
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Geoscape.Entities.DifficultySystem;
using PhoenixPoint.Geoscape.Events.Eventus;
using PhoenixPoint.Tactical;
using PhoenixPoint.Tactical.AI.Actions;
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
    internal class Clone
    {
        public static T CreateDefFromClone<T>(T source, string guid, string name) where T : BaseDef
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            try
            {
                if (Repo.GetDef(guid) != null)
                {
                    if (!(Repo.GetDef(guid) is T tmp))
                    {
                        throw new TypeAccessException($"An item with the GUID <{guid}> has already been added to the Repo, but the type <{Repo.GetDef(guid).GetType().Name}> does not match <{typeof(T).Name}>!");
                    }
                    else
                    {
                        if (tmp != null)
                        {
                            return tmp;
                        }
                    }
                }
                T tmp2 = Repo.GetRuntimeDefs<T>(true).FirstOrDefault(rt => rt.Guid.Equals(guid));
                if (tmp2 != null)
                {
                    return tmp2;
                }
                Type type = null;
                string resultName = "";
                if (source != null)
                {
                    int start = source.name.IndexOf('[') + 1;
                    int end = source.name.IndexOf(']');
                    string toReplace = !name.Contains("[") && start > 0 && end > start ? source.name.Substring(start, end - start) : source.name;
                    resultName = source.name.Replace(toReplace, name);
                }
                else
                {
                    type = typeof(T);
                    resultName = name;
                }
                T result = (T)Repo.CreateRuntimeDef(
                    source,
                    type,
                    guid);
                result.name = resultName;
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
    internal class ModConfig
    {
        public bool SameTurnMindControl = false;
    }
    public static class MyMod
    {
        internal static ModConfig Config;
        public static void HomeMod(Func<string, object, object> api = null)
        {
            MyMod.Config = ((api("config", null) as ModConfig) ?? new ModConfig());
            HarmonyInstance.Create("your.mod.id").PatchAll();
            api?.Invoke("log verbose", "Mod Initialised.");

            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            TacticalItemDef queenSpawner = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Spawner_BodyPartDef"));
            TacticalItemDef queenBelcher = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Belcher_BodyPartDef"));
            BodyPartAspectDef queenHeavyHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Heavy_BodyPartDef]"));
            BodyPartAspectDef queenSpitterHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Spitter_Goo_WeaponDef]"));
            BodyPartAspectDef queenSonicHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Sonic_WeaponDef]"));

            PsychicScreamAbilityDef sirenPsychicScream = Repo.GetAllDefs<PsychicScreamAbilityDef>().FirstOrDefault(a => a.name.Equals("Siren_PsychicScream_AbilityDef"));

            TacticalItemDef crabmanHeavyHead = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Crabman_Head_EliteHumanoid_BodyPartDef"));
            MindControlStatusDef mcStatus = Repo.GetAllDefs<MindControlStatusDef>().FirstOrDefault(a => a.name.Equals("MindControl_StatusDef"));
            WeaponDef chironBlastMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Mortar_WeaponDef"));
            WeaponDef chironCristalMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Crystal_Mortar_WeaponDef"));
            WeaponDef chironAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Acid_Mortar_WeaponDef"));
            WeaponDef chironFireWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_FireWorm_Launcher_WeaponDef"));
            WeaponDef chironAcidWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_AcidWorm_Launcher_WeaponDef"));
            WeaponDef chironPoisonWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_PoisonWorm_Launcher_WeaponDef"));

            WeaponDef crabmanacidGrenadeAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_Grenade_WeaponDef"));
            WeaponDef crabmanadvancedacidGrenadeAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_EliteGrenade_WeaponDef"));
            TacticalItemDef sirenLegsHeavy = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Heavy_BodyPartDef"));
            TacticalItemDef sirenLegsAgile = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Agile_BodyPartDef"));
            TacticalItemDef sirenLegsOrichalcum = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Orichalcum_BodyPartDef"));
            TacCharacterDef crabShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman9_Shielder_AlienMutationVariationDef"));
            TacCharacterDef crabAShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman10_AdvancedShielder_AlienMutationVariationDef"));
            TacCharacterDef crabAShielder2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman11_AdvancedShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman12_EliteShielder_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman13_EliteShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman14_EliteShielder3_AlienMutationVariationDef"));
            TacCharacterDef crabUShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman15_UltraShielder_AlienMutationVariationDef"));
            TacCharacterDef sirenBanshee = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Siren3_InjectorBuffer_AlienMutationVariationDef"));
            TacCharacterDef sirenHarbinger = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Siren4_SlasherBuffer_AlienMutationVariationDef"));
            TacticalPerceptionDef sirenPerception = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault(a => a.name.Equals("Siren_PerceptionDef"));
            TacCharacterDef sirenArmis = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Siren5_Orichalcum_AlienMutationVariationDef"));
            WeaponDef sirenInjectorArms = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Siren_Arms_Injector_WeaponDef"));
            TacticalPerceptionDef tacticalPerceptionEgg = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("Fireworm_Egg_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionHatchling = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("SentinelHatching_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionTerror = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("SentinelTerror_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionMindFraggerEgg = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("EggFacehugger_PerceptionDef"));
            WeaponDef fishArmsParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Fishman_UpperArms_Paralyzing_BodyPartDef"));
            WeaponDef fishArmsEliteParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_UpperArms_Paralyzing_BodyPartDef"));
            TacCharacterDef chironFireHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron2_FireWormHeavy_AlienMutationVariationDef"));
            TacCharacterDef chironPoisonHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron4_PoisonWormHeavy_AlienMutationVariationDef"));
            TacCharacterDef chironAcidHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron6_AcidWormHeavy_AlienMutationVariationDef"));
            TacCharacterDef chironGooHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron8_GooHeavy_AlienMutationVariationDef"));
            TacticalNavigationComponentDef queenNav = Repo.GetAllDefs<TacticalNavigationComponentDef>().FirstOrDefault(a => a.name.Equals("Queen_NavigationDef"));
            TacticalNavigationComponentDef scarabNav = Repo.GetAllDefs<TacticalNavigationComponentDef>().FirstOrDefault(a => a.name.Equals("PX_Scarab_NavigationDef"));

            fishArmsParalyze.DamagePayload.DamageKeywords[1].Value = 8;
            fishArmsEliteParalyze.DamagePayload.DamageKeywords[1].Value = 16;

            tacticalPerceptionMindFraggerEgg.PerceptionRange = 7;
            tacticalPerceptionTerror.PerceptionRange = 18;
            tacticalPerceptionEgg.PerceptionRange = 7;
            tacticalPerceptionHatchling.PerceptionRange = 18;

            
            queenNav.NavAreas = queenNav.NavAreas.AddToArray("WalkableArmadillo");
            queenSpawner.Armor = 60;
            queenBelcher.Armor = 60;

            queenHeavyHead.WillPower = 175;
            queenSpitterHead.WillPower = 165;
            queenSonicHead.WillPower = 170;

            //sirenInjectorArms.BodyPartAspectDef.Stealth = 0.4f;
            sirenPerception.PerceptionRange = 38;
            sirenBanshee.Data.Will = 14;
            sirenInjectorArms.DamagePayload.DamageKeywords[2].Value = 10;
            sirenLegsAgile.Armor = 30;
            sirenLegsOrichalcum.Armor = 30;
            sirenPsychicScream.ActionPointCost = 0.25f;
            sirenPsychicScream.UsesPerTurn = 1;

            chironFireHeavy.Data.Speed = 8;
            chironPoisonHeavy.Data.Speed = 8;
            chironAcidHeavy.Data.Speed = 8;
            chironGooHeavy.Data.Speed = 8;
            chironAcidMortar.DamagePayload.DamageKeywords[0].Value = 20;
            chironAcidMortar.ChargesMax = 18;
            crabmanacidGrenadeAcidMortar.DamagePayload.DamageKeywords[1].Value = 20; //this is second the first being the blast           
            crabmanacidGrenadeAcidMortar.DamagePayload.DamageKeywords[1].Value = 30; //this is second the first being the blast
            chironFireWormMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironFireWormMortar.ChargesMax = 18;    // 15            
            chironAcidWormMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironAcidWormMortar.ChargesMax = 18;    // 15            
            chironPoisonWormMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironPoisonWormMortar.ChargesMax = 18;    // 15            
            chironBlastMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironBlastMortar.ChargesMax = 18;   // 12           
            chironCristalMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironCristalMortar.ChargesMax = 30;    // 12
                  
            if(Config.SameTurnMindControl == true)
            {
                mcStatus.StartActorTurnOnApply = true;
            }

            sirenBanshee.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Siren_PsychicScream_AbilityDef")),
            };
            sirenHarbinger.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            sirenArmis.Data.Abilites = new TacticalAbilityDef[]
            {
                sirenArmis.Data.Abilites[0],
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("IgnorePain_AbilityDef")),
            };

            queenSpawner.Abilities = new AbilityDef[]
            {
                queenSpawner.Abilities[0],
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("AcidResistant_DamageMultiplierAbilityDef")),
            };
            queenBelcher.Abilities = new AbilityDef[]
            {
                queenBelcher.Abilities[0],
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("AcidResistant_DamageMultiplierAbilityDef")),
            };

            crabmanHeavyHead.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("BloodLust_AbilityDef")),
            };
            crabShielder.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            crabAShielder.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            crabAShielder2.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            crabEShielder.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            crabEShielder2.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            crabEShielder3.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            crabUShielder.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Regeneration_Torso_Passive_AbilityDef")),

            };

            crabShielder.Data.Speed = 8;
            crabAShielder.Data.Speed = 8;
            crabAShielder2.Data.Speed = 8;
            crabEShielder.Data.Speed = 8;
            crabEShielder2.Data.Speed = 8;
            crabEShielder3.Data.Speed = 8;
            crabUShielder.Data.Speed = 8;

            int faceHuggerBlastDamage = 1;
            int faceHuggerAcidDamage = 10;
            int faceHuggerAOERadius = 2;

            TacCharacterDef faceHuggerTac = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(p => p.name.Equals("Facehugger_TacCharacterDef"));
            TacCharacterDef faceHuggerVariation = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(p => p.name.Equals("Facehugger_AlienMutationVariationDef"));

            string skillName = "BC_SwarmerAcidExplosion_Die_AbilityDef";
            RagdollDieAbilityDef source = Repo.GetAllDefs<RagdollDieAbilityDef>().FirstOrDefault(p => p.name.Equals("SwarmerAcidExplosion_Die_AbilityDef"));
            RagdollDieAbilityDef sAE = Clone.CreateDefFromClone(
                source,
                "1137345a-a18d-4800-b52e-b15d49f4dabf",
                skillName);
            sAE.ViewElementDef = Clone.CreateDefFromClone(
                source.ViewElementDef,
                "10729876-f764-41b5-9b4e-c8cb98dca771",
                skillName);
            DamagePayloadEffectDef sAEEffect = Clone.CreateDefFromClone(
                Repo.GetAllDefs<DamagePayloadEffectDef>().FirstOrDefault(p => p.name.Equals("E_Element0 [SwarmerAcidExplosion_Die_AbilityDef]")),
                "ac9cd527-72d4-42d2-af32-5efbdf32812e",
                "E_Element0 [BC_SwarmerAcidExplosion_Die_AbilityDef]");

            sAE.DeathEffect = sAEEffect;
            sAEEffect.DamagePayload.DamageKeywords[0].Value = faceHuggerBlastDamage;
            sAEEffect.DamagePayload.DamageKeywords[1].Value = faceHuggerAcidDamage;
            sAEEffect.DamagePayload.AoeRadius = faceHuggerAOERadius;

            sAE.ViewElementDef.DisplayName1 = new LocalizedTextBind("ACID EXPLOSION");
            sAE.ViewElementDef.Description = new LocalizedTextBind("Upon death, the mind fragger bursts in an acid explosion damaging nearby targets");

            faceHuggerTac.Data.Abilites = new TacticalAbilityDef[]
            {
                sAE,
            };
            faceHuggerVariation.Data.Abilites = new TacticalAbilityDef[]
            {
                sAE,
            };

    }
        public static void MainMod(Func<string, object, object> api)
        {
            HarmonyInstance.Create("your.mod.id").PatchAll();
            api("log verbose", "Mod Initialised.");

            /*
            MindControlStatusDef mcStatus = Repo.GetAllDefs<MindControlStatusDef>().FirstOrDefault(a => a.name.Equals("MindControl_StatusDef"));
            StartPreparingShootAbilityDef queenBlastPrepare = Repo.GetAllDefs<StartPreparingShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_StartPreparing_AbilityDef"));
            PreparingStatusDef queenBlastPrepareStatus = Repo.GetAllDefs<PreparingStatusDef>().FirstOrDefault(a => a.name.Equals("Preparing_Queen_StatusDef"));
            AdditionalEffectShootAbilityDef queenBlast = Repo.GetAllDefs<AdditionalEffectShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_GunsFire_ShootAbilityDef"));
            WeaponDef queenLeftBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_LeftArmGun_WeaponDef"));
            WeaponDef queenRightBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_RightArmGun_WeaponDef"));
            DeathBelcherAbilityDef facehuggerDeathSpawn = Repo.GetAllDefs<DeathBelcherAbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Die_Belcher_AbilityDef"));
            TacticalItemDef sirenLegsHeavy = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Heavy_BodyPartDef"));
            TacticalItemDef sirenLegsAgile = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Agile_BodyPartDef"));
            TacticalItemDef sirenLegsOrichalcum = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Orichalcum_BodyPartDef"));
            EquipmentDef crabmanEliteShield = Repo.GetAllDefs<EquipmentDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_EliteShield_EquipmentDef"));
            
            queenBlastPrepareStatus.DisablesActor = false;
            queenBlastPrepare.EndsTurn = false;  
            queenBlast.TraitsRequired[0] = "start";
            queenBlast.TraitsRequired = handgunShoot.TraitsRequired;
            queenBlast.UsesPerTurn = 1;
            queenBlast.EffectTarget = AdditionalEffectTarget.ShootTarget;
            queenBlast.EffectSource = AdditionalEffectSource.SelectedWeapon;
            queenBlast.EffectApplicationMoment = AdditionalEffectApplicationMoment.ShootShotEvent;
            queenBlast.RequiredCharges = 0;
            queenBlast.UsableOnEquipmentWithInsufficientCharges = true;
            queenBlast.TargetingDataDef = queenBlastPrepare.TargetingDataDef;
            queenBlast.UpdatePrediction = true;
            queenBlast.SnapToBodyparts = true;
            queenBlast.CanUseFirstPersonCam = true;
            mcStatus.StartActorTurnOnApply = true;
            queenLeftBlastWeapon.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_GunsFire_ShootAbilityDef")),
            };
            queenRightBlastWeapon.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_GunsFire_ShootAbilityDef")),
            };
            sirenLegsHeavy.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Die_Belcher_AbilityDef")),
            };
            sirenLegsAgile.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Die_Belcher_AbilityDef")),
            };
            sirenLegsOrichalcum.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Die_Belcher_AbilityDef")),
            };
            crabmanEliteShield.Abilities = new AbilityDef[]
            {
                crabmanEliteShield.Abilities[0],
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
            };
            */
        }
    }
}
