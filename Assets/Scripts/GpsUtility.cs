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
    private static AbstractMap _map;
    private static bool _initialized => _map != null;
    private const double uupmMagicNumber = 600.0;

    // Initialize the GpsUtility
    public static void InitialzeUtility(AbstractMap map)
    {
        // Ensures only one GpsUtility can exist
        if (_initialized)
            return;

        // Obtain refernce to the overworld map
        _map = map;
        
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
            Vector2d objectGeoLoc = go.transform.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);

            int zoom = (int)_map.Zoom;
            Vector2 m2t = Conversions.MetersToTile(objectGeoLoc, zoom);
            double tileLength = Conversions.TileBounds(m2t, zoom).Size.x;

            // (tiles / meter) * (unityUnitys / tiles) = (unityUnits / meter)
            return (1 / _map.UnityTileSize) * tileLength;
        } catch (ArgumentException ae) {
            Debug.Log(ae);
            return uupmMagicNumber;
        }
    }
}