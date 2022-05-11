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
using PhoenixPoint.Geoscape.Entities.Research.Reward;
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
    internal class SoldierDeployment
    {
        public static void Change_Deployment()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();

            ResearchDef crabGunResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_CrabmanGunner_ResearchDef"));
            ResearchDef crabBasicResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_CrabmanBasic_ResearchDef"));
            ResearchDef fishWretchResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanSneaker_ResearchDef"));
            ResearchDef fishBasicResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanBasic_ResearchDef"));
            ResearchDef fishFootpadResearch = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("ALN_FishmanAssault_ResearchDef"));


            TacCharacterDef syass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Assault1_CharacterTemplateDef"));
            TacCharacterDef sysniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper1_CharacterTemplateDef"));
            TacCharacterDef syinf1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator1_CharacterTemplateDef"));
            TacCharacterDef syass2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Assault2_CharacterTemplateDef"));
            TacCharacterDef sysniper2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper2_CharacterTemplateDef"));
            TacCharacterDef syinf2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator2_CharacterTemplateDef"));
            TacCharacterDef syass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Assault3_CharacterTemplateDef"));
            TacCharacterDef sysniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper3_CharacterTemplateDef"));
            TacCharacterDef syinf3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator3_CharacterTemplateDef"));

            TacCharacterDef anass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault_CharacterTemplateDef"));
            TacCharacterDef anassault1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault1_CharacterTemplateDef"));
            TacCharacterDef anzerker1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker1_CharacterTemplateDef"));
            TacCharacterDef anBerzerker1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_CharacterTemplateDef"));
            TacCharacterDef anpriestJ1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_JudgementPriest1_CharacterTemplateDef"));
            TacCharacterDef anass2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault2_CharacterTemplateDef"));
            TacCharacterDef anass2one = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault2_1_CharacterTemplateDef"));
            TacCharacterDef anass2two = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault2_2_CharacterTemplateDef"));
            TacCharacterDef anzerker2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker2_CharacterTemplateDef"));
            TacCharacterDef anzerker21 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker2_1_CharacterTemplateDef"));
            TacCharacterDef anzerker22 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker2_2_CharacterTemplateDef"));
            TacCharacterDef anpriestJ2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_JudgementPriest2_CharacterTemplateDef"));
            TacCharacterDef anass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Assault3_1_CharacterTemplateDef"));
            TacCharacterDef anzerker3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker3_1_CharacterTemplateDef"));
            TacCharacterDef anpriestJ3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_JudgementPriest3_CharacterTemplateDef"));
            TacCharacterDef anpriestSC1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_ScreamingPriest1_CharacterTemplateDef"));
            TacCharacterDef anpriestSC2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_ScreamingPriest2_CharacterTemplateDef"));
            TacCharacterDef anpriestSC3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_ScreamingPriest3_CharacterTemplateDef"));
            TacCharacterDef anpriestSY1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_SynodPriest1_CharacterTemplateDef"));
            TacCharacterDef anpriestSY2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_SynodPriest2_CharacterTemplateDef"));
            TacCharacterDef anpriestSY3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("AN_SynodPriest3_CharacterTemplateDef"));

            TacCharacterDef njass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault1_CharacterTemplateDef"));
            TacCharacterDef njheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy1_CharacterTemplateDef"));
            TacCharacterDef njsniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper1_CharacterTemplateDef"));
            TacCharacterDef njtech1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician1_CharacterTemplateDef"));
            TacCharacterDef njass2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault2_CharacterTemplateDef"));
            TacCharacterDef njheavy2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy2_CharacterTemplateDef"));
            TacCharacterDef njsniper2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper2_CharacterTemplateDef"));
            TacCharacterDef njtech2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician2_CharacterTemplateDef"));
            TacCharacterDef njass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault3_CharacterTemplateDef"));
            TacCharacterDef njheavy3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy3_CharacterTemplateDef"));
            TacCharacterDef njsniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper3_CharacterTemplateDef"));
            TacCharacterDef njtech3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician3_CharacterTemplateDef"));

            TacCharacterDef fkass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Assault1_CharacterTemplateDef"));
            TacCharacterDef fkzerker1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Berserker1_CharacterTemplateDef"));
            TacCharacterDef fkpriestJ1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_JudgementPriest1_CharacterTemplateDef"));
            TacCharacterDef fkpriestSC1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_ScreamingPriest1_CharacterTemplateDef"));
            TacCharacterDef fkpriestSY1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_SynodPriest1_CharacterTemplateDef"));
            TacCharacterDef fkass2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Assault2_CharacterTemplateDef"));
            TacCharacterDef fkzerker2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Berserker2_CharacterTemplateDef"));
            TacCharacterDef fkpriestJ2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_JudgementPriest2_CharacterTemplateDef"));
            TacCharacterDef fkpriestSC2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_ScreamingPriest2_CharacterTemplateDef"));
            TacCharacterDef fkpriestSY2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_SynodPriest2_CharacterTemplateDef"));
            TacCharacterDef fkass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Assault3_CharacterTemplateDef"));
            TacCharacterDef fkzerker3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_Berserker3_CharacterTemplateDef"));
            TacCharacterDef fkpriestJ3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_JudgementPriest3_CharacterTemplateDef"));
            TacCharacterDef fkpriestSC3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_ScreamingPriest3_CharacterTemplateDef"));
            TacCharacterDef fkpriestSY3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("FK_SynodPriest3_CharacterTemplateDef"));

            TacCharacterDef banass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Assault1_CharacterTemplateDef"));
            TacCharacterDef banass2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Assault2_CharacterTemplateDef"));
            TacCharacterDef banass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Assault3_CharacterTemplateDef"));
            TacCharacterDef bansniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Sniper1_CharacterTemplateDef"));
            TacCharacterDef bansniper2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Sniper2_CharacterTemplateDef"));
            TacCharacterDef bansniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Sniper3_CharacterTemplateDef"));
            TacCharacterDef banheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Heavy1_CharacterTemplateDef"));
            TacCharacterDef banheavy2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Heavy2_CharacterTemplateDef"));
            TacCharacterDef banheavy3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("BAN_Heavy3_CharacterTemplateDef"));

            TacCharacterDef inAss = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("IN_Assault_TacCharacterDef"));
            TacCharacterDef inheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("IN_Heavy_TacCharacterDef"));
            TacCharacterDef insniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("IN_Sniper_TacCharacterDef"));
            TacCharacterDef SinAss = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("S_IN_Assault_TacCharacterDef"));

            TacCharacterDef NEUAss = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NEU_Assault_TacCharacterDef"));
            TacCharacterDef NEUheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NEU_Heavy_TacCharacterDef"));
            TacCharacterDef NEUsniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("NEU_Sniper_TacCharacterDef"));
            TacCharacterDef SNEUAss = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("S_NEU_Assault_TacCharacterDef"));
            TacCharacterDef SNEUheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("S_NEU_Heavy_TacCharacterDef"));
            TacCharacterDef SNEUsniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("S_NEU_Sniper_TacCharacterDef"));

            TacCharacterDef puass1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Assault1_CharacterTemplateDef"));
            TacCharacterDef puheavy1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Heavy1_CharacterTemplateDef"));
            TacCharacterDef pusniper1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Sniper1_CharacterTemplateDef"));
            TacCharacterDef puinf1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Infiltrator1_CharacterTemplateDef"));
            TacCharacterDef putech1 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Technician1_CharacterTemplateDef"));
            TacCharacterDef puass2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Assault2_CharacterTemplateDef"));
            TacCharacterDef puheavy2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Heavy2_CharacterTemplateDef"));
            TacCharacterDef pusniper2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Sniper2_CharacterTemplateDef"));
            TacCharacterDef puinf2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Infiltrator2_CharacterTemplateDef"));
            TacCharacterDef putech2 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Technician2_CharacterTemplateDef"));
            TacCharacterDef puass3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Assault3_Jugg_CharacterTemplateDef"));
            TacCharacterDef puheavy3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Heavy3_Jugg_CharacterTemplateDef"));
            TacCharacterDef pusniper3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Sniper3_Exo_CharacterTemplateDef"));
            TacCharacterDef puinf4 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Infiltrator4_Shin_CharacterTemplateDef"));
            TacCharacterDef putech3 = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("PU_Technician3_Jugg_CharacterTemplateDef"));            

            syass1.Data = syass3.Data;
            sysniper1.Data = sysniper3.Data;
            syinf1.Data = syinf3.Data;
            syass2.Data = syass3.Data;
            sysniper2.Data = sysniper3.Data;
            syinf2.Data = syinf3.Data;

            anass1.Data = anass3.Data;
            anassault1.Data = anass3.Data;
            anass2one.Data = anass3.Data;
            anass2two.Data = anass3.Data;
            anBerzerker1.Data = anzerker3.Data;
            anzerker1.Data = anzerker3.Data;
            anzerker21.Data = anzerker3.Data;
            anzerker22.Data = anzerker3.Data;
            anpriestJ1.Data = anpriestJ3.Data;
            anpriestSC1.Data = anpriestSC3.Data;
            anpriestSY1.Data = anpriestSY3.Data;
            anass2.Data = anass3.Data;
            anzerker2.Data = anzerker3.Data;
            anpriestJ2.Data = anpriestJ3.Data;
            anpriestSC2.Data = anpriestSC3.Data;
            anpriestSY2.Data = anpriestSY3.Data;

            njass1.Data = njass3.Data;
            njheavy1.Data = njheavy3.Data;
            njsniper1.Data = njsniper3.Data;
            njtech1.Data = njtech3.Data;
            njass2.Data = njass3.Data;
            njheavy2.Data = njheavy3.Data;
            njsniper2.Data = njsniper3.Data;
            njtech2.Data = njtech3.Data;

            fkass1.Data = fkass3.Data;
            fkzerker1.Data = fkzerker3.Data;
            fkpriestJ1.Data = fkpriestJ3.Data;
            fkpriestSC1.Data = fkpriestSC3.Data;
            fkpriestSY1.Data = fkpriestSY3.Data;
            fkass2.Data = fkass3.Data;
            fkzerker2.Data = fkzerker3.Data;
            fkpriestJ2.Data = fkpriestJ3.Data;
            fkpriestSC2.Data = fkpriestSC3.Data;
            fkpriestSY2.Data = fkpriestSY3.Data;

            banass1.Data = banass3.Data;
            banheavy1.Data = banheavy3.Data;
            bansniper1.Data = bansniper3.Data;
            banass2.Data = banass3.Data;
            banheavy2.Data = banheavy3.Data;
            bansniper2.Data = bansniper3.Data;

            inAss.Data = banass3.Data;
            SinAss.Data = banass3.Data;
            inheavy1.Data = banheavy3.Data;
            insniper1.Data = bansniper3.Data;

            //NEUAss.Data = banass3.Data;        
            //SNEUAss.Data = banass3.Data;       
            //NEUsniper1.Data = bansniper3.Data;
            //SNEUsniper1.Data = bansniper3.Data;         
            //NEUheavy1.Data = banheavy3.Data;          
            //SNEUheavy1.Data = banheavy3.Data;

            puass1.Data = puass3.Data;
            puheavy1.Data = puheavy3.Data;
            pusniper1.Data = pusniper3.Data;
            puinf1.Data = puinf4.Data;
            putech1.Data = putech3.Data;
            puass2.Data = puass3.Data;
            puheavy2.Data = puheavy3.Data;
            pusniper2.Data = pusniper3.Data;
            puinf2.Data = puinf4.Data;
            putech2.Data = putech3.Data;

            crabGunResearch.InitialStates[4].State = ResearchState.Completed;
            fishWretchResearch.InitialStates[4].State = ResearchState.Completed;
            fishFootpadResearch.InitialStates[4].State = ResearchState.Completed;
            fishBasicResearch.Unlocks = new PhoenixPoint.Geoscape.Entities.Research.Reward.ResearchRewardDef[0];

           //ResearchDef venomBolt = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("SYN_VenomBolt_ResearchDef"));
           //ResearchDef laserWeapons = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("SYN_LaserWeapons_ResearchDef"));
           //ResearchDef poisonWeapons = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("SYN_PoisonWeapons_ResearchDef"));
           //ResearchDef nightVision = Repo.GetAllDefs<ResearchDef>().FirstOrDefault(a => a.name.Equals("SYN_NightVision_ResearchDef"));
           //UnitTemplateResearchRewardDef thief1 = Repo.GetAllDefs<UnitTemplateResearchRewardDef>().FirstOrDefault(a => a.name.Equals("SYN_InfiltratorTech_ResearchDef_UnitTemplateResearchRewardDef_2"));
           //UnitTemplateResearchRewardDef thief2 = Repo.GetAllDefs<UnitTemplateResearchRewardDef>().FirstOrDefault(a => a.name.Equals("SYN_InfiltratorTech_ResearchDef_UnitTemplateResearchRewardDef_3"));
           //UnitTemplateResearchRewardDef sy_assault_4 = Repo.GetAllDefs<UnitTemplateResearchRewardDef>().FirstOrDefault(a => a.name.Equals("SYN_VenomBolt_ResearchDef_UnitTemplateResearchRewardDef_3"));
           //
           //laserWeapons.Unlocks = new ResearchRewardDef[]
           //{
           //    laserWeapons.Unlocks[0],
           //    laserWeapons.Unlocks[1],
           //    laserWeapons.Unlocks[2],
           //    venomBolt.Unlocks[4],
           //    venomBolt.Unlocks[5],
           //};
           //
           //thief1.Add = false;
           //thief2.Add = false;
           //
           //poisonWeapons.Unlocks = new ResearchRewardDef[]
           //{
           //    poisonWeapons.Unlocks[0],
           //    poisonWeapons.Unlocks[1],
           //    poisonWeapons.Unlocks[2],
           //    poisonWeapons.Unlocks[3],
           //    nightVision.Unlocks[5],
           //    nightVision.Unlocks[6],
           //    nightVision.Unlocks[7],
           //};
        }
    }
}
