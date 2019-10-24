using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using Unity.Collections;

/* NodeFactory Description: 
 * The NodeFactory is a singleton that is responsible the creation of Node GameObjects to be used on the overworld.
 * It retrieves Nodes' locations, as well as the corresponding NodeStructure currently associated with that Node from the web server and assembles corresponding GameObject.
 * Is referenced by the NodeUpdateService to update the state of Nodes on the overworld
 */

public class NodeFactory : Singleton<NodeFactory>
{
    //fields
    [ReadOnly] private List<INodeStructure> _nodeStructures;

    //constructor

    //methods

}
