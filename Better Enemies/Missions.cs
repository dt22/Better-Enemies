using Base;
using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Levels.Missions;
using PhoenixPoint.Geoscape.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Better_Enemies
{
    internal class Missions
    {
        public static void Change_Ambush()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();

            CustomMissionTypeDef px14 = Repo.GetAllDefs<CustomMissionTypeDef>().FirstOrDefault(a => a.name.Equals("StoryPX14_CustomMissionTypeDef"));
            CustomMissionTypeDef px1 = Repo.GetAllDefs<CustomMissionTypeDef>().FirstOrDefault(a => a.name.Equals("StoryPX1_CustomMissionTypeDef"));
            CustomMissionTypeDef px15 = Repo.GetAllDefs<CustomMissionTypeDef>().FirstOrDefault(a => a.name.Equals("StoryPX15_CustomMissionTypeDef"));

            px14.IsAiAlertedInitially = true;
            px1.IsAiAlertedInitially = true;
            px15.IsAiAlertedInitially = true;
        }
    }       
}
