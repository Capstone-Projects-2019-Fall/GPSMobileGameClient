using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.GameEngine
{

    /// <summary>
    /// The container that holds UI elements and element behavior for the overworld screen
    /// </summary>
    /// <description>
    /// The overworld screen consists of the street map surrounding the player, the player avatar themselves, as well as map markers representing every nearby Node that the player can interact with.
    /// The OverworldCanvas is responsible for making sure all of these UI elements render properly to the screen, adding them and removing them when necessary.
    /// </description>
    public class OverworldCanvas : MonoBehaviour
    {
        /// <summary>
        /// List of Nodes near enough to render
        /// </summary>
        public List<Node> nearbyNodes;

        /// <summary>
        /// The Unity GameObject the map will be rendered onto
        /// </summary>
        public GameObject mapObject;

        /// <summary>
        /// A raw texture of the map tiles to be rendered
        /// </summary>
        public Texture mapUV;
        

        /// <summary>
        /// Apply updates to the screen
        /// </summary>
        /// <description>
        /// The majority of the heavy lifting done in the Update() method for the OverworldCanvas is updating the position of each Node that surrounds the player. They must be managed dynamically;
        /// they will have to enter and exit the edges of the screen as the player moves around the overworld.
        /// </description>
        public void Update() { }

        /// <summary>
        /// Add a node to nearbyNodes
        /// </summary>
        /// <param name="node">The Node to be added (including its location)</param>
        public void addNode(Node node) { }

        /// <summary>
        /// Remove an existing node from nearbyNodes
        /// </summary>
        /// <param name="node">The Node to be removed</param>
        public void removeNode(Node node) { }

        /// <summary>
        /// Apply a graphical alteration to the overworld map
        /// </summary>
        /// <param name="alteration"></param>
        public void alterMap(IEnumerator alteration) { }
    }
}