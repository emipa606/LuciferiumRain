using RimWorld;
using Verse;

namespace LuciferiumRain;

public abstract class WeatherOutcomeDoer
{
    public float chance = 1f;

    public bool doToGeneratedPawnIfAddicted;

    public void DoWeathernOutcome(Pawn pawn, GameCondition gameCondition)
    {
        if (Rand.Value < chance)
        {
            DoWeatherOutcomeSpecial(pawn, gameCondition);
        }
    }

    protected abstract void DoWeatherOutcomeSpecial(Pawn pawn, GameCondition gameCondition);
}