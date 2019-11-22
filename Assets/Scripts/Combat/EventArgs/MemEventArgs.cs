using System;

/* MemEventArgs Description:
 * Simple class that extends EventArgs to wrap data related to the MemoryChanged event in CombatController
 */
public class MemEventArgs : EventArgs
{
    public int MemDiff {
        get; set;
    }
}
