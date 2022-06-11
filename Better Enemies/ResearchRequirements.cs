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
using PhoenixPoint.Geoscape.Entities.Research.Requirement;
using PhoenixPoint.Geoscape.Entities.Research.Reward;
using PhoenixPoint.Geoscape.Events.Eventus;
using PhoenixPoint.Geoscape.Levels;
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
    internal class ResearchRequirements
    {
        private static readonly DefRepository Repo = MyMod.Repo;
        private static readonly SharedData Shared = MyMod.Shared;
        public static void Create_ResearchRequirements()
        {
            ResearchDef crabGunResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_CrabmanGunner_ResearchDef"));
            ResearchDef crabBasicResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_CrabmanBasic_ResearchDef"));
            ResearchDef fishWretchResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanSneaker_ResearchDef"));
            ResearchDef fishBasicResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanBasic_ResearchDef"));
            ResearchDef fishFootpadResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanAssault_ResearchDef"));
            ResearchDef fishPiercerAssault = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanPiercerAssault_ResearchDef"));
            ResearchDef fishPiercerSniper = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanPiercerSniper_ResearchDef"));
            ResearchDef FishThugAlpha = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanEliteStriker_ResearchDef"));

            ResearchDef Chiron8 = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_Chiron8_ResearchDef"));
            ResearchDef Chiron13 = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_Chiron13_ResearchDef"));
            ResearchDef siren5 = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_Siren5_ResearchDef"));

            crabGunResearch.InitialStates[4].State = ResearchState.Completed;
            fishWretchResearch.InitialStates[4].State = ResearchState.Completed;
            fishFootpadResearch.InitialStates[4].State = ResearchState.Completed;
            fishBasicResearch.Unlocks = new ResearchRewardDef[0];
            
            fishPiercerAssault.RevealRequirements.Container = new ReseachRequirementDefOpContainer[]
            {
                fishPiercerAssault.RevealRequirements.Container[0],
            };

            fishPiercerSniper.RevealRequirements.Container = new ReseachRequirementDefOpContainer[]
            {
                fishPiercerSniper.RevealRequirements.Container[0],
            };

            FishThugAlpha.RevealRequirements.Container = new ReseachRequirementDefOpContainer[]
            {
                FishThugAlpha.RevealRequirements.Container[0],
            };

            string skillName2 = "BE_ALN_Chiron8_ResearchDef_ExistingResearchRequirementDef_0";
            ExistingResearchRequirementDef source2 = Repo.GetAllDefs<ExistingResearchRequirementDef>().FirstOrDefault(p => p.name.Equals("ALN_Chiron8_ResearchDef_ExistingResearchRequirementDef_0"));
            ExistingResearchRequirementDef Chiron9Requirements = Helper.CreateDefFromClone(
                source2,
                "8DED4D57-1EA3-42F1-948F-0AD5C5557430",
                skillName2);

            string skillName3 = "BE_ALN_Chiron13OrSiren5_ResearchDef_ExistingResearchRequirementDef_0";
            ExistingResearchRequirementDef source3 = Repo.GetAllDefs<ExistingResearchRequirementDef>().FirstOrDefault(p => p.name.Equals("ALN_Chiron8_ResearchDef_ExistingResearchRequirementDef_0"));
            ExistingResearchRequirementDef Chiron13OrSiren5 = Helper.CreateDefFromClone(
                source3,
                "BF164326-91E2-4C40-8544-35464717170F",
                skillName3);

            Chiron8.ResearchCost = 0;
            ExistingResearchRequirementDef Chiron8Requirements = (ExistingResearchRequirementDef)Chiron8.RevealRequirements.Container[0].Requirements[0];
            Chiron8Requirements.ResearchID = "NJ_PurificationTech_ResearchDef";
            Chiron8Requirements.Faction = Repo.GetAllDefs<GeoFactionDef>().FirstOrDefault(a => a.name.Equals("Phoenix_GeoPhoenixFactionDef"));

            Chiron9Requirements.ResearchID = "PX_NJ_IncindieryGrenade_WeaponDef_ResearchDef";
            Chiron9Requirements.Faction = Repo.GetAllDefs<GeoFactionDef>().FirstOrDefault(a => a.name.Equals("NewJericho_GeoFactionDef"));

            Chiron8.RevealRequirements.Container[0].Requirements = new ResearchRequirementDef[]
            {
                Chiron8Requirements,
                Chiron9Requirements,
            };

            Chiron13OrSiren5.ResearchID = "PX_AdvancedLaserTech_ResearchDef";
            Chiron13OrSiren5.Faction = Repo.GetAllDefs<GeoFactionDef>().FirstOrDefault(a => a.name.Equals("Phoenix_GeoPhoenixFactionDef"));

            Chiron13.RevealRequirements.Operation = ResearchContainerOperation.ANY;
            Chiron13.RevealRequirements.Container = new ReseachRequirementDefOpContainer[]
            {
                Chiron13.RevealRequirements.Container[0],
                Chiron13.RevealRequirements.Container[1],
                new ReseachRequirementDefOpContainer()
                {
                    Requirements = new ResearchRequirementDef[]
                    {
                        Chiron13OrSiren5,
                    },
                    Operation = ResearchContainerOperation.ANY,
                },
            };

            siren5.RevealRequirements.Operation = ResearchContainerOperation.ANY;
            siren5.RevealRequirements.Container = new ReseachRequirementDefOpContainer[]
            {
                siren5.RevealRequirements.Container[0],
                siren5.RevealRequirements.Container[1],
                new ReseachRequirementDefOpContainer()
                {
                    Requirements = new ResearchRequirementDef[]
                    {
                        Chiron13OrSiren5,
                    },
                    Operation = ResearchContainerOperation.ANY,
                },
            };
        }  
    }
}
