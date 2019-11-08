using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

/* NodeFactory Description: 
 * The NodeFactory is a singleton that is responsible the creation of Node GameObjects to be used on the overworld.
 * It retrieves Nodes' locations, as well as the corresponding NodeStructure currently associated with that Node from the web server and assembles corresponding GameObject.
 * Is referenced by the NodeUpdateService to update the state of Nodes on the overworld
 */
public static class NodeFactory
{
    private static Dictionary<string, NodeStructure> NodeStructuresByName;
    private static Dictionary<string, Type> NodeStructTypesByName;
    private static bool _initialized => NodeStructuresByName != null;

    // Initialize the NodeFactory
    public static void InitializeFactory()
    {
        // Ensures only one NodeFactory can exist
        if (_initialized)
            return;

        // Get all concrete subclasses of NodeStructure 
        var nodeStructures = Assembly.GetAssembly(typeof(NodeStructure)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(NodeStructure)));

        NodeStructuresByName = new Dictionary<string, NodeStructure>();
        NodeStructTypesByName = new Dictionary<string, Type>();

        // Add each NodeStructure to the dictionary
        foreach (var type in nodeStructures)
        {
            var _tstruct = Activator.CreateInstance(type) as NodeStructure;
            NodeStructuresByName.Add(_tstruct.Type, _tstruct);
        }

        foreach (var type in nodeStructures)
        {
            var _tstruct = Activator.CreateInstance(type) as NodeStructure;
            NodeStructTypesByName.Add(_tstruct.Type, type);
        }
    }

    /* Main creational methods for Nodes within the NodeFactory
     * Parameters:
     *    -> string locString: longitudinal and latitudinal coordinates of Node
     *    -> string nodeStructureType: string representation of the NodeStructure to be returned
     * Returns:
     *    -> A GameObject: the Node prefab to be placed in the Unity scene via Mapbox
     */
    public static GameObject CreateNode(string locString, string nodeStructureType)
    {
        // If the given NodeStructure exists then attach it to a new Node and return it
        if (NodeStructuresByName.ContainsKey(nodeStructureType))
        {
            NodeStructure nodeStruct = NodeStructuresByName[nodeStructureType];

            // Create a new Node prefab and initialize its fields
            GameObject nodePrefab = Resources.Load<GameObject>("Prefabs/Node");
            GameObject nodeInstance = MonoBehaviour.Instantiate(nodePrefab);
            Node nodeCode = nodeInstance.GetComponent<Node>();

            nodeCode.NodeStruct = nodeStruct;
            nodeCode.LocationString = locString;

            return nodeInstance;
        }

        throw new ArgumentException("Non-existent NodeStruct passed to CreateNode.");
    }

    /* 1st Overload ---
     * New Parameters:
     *    -> NodeStructure nodeStruct: Create a Node by passing a reference to a NodeStructure
     */
    public static GameObject CreateNode(string locString, NodeStructure nodeStruct)
    {
        if (NodeStructuresByName.ContainsValue(nodeStruct))
        {
            GameObject nodePrefab = Resources.Load<GameObject>("Prefabs/Node");
            GameObject nodeInstance = MonoBehaviour.Instantiate(nodePrefab);
            Node nodeCode = nodeInstance.GetComponent<Node>();

            nodeCode.NodeStruct = nodeStruct;
            nodeCode.LocationString = locString;

            return nodeInstance;
        }

        throw new ArgumentException("Non-existent NodeStruct passed to CreateNode.");
    }

    /* 2nd Overload ---
     * Takes only a location as an argument. Returns a Node with a random NodeStructure.
     */
    public static GameObject CreateNode(string locString)
    {
        GameObject nodePrefab = Resources.Load<GameObject>("Prefabs/Node");
        GameObject nodeInstance = MonoBehaviour.Instantiate(nodePrefab);
        Node nodeCode = nodeInstance.GetComponent<Node>();
 
        // Select a random NodeStructure from the dictionary
        System.Random rand = new System.Random();
        IList<Type> values = new List<Type>(NodeStructTypesByName.Values);
        int size = values.Count;
        int roll = rand.Next(size);

        NodeStructure _nodeStruct = Activator.CreateInstance(values[roll]) as NodeStructure;

        nodeCode.NodeStruct = _nodeStruct;
        nodeCode.LocationString = locString;
        
        return nodeInstance;
    }


    /* Returns an instance of a NodeStructure by name from within the private dictionary
     * Parameters:
     *    -> nodeStructureType: string representation of the NodeStructure to be returned
     * Returns:
     *    -> A NodeStructure: An object that sits within a Node on the map
     */
    public static NodeStructure GetNodeStructureByString(string nodeStructureType)
    {
        // If the given NodeStructure is in the dictionary, then return it.
        if (NodeStructuresByName.ContainsKey(nodeStructureType))
        {
            return NodeStructuresByName[nodeStructureType];
        }

        throw new ArgumentException("Invalid NodeStructure.");
    }
}
