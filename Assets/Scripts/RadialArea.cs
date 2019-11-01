using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;

/* RadialArea Description:
 * RadialAreas are attached to certain types of NodeStructures. They define a circular area around thier corresponding Node.
 * They utilize Events to communicate with the player whenever the player enters or exits their defined radius.
 * Additionally, they are able to passively affect the client while they are within the area (continuously spawning enemies or items).
 * 
 * 
 */

public class RadialArea : MonoBehaviour
{
    [SerializeField] private AbstractMap _map;
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        _map = (AbstractMap) FindObjectOfType(typeof(AbstractMap));
        _player = gameObject;
    }

    private void Update()
    {

    }
}
