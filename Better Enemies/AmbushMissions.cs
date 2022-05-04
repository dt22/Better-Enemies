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
            ambush.MissionData.MissionType.ParticipantsRelations[1].FirstParticipant = TacMissionParticipant.Player;
        }
    }
}
