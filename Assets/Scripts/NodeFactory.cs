using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Unity.Collections;
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
    //fields
    private static Dictionary<string, Type> NodeStructuresByName;
    private static bool IsInitialized => NodeStructuresByName != null;

    //methods
    /* Initialize the NodeFactory
     */
    private static void InitializeFactory()
    {
        // Ensures only one instance of the NodeFactory can exist
        if (IsInitialized)
            return;

        // Get all concrete subclasses of NodeStructure 
        var nodeStructures = Assembly.GetAssembly(typeof(NodeStructure)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(NodeStructure)));

        NodeStructuresByName = new Dictionary<string, Type>();

        foreach (var type in nodeStructures)
        {
            var _tstruct = Activator.CreateInstance(type) as NodeStructure;
            NodeStructuresByName.Add(_tstruct.Type, type);
        }
    }

    /* Main creational methods for Nodes within the NodeFactory
     * Parameters:
     *    -> string locString: longitudinal and latitudinal coordinates of Node
     *    -> string nodeStructureType: string representation of the NodeStructure to be returned
     */
    public static Node CreateNode(string locString, string nodeStructureType)
    {
        // If the given NodeStructure exists then attach it to a new Node and return it
        if (NodeStructuresByName.ContainsKey(nodeStructureType))
        {
            Type type = NodeStructuresByName[nodeStructureType];
            var nodeStruct = Activator.CreateInstance(type) as NodeStructure;

            GameObject node = Resources.Load<GameObject>("Prefabs/Node");
        }

        throw new ArgumentException("NodeFactory.CreateNode: Invalid NodeStructure.");
    }
}
