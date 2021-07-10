using UnityEngine;
using Verse;

namespace LuciferiumRain
{
    // Token: 0x02000009 RID: 9
    [StaticConstructorOnStartup]
    public class WeatherOverlay_LuciferiumRain : SkyOverlay
    {
        // Token: 0x04000015 RID: 21
        private static readonly Material LuciferiumRainOverlayWorld = MatLoader.LoadMat("Weather/RainOverlayWorld");

        // Token: 0x06000012 RID: 18 RVA: 0x00002494 File Offset: 0x00000694
        public WeatherOverlay_LuciferiumRain()
        {
            worldOverlayMat = LuciferiumRainOverlayWorld;
            worldOverlayPanSpeed1 = 0.15f;
            worldPanDir1 = new Vector2(-0.25f, -1f);
            worldPanDir1.Normalize();
            worldOverlayPanSpeed2 = 0.022f;
            worldPanDir2 = new Vector2(0.24f, -1f);
            worldPanDir2.Normalize();
        }
    }
}