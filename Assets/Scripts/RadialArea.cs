using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;

/* RadialArea Description:
 * RadialAreas are attached to certain types of NodeStructures. They define a circular area around thier corresponding Node.
 * They utilize Events to communicate with the player whenever the player enters or exits their defined radius.
 * Additionally, they are able to passively affect the client while they are within the area (continuously spawning enemies or items).
 */

[RequireComponent(typeof(LineRenderer))]
public class RadialArea : MonoBehaviour
{
    // Delegates and events for this Radial Area's OnEnter and OnExit
    public delegate void EnterAction();
    public event EnterAction OnEnterArea;
    public delegate void ExitAction();
    public event ExitAction OnExitArea;

    [SerializeField] private AbstractMap _map;
    [SerializeField] private GameObject _player;
    private float _radius;

    private bool _inRange; // state variable representing if player is within range of RadialArea

    private int _vertexCount = 40; // used to control smoothness of drawn circle, default 40
    private float _lineWidth = 1.0f; // used to control circle line width, default 1.0
    private LineRenderer _lineRenderer;

    // getters and setters
    public float Radius 
    {
        get => _radius;
        set => _radius = value;
    }
    public int VertexCount {
        get => _vertexCount;
        set => _vertexCount = value;
    }
    public float LineWidth 
    {
        get => _lineWidth;
        set => _lineWidth = value;
    }
    public bool InRange {
        get => _inRange;
    }

    // methods
    private void Awake()
    {
        _map = (AbstractMap) FindObjectOfType(typeof(AbstractMap));
        _player = GameObject.Find("PlayerTarget");
        _lineRenderer = GetComponent<LineRenderer>();

        _inRange = PollRange(_player); // Test whether the Player is within the range of the radius
    }

    // The RangeHandler is called every 2.0 seconds, instead of during every update frame.
    public void Start()
    {
        InvokeRepeating("RangeHandler", 0, 2.0f);
    }

    /* Method is run in regular intervals to determine if the player has entered or exited the radial area.
     * Is able to update the state variable _inRange and interact with the event system.
     */
    public void RangeHandler()
    {
        if(!_inRange && PollRange(_player)) // _inRange is false, PollRange returns true: player has entererd the area
        {
            OnEnterArea(); // Signal to subscribers
            _inRange = true;
        } else if(_inRange && PollRange(_player)) // _inRange is true, PollRange returns false: player has exited the area
        {
            OnExitArea(); // Signal to subscribers
            _inRange = false;
        }
    }

    /* Returns true if another GameObject is within range of the RadialArea
     * Parameters:
     *    -> GameObject other: Test if this game object is within range
     * Returns:
     *    -> bool: True if the other object is within range
     */ 
    public bool PollRange(GameObject other)
    {
        // Nullify the y component of true vector distance
        Vector3 vecToTarget = gameObject.transform.position - other.transform.position;
        vecToTarget.y = 0;

        // Check if object is within range
        float distToTarget = vecToTarget.magnitude;
        return (distToTarget < Radius);
    }
    
    /* Draw a circle indicating the area of effect of the radial area using a LineRenderer
     * Parameters:
     *    -> Material lineMaterial: Control the visual effects of the circle with this material
     */
    public void DrawAreaOfEffect(Material lineMaterial)
    {
        _lineRenderer.widthMultiplier = LineWidth;
        float deltaTheta = (2f * Mathf.PI) / VertexCount;
        float theta = 0f;

        _lineRenderer.positionCount = VertexCount;

        // Each segment of the LineRenderer
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(Radius * Mathf.Cos(theta), 0f, Radius * Mathf.Sin(theta));
            _lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
    
    /* Instantiate a GameObject at a random location within the RadialArea's range. Note that the caller
     * is responsible for validating the GameObject/Prefab is loaded properly.
     * 
     * Parameters:
     *    -> GameObject obj: Object to instantiate within the are of effect
     * Returns:
     *    -> A reference to the spawned GameObject
     */
    public GameObject SpawnObjectInRange(GameObject obj)
    {
        obj.transform.SetParent(gameObject.transform); // Set the parent of obj to the RadialArea's gameObject

        // Select random locations within the radius
        float xLoc = Random.Range(-Radius, Radius);
        float zLoc = Random.Range(-Radius, Radius);

        GameObject instance = Instantiate(obj);
        instance.transform.localPosition = new Vector3 (xLoc, 0.0f, zLoc);

        return instance;
    }
}
