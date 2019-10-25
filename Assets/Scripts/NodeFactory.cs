using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Unity.Collections;
using System.Reflection;

/* NodeFactory Description: 
 * The NodeFactory is a singleton that is responsible the creation of Node GameObjects to be used on the overworld.
 * It retrieves Nodes' locations, as well as the corresponding NodeStructure currently associated with that Node from the web server and assembles corresponding GameObject.
 * Is referenced by the NodeUpdateService to update the state of Nodes on the overworld
 */

public static class NodeFactory
{
    //fields
    private static List<INodeStructure> NodeStructures;
    private static bool IsInitialized => NodeStructures != null;

    //methods
    /* Initialize the NodeFactory
     */
    private static void InitializeFactory()
    {
        if (IsInitialized)
            return;

        NodeStructures = Assembly.GetAssembly(typeof())
    }

    /* Main creational methods for Nodes within the NodeFactory
     * Parameters:
     *    -> string locString: longitudinal and latitudinal coordinates of Node
     *    -> INodeStructure nodeStructure: Node structure interface ('concrete'/implementer is passed), i.e. Strategy pattern
     */
    public Node CreateNode(string locString, INodeStructure nodeStructure) {



        return node;
    }
}
