using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Node Description: 
 * Nodes are the in-game objects that are assigned directly to Points of Interest (POIs) from the Mapbox API.
 * Nodes are containers for NodeStructures. NodeStructures are 'slotted into' Nodes on the server side (i.e. the server decides what type of NodeStructure goes in which Node)
 * 
 * The most important pieces of functionality for the Node class are:
 *      - Handle interfacing with the Player
 *      - Forward salient Client requests to the web server
 *      - Maintain a reference to its corresponding NodeStructure 
 */

public class Node : MonoBehaviour
{
    //fields

    // References an abstract class, essentially a strategy pattern
    private NodeStructure _nodeStruct = null;
    private string _locationString = null;

    // methods

    // Setter and getter for nodeStruct
    public NodeStructure NodeStruct {
        get => _nodeStruct;
        set => _nodeStruct = value;
    }

    // Setter and getter locationString
    public string LocationString {
        get => _locationString;
        set => _locationString = value;
    }

    private void Start()
    {

    }

}
