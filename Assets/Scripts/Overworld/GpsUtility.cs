using Mapbox.Unity.Map;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using System;

/* GpsUtility Description:
 * A utility class (and Unity singleton) that serves as a wrapper around custom Mapbox SDK calls. Primarily it contains refernces to 
 * useful conversion shorthands and methods that agglomerate several Mapbox API calls.
 */

public static class GpsUtility
{
    public static AbstractMap Map;
    private static bool _initialized => Map != null;
    private const double uupmMagicNumber =  0.18; // Default radius for handled exception

    // Initialize the GpsUtility
    public static void InitialzeUtility(AbstractMap map)
    {
        // Ensures only one GpsUtility can exist
        if (_initialized)
            return;

        // Obtain refernce to the overworld map
        Map = map;
        
    }

    /* Get a conversion from real-world meters to unity units given a GameObject on the overworld.
     * Parameters:
     *   -> Gameobject go: Game object present on the overworld (used to locate an OSM MapTile)
     * Returns:
     *    -> A double representing the number of unity units per real-world meter
     */   
    public static double UnityUnitsPerMeter(GameObject go)
    {
        try
        {
            Vector2d objectGeoLoc = go.transform.GetGeoPosition(Map.CenterMercator, Map.WorldRelativeScale);

            int zoom = (int)Map.Zoom;
            Vector2 m2t = Conversions.MetersToTile(objectGeoLoc, zoom);
            double tileLength = Conversions.TileBounds(m2t, zoom).Size.x;

            // (unityUnitys / tiles) * (tiles / meter) = (unityUnits / meter)
            // Debug.LogFormat("UUPM: {0}", tileLength);
            return 0.0003 * tileLength;
        } catch (Exception e) {
            Debug.Log(e);
            return uupmMagicNumber;
        }
    }
}