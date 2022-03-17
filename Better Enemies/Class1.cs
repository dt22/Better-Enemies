﻿using Base.AI.Defs;
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
    public static class MyMod
    {
        public static void HomeMod(Func<string, object, object> api = null)
        {
            HarmonyInstance.Create("your.mod.id").PatchAll();
            api?.Invoke("log verbose", "Mod Initialised.");
            
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();        
            TacticalItemDef queenSpawner = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Spawner_BodyPartDef"));
            TacticalItemDef queenBelcher = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Belcher_BodyPartDef"));            
            BodyPartAspectDef queenHeavyHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Heavy_BodyPartDef]"));
            BodyPartAspectDef queenSpitterHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Spitter_Goo_WeaponDef]"));
            BodyPartAspectDef queenSonicHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Sonic_WeaponDef]"));
                                    
            AIActionMoveAndAttackDef SirenAcidAI = Repo.GetAllDefs<AIActionMoveAndAttackDef>().FirstOrDefault(a => a.name.Equals("Siren_MoveAndSpitAcid_AIActionDef"));
            PsychicScreamAbilityDef sirenPsychicScream = Repo.GetAllDefs<PsychicScreamAbilityDef>().FirstOrDefault(a => a.name.Equals("Siren_PsychicScream_AbilityDef"));
           
            TacticalItemDef crabmanHeavyHead = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Crabman_Head_EliteHumanoid_BodyPartDef"));

            WeaponDef chironBlastMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Mortar_WeaponDef"));
            WeaponDef chironCristalMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Crystal_Mortar_WeaponDef"));
            WeaponDef chironAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Acid_Mortar_WeaponDef"));
            WeaponDef chironFireWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_FireWorm_Launcher_WeaponDef"));
            WeaponDef chironAcidWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_AcidWorm_Launcher_WeaponDef"));
            WeaponDef chironPoisonWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_PoisonWorm_Launcher_WeaponDef"));
             
            WeaponDef crabmanacidGrenadeAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_Grenade_WeaponDef"));
            WeaponDef crabmanadvancedacidGrenadeAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_EliteGrenade_WeaponDef"));

            TacCharacterDef crabShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman9_Shielder_AlienMutationVariationDef"));
            TacCharacterDef crabAShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman10_AdvancedShielder_AlienMutationVariationDef"));
            TacCharacterDef crabAShielder2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman11_AdvancedShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman12_EliteShielder_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman13_EliteShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman14_EliteShielder3_AlienMutationVariationDef"));
            TacCharacterDef crabUShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman15_UltraShielder_AlienMutationVariationDef"));

            queenSpawner.Armor = 60;
            queenBelcher.Armor = 60;
            
            queenHeavyHead.WillPower = 175;
            queenSpitterHead.WillPower = 165;
            queenSonicHead.WillPower = 170;
            
            SirenAcidAI.Weight = 250;
            sirenPsychicScream.ActionPointCost = 0.25f;
            sirenPsychicScream.UsesPerTurn = 1;
            
            chironAcidMortar.DamagePayload.DamageKeywords[0].Value = 20;
            chironAcidMortar.ChargesMax = 18;
            crabmanacidGrenadeAcidMortar.DamagePayload.DamageKeywords[1].Value = 20; //this is second the first being the blast           
            crabmanacidGrenadeAcidMortar.DamagePayload.DamageKeywords[1].Value = 30; //this is second the first being the blast
            chironFireWormMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironFireWormMortar.ChargesMax = 30;    // 15            
            chironAcidWormMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironAcidWormMortar.ChargesMax = 30;    // 15            
            chironPoisonWormMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironPoisonWormMortar.ChargesMax = 30;    // 15            
            chironBlastMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironBlastMortar.ChargesMax = 18;   // 12           
            chironCristalMortar.DamagePayload.ProjectilesPerShot = 3;    // 3
            chironCristalMortar.ChargesMax = 30;    // 12                                                                         

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
