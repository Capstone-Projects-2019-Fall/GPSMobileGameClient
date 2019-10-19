using System;
using UnityEngine;

namespace Client.GPS
{
    /// <summary>
    /// Location contains orientation, latitude, longitute, accuracy and timestamp.
    /// </summary>
    /// <description> 
    /// It also tracks if device has location services enabled and if device is using
    /// GPS or a network.
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
}


