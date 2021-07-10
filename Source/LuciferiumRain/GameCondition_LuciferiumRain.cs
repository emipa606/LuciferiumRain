using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RimWorld;
using UnityEngine;
using Verse;

namespace LuciferiumRain
{
    // Token: 0x02000003 RID: 3
    public class GameCondition_LuciferiumRain : GameCondition
    {
        // Token: 0x04000002 RID: 2
        private const int LerpTicks = 5000;

        // Token: 0x04000003 RID: 3
        private const float MaxSkyLerpFactor = 0.5f;

        // Token: 0x04000004 RID: 4
        private const float SkyGlow = 0.85f;

        // Token: 0x04000005 RID: 5
        private const int CheckInterval = 3451;

        // Token: 0x04000006 RID: 6
        private const float ToxicPerDay = 0.5f;

        // Token: 0x04000007 RID: 7
        private const float PlantKillChance = 0f;

        // Token: 0x04000008 RID: 8
        private readonly SkyColorSet LuciferiumRainColors;

        // Token: 0x04000009 RID: 9
        private readonly List<SkyOverlay> overlays;

        // Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
        public GameCondition_LuciferiumRain()
        {
            var colorInt = new ColorInt(216, 25, 0);
            var toColor = colorInt.ToColor;
            var colorInt2 = new ColorInt(234, 10, 25);
            LuciferiumRainColors = new SkyColorSet(toColor, colorInt2.ToColor, new Color(0.6f, 0.8f, 0.5f), 0.85f);
            overlays = new List<SkyOverlay>
            {
                new WeatherOverlay_LuciferiumRain()
            };
        }

        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public void GameCondition()
        {
            RuntimeHelpers.RunClassConstructor(typeof(GameCondition).TypeHandle);
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000020E4 File Offset: 0x000002E4
        public override void Init()
        {
            LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
            LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Critical);
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
        public override void GameConditionTick()
        {
            var affectedMaps = AffectedMaps;
            if (Find.TickManager.TicksGame % 3451 == 0)
            {
                foreach (var map in affectedMaps)
                {
                    DoPawnsNanites(map);
                }
            }

            foreach (var skyOverlay in overlays)
            {
                foreach (var map in affectedMaps)
                {
                    skyOverlay.TickOverlay(map);
                }
            }
        }

        private void DoPawnsNanites(Map map)
        {
            var allPawnsSpawned = map.mapPawns.AllPawnsSpawned;
            foreach (var pawn in allPawnsSpawned)
            {
                DoPawnNanites(pawn);
            }
        }

        public static void DoPawnNanites(Pawn p)
        {
            if (p.Spawned && p.Position.Roofed(p.Map))
            {
                return;
            }

            if (!p.RaceProps.IsFlesh)
            {
                return;
            }

            var num = 0.028758334f;
            num *= p.GetStatValue(StatDefOf.ToxicSensitivity);
            if (num != 0f)
            {
                var num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                num *= num2;
                HealthUtility.AdjustSeverity(p, HediffDefOf_LuciferiumHigh.LuciferiumBuildup, num);
            }

            var item = p.health.hediffSet.hediffs.Find(x => x.def.label.ToLower().StartsWith("luciferium buildup"));
            if (p.health.hediffSet.HasHediff(HediffDefOf_LuciferiumHigh.LuciferiumAddiction) &&
                p.health.hediffSet.HasHediff(HediffDefOf_LuciferiumHigh.LuciferiumBuildup))
            {
                p.health.hediffSet.hediffs.Remove(item);
            }
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000022AC File Offset: 0x000004AC
        public override void DoCellSteadyEffects(IntVec3 c, Map map)
        {
            if (c.Roofed(map))
            {
                return;
            }

            var thingList = c.GetThingList(map);
            foreach (var thing in thingList)
            {
                if (thing is Plant)
                {
                    if (Rand.Value < 0.0065f)
                    {
                        thing.Kill();
                    }
                }
                else if (thing.def.category == ThingCategory.Item)
                {
                    var compRottable = thing.TryGetComp<CompRottable>();
                    if (compRottable != null && compRottable.Stage < RotStage.Dessicated)
                    {
                        compRottable.RotProgress += 3000f;
                    }
                }
            }
        }

        // Token: 0x06000006 RID: 6 RVA: 0x0000234C File Offset: 0x0000054C
        public override void GameConditionDraw(Map map)
        {
            foreach (var skyOverlay in overlays)
            {
                skyOverlay.DrawOverlay(map);
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002388 File Offset: 0x00000588
        public override float SkyTargetLerpFactor(Map map)
        {
            return GameConditionUtility.LerpInOutValue(TicksPassed, TicksLeft, 5000f, 0.5f);
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000023A7 File Offset: 0x000005A7
        public override SkyTarget? SkyTarget(Map map)
        {
            return new SkyTarget(0.85f, LuciferiumRainColors, 1f, 1f);
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000023C8 File Offset: 0x000005C8
        public override bool AllowEnjoyableOutsideNow(Map map)
        {
            return true;
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000023CB File Offset: 0x000005CB
        public override List<SkyOverlay> SkyOverlays(Map map)
        {
            return overlays;
        }
    }
}