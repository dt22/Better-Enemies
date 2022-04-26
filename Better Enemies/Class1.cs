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
        public bool DoubleTheTimeAICanThink = false;
        public bool AdjustHumanPerception = false;
        public float Human_Soldier_Perception = 30;
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

            SoldierDeployment.Change_Deployment();
            Scylla.Chnage_Queen();                                   
            SirenChiron.Chnage_SirenChiron(); 
            ArthronsTriotons.Change_ArthronsTritons();
            SmallCharactersAndSentinels.Change_SmallCharactersAndSentinels();
            BetterAI.Change_AI();
            Perception.Change_Perception();
            AbilityChanges.Change_Abilities();

            MindControlStatusDef mcStatus = Repo.GetAllDefs<MindControlStatusDef>().FirstOrDefault(a => a.name.Equals("MindControl_StatusDef"));                                 
            AISettingsDef aiSettings = Repo.GetAllDefs<AISettingsDef>().FirstOrDefault(a => a.name.Equals("AISettingsDef"));

            if (Config.SameTurnMindControl == true)
            {
                mcStatus.StartActorTurnOnApply = true;
            }

            if (Config.DoubleTheTimeAICanThink == true)
            {
                aiSettings.MaxActorEvaluationTimeInSeconds = 60;
                aiSettings.MillisecondsEvaluationBudget = 20;
            }
            aiSettings.NumberOfActionsConsidered = 3;                                                                        
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
