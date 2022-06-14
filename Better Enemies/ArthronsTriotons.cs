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
using PhoenixPoint.Common.Entities.Items;
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
    internal class ArthronsTriotons
    {
        private static readonly DefRepository Repo = MyMod.Repo;
        private static readonly SharedData Shared = MyMod.Shared;
        public static void Change_ArthronsTritons()
        {     
            TacticalItemDef crabmanHeavyHead = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("Crabman_Head_EliteHumanoid_BodyPartDef"));
            
            TacCharacterDef crab9 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman9_Shielder_AlienMutationVariationDef"));
            TacCharacterDef crab10 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman10_AdvancedShielder_AlienMutationVariationDef"));
            TacCharacterDef crab11 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman11_AdvancedShielder2_AlienMutationVariationDef"));
            TacCharacterDef crab12 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman12_EliteShielder_AlienMutationVariationDef"));
            TacCharacterDef crab13 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman13_EliteShielder2_AlienMutationVariationDef"));
            TacCharacterDef crab14 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman14_EliteShielder3_AlienMutationVariationDef"));
            TacCharacterDef crab15 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman15_UltraShielder_AlienMutationVariationDef"));            
            TacCharacterDef crab24 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman24_Pretorian_AlienMutationVariationDef"));
            TacCharacterDef crab25= Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman25_AdvancedPretorian_AlienMutationVariationDef"));
            TacCharacterDef crab26 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman26_AdvancedPretorian2_AlienMutationVariationDef"));           
            TacCharacterDef crab30 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman30_UltraPretorian_AlienMutationVariationDef"));         
            TacCharacterDef crab38 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman38_UltraAcidRanger_AlienMutationVariationDef"));
            TacCharacterDef crab34 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Crabman34_UltraRanger_AlienMutationVariationDef"));

            WeaponDef arthronGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Grenade_WeaponDef"));
            WeaponDef arthronEliteGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_EliteGrenade_WeaponDef"));
            WeaponDef arthronAcidGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_Grenade_WeaponDef"));
            WeaponDef arthronAcidEliteGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_EliteGrenade_WeaponDef"));
            
            WeaponDef fishArmsParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Fishman_UpperArms_Paralyzing_BodyPartDef"));
            WeaponDef fishArmsEliteParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_UpperArms_Paralyzing_BodyPartDef"));

            TacCharacterDef fish7 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman7_EliteStriker_AlienMutationVariationDef"));
            TacCharacterDef fish8 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman8_PiercerAssault_AlienMutationVariationDef"));
            TacCharacterDef fish11 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman11_Sniper_AlienMutationVariationDef"));
            TacCharacterDef fish12 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman12_FocusSniper_AlienMutationVariationDef"));
            TacCharacterDef fish13 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman13_AgroSniper_AlienMutationVariationDef"));
            TacCharacterDef fish14 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman14_PiercerSniper_AlienMutationVariationDef"));
            TacCharacterDef fish15 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman15_ViralAssault_AlienMutationVariationDef"));
            TacCharacterDef fish17 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman15_ViralAssault_AlienMutationVariationDef"));
            TacCharacterDef fishSniper5 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_Shrowder_Sniper"));
            TacCharacterDef fishSniper6 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman_Shrowder_TacCharacterDef"));

            fishArmsParalyze.DamagePayload.DamageKeywords[1].Value = 8;
            fishArmsEliteParalyze.DamagePayload.DamageKeywords[1].Value = 16;

            fish7.Data.EquipmentItems = new ItemDef[]
            {
                Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Redemptor_WeaponDef")),
            };

            fish8.Data.EquipmentItems = new ItemDef[]
            {
                Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Obliterator_WeaponDef")),
            };

            fish14.Data.EquipmentItems = new ItemDef[]
            {
                Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Subjector_WeaponDef")),
            };

            fish15.Data.BodypartItems[3] = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_UpperArms_BloodSucker_BodyPartDef"));
            fish17.Data.BodypartItems[3] = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_UpperArms_BloodSucker_BodyPartDef"));
            
            //foreach (TacActorSimpleAbilityAnimActionDef animActionDef in Repo.GetAllDefs<TacActorSimpleAbilityAnimActionDef>().Where(aad => aad.name.Contains("Queen_AnimActionsDef")))
            //{
            //    if (animActionDef.AbilityDefs != null && !animActionDef.AbilityDefs.Contains(MindControl))
            //    {
            //        animActionDef.AbilityDefs = animActionDef.AbilityDefs.Append(MindControl).ToArray();
            //    }
            //}

            arthronAcidGL.DamagePayload.DamageKeywords[1].Value = 20; //this is second the first being the blast           
            arthronAcidEliteGL.DamagePayload.DamageKeywords[1].Value = 30; //this is second the first being the blast

            crab38.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Dash_AbilityDef")),
            };

            crab30.Data.Abilites = new TacticalAbilityDef[]
            {
                Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("MasterMarksman_AbilityDef")),
            };

            foreach (TacCharacterDef TriotonSniper in Repo.GetAllDefs<TacActorDef>().Where(a => a.name.Contains("Fishman") && a.name.Contains("Sniper")))
            {
                TriotonSniper.Data.Abilites = new TacticalAbilityDef[]
                {
                    Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("ExtremeFocus_AbilityDef")),
                };
            }

            foreach (TacCharacterDef crab in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman") && aad.name.Contains("Shielder")))
            {
                crab.Data.Abilites = new TacticalAbilityDef[]
                {
                    Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
                };
            }

            foreach (TacticalAbilityDef ability in Repo.GetAllDefs<TacticalAbilityDef>())
            {
                if (ability.name.Contains("Skirmisher_AbilityDef"))
                {
                    crab15.Data.Abilites = new TacticalAbilityDef[]
                    {
                        Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("CloseQuarters_AbilityDef")),
                        Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Skirmisher_AbilityDef")),

                    };
                } 
            }

            foreach (PassiveModifierAbilityDef ability in Repo.GetAllDefs<TacticalAbilityDef>())
            {
                if (ability.name.Contains("Paranoid_AbilityDef"))
                {
                    foreach (TacCharacterDef crabMyr in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman") && aad.name.Contains("EliteRanger")))
                    {
                        crabMyr.Data.Abilites = new TacticalAbilityDef[]
                        {
                            Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Paranoid_AbilityDef")),
                        };
                    }

                    crab34.Data.Abilites = new TacticalAbilityDef[]
                    {
                        Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Paranoid_AbilityDef")),
                        Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("EnhancedVision_AbilityDef")),
                    };
                }
                else
                {
                    crab34.Data.Abilites = new TacticalAbilityDef[]
                    {
                        Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("EnhancedVision_AbilityDef")),
                    };
                }
                
            }

            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman") &&( aad.name.Contains("Pretorian") || aad.name.Contains("Tank"))))
            {               
               character.Data.Speed = 6;             
            }

            foreach (TacCharacterDef crabShield in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman") && aad.name.Contains("Shielder")))
            {
                crabShield.Data.Speed = 8;
            }            

            foreach(WeaponDef crabmanGl in Repo.GetAllDefs<WeaponDef>().Where(a => a.name.Contains("Crabman_LeftHand_") && a.name.Contains("Grenade_WeaponDef")))
            {
                crabmanGl.DamagePayload.Range = 20;
            }

            foreach(TacCharacterDef commando in Repo.GetAllDefs<TacActorDef>().Where(a => a.name.Contains("Crabman") && a.name.Contains("Commando")))
            {
                commando.Data.Abilites = new TacticalAbilityDef[]
                {
                    Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("BloodLust_AbilityDef")),
                };
            }
        }
    }
}
