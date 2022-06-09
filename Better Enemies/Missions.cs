using Base;
using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Levels.Missions;
using PhoenixPoint.Geoscape.Levels;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Better_Enemies
{
    internal class Missions
    {
        private static readonly DefRepository Repo = MyMod.Repo;
        private static readonly SharedData Shared = MyMod.Shared;
        public static void Change_Ambush()
        {
            CustomMissionTypeDef px14 = Repo.GetAllDefs<CustomMissionTypeDef>().FirstOrDefault(a => a.name.Equals("StoryPX14_CustomMissionTypeDef"));
            CustomMissionTypeDef px1 = Repo.GetAllDefs<CustomMissionTypeDef>().FirstOrDefault(a => a.name.Equals("StoryPX1_CustomMissionTypeDef"));
            CustomMissionTypeDef px15 = Repo.GetAllDefs<CustomMissionTypeDef>().FirstOrDefault(a => a.name.Equals("StoryPX15_CustomMissionTypeDef"));
            ApplyStatusAbilityDef coCorruption = Repo.GetAllDefs<ApplyStatusAbilityDef>().FirstOrDefault(a => a.name.Equals("Acheron_CoCorruption_AbilityDef"));
            TacCharacterDef pool = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SpawningPoolCrabman_TacCharacterDef"));
            TacCharacterDef node = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("CorruptionNode_TacCharacterDef"));

            pool.Data.Abilites = new TacticalAbilityDef[]
            {
                coCorruption,
            };

            node.Data.Abilites = new TacticalAbilityDef[]
            {
                coCorruption,
            };

            px14.IsAiAlertedInitially = true;
            px1.IsAiAlertedInitially = true;
            px15.IsAiAlertedInitially = true;
        }
    }       
}
