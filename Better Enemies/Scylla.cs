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
    internal class Scylla
    {
        private static readonly DefRepository Repo = MyMod.Repo;
        private static readonly SharedData Shared = MyMod.Shared;
        public static void Chnage_Queen()
        {
            TacticalItemDef queenSpawner = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Spawner_BodyPartDef"));
            TacticalItemDef queenBelcher = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Queen_Abdomen_Belcher_BodyPartDef"));
            
            BodyPartAspectDef queenHeavyHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Heavy_BodyPartDef]"));
            BodyPartAspectDef queenSpitterHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Spitter_Goo_WeaponDef]"));
            BodyPartAspectDef queenSonicHead = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Queen_Head_Sonic_WeaponDef]"));
            
            WeaponDef queenLeftBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_LeftArmGun_WeaponDef"));
            WeaponDef queenRightBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_RightArmGun_WeaponDef"));
            WeaponDef queenBlastWeapon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Queen_Arms_Gun_WeaponDef"));            
            
            AdditionalEffectShootAbilityDef queenBlast = Repo.GetAllDefs<AdditionalEffectShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Queen_GunsFire_ShootAbilityDef"));
            ShootAbilityDef guardianBeam = Repo.GetAllDefs<ShootAbilityDef>().FirstOrDefault(a => a.name.Equals("Guardian_Beam_ShootAbilityDef"));      

            queenLeftBlastWeapon.Abilities = new AbilityDef[]
            {
                guardianBeam,
            };

            queenRightBlastWeapon.Abilities = new AbilityDef[]
            {
                guardianBeam,
            };

            queenBlastWeapon.Abilities = new AbilityDef[]
            {
                guardianBeam,
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

            queenBlastWeapon.Tags = new GameTagsList
            {
                queenBlastWeapon.Tags[0],
                queenBlastWeapon.Tags[1],
                queenBlastWeapon.Tags[2],
                queenBlastWeapon.Tags[3],
                Repo.GetAllDefs<ItemClassificationTagDef>().FirstOrDefault(p => p.name.Equals("ExplosiveWeapon_TagDef"))
            };

            queenLeftBlastWeapon.Tags = new GameTagsList
            {
                queenLeftBlastWeapon.Tags[0],
                queenLeftBlastWeapon.Tags[1],
                queenLeftBlastWeapon.Tags[2],
                Repo.GetAllDefs<ItemClassificationTagDef>().FirstOrDefault(p => p.name.Equals("ExplosiveWeapon_TagDef"))
            };

            queenBlastWeapon.Tags = new GameTagsList
            {
                queenRightBlastWeapon.Tags[0],
                queenRightBlastWeapon.Tags[1],
                queenRightBlastWeapon.Tags[2],
                Repo.GetAllDefs<ItemClassificationTagDef>().FirstOrDefault(p => p.name.Equals("ExplosiveWeapon_TagDef"))
            };

            

            foreach (TacCharacterDef taccharacter in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Contains("Queen")))
            {
                taccharacter.Data.Abilites = new TacticalAbilityDef[]
                {
                    Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CaterpillarMoveAbilityDef")),
                };
             }

            queenBlastWeapon.DamagePayload.DamageKeywords[0].Value = 40;
            queenBlastWeapon.DamagePayload.DamageKeywords[1].Value = 3;
            queenLeftBlastWeapon.DamagePayload.DamageKeywords[0].Value = 40;
            queenLeftBlastWeapon.DamagePayload.DamageKeywords[1].Value = 3;
            queenRightBlastWeapon.DamagePayload.DamageKeywords[0].Value = 40;
            queenRightBlastWeapon.DamagePayload.DamageKeywords[1].Value = 3;           
            
            queenSpawner.Armor = 60;
            queenBelcher.Armor = 60;
            queenHeavyHead.WillPower = 175;
            queenSpitterHead.WillPower = 165;
            queenSonicHead.WillPower = 170;
        }
    }
}
