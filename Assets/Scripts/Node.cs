using System;
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
    private NodeStructure _nodeStruct = null; // References an abstract class, essentially a strategy pattern
    private Sprite _nodeSprite = null;
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    private string _locationString = null;
    // methods

    // Setter and getter for nodeStruct
    // Node's visuals and behaviors are updated immediately when the NodeStructure are updated
    public NodeStructure NodeStruct {
        get => _nodeStruct;
        set 
        {
            try {
                _nodeStruct = value;
                _nodeSprite = _nodeStruct.Sprite;
                _spriteRenderer.sprite = _nodeSprite;
            } catch (Exception e) { Debug.Log(e); }
        }
    }

    // Setter and getter locationString
    public string LocationString {
        get => _locationString;
        set => _locationString = value;
    }

    // Setter and getter for mySprite (obtained from NodeStructure)
    public Sprite NodeSprite 
    {
        get => _nodeSprite;
        set => _nodeSprite = value;
    }

    /* 
     * 
     */
    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    private void Update()
    {
       
    }

}
