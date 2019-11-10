using UnityEngine;

/* TouchInput Description:
 * A simple utility script that can be attached to a GameObject to properly handle touch device input. Effectively this function will wrap the usual OnMouseDown calls in script in a touch-friendly way
 * To use, attach this script to the cmera being used to render clickable objects (generally the main camera).
 */
public class TouchInput : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals (TouchPhase.Began))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if (hit)
                {
                    hit.transform.gameObject.SendMessage("OnMouseDown");
                }
            }
        }
    }
}
