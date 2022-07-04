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
    internal class WillPower
    {
        private static readonly DefRepository Repo = MyMod.Repo;
        private static readonly SharedData Shared = MyMod.Shared;
        public static void Change_WillPower()
        {
            foreach(TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Equals("Crabman12_EliteShielder_AlienMutationVariationDef") || a.name.Equals("Crabman12_EliteShielder2_AlienMutationVariationDef") 
            || a.name.Equals("Crabman15_UltraShielder_AlienMutationVariationDef")))
            {
                character.Data.Will -= 4;
            }
            
            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Equals("Crabman12_EliteShielder3_AlienMutationVariationDef")))
            {
                character.Data.Will -= 2;
            }
            
            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Contains("Crabman") && a.name.Contains("Pretorian")))
            {
                character.Data.Will -= 5;
            }
            
            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Contains("Crabman") && (a.name.Contains("EliteViralCommando") || a.name.Contains("UltraViralCommando"))))
            {
                character.Data.Will -= 5;
            }
            
            foreach (TacCharacterDef crabMyr in Repo.GetAllDefs<TacCharacterDef>().Where(aad => aad.name.Contains("Crabman") && (aad.name.Contains("EliteRanger") || aad.name.Contains("UltraRanger"))))
            {
                crabMyr.Data.Will -= 5;
            }

            foreach (TacCharacterDef character in Repo.GetAllDefs<TacCharacterDef>().Where(a => a.name.Contains("Fishman")))
            {
                character.Data.Will -= 5;
            }
        }
    }
}
