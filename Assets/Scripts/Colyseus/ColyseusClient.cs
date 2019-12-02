using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

using Colyseus;
using Colyseus.Schema;

using GameDevWare.Serialization;

public class ColyseusClient : MonoBehaviour
{
    private static readonly string roomName = "battle";
    private static readonly string endpoint = "ws://gps-mobile-game-battle-server.herokuapp.com";
    // private static readonly string endpoint = "ws://localhost:3000";

    public Colyseus.Client client;
    public Room<State> room;

    public ColyseusClient()
    {
        ConnectToServer();
    }

    /*
     * Sets the endpoint to the Colyseus server.
     */
    public async void ConnectToServer()
    {
        Debug.Log("Connecting to " + endpoint);
        client = ColyseusManager.Instance.CreateClient(endpoint);
    }

    /*
     * Joins an already created room. If the room does not exist it is created and then the client joins the newly created room.
     * Username is the clients name, battleName is the name of the room to join, and stateHandler is a callback function that gets
     * invoked on a state change.
     */
    public async void JoinOrCreateRoom(string username, float playerHealth, string battleName, Colyseus.Room<State>.RoomOnStateChangeEventHandler stateHandler, Colyseus.Room<State>.RoomOnMessageEventHandler messageHandler)
    {
        // Joins/sets up the room.
        Debug.LogFormat("{0} is trying to join room: {1}", username, battleName);
        room = await client.JoinOrCreate<State>(roomName, new Dictionary<string, object>() { { "name", username }, { "playerHealth", playerHealth }, { "battleName", battleName } });
        Debug.LogFormat("Room Id: {0}\tSession Id: {1}", room.Id, room.SessionId);

        // Sets event callback functions.
        room.OnStateChange += stateHandler; // look at OnStateChangeHandler below as an example.
        room.OnMessage += messageHandler;
        room.OnLeave += (code) => Debug.LogFormat("Left Room with Code: {0}", code);
        room.OnError += (message) => Debug.LogErrorFormat("Colyseus Error: {0}", message);        
    }
     /*
      * Leaves the current room.
      */
    public async void LeaveRoom()
    {
        if(room != null)
        {
            await room.Leave(false);
        }
        else
        {
            Debug.Log("Cannot leave room, currently not connected to one!");
        }        
    }

    /*
     * Sends the damage dealt to the enemy to the Colyseus server.
     */
    public async Task SendMessage(string delta)
    {
        if (room != null)
        {
            Debug.LogFormat("Sending Delta: {0}", delta);
            await room.Send(delta);
        }
        else
        {
            Debug.Log("Cannot send delta, room is not connected!");
        }
    }

    /*
     * An example of a state callback function. It will most likely be more useful to pass in a 
     * custom stateHandler to JoinOrCreateRoom().
     */
    public void OnStateChangeHandler(State state, bool isFirstState)
    {
        Debug.Log("State has been updated!");
    }
}
