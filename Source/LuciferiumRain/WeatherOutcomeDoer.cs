using System;
using RimWorld;
using Verse;

namespace LuciferiumRain
{
	// Token: 0x02000007 RID: 7
	public abstract class WeatherOutcomeDoer
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000023DB File Offset: 0x000005DB
		public void DoWeathernOutcome(Pawn pawn, GameCondition gameCondition)
		{
			if (Rand.Value < this.chance)
			{
				this.DoWeatherOutcomeSpecial(pawn, gameCondition);
			}
		}

		// Token: 0x0600000E RID: 14
		protected abstract void DoWeatherOutcomeSpecial(Pawn pawn, GameCondition gameCondition);

		// Token: 0x0400000F RID: 15
		public float chance = 1f;

		// Token: 0x04000010 RID: 16
		public bool doToGeneratedPawnIfAddicted;
	}
}
