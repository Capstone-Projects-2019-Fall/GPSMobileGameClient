using System;

/* DrawEventArgs Description:
 * Simple class that extends EventArgs and contains data relevant to the CardsDrawn event in CombatController
 */
public class DrawEventArgs : EventArgs
{
    public int NumCards {
        get; set;
    }
}
