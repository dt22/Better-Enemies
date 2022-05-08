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
using PhoenixPoint.Tactical.AI;
using PhoenixPoint.Tactical.AI.Actions;
using PhoenixPoint.Tactical.AI.Considerations;
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
using Base.AI;
using PhoenixPoint.Tactical.AI.TargetGenerators;

namespace Better_Enemies
{
    internal class BetterAI
    {
        internal class AIChanges
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

        public static void Change_AI()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();

            AIActorRangeZoneTargetGeneratorDef chironStrikeTargetDef = Repo.GetAllDefs<AIActorRangeZoneTargetGeneratorDef>().FirstOrDefault(a => a.name.Equals("StrikeAbilityZone3x3_AITargetGeneratorDef"));
            AIAbilityDisabledStateConsiderationDef chironStrikeAvailable = Repo.GetAllDefs<AIAbilityDisabledStateConsiderationDef>().FirstOrDefault(a => a.name.Equals("Chiron_StrikeAbilityAvailable_AIConsiderationDef"));

            TacticalNavigationComponentDef queenNav = Repo.GetAllDefs<TacticalNavigationComponentDef>().FirstOrDefault(a => a.name.Equals("Queen_NavigationDef"));
            TacticalNavigationComponentDef scarabNav = Repo.GetAllDefs<TacticalNavigationComponentDef>().FirstOrDefault(a => a.name.Equals("PX_Scarab_NavigationDef"));
            AIActionsTemplateDef queenAITemplate = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("Queen_AIActionsTemplateDef"));
            AIActionsTemplateDef QueenAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("Queen_AIActionsTemplateDef"));

            AIActionsTemplateDef soldierAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("AIActionsTemplateDef"));
            
            AIActionsTemplateDef crabmanAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("Crabman_AIActionsTemplateDef"));
            AIActionsTemplateDef crabmanTankAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("CrabmanTank_AIActionsTemplateDef"));
            AIActionsTemplateDef crabmanBrawlerAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("CrabmanBrawler_AIActionsTemplateDef"));
            AIActionDeployShieldDef crabDeployShield = Repo.GetAllDefs<AIActionDeployShieldDef>().FirstOrDefault(a => a.name.Equals("Crabman_DeployShield_AIActionDef"));

            AIActionsTemplateDef fishmanAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("Fishman_AIActionsTemplateDef"));
            AISafePositionConsiderationDef fishmanSafeAI = Repo.GetAllDefs<AISafePositionConsiderationDef>().FirstOrDefault(a => a.name.Equals("Fishman_SafePosition_AIConsiderationDef"));

            AIActionsTemplateDef SirenAITemplate = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("Siren_AIActionsTemplateDef"));
            WeaponDef sirenAcidTorso = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Siren_Torso_AcidSpitter_WeaponDef"));
            WeaponDef sirenArmisAcidTorso = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("Siren_Torso_Orichalcum_WeaponDef"));

            AIActionsTemplateDef acheronAAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("AcheronAggressive_AIActionsTemplateDef"));
            AIActionsTemplateDef acheronDAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("AcheronDefensive_AIActionsTemplateDef"));
             
            AIActionEndCharacterTurnDef endturn = Repo.GetAllDefs<AIActionEndCharacterTurnDef>().FirstOrDefault(a => a.name.Equals("EndCharacterTurn_AIActionDef"));
            AIActionMoveAndAttackDef moveAndShoot = Repo.GetAllDefs<AIActionMoveAndAttackDef>().FirstOrDefault(a => a.name.Equals("MoveAndShoot_AIActionDef"));
            AIActionMoveAndAttackDef moveAndStrike = Repo.GetAllDefs<AIActionMoveAndAttackDef>().FirstOrDefault(a => a.name.Equals("MoveAndStrike_AIActionDef"));
            AIActionDeployShieldDef deployShield = Repo.GetAllDefs<AIActionDeployShieldDef>().FirstOrDefault(a => a.name.Equals("DeployShield_AIActionDef"));            
            AIActionMoveToPositionDef moveRandom = Repo.GetAllDefs<AIActionMoveToPositionDef>().FirstOrDefault(a => a.name.Equals("MoveToRandomWaypoint_AIActionDef"));
            AIActionMoveToPositionDef moveSafe = Repo.GetAllDefs<AIActionMoveToPositionDef>().FirstOrDefault(a => a.name.Equals("MoveToSafePosition_AIActionDef"));
            AIActionMoveToPositionDef moveNoShield = Repo.GetAllDefs<AIActionMoveToPositionDef>().FirstOrDefault(a => a.name.Equals("Crabman_Advance_Normal_WithoutShield_AIActionDef"));
            AIActionMoveAndEscapeDef flee = Repo.GetAllDefs<AIActionMoveAndEscapeDef>().FirstOrDefault(a => a.name.Equals("Flee_AIActionDef"));       
            AIActionOverwatchDef overwatch = Repo.GetAllDefs<AIActionOverwatchDef>().FirstOrDefault(a => a.name.Equals("Overwatch_AIActionDef"));
            AIActionMoveAndExecuteAbilityDef moveAndQuickAimAI = Repo.GetAllDefs<AIActionMoveAndExecuteAbilityDef>().FirstOrDefault(a => a.name.Equals("MoveAndQuickAim_AIActionDef"));

            AIActionMoveAndAttackDef mAShoot = AIChanges.CreateDefFromClone(
                    Repo.GetAllDefs<AIActionMoveAndAttackDef>().FirstOrDefault(t => t.name.Equals("MoveAndShoot_AIActionDef")),
                    "3fd2dfd1-3cc0-4c71-b427-22afd020b45d",
                    "BC_MoveAndShoot_AIActionDef");
            AIActionMoveAndAttackDef mAStrike = AIChanges.CreateDefFromClone(
                Repo.GetAllDefs<AIActionMoveAndAttackDef>().FirstOrDefault(a => a.name.Equals("MoveAndStrike_AIActionDef")),
                "78c28fb8-0573-467a-a1c3-94b40673ef47",
                "VC_MoveAndStrike_AIActionDef");


            fishmanAI.ActionDefs[2] = mAShoot;
            fishmanAI.ActionDefs[3] = mAStrike;
            fishmanAI.ActionDefs[2].Weight = 500;
            mAShoot.Weight = 500;
            fishmanAI.ActionDefs[3].Weight = 300;

            //fishmanAI.ActionDefs[4].Weight = 200;
            //fishmanAI.ActionDefs[5].Weight = 1000;
            //fishmanSafeAI.NoneCoverProtection = 0.5f;
            //fishmanSafeAI.VisionScoreWhenVisibleByAllEnemies = 0.1f;

            moveAndQuickAimAI.Weight = 75;

            soldierAI.ActionDefs[7].Weight = 2;
            soldierAI.ActionDefs[26].Weight = 350;
                                  
            acheronAAI.ActionDefs[1].Weight = 250;
            
            QueenAI.ActionDefs[9].Weight = 0.01f;
            queenNav.NavAreas = queenNav.NavAreas.AddToArray("WalkableArmadillo");
           
            queenNav.NavMeshAreasDef.Areas = new Base.Entities.NavAreaData[]
            {
               new Base.Entities.NavAreaData
               {
                   Name = "WalkableBigMonster",
                   SpeedBase = 0,
                   SpeedFactor = 1,
               },

               new Base.Entities.NavAreaData
               {
                   Name = "WalkableArmadillo",
                   SpeedBase = 1,
                   SpeedFactor = 1,
               },
            };

            queenAITemplate.ActionDefs = new AIActionDef[]
            {
                queenAITemplate.ActionDefs[0],
                queenAITemplate.ActionDefs[1],
                queenAITemplate.ActionDefs[2],
                queenAITemplate.ActionDefs[3],
                queenAITemplate.ActionDefs[4],
                queenAITemplate.ActionDefs[5],
                queenAITemplate.ActionDefs[6],
                queenAITemplate.ActionDefs[7],
                queenAITemplate.ActionDefs[8],
                queenAITemplate.ActionDefs[9],
                queenAITemplate.ActionDefs[10],
                queenAITemplate.ActionDefs[12],
                queenAITemplate.ActionDefs[13],
            };

            SirenAITemplate.ActionDefs = new AIActionDef[]
            {
                SirenAITemplate.ActionDefs[0],
                SirenAITemplate.ActionDefs[1],
                SirenAITemplate.ActionDefs[2],
                SirenAITemplate.ActionDefs[3],
                SirenAITemplate.ActionDefs[4],
                SirenAITemplate.ActionDefs[5],
                SirenAITemplate.ActionDefs[6],
                SirenAITemplate.ActionDefs[7],
                SirenAITemplate.ActionDefs[8],
                SirenAITemplate.ActionDefs[9],
                mAShoot,
            };

            sirenArmisAcidTorso.Tags = new GameTagsList
            {
                sirenArmisAcidTorso.Tags[0],
                sirenArmisAcidTorso.Tags[1],
                sirenArmisAcidTorso.Tags[2],
                sirenArmisAcidTorso.Tags[3],
                sirenArmisAcidTorso.Tags[4],
                Repo.GetAllDefs<ItemClassificationTagDef>().FirstOrDefault(p => p.name.Equals("GunWeapon_TagDef")),
            };

            chironStrikeAvailable.IgnoredStates = new string[0];
            chironStrikeTargetDef.MaxRange = 99;            
        }
    }
}
