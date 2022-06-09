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
    internal class ArthronsTriotons
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
        public static void Change_ArthronsTritons()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
          
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
            
            WeaponDef arthronGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Grenade_WeaponDef"));
            WeaponDef arthronEliteGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_EliteGrenade_WeaponDef"));
            WeaponDef arthronAcidGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_Grenade_WeaponDef"));
            WeaponDef arthronAcidEliteGL = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Crabman_LeftHand_Acid_EliteGrenade_WeaponDef"));
            
            WeaponDef fishArmsParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Fishman_UpperArms_Paralyzing_BodyPartDef"));
            WeaponDef fishArmsEliteParalyze = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_UpperArms_Paralyzing_BodyPartDef"));          
            
            TacCharacterDef fishSniper = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman11_Sniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman12_FocusSniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman13_AgroSniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper4 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman14_PiercerSniper_AlienMutationVariationDef"));
            TacCharacterDef fishSniper5 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FishmanElite_Shrowder_Sniper"));
            TacCharacterDef fishSniper6 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fishman_Shrowder_TacCharacterDef"));

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


            crabmanHeavyHead.Abilities = new AbilityDef[]
            {
                //Repo.GetAllDefs<AbilityDef>().FirstOrDefault(a => a.name.Equals("BloodLust_AbilityDef")),
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
                //Repo.GetAllDefs<TacticalAbilityDef>().FirstOrDefault(a => a.name.Equals("Regeneration_Torso_Passive_AbilityDef")),

            };

            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman") && (aad.name.Contains("Pretorian") || aad.name.Contains("Tank"))))
            {               
               character.Data.Speed = 6;             
            }

            crabShielder.Data.Speed = 8;
            crabAShielder.Data.Speed = 8;
            crabAShielder2.Data.Speed = 8;
            crabEShielder.Data.Speed = 8;
            crabEShielder2.Data.Speed = 8;
            crabEShielder3.Data.Speed = 8;
            crabUShielder.Data.Speed = 8;
            arthronAcidGL.DamagePayload.DamageKeywords[1].Value = 20; //this is second the first being the blast           
            arthronAcidEliteGL.DamagePayload.DamageKeywords[1].Value = 30; //this is second the first being the blast

            foreach(WeaponDef crabmanGl in Repo.GetAllDefs<WeaponDef>().Where(a => a.name.Contains("Crabman_LeftHand_") && a.name.Contains("Grenade_WeaponDef")))
            {
                crabmanGl.DamagePayload.Range = 20;
            }

        }
    }
}
