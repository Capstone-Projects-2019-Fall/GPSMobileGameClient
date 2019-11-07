using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

/*
 * This singleton class serves as a C# wrapper to the server HTTP API.
 */
public class APIWrapper
{
    private readonly string baseURL = "https://gps-mobile-game-server.herokuapp.com";
    private static APIWrapper apiWrapper;
    public delegate void Callback<T>(T obj); // This allows different objects to be returned in the callback depending on the request.

    private APIWrapper() { }

    public static APIWrapper getInstance()
    {
        if (apiWrapper == null)
        {
            apiWrapper = new APIWrapper();
        }
        return apiWrapper;
    }

    /*
     * Calls the base URL of the API. Used for testing. Should return the string "Hello World!".
     */
    public IEnumerator getRoot(Callback<UnityWebRequest> callback)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(baseURL);
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
            callback(null);
        }
        callback(unityWebRequest);
    }

    /*
     * Retrieves a list of surrounding nodes based on the given latitude and longitude.
     * The callback function returns a JSONNode object that contains a list of nodes in JSON format.
     */
    public IEnumerator getSurroundingNodes(double latitude, double longitude, Callback<JSONNode> callback, int maxDistance = 2000)
    {
        if (latitude == 0 && longitude == 0)
        {
            yield break;
        }
        //Debug.LogFormat("getSurroundingNodes() Latitude: {0}, Longitude: {1}, Max Distance: {2}", latitude, longitude, maxDistance);

        // Sets up the endpoint  and query parameters and waits for the request to finish.
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(string.Format("{0}/geodata?long={1}&lat={2}&maxDist={3}", baseURL, longitude, latitude, maxDistance));
        yield return unityWebRequest.SendWebRequest();

        //Debug.Log(unityWebRequest.downloadHandler.text);
        JSONNode responseJsonNodes = JSON.Parse(unityWebRequest.downloadHandler.text);

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
            callback(null);
        }
        callback(responseJsonNodes);
    }
}
