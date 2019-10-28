using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    int NodeQueryInSeconds = 10;

    [SerializeField]
    float _spawnScale = 20f;

    [SerializeField]
    GameObject _markerPrefab;

    List<Vector2d> _locations;
    List<GameObject> _spawnedObjects;

    private APIWrapper api;

    void Start()
    {
        _locations = new List<Vector2d>();
        _spawnedObjects = new List<GameObject>();
        api = new APIWrapper(this);
        InvokeRepeating("QueryNodes", 0, NodeQueryInSeconds);
    }

    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            Vector3 position = _map.GeoToWorldPosition(location, true);
            position.y = 5;
            spawnedObject.transform.localPosition = position;
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
    }

    private void QueryNodes()
    {
        Vector2d latLon = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;
        StartCoroutine(api.UpdateSurroundingNodes(latLon.x, latLon.y));
    }

    public void SpawnNodes(JSONNode jsonNode)
    {

        foreach (var gameObject in _spawnedObjects)
        {
            Destroy(gameObject);
        }
        _spawnedObjects.Clear();
        _locations.Clear();

        for (int i = 0; i < jsonNode.Count; i++)
        {
            string nodeLongitude = jsonNode[i]["location"]["coordinates"][0];
            string nodeLatitude = jsonNode[i]["location"]["coordinates"][1];
            SpawnMarker(string.Format("{0}, {1}", nodeLatitude, nodeLongitude));
        }
    }

    public void SpawnMarker(string locationString)
    {
        Vector2d latLon = Conversions.StringToLatLon(locationString);
        _locations.Add(latLon);
        Debug.Log(locationString);
        
        var instance = Instantiate(_markerPrefab);
        instance.transform.localPosition = _map.GeoToWorldPosition(latLon, true);
        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
   
        /* TODO: 
        GameObject myNode = NodeFactory.CreateNode(locationString);
        myNode.transform.localPosition = _map.GeoToWorldPosition(latLon, true);
        myNode.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        */
        _spawnedObjects.Add(instance);
    }
}
