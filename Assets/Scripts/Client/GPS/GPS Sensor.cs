using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client.GPS
{
    /// <summary>
    /// Clientside GPS sensor module will be handled by using Mapbox library.
    /// </summary>
    /// <description>
    /// The Mapbox library will be used to handle all of our GPS clientside calls.
    /// The Mapbox Maps SDK for Unity is a C#-based wrapper for Mapbox's location APIs.
    /// It allows us to easily access and interact with our Maps, Geocoding, and Directions
    /// services from within the Unity 3D platform and there we won't necessarily need to
    /// focus on coding the client GPS sensors since Mapbox will handle it. 
    /// For further Mapbox API information please check this link: https://docs.mapbox.com/mapbox-unity-sdk/api/unity/Mapbox.Unity.html
    /// 
    /// \n\n
    /// All other classes included in the Client.GPS of the API documentation are the primary classes from Mapbox of what we'll be using.
    /// </description>
    public class GPSSensor : MonoBehaviour
    {

        /// <summary>
        /// Location contains orientation, latitude, longitute, accuracy and timestamp. 
        /// This is what will contain all the curernt location information.
        /// </summary>
        /// <description> 
        /// It also tracks if device has location services enabled and if device is using
        /// GPS or a network. Here is the Mapbox API link for location: https://docs.mapbox.com/mapbox-unity-sdk/api/unity/Mapbox.Unity.Location.Location.html
        /// </description> 
        public struct Location
        {
            /// <summary>
            /// Horizontal accuracy of the location
            /// </summary>
            /// <returns>
            /// System.Single
            /// </returns>
            public float accuracy;

            /// <summary>
            /// Orientation (where the device is facing)
            /// </summary>
            /// <returns>
            /// System.Single
            /// </returns>
            public float deviceOrientation;

            /// <summary>
            /// If location was acquired via the client GPS or active location provider (GoogleMaps).
            /// </summary>
            /// <returns>
            /// System.Boolean
            /// </returns>
            public bool hasGPS;

            /// <summary>
            /// If the location service has been enabled on the device
            /// </summary>
            /// <returns>
            /// System.Boolean
            /// </returns>
            public bool isLocationServiceEnabled;

            /// <summary>
            /// If the location service has been enabled on the device
            /// </summary>
            /// <returns>
            /// Pair value (lat,long); Vector2
            /// </returns>
            public Vector2 latLong;

            /// <summary>
            /// Name of location provider. Name can be GPS, network (wireless) or 'null' if there is no current provider.
            /// </summary>
            /// <returns>
            /// System.String
            /// </returns>
            public string provider;

            /// <summary>
            /// Name of the location provider script class in Unity
            /// </summary>
            /// <returns>
            /// System.String
            /// </returns>
            public string providerClass;

            /// <summary>
            /// Number of satellites in view when location was acquired. "Null" if GPS or location provider is not enabled.
            /// </summary>
            /// <returns>
            /// System.Nullable<System.Int32>
            /// </returns>
            public int? satellitesInView;

            /// <summary>
            /// Number of satellites actually used to acquire location. "Null" if GPS or location provider is not enabled.
            /// </summary>
            /// <returns>
            /// System.Nullable<System.Int32>
            /// </returns>
            public int? satellitesUsed;

            /// <summary>
            /// Speed of device in meters per second
            /// </summary>
            /// <returns>
            /// System.Single
            /// </returns>
            public float speed;

            /// <summary>
            /// Datetime when location was last updated.
            /// </summary>
            /// <returns>
            /// System.DateTime
            /// </returns>
            public DateTime timeStamp;
        }

        /// <summary>
        /// The DeviceLocationProvider is responsible for providing real world location and heading data,
        /// served directly from native hardware and OS. This relies on Unity's LocationService for location
        /// and Compass for heading.
        /// </summary>
        /// <description> 
        /// It also tracks if device has location services enabled and if device is using GPS or a network.
        /// For more infomration here is the Mapbox API link for DeviceLocationProvider: https://docs.mapbox.com/mapbox-unity-sdk/api/unity/Mapbox.Unity.Location.DeviceLocationProvider.html
        /// </description> 
        public class DeviceLocationProvider
        {
            private Location currentLocation;

            /// <summary>
            /// Accuracy of location in meters. Values above 500 do not turn on the GPS chip, saving battery power.
            /// </summary>
            /// <returns>
            /// System.Single
            /// </returns>
            public float desiredAccuracy;

            /// <summary>
            /// The minimum distance in meters a device must move laterally before Input.location property 
            /// is updated. 
            /// </summary>
            /// <returns>
            /// System.Single
            /// </returns>
            public float updateDistance;

            /// <summary>
            /// Gets the last known location of device
            /// </summary>
            /// <returns>
            /// Location
            /// </returns>
            public Location CurrentLocation { get; }
            
        }

        /// <summary>
        /// Map Visualizer represents a map.
        /// </summary>
        /// <description>
        /// It creates requested tiles and relays them to the factories under itself. It has a caching mechanism
        /// to reuse tiles and does the tile positioning in unity world. The actual API itself inherits multiple 
        /// abstract classes as seen here: https://docs.mapbox.com/mapbox-unity-sdk/api/unity/Mapbox.Unity.Map.MapVisualizer.html
        /// </description>
        public class MapVisualizer
        {
            /// <summary>
            /// Places a tile onto the UI map.
            /// </summary>
            /// <param name="tileId">A tileID that is retrieved from device API</param>
            /// <param name="UnityTile">A unity tile that that is generated from Mapbox API to visualize map styling</param>
            /// <param name="map">The base map itself that is seen by the device</param>
            protected override void PlaceTile(UnwrappedTileId tileId, UnityTile tile, IMapReadable map);
        }

        /// <summary>
        /// The EditorLocationProvider is responsible for providing mock location and heading data for testing purposes in the Unity editor.
        /// </summary>
        /// <description>
        /// This will allow us to test locations by not actually physically being at that location within Unity.
        /// For more information: https://docs.mapbox.com/mapbox-unity-sdk/api/unity/Mapbox.Unity.Map.MapVisualizer.html
        /// </description>
        public class EditorLocationProvider
        {
            /// <summary>
            /// Sets the client location with a spoofed location. This class does have a location property which needs to not be null and filled
            /// in by us for testing and location spoofing.
            /// </summary>
            protected override void SetLocation();
        }


    } //end GPSSensor
}
