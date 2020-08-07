using System;
using RimWorld;
using Verse;

namespace LuciferiumRain
{
	// Token: 0x02000008 RID: 8
	public class WeatherOutComeDoer_GiveHediff : WeatherOutcomeDoer
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002408 File Offset: 0x00000608
		protected override void DoWeatherOutcomeSpecial(Pawn pawn, GameCondition gameCondition)
		{
			Hediff hediff = HediffMaker.MakeHediff(this.hediffDef, pawn, null);
			float num;
			if (this.severity > 0f)
			{
				num = this.severity;
			}
			else
			{
				num = this.hediffDef.initialSeverity;
			}
			if (this.divideByBodySize)
			{
				num /= pawn.BodySize;
			}
			AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, this.toleranceChemical, ref num);
			hediff.Severity = num;
			pawn.health.AddHediff(hediff, null, null);
		}

		// Token: 0x04000011 RID: 17
		public HediffDef hediffDef;

		// Token: 0x04000012 RID: 18
		public float severity = -1f;

		// Token: 0x04000013 RID: 19
		public ChemicalDef toleranceChemical;

		// Token: 0x04000014 RID: 20
		private bool divideByBodySize;
	}
}
