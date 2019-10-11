using System.Collections;
using System.Collections.Generic;
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
    public class DeviceLocationProvider : MonoBehaviour
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
        /// Gets the last known location of device
        /// </summary>
        /// <returns>
        /// Location
        /// </returns>
        public Location CurrentLocation { get; }

        
    }
}
