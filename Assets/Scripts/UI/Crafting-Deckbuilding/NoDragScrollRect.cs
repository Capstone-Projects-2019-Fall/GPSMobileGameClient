using UnityEngine.EventSystems;
using UnityEngine.UI;


/* NoDragScrollRect Description:
 * Simply utility class to overrdie the default functionality of a Unity ScrollRect. The Drag handlers normally associated with a ScrollRect
 * will be overridden, thus forcing the interface to control the scrolling of the ScrollRect's content via a decdicated scrollbar.
 */
public class NoDragScrollRect : ScrollRect  
{
    public override void OnBeginDrag(PointerEventData eventData) { }

    public override void OnDrag(PointerEventData eventData) { }

    public override void OnEndDrag(PointerEventData eventData) { }
}
