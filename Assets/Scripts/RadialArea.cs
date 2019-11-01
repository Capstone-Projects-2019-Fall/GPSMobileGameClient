using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Map;

/* RadialArea Description:
 * RadialAreas are attached to certain types of NodeStructures. They define a circular area around thier corresponding Node.
 * They utilize Events to communicate with the player whenever the player enters or exits their defined radius.
 * Additionally, they are able to passively affect the client while they are within the area (continuously spawning enemies or items).
 * 
 * 
 */

public class RadialArea : MonoBehaviour
{
    [SerializeField] private AbstractMap _map;
    [SerializeField] private GameObject _player;
    private Vector2d playerGeoLocation;
    private Vector2 m2t;
    private RectD tileBounds;
    private double sizeLen;

    private void Awake()
    {
        _map = (AbstractMap) FindObjectOfType(typeof(AbstractMap));
        _player = gameObject;
    }

    private void Update()
    {
        playerGeoLocation = _player.transform.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
        m2t = Conversions.MetersToTile(playerGeoLocation, 16);
        tileBounds = Conversions.TileBounds(m2t, 16);
        sizeLen = tileBounds.Size.x;

        Debug.LogFormat("Tile Width: {0}", sizeLen);
    }
}
