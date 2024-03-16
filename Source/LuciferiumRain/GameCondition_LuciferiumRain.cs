using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RimWorld;
using UnityEngine;
using Verse;

namespace LuciferiumRain;

public class GameCondition_LuciferiumRain : GameCondition
{
    private const int LerpTicks = 5000;

    private const float MaxSkyLerpFactor = 0.5f;

    private const float SkyGlow = 0.85f;

    private const int CheckInterval = 3451;

    private const float ToxicPerDay = 0.5f;

    private const float PlantKillChance = 0f;

    private readonly SkyColorSet LuciferiumRainColors;

    private readonly List<SkyOverlay> overlays;

    public GameCondition_LuciferiumRain()
    {
        var colorInt = new ColorInt(216, 25, 0);
        var toColor = colorInt.ToColor;
        var colorInt2 = new ColorInt(234, 10, 25);
        LuciferiumRainColors = new SkyColorSet(toColor, colorInt2.ToColor, new Color(0.6f, 0.8f, 0.5f), 0.85f);
        overlays = [new WeatherOverlay_LuciferiumRain()];
    }

    public void GameCondition()
    {
        RuntimeHelpers.RunClassConstructor(typeof(GameCondition).TypeHandle);
    }

    public override void Init()
    {
        LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
        LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Critical);
    }

    public override void GameConditionTick()
    {
        var affectedMaps = AffectedMaps;
        if (Find.TickManager.TicksGame % 3451 == 0)
        {
            foreach (var map in affectedMaps)
            {
                var allPawnsSpawned = map.mapPawns.AllPawnsSpawned;
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var index = 0; index < allPawnsSpawned.Count; index++)
                {
                    var pawn = allPawnsSpawned[index];
                    DoPawnNanites(pawn);
                }
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
        if (ModLister.BiotechInstalled)
        {
            num *= 1 - p.GetStatValue(StatDefOf.ToxicEnvironmentResistance);
        }
        else
        {
            num *= 1 - p.GetStatValue(StatDefOf.ToxicResistance);
        }

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

    public override void DoCellSteadyEffects(IntVec3 c, Map map)
    {
        if (c.Roofed(map))
        {
            return;
        }

        var thingList = c.GetThingList(map);
        // ReSharper disable once ForCanBeConvertedToForeach, plant can get killed during iteration
        for (var index = 0; index < thingList.Count; index++)
        {
            var thing = thingList[index];
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
                if (compRottable is { Stage: < RotStage.Dessicated })
                {
                    compRottable.RotProgress += 3000f;
                }
            }
        }
    }

    public override void GameConditionDraw(Map map)
    {
        foreach (var skyOverlay in overlays)
        {
            skyOverlay.DrawOverlay(map);
        }
    }

    public override float SkyTargetLerpFactor(Map map)
    {
        return GameConditionUtility.LerpInOutValue(TicksPassed, TicksLeft, 5000f, 0.5f);
    }

    public override SkyTarget? SkyTarget(Map map)
    {
        return new SkyTarget(0.85f, LuciferiumRainColors, 1f, 1f);
    }

    public override bool AllowEnjoyableOutsideNow(Map map)
    {
        return true;
    }

    public override List<SkyOverlay> SkyOverlays(Map map)
    {
        return overlays;
    }
}