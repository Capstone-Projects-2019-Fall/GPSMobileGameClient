using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AbstractMap _map;
    [SerializeField] private int _nodeQueryInSeconds = 10;
    [SerializeField] private float _spawnScale = 20f;
    private Vector3 _spawnScaleVector;
    [SerializeField] private GameObject _markerPrefab;

    private List<Vector2d> _locations;
    private List<GameObject> _spawnedObjects;

    private Dictionary<GameObject, Vector2d> nodeLocations;

    private APIWrapper api;

    // accessors
    public AbstractMap Map 
    {
        get => _map;
        set => _map = value;
    }
    public int NodeQueryInSeconds 
    {
        get => _nodeQueryInSeconds;
        set => _nodeQueryInSeconds = value;
    }
    public float SpawnScale 
    {
        get => _spawnScale;
        set {
            try
            {
                _spawnScale = value;
                _spawnScaleVector = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
            catch (Exception e) { Debug.Log(e); }
        }
    }

    void Start()
    {
        //_locations = new List<Vector2d>();
        //_spawnedObjects = new List<GameObject>();
        api = new APIWrapper(this);
        InvokeRepeating("QueryNodes", 0, NodeQueryInSeconds);
        nodeLocations = new Dictionary<GameObject, Vector2d>();
        _spawnScaleVector = new Vector3(SpawnScale, SpawnScale, SpawnScale);
    }

    private void Update()
    {
        foreach (KeyValuePair<GameObject, Vector2d> node in nodeLocations)
        {
            Vector3 position = Map.GeoToWorldPosition(node.Value, true);
            position.y = 5;
            node.Key.transform.localPosition = position;
            node.Key.transform.localScale = _spawnScaleVector;
            
        }
        /*
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            Vector3 position = _map.GeoToWorldPosition(location, true);
            position.y = 5;
            spawnedObject.transform.localPosition = position;
            spawnedObject.transform.localScale = _spawnScaleVector;
        }*/
    }

    private void QueryNodes()
    {
        Vector2d latLon = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;
        StartCoroutine(api.UpdateSurroundingNodes(latLon.x, latLon.y));
    }

    public void SpawnNodes(JSONNode jsonNode)
    {

        foreach (KeyValuePair<GameObject, Vector2d> node in nodeLocations)
        {
            Destroy(node.Key);
        }

        nodeLocations.Clear();
        //_spawnedObjects.Clear();
        //_locations.Clear();

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

        // TODO: Create global controller to call InitializeFactory
        NodeFactory.InitializeFactory();

        // TODO: Get NodeStructure from API (currently calling random Node
        GameObject myNode = NodeFactory.CreateNode(locationString);
        var instance = Instantiate(myNode);
        myNode.transform.localPosition = _map.GeoToWorldPosition(latLon, true);
        myNode.transform.localScale = _spawnScaleVector;

        nodeLocations.Add(instance, latLon);
        //_locations.Add(latLon);
        //_spawnedObjects.Add(instance);
    }
}
