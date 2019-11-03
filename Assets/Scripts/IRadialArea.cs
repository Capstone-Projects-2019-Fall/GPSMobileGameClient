using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* IRadialArea Description:
 * A class should implement IRadialArea if it is an overworld game object that should have some game behavior that is
 * dependent upon the player being within a certain radius around it.
 * The primary implementers of IRadialArea are NodeStructures.
 */

public interface IRadialArea
{
    float Radius { get; set; }
    RadialArea MyRadialArea { get; }

    // The behavior that occurs when a player moves into a RadialArea
    void EnterAction();

    // The behavior that occurs when a player exits a RadialArea
    void ExitAction();

    // The behavior that occurs while the player object is within the area.
    void UpdateAction();
}
