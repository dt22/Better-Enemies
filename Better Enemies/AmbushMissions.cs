using Base.Core;
using Base.Defs;
using PhoenixPoint.Common.Levels.Missions;
using System.Linq;

namespace Better_Enemies
{
    internal class AmbushMissions
    {
        public static void Change_Ambush()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();

            TacMissionDef ambush = Repo.GetAllDefs<TacMissionDef>().FirstOrDefault(a => a.name.Equals("Ambush_TacMissionDef"));

            ambush.MissionData.MissionType.ParticipantsRelations[0].FirstParticipant = TacMissionParticipant.Intruder;
            ambush.MissionData.MissionType.ParticipantsRelations[1].SecondParticipant = TacMissionParticipant.Player;

            ambush.MissionData.MissionType.DefaultDeploymentRules.OrderOfDeployment[0] = TacMissionParticipant.Intruder;
            ambush.MissionData.MissionType.DefaultDeploymentRules.OrderOfDeployment[1] = TacMissionParticipant.Player;

            ambush.MissionData.MissionParticipants = new System.Collections.Generic.List<TacMissionFactionData>
            {
                ambush.MissionData.MissionParticipants[1],
                ambush.MissionData.MissionParticipants[2],
                ambush.MissionData.MissionParticipants[0],
            };
        }
    }
}
