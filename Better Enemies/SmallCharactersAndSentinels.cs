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
    internal class SmallCharactersAndSentinels
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

        public static void Change_SmallCharactersAndSentinels()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            SharedData Shared = GameUtl.GameComponent<SharedData>();

            TacCharacterDef fireworm = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Fireworm_AlienMutationVariationDef"));
            TacCharacterDef acidworm = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Acidworm_AlienMutationVariationDef"));
            TacCharacterDef poisonworm = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(a => a.name.Equals("Poisonworm_AlienMutationVariationDef"));
            BodyPartAspectDef acidWorm = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Acidworm_Torso_BodyPartDef]"));
            BodyPartAspectDef fireWorm = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Fireworm_Torso_BodyPartDef]"));
            BodyPartAspectDef poisonWorm = Repo.GetAllDefs<BodyPartAspectDef>().FirstOrDefault(a => a.name.Equals("E_BodyPartAspect [Poisonworm_Torso_BodyPartDef]"));
            ApplyDamageEffectAbilityDef aWormDamage = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("AcidwormExplode_AbilityDef"));
            ApplyDamageEffectAbilityDef fWormDamage = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("FirewormExplode_AbilityDef"));
            ApplyDamageEffectAbilityDef pWormDamage = Repo.GetAllDefs<ApplyDamageEffectAbilityDef>().FirstOrDefault(a => a.name.Equals("PoisonwormExplode_AbilityDef"));

            TacticalPerceptionDef tacticalPerceptionEgg = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("Fireworm_Egg_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionHatchling = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("SentinelHatching_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionTerror = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("SentinelTerror_PerceptionDef"));
            TacticalPerceptionDef tacticalPerceptionMindFraggerEgg = Repo.GetAllDefs<TacticalPerceptionDef>().FirstOrDefault((TacticalPerceptionDef a) => a.name.Equals("EggFacehugger_PerceptionDef"));

            TacCharacterDef faceHuggerTac = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(p => p.name.Equals("Facehugger_TacCharacterDef"));
            TacCharacterDef faceHuggerVariation = Repo.GetAllDefs<TacCharacterDef>().FirstOrDefault(p => p.name.Equals("Facehugger_AlienMutationVariationDef"));

            GameTagDef damagedByCaterpillar = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("DamageByCaterpillarTracks_TagDef"));

            int faceHuggerBlastDamage = 1;
            int faceHuggerAcidDamage = 10;
            int faceHuggerAOERadius = 2;

            string skillName = "BC_SwarmerAcidExplosion_Die_AbilityDef";
            RagdollDieAbilityDef source = Repo.GetAllDefs<RagdollDieAbilityDef>().FirstOrDefault(p => p.name.Equals("SwarmerAcidExplosion_Die_AbilityDef"));
            RagdollDieAbilityDef sAE = Clone.CreateDefFromClone(
                source,
                "1137345a-a18d-4800-b52e-b15d49f4dabf",
                skillName);
            sAE.ViewElementDef = Clone.CreateDefFromClone(
                source.ViewElementDef,
                "10729876-f764-41b5-9b4e-c8cb98dca771",
                skillName);
            DamagePayloadEffectDef sAEEffect = Clone.CreateDefFromClone(
                Repo.GetAllDefs<DamagePayloadEffectDef>().FirstOrDefault(p => p.name.Equals("E_Element0 [SwarmerAcidExplosion_Die_AbilityDef]")),
                "ac9cd527-72d4-42d2-af32-5efbdf32812e",
                "E_Element0 [BC_SwarmerAcidExplosion_Die_AbilityDef]");

            sAE.DeathEffect = sAEEffect;
            sAEEffect.DamagePayload.DamageKeywords[0].Value = faceHuggerBlastDamage;
            sAEEffect.DamagePayload.DamageKeywords[1].Value = faceHuggerAcidDamage;
            sAEEffect.DamagePayload.AoeRadius = faceHuggerAOERadius;

            sAE.ViewElementDef.DisplayName1 = new LocalizedTextBind("ACID EXPLOSION");
            sAE.ViewElementDef.Description = new LocalizedTextBind("Upon death, the mind fragger bursts in an acid explosion damaging nearby targets");

            faceHuggerTac.Data.Abilites = new TacticalAbilityDef[]
            {
                sAE,
            };
            faceHuggerVariation.Data.Abilites = new TacticalAbilityDef[]
            {
                sAE,
            };


            tacticalPerceptionMindFraggerEgg.PerceptionRange = 7;
            tacticalPerceptionTerror.PerceptionRange = 18;
            tacticalPerceptionEgg.PerceptionRange = 7;
            tacticalPerceptionHatchling.PerceptionRange = 18;

            foreach (SurveillanceAbilityDef eggSurv in Repo.GetAllDefs<SurveillanceAbilityDef>().Where(p => p.name.Contains("Egg")))
            {
                eggSurv.TargetingDataDef.Origin.Range = 7;
            }

            foreach (SurveillanceAbilityDef sentinelSurv in Repo.GetAllDefs<SurveillanceAbilityDef>().Where(p => p.name.Contains("Sentinel")))
            {
                sentinelSurv.TargetingDataDef.Origin.Range = 18;
            }

            int wormSpeed = 9;
            int wormShredDamage = 3;
            int aWormAcidDamage = 30;
            int aWormBlastDamage = 10;
            int fWormFireDamage = 40;
            int pWormBlastDamage = 25;
            int pWormPoisonDamage = 50;
            fireworm.DeploymentCost = 10;    // 35
            acidworm.DeploymentCost = 10;    // 35
            poisonworm.DeploymentCost = 10;  // 35
            acidWorm.Speed = wormSpeed;
            fireWorm.Speed = wormSpeed;
            poisonWorm.Speed = wormSpeed;

            aWormDamage.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.BlastKeyword, Value = aWormBlastDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.AcidKeyword, Value = aWormAcidDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword, Value = wormShredDamage },
                };

            fWormDamage.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.BurningKeyword, Value = fWormFireDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword, Value = wormShredDamage },
                };

            pWormDamage.DamagePayload.DamageKeywords = new List<DamageKeywordPair>()
                {
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.BlastKeyword, Value = pWormBlastDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.PoisonousKeyword, Value = pWormPoisonDamage },
                new DamageKeywordPair{DamageKeywordDef = Shared.SharedDamageKeywords.ShreddingKeyword, Value = wormShredDamage },
                };

            foreach (TacticalActorDef actor in Repo.GetAllDefs<TacticalActorDef>().Where(a => a.name.Contains("worm") || a.name.Contains("SpiderDrone")))
            {
                actor.GameTags.Add(damagedByCaterpillar);
            }
        }
    }
}
