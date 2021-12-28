using UnityEngine;
using Verse;

namespace LuciferiumRain;

[StaticConstructorOnStartup]
public class WeatherOverlay_LuciferiumRain : SkyOverlay
{
    private static readonly Material LuciferiumRainOverlayWorld = MatLoader.LoadMat("Weather/RainOverlayWorld");

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