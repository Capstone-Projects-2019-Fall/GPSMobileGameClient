using System;

/* DrawEventArgs Description:
 * Simple class that extends EventArgs and contains data relevant to the CardDrawn event in CombatController
 */
public class DrawEventArgs : EventArgs
{
    public int NumCards {
        get; set;
    }
}
