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
            SharedData Shared = GameUtl.GameComponent<SharedData>();

            TacticalItemDef queenSpawner = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Spawner_BodyPartDef"));
            TacticalItemDef queenBelcher = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Belcher_BodyPartDef"));
            BodyPartAspectDef queenHeavyHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Heavy_BodyPartDef]"));
            BodyPartAspectDef queenSpitterHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Spitter_Goo_WeaponDef]"));
            BodyPartAspectDef queenSonicHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Sonic_WeaponDef]"));
            TacticalNavigationComponentDef queenNav = Repo.GetAllDefs<TacticalNavigationComponentDef>().FirstOrDefault(a => a.name.Equals("Queen_NavigationDef"));
            AdditionalEffectShootAbilityDef queenBlast = Repo.GetAllDefs<AdditionalEffectShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_GunsFire_ShootAbilityDef"));
            WeaponDef queenLeftBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_LeftArmGun_WeaponDef"));
            WeaponDef queenRightBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_RightArmGun_WeaponDef"));
            WeaponDef queenBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_Arms_Gun_WeaponDef"));

            TacticalItemDef sirenLegsHeavy = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Heavy_BodyPartDef"));
            TacticalItemDef sirenLegsAgile = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Agile_BodyPartDef"));
            TacticalItemDef sirenLegsOrichalcum = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Orichalcum_BodyPartDef"));
            PsychicScreamAbilityDef sirenPsychicScream = Repo.GetAllDefs<PsychicScreamAbilityDef>().FirstOrDefault(a => a.name.Equals("Siren_PsychicScream_AbilityDef"));
            TacCharacterDef sirenBanshee = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Siren3_InjectorBuffer_AlienMutationVariationDef"));
            TacCharacterDef sirenHarbinger = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Siren4_SlasherBuffer_AlienMutationVariationDef"));
            TacticalPerceptionDef sirenPerception = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault(a => a.name.Equals("Siren_PerceptionDef"));
            TacCharacterDef sirenArmis = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Siren5_Orichalcum_AlienMutationVariationDef"));
            WeaponDef sirenInjectorArms = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Siren_Arms_Injector_WeaponDef"));
          
            WeaponDef chironBlastMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Mortar_WeaponDef"));
            WeaponDef chironCristalMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Crystal_Mortar_WeaponDef"));
            WeaponDef chironAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_Acid_Mortar_WeaponDef"));
            WeaponDef chironFireWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_FireWorm_Launcher_WeaponDef"));
            WeaponDef chironAcidWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_AcidWorm_Launcher_WeaponDef"));
            WeaponDef chironPoisonWormMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Chiron_Abdomen_PoisonWorm_Launcher_WeaponDef"));
            TacCharacterDef chironFireHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron2_FireWormHeavy_AlienMutationVariationDef"));
            TacCharacterDef chironPoisonHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron4_PoisonWormHeavy_AlienMutationVariationDef"));
            TacCharacterDef chironAcidHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron6_AcidWormHeavy_AlienMutationVariationDef"));
            TacCharacterDef chironGooHeavy = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Chiron8_GooHeavy_AlienMutationVariationDef"));
            
            WeaponDef crabmanacidGrenadeAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_Grenade_WeaponDef"));
            WeaponDef crabmanadvancedacidGrenadeAcidMortar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_EliteGrenade_WeaponDef"));           
            TacticalItemDef crabmanHeavyHead = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Crabman_Head_EliteHumanoid_BodyPartDef"));
            TacCharacterDef crabShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman9_Shielder_AlienMutationVariationDef"));
            TacCharacterDef crabAShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman10_AdvancedShielder_AlienMutationVariationDef"));
            TacCharacterDef crabAShielder2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman11_AdvancedShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman12_EliteShielder_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman13_EliteShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabEShielder3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman14_EliteShielder3_AlienMutationVariationDef"));
            TacCharacterDef crabUShielder = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman15_UltraShielder_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman24_Pretorian_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman25_AdvancedPretorian_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman26_AdvancedPretorian2_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant4 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman12_EliteShielder_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant5 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman13_EliteShielder2_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant6 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman14_EliteShielder3_AlienMutationVariationDef"));
            TacCharacterDef crabTyrant7 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman14_EliteShielder3_AlienMutationVariationDef"));
                      
            WeaponDef fishArmsParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Fishman_UpperArms_Paralyzing_BodyPartDef"));
            WeaponDef fishArmsEliteParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_UpperArms_Paralyzing_BodyPartDef"));
            TacCharacterDef fishSniper = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman11_Sniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman12_FocusSniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman13_AgroSniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper4 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman14_PiercerSniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper5 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_Shrowder_Sniper"));
            TacCharacterDef fishSniper6 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman_Shrowder_TacCharacterDef"));

            MindControlStatusDef mcStatus = Repo.GetAllDefs<MindControlStatusDef>().FirstOrDefault(a => a.name.Equals("MindControl_StatusDef"));
            TacticalNavigationComponentDef scarabNav = Repo.GetAllDefs<TacticalNavigationComponentDef>().FirstOrDefault(a => a.name.Equals("PX_Scarab_NavigationDef"));
            ShootAbilityDef guardianBeam = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef"));            
            TacCharacterDef fireworm = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fireworm_AlienMutationVariationDef"));
            TacCharacterDef acidworm = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Acidworm_AlienMutationVariationDef"));
            TacCharacterDef poisonworm = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Poisonworm_AlienMutationVariationDef"));
            BodyPartAspectDef acidWorm = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Acidworm_Torso_BodyPartDef]"));
            BodyPartAspectDef fireWorm = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Fireworm_Torso_BodyPartDef]"));
            BodyPartAspectDef poisonWorm = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Poisonworm_Torso_BodyPartDef]"));
            ApplyDamageEffectAbilityDef aWormDamage = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("AcidwormExplode_AbilityDef"));
            ApplyDamageEffectAbilityDef fWormDamage = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("FirewormExplode_AbilityDef"));
            ApplyDamageEffectAbilityDef pWormDamage = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("PoisonwormExplode_AbilityDef"));
           
            TacticalPerceptionDef tacticalPerceptionEgg = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("Fireworm_Egg_PerceptionDef"));         
            TacticalPerceptionDef tacticalPerceptionHatchling = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("SentinelHatching_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionTerror = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("SentinelTerror_PerceptionDef"));           
            TacticalPerceptionDef tacticalPerceptionMindFraggerEgg = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("EggFacehugger_PerceptionDef"));
            

            TacCharacterDef syass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Assault1_CharacterTemplateDef"));
            TacCharacterDef sysniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper1_CharacterTemplateDef"));
            TacCharacterDef syinf1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator1_CharacterTemplateDef"));
            TacCharacterDef syass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Assault3_CharacterTemplateDef"));
            TacCharacterDef sysniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper3_CharacterTemplateDef"));
            TacCharacterDef syinf3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator3_CharacterTemplateDef"));

            TacCharacterDef anass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault1_CharacterTemplateDef"));
            TacCharacterDef anzerker1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker1_CharacterTemplateDef"));
            TacCharacterDef anpriestJ1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_JudgementPriest1_CharacterTemplateDef"));
            TacCharacterDef anass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault3_CharacterTemplateDef"));
            TacCharacterDef anzerker3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker3_CharacterTemplateDef"));
            TacCharacterDef anpriestJ3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_JudgementPriest3_CharacterTemplateDef"));
            TacCharacterDef anpriestSC1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_ScreamingPriest1_CharacterTemplateDef"));
            TacCharacterDef anpriestSC3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_ScreamingPriest3_CharacterTemplateDef"));
            TacCharacterDef anpriestSY1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_SynodPriest1_CharacterTemplateDef"));
            TacCharacterDef anpriestSY3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_SynodPriest3_CharacterTemplateDef"));

            TacCharacterDef njass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault1_CharacterTemplateDef"));
            TacCharacterDef njheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy1_CharacterTemplateDef"));
            TacCharacterDef njsniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper1_CharacterTemplateDef"));
            TacCharacterDef njtech1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician1_CharacterTemplateDef"));
            TacCharacterDef njass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault3_CharacterTemplateDef"));
            TacCharacterDef njheavy3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy3_CharacterTemplateDef"));
            TacCharacterDef njsniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper3_CharacterTemplateDef"));
            TacCharacterDef njtech3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician3_CharacterTemplateDef"));

            TacCharacterDef fkass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Assault1_CharacterTemplateDef"));
            TacCharacterDef fkzerker1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Berserker1_CharacterTemplateDef"));
            TacCharacterDef fkpriestJ1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_JudgementPriest1_CharacterTemplateDef"));
            TacCharacterDef fkpriestSC1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_ScreamingPriest1_CharacterTemplateDef"));
            TacCharacterDef fkpriestSY1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_SynodPriest1_CharacterTemplateDef"));
            TacCharacterDef fkass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Assault3_CharacterTemplateDef"));
            TacCharacterDef fkzerker3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Berserker3_CharacterTemplateDef"));
            TacCharacterDef fkpriestJ3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_JudgementPriest3_CharacterTemplateDef"));
            TacCharacterDef fkpriestSC3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_ScreamingPriest3_CharacterTemplateDef"));
            TacCharacterDef fkpriestSY3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_SynodPriest3_CharacterTemplateDef"));

            TacCharacterDef banass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Assault1_CharacterTemplateDef"));
            TacCharacterDef banass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Assault3_CharacterTemplateDef"));
            TacCharacterDef bansniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Sniper1_CharacterTemplateDef"));
            TacCharacterDef bansniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Sniper3_CharacterTemplateDef"));
            TacCharacterDef banheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Heavy1_CharacterTemplateDef"));
            TacCharacterDef banheavy3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Heavy3_CharacterTemplateDef"));

            TacCharacterDef puass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Assault1_CharacterTemplateDef"));
            TacCharacterDef puheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Heavy1_CharacterTemplateDef"));
            TacCharacterDef pusniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Sniper1_CharacterTemplateDef"));
            TacCharacterDef puinf1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Infiltrator1_CharacterTemplateDef"));
            TacCharacterDef putech1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Technician1_CharacterTemplateDef"));
            TacCharacterDef puass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Assault3_Jugg_CharacterTemplateDef"));
            TacCharacterDef puheavy3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Heavy3_Jugg_CharacterTemplateDef"));
            TacCharacterDef pusniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Sniper3_Exo_CharacterTemplateDef"));
            TacCharacterDef puinf4 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Infiltrator4_Shin_CharacterTemplateDef"));
            TacCharacterDef putech3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Technician3_Jugg_CharacterTemplateDef"));

            ResearchDef crabGunResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_CrabmanGunner_ResearchDef"));
            ResearchDef crabBasicResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_CrabmanBasic_ResearchDef"));
            ResearchDef fishWretchResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanSneaker_ResearchDef"));
            ResearchDef fishBasicResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanBasic_ResearchDef"));
            ResearchDef fishFootpadResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("Fishman3_Assault_AlienMutationVariationDef"));

            crabGunResearch.UnlockRequirements = crabBasicResearch.UnlockRequirements;
            crabGunResearch.InitialStates[4].State = ResearchState.Completed;
            fishWretchResearch.InitialStates[4].State = ResearchState.Completed;
            fishFootpadResearch.InitialStates[4].State = ResearchState.Completed;

            syass1.Data = syass3.Data;
            sysniper1.Data = sysniper3.Data;
            syinf1.Data = syinf3.Data;
            anass1.Data = anass3.Data;
            anzerker1.Data = anzerker3.Data;
            anpriestJ1.Data = anpriestJ3.Data;
            anpriestSC1.Data = anpriestSC3.Data;
            anpriestSY1.Data = anpriestSY3.Data;
            njass1.Data = njass3.Data;
            njheavy1.Data = njheavy3.Data;
            njsniper1.Data = njsniper3.Data;
            njtech1.Data = njtech3.Data;
            fkass1.Data = fkass3.Data;
            fkzerker1.Data = fkzerker3.Data;
            fkpriestJ1.Data = fkpriestJ3.Data;
            fkpriestSC1.Data = fkpriestSC3.Data;
            fkpriestSY1.Data = fkpriestSY3.Data;
            banass1.Data = banass3.Data;
            banheavy1.Data = banheavy3.Data;
            bansniper1.Data = bansniper3.Data;

            puass1.Data = puass3.Data;          
            puheavy1.Data = puheavy3.Data;          
            pusniper1.Data = pusniper3.Data;         
            puinf1.Data = puinf4.Data;           
            putech1.Data = putech3.Data;         

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

            queenLeftBlastWeapon.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef")),
            };
            queenRightBlastWeapon.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef")),
            };
            queenBlastWeapon.Abilities = new AbilityDef[]
            {
                Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef")),
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

            queenBlastWeapon.DamagePayload.DamageKeywords[0].Value = 40;
            queenBlastWeapon.DamagePayload.DamageKeywords[1].Value = 10;            
            queenLeftBlastWeapon.DamagePayload.DamageKeywords[0].Value = 40;
            queenLeftBlastWeapon.DamagePayload.DamageKeywords[1].Value = 10;
            queenRightBlastWeapon.DamagePayload.DamageKeywords[0].Value = 40;
            queenRightBlastWeapon.DamagePayload.DamageKeywords[1].Value = 10;
            queenNav.NavAreas = queenNav.NavAreas.AddToArray("WalkableArmadillo");
            queenSpawner.Armor = 60;
            queenBelcher.Armor = 60;
            queenHeavyHead.WillPower = 175;
            queenSpitterHead.WillPower = 165;
            queenSonicHead.WillPower = 170;

            foreach(TacCharacterDef taccharacter in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Contains("Queen")))
            {
                taccharacter.Data.Abilites = new TacticalAbilityDef[]
                {
                    Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CaterpillarMoveAbilityDef")),
                };
            }

            queenBlastWeapon.Tags = new GameTagsList
            {
                queenBlastWeapon.Tags[0],
                queenBlastWeapon.Tags[1],
                queenBlastWeapon.Tags[2],
                queenBlastWeapon.Tags[3],
                Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("ExplosiveWeapon_TagDef"))
            };

            queenLeftBlastWeapon.Tags = new GameTagsList
            {
                queenLeftBlastWeapon.Tags[0],
                queenLeftBlastWeapon.Tags[1],
                queenLeftBlastWeapon.Tags[2],
                Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("ExplosiveWeapon_TagDef"))
            };

            queenBlastWeapon.Tags = new GameTagsList
            {
                queenRightBlastWeapon.Tags[0],
                queenRightBlastWeapon.Tags[1],
                queenRightBlastWeapon.Tags[2],
                Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("ExplosiveWeapon_TagDef"))
            };

            guardianBeam.TrackWithCamera = false;
            guardianBeam.ShownModeToTrack = PhoenixPoint.Tactical.Levels.KnownState.Revealed;
            ShootAbilitySceneViewDef guardianBeamSVE = (ShootAbilitySceneViewDef)guardianBeam.SceneViewElementDef;
            guardianBeamSVE.HoverMarkerInvalidTarget = PhoenixPoint.Tactical.View.GroundMarkerType.AttackConeNoTarget;
            guardianBeamSVE.LineToCursorInvalidTarget = PhoenixPoint.Tactical.View.GroundMarkerType.AttackLineNoTarget;
            guardianBeam.TargetingDataDef = Repo.GetAllDefs<TacticalTargetingDataDef>().FirstOrDefault(a => a.name.Equals("E_TargetingData [Queen_GunsFire_ShootAbilityDef]"));
            guardianBeam.SceneViewElementDef.HoverMarker = PhoenixPoint.Tactical.View.GroundMarkerType.AttackCone;

            fishArmsParalyze.DamagePayload.DamageKeywords[1].Value = 8;
            fishArmsEliteParalyze.DamagePayload.DamageKeywords[1].Value = 16;

            fishSniper.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
            };
            fishSniper2.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
            };
            fishSniper3.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
            };
            fishSniper4.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
            };
            fishSniper5.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
            };
            fishSniper6.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
            };
            
            sirenPerception.PerceptionRange = 38;
            sirenBanshee.Data.Will = 14;
            sirenInjectorArms.DamagePayload.DamageKeywords[2].Value = 10;
            sirenLegsAgile.Armor = 30;          
            sirenPsychicScream.ActionPointCost = 0.25f;
            sirenPsychicScream.UsesPerTurn = 1;

            if (Config.SameTurnMindControl == true)
            {
                mcStatus.StartActorTurnOnApply = true;
            }

            sirenBanshee.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Siren_PsychicScream_AbilityDef")),
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Thief_AbilityDef")),
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("StealthSpecialist_AbilityDef"))
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
            crabmanacidGrenadeAcidMortar.DamagePayload.DamageKeywords[1].Value = 20; //this is second the first being the blast           
            crabmanacidGrenadeAcidMortar.DamagePayload.DamageKeywords[1].Value = 30; //this is second the first being the blast

            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman")))
            {
                if (character.name.Contains("Pretorian") || character.name.Contains("Tank"))
                {
                    character.Data.Speed = 6;
                }
            }

            chironFireHeavy.Data.Speed = 8;
            chironPoisonHeavy.Data.Speed = 8;
            chironAcidHeavy.Data.Speed = 8;
            chironGooHeavy.Data.Speed = 8;
            chironAcidMortar.DamagePayload.DamageKeywords[0].Value = 20;
            chironAcidMortar.ChargesMax = 18;
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

            tacticalPerceptionMindFraggerEgg.PerceptionRange = 7;
            tacticalPerceptionTerror.PerceptionRange = 18;
            tacticalPerceptionEgg.PerceptionRange = 7;
            tacticalPerceptionHatchling.PerceptionRange = 18;
            
            foreach(SurveillanceAbilityDef eggSurv in Repo.GetAllDefs<SurveillanceAbilityDef>().Where(p => p.name.Contains("Egg")))
            {
                eggSurv.TargetingDataDef.Origin.Range = 7;
            }

            foreach (SurveillanceAbilityDef sentinelSurv in Repo.GetAllDefs<SurveillanceAbilityDef>().Where(p => p.name.Contains("Sentinel")))
            {
                sentinelSurv.TargetingDataDef.Origin.Range = 18;
            }

            int wormSpeed = 9;
            int wormShredDamage = 3;
            int aWormAcidDamage = 30;
            int aWormBlastDamage = 10;
            int fWormFireDamage = 40;
            int pWormBlastDamage = 25;
            int pWormPoisonDamage = 50;
            fireworm.DeploymentCost = 10;    // 35
            acidworm.DeploymentCost = 10;    // 35
            poisonworm.DeploymentCost = 10;  // 35
            acidWorm.Speed = wormSpeed;
            fireWorm.Speed = wormSpeed;
            poisonWorm.Speed = wormSpeed;

            aWormDamage.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.BlastKeyword, Value = aWormBlastDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.AcidKeyword, Value = aWormAcidDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword, Value = wormShredDamage },
                };

            fWormDamage.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.BurningKeyword, Value = fWormFireDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword, Value = wormShredDamage },
                };

            pWormDamage.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.BlastKeyword, Value = pWormBlastDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.PoisonousKeyword, Value = pWormPoisonDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword, Value = wormShredDamage },
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
            
            DeathBelcherAbilityDef facehuggerDeathSpawn = Repo.GetAllDefs<DeathBelcherAbilityDef>().FirstOrDefault(a => a.name.Equals("Mutoid_Die_Belcher_AbilityDef"));
            TacticalItemDef sirenLegsHeavy = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Heavy_BodyPartDef"));
            TacticalItemDef sirenLegsAgile = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Agile_BodyPartDef"));
            TacticalItemDef sirenLegsOrichalcum = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Siren_Legs_Orichalcum_BodyPartDef"));
            EquipmentDef crabmanEliteShield = Repo.GetAllDefs<EquipmentDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_EliteShield_EquipmentDef"));
            
            //guardianBeam.UsableOnDisabledActor = true;
            //guardianBeam.UsableOnNonInteractableActor = true;
            //queenBlastWeapon.Tags = queenLeftBlastWeapon.Tags;
            //queenBlastWeapon.HandsToUse = 0;
            //queenBlastWeapon.WeakAddon = false;
            //queenLeftBlastWeapon.HandsToUse = 0;
            //queenLeftBlastWeapon.AimTransform = queenBlastWeapon.AimTransform;
            //queenLeftBlastWeapon.Tags = queenBlastWeapon.Tags;
            //queenLeftBlastWeapon.WeakAddon = true;
            //queenRightBlastWeapon.HandsToUse = 0;
            //queenRightBlastWeapon.AimTransform = queenBlastWeapon.AimTransform;
            //queenRightBlastWeapon.Tags = queenBlastWeapon.Tags;
            //queenRightBlastWeapon.WeakAddon = true;
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
            AdditionalEffectShootAbilityDef queenBlast = Repo.GetAllDefs<AdditionalEffectShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_GunsFire_ShootAbilityDef"));
            WeaponDef queenLeftBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_LeftArmGun_WeaponDef"));
            WeaponDef queenRightBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_RightArmGun_WeaponDef"));
            /*
            queenBlastWeapon.DamagePayload.DamageDeliveryType = DamageDeliveryType.DirectLine;
            queenBlastWeapon.SpreadDegrees = 1.5f;
            queenBlastWeapon.DamagePayload.Range = -1;
            queenBlastWeapon.TargetBodyPartsOrder = TargetOrderType.FixedOrder;
            queenBlastWeapon.TargetBodyPartGroup = BodyPartGroup.None;
            
            queenLeftBlastWeapon.DamagePayload.DamageDeliveryType = DamageDeliveryType.DirectLine;                      
            queenLeftBlastWeapon.SpreadDegrees = 1.5f;
            queenLeftBlastWeapon.DamagePayload.Range = -1;
            queenLeftBlastWeapon.DamagePayload.OverrideAimIKSettings = true;
            queenLeftBlastWeapon.TargetBodyPartsOrder = TargetOrderType.FixedOrder;
            queenLeftBlastWeapon.TargetBodyPartGroup = BodyPartGroup.None;            
          
            queenRightBlastWeapon.DamagePayload.DamageDeliveryType = DamageDeliveryType.DirectLine;
            queenRightBlastWeapon.DamagePayload.OverrideAimIKSettings = true;
            queenRightBlastWeapon.SpreadDegrees = 1.5f;
            queenRightBlastWeapon.DamagePayload.Range = -1;
            queenRightBlastWeapon.TargetBodyPartsOrder = TargetOrderType.FixedOrder;
            queenRightBlastWeapon.TargetBodyPartGroup = BodyPartGroup.None;
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
            /*
            //puass1.Data.InventoryItems = puass3.Data.InventoryItems;
           //puass1.Data.EquipmentItems = puass3.Data.EquipmentItems;
           //puass1.Data.LevelProgression = puass3.Data.LevelProgression;
           //puass1.Data.LevelProgression.Def = puass3.Data.LevelProgression.Def;
           //puass1.Data.Speed = puass3.Data.Speed;
           //puass1.Data.Strength = puass3.Data.Strength;
           //puass1.Data.Will = puass3.Data.Will;
           //puass1.ComponentSetDef.Equals(puass3.ComponentSetDef);
           //puass1.TacticalActorBaseDef.Equals(puass3.TacticalActorBaseDef);
            */
        }
    }
}
