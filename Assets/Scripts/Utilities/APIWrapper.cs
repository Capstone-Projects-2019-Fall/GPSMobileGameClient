using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class APIWrapper
{
    private readonly string baseURL = "https://gps-mobile-game-server.herokuapp.com";

    public SpawnManager spawnManager;

    public APIWrapper(SpawnManager spawnManager)
    {
        this.spawnManager = spawnManager;
    }

    IEnumerator Root()
    {
        UnityWebRequest req = UnityWebRequest.Get(baseURL);
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            Debug.Log(req.downloadHandler.text);
        }
    }

    public IEnumerator UpdateSurroundingNodes(double latitude, double longitude, int maxDistance = 2000)
    {
        if (latitude == 0 && longitude == 0)
        {
            yield break;
        }
        // Debug.LogFormat("UpdateSurroundingNodes() Latitude: {0}, Longitude: {1}, Max Distance: {2}", longitude, longitude, longitude);

        UnityWebRequest req = UnityWebRequest.Get(baseURL + "/geodata?long=" + longitude + "&lat=" + latitude + "&maxDist=" + maxDistance);
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            string responseText = req.downloadHandler.text;
            // Debug.Log(responseText);
            JSONNode responseJsonNodes = JSON.Parse(responseText);
            spawnManager.SpawnNodes(responseJsonNodes);
        }
    }
}
