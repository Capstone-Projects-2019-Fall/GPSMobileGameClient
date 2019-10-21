using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NodeFactory Description: 
 * The NodeFactory is a singleton that is responsible for populating the clientside overworld with Nodes.
 * It retrieves Nodes' locations, as well as the corresponding NodeStructure currently associated with that Node from the web server and populates them on the map.
 * Maintains a local reference to a list of all concrete NodeStructures.
 * Is referenced by the NodeUpdateService to update the state of Nodes on the overworld
 */

public class NodeFactory : Singleton<NodeFactory>
{
    //fields

    //constructor

    //methods

}
