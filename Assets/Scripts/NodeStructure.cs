using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NodeStructure Description: 
 * NodeStructures define a behavioral interface for implementers. The Node class will maintain a reference to a NodeStructure. This is effectively a Strategy design pattern.
 * 
 * A list of implementing NodeStructures:
 *      - Friendly
 *      - Enemy
 */

public abstract class NodeStructure
{
    // fields (required by each NodeStructure)
    public abstract string Type { get; set; }

    // methods (to be implemented by specific NodeStructures)
}
