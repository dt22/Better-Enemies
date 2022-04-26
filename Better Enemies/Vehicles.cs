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
using Code.PhoenixPoint.Tactical.Entities.Equipments;
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
    internal class Vehicles
    {
        public static void Change_Vehicles()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            
            GroundVehicleWeaponDef ArmadilloFT = Repo.GetAllDefs<GroundVehicleWeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Armadillo_Mephistopheles_GroundVehicleWeaponDef"));
            AIActionsTemplateDef AspidaAI = Repo.GetAllDefs<AIActionsTemplateDef>().FirstOrDefault(a => a.name.Equals("Aspida_AIActionsTemplateDef"));
            AIActionMoveAndAttackDef VehicleShoot = Repo.GetAllDefs<AIActionMoveAndAttackDef>().FirstOrDefault(a => a.name.Equals("MoveAndShoot3x3_AIActionDef"));

            ArmadilloFT.Tags = new GameTagsList
            {
                ArmadilloFT.Tags[0],
                ArmadilloFT.Tags[1],
                ArmadilloFT.Tags[2],
                ArmadilloFT.Tags[3],
                ArmadilloFT.Tags[4],
                ArmadilloFT.Tags[5],
                ArmadilloFT.Tags[6],
                Repo.GetAllDefs<ItemClassificationTagDef>().FirstOrDefault(p => p.name.Equals("GunWeapon_TagDef")),
            };

            AspidaAI.ActionDefs = new AIActionDef[]
            {
                AspidaAI.ActionDefs[0],
                AspidaAI.ActionDefs[1],
                AspidaAI.ActionDefs[2],
                AspidaAI.ActionDefs[3],
                AspidaAI.ActionDefs[4],
                AspidaAI.ActionDefs[5],
                VehicleShoot,
            };
        }
    }
}
