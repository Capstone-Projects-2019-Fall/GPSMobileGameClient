using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

/*
 * This singleton class serves as a C# wrapper to the server HTTP API.
 */
public static class APIWrapper
{
    private static readonly string baseURL = "https://gps-mobile-game-server.herokuapp.com";
    // private static readonly string baseURL = "localhost:3000";
    private static readonly string mapboxBaseURL = "https://api.mapbox.com";
    private static readonly string mapboxAPIKey = "pk.eyJ1IjoiZ3BzNDM5OCIsImEiOiJjazFvMThha2QwaGhuM25yeXg0cXBvZGtsIn0.P478dP-OvvW_0JQOiI9RZA";
    public delegate void Callback<T>(T obj); // This allows different objects to be returned in the callback depending on the request.

    /*
     * Base definition for all GET requests. URL specifies the endpoint and callback returns
     * a JSONNode which is the server's response.
     */
    private static IEnumerator GET(string URL, Callback<JSONNode> callback)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(URL);
        return SendRequest(unityWebRequest, callback);
    }

    /*
     * Base definition for all DELETE requests. URL specifies the endpoint and callback returns
     * a JSONNode which is the server's response.
     */
    private static IEnumerator DELETE(string URL, Callback<JSONNode> callback)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Delete(URL);
        return SendRequest(unityWebRequest, callback);
    }

    /*
     * Base definition for all POST requests. URL specifies the endpoint, body is the POST's body, and
     * callback returns a JSONNode which is the server's response.
     */
    private static IEnumerator POST(string URL, string body, Callback<JSONNode> callback)
    {
        Debug.LogFormat("POST Body: {0}", body);

        UnityWebRequest unityWebRequest = new UnityWebRequest(URL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(body);
        unityWebRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        unityWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        return SendRequest(unityWebRequest, callback);
    }

    /*
     * Base definition for all Web requests. unityWebRequest is a pre-setup object (i.e. GET or POST)
     * and callback returns a JSONNode which is the server's response.
     */
    private static IEnumerator SendRequest(UnityWebRequest unityWebRequest, Callback<JSONNode> callback)
    {
        Debug.LogFormat("Requesting: {0}", unityWebRequest.url);
        yield return unityWebRequest.SendWebRequest();
        //Debug.Log(unityWebRequest.downloadHandler.text);

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.LogFormat("Error: {0}\nServer Message: {1}", unityWebRequest.error, unityWebRequest.downloadHandler.text);
            callback(null);
        }
        else
        {
            JSONNode jsonResponse = JSON.Parse(unityWebRequest.downloadHandler.text);
            callback(jsonResponse);
        }
    }

    /*
     * Calls the base URL of the API. Used for testing. Should return the string "Hello World!".
     */
    public static IEnumerator getRoot(Callback<string> callback)
    {
        return GET(baseURL, (jsonResponse) => {
            callback(jsonResponse.ToString());
        });
    }

    /*
     * Retrieves a list of surrounding nodes based on the given latitude and longitude.
     * The callback function returns a JSONNode object that contains a list of nodes in JSON format.
     */
    public static IEnumerator getSurroundingNodes(double latitude, double longitude, Callback<JSONNode> callback, int maxDistance = 2000)
    {
        if (latitude == 0 && longitude == 0)
        {
            yield break;
        }
        yield return GET(string.Format("{0}/geodata?long={1}&lat={2}&maxDist={3}", baseURL, longitude, latitude, maxDistance), callback);
    }

    /*
     * Creates a new player in the database and returns a player object with updated fields.
     */
    public static IEnumerator createPlayer(string username, string password, double latitude, double longitude, Callback<JSONNode> callback)
    {
        JSONObject newPlayer = new JSONObject();
        newPlayer["name"] = username;
        newPlayer["password"] = password;
        newPlayer["lat"] = latitude;
        newPlayer["lon"] = longitude;
        return POST(string.Format("{0}/user", baseURL), newPlayer.ToString(), callback);
    }

    /*
     * Retrieves a JSON object representation of a desired player's data.
     * The callback function returns a JSONNode array and the first element should be 
     * a JSON object containing the player's data. If the array is empty then no player 
     * matching the given username was found.
     */
    public static IEnumerator getPlayer(string username, Callback<JSONNode> callback)
    {
        return GET(string.Format("{0}/user/{1}", baseURL, username), callback);
    }

    /*
     * Syncs a player's deck with the server. Username is the name of the user who the deck belongs to,
     * cards is a list of the cards that will overwrite the server's collection, and callback
     * returns a string which is the server's response. Should return "OK" status code 200.
     */
    public static IEnumerator syncPlayerDeck(string username, List<Card> cards, Callback<string> callback)
    {
        JSONObject jsonObject = new JSONObject();
        JSONArray jsonArray = new JSONArray();
        foreach(int cardId in CardFactory.CardsToIdArray(cards))
        {
            jsonArray.Add(cardId);
        }
        jsonObject["deck"] = jsonArray;
        return POST(string.Format("{0}/user/{1}/deck", baseURL, username), jsonObject.ToString(), (jsonResponse) => {
            callback(jsonResponse.ToString());
        });
    }

    public static IEnumerator updatePlayerHomebase(string username, double latitude, double longitude, Callback<string> callback)
    {
        JSONObject jsonObject = new JSONObject();
        jsonObject["lon"] = longitude;
        jsonObject["lat"] = latitude;
        return POST(string.Format("{0}/user/{1}/homebase", baseURL, username), jsonObject.ToString(), (jsonResponse) => {
            callback(jsonResponse.ToString());
        });
    }

     /*
     * Update the enemy health with enemy node id
     */
    public static IEnumerator updateEnemyHealth(string enemyname, float current_health, Callback<string> callback)
    {
        JSONObject jsonObject = new JSONObject();
        
        jsonObject["hp"] = current_health;
        return POST(string.Format("{0}/enemy/update/{1}", baseURL, enemyname), jsonObject.ToString(), (jsonResponse) => {
            callback(jsonResponse.ToString());
        });
    }

     /*
     * Update the structure of the node
     */
    public static IEnumerator updateNodeStructure(string nodename, string new_structure, Callback<string> callback)
    {
        JSONObject jsonObject = new JSONObject();
        
        jsonObject["structure"] = new_structure;
        jsonObject["name"] = nodename;
        return POST(string.Format("{0}/geodata/updatebynamete/", baseURL), jsonObject.ToString(), (jsonResponse) => {
            callback(jsonResponse.ToString());
        });
    }

    /*
     * Update the structure of the node
     */
    public static IEnumerator deleteEnemy(string enemyname, Callback<string> callback)
    {
        
        return DELETE(string.Format("{0}/enemy/{1}", baseURL, enemyname), (jsonResponse) => {
            callback(jsonResponse.ToString());
        });
        
    }

    /*
     * Retrieves an enemy with the specified nodename. Returns a 404 error if there is no
     * enemy with the given nodename.
     */
    public static IEnumerator getEnemy(string nodename, Callback<JSONNode> callback)
    {
        return GET(string.Format("{0}/enemy/{1}", baseURL, nodename), callback);
    }

    public static IEnumerator geocodeAddress(string address, Callback<JSONNode> callback)
    {
        return GET(string.Format("{0}/geocoding/v5/mapbox.places/{1}.json?access_token={2}&limit=1", mapboxBaseURL, address, mapboxAPIKey), callback);
    }
}
