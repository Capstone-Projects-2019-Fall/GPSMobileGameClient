using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;

/* RadialArea Description:
 * RadialAreas are attached to certain types of NodeStructures. They define a circular area around thier corresponding Node.
 * They utilize Events to communicate with the player whenever the player enters or exits their defined radius.
 * Additionally, they are able to passively affect the client while they are within the area (continuously spawning enemies or items).
 */

[RequireComponent(typeof(LineRenderer))]
public class RadialArea : MonoBehaviour
{
    // Delegates and events for this Radial Area's OnEnter and OnExit
    public event EventHandler EnteredArea;
    public event EventHandler ExitedArea;

    private AbstractMap _map;
    private GameObject _player;
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
        set => _inRange = value;
    }

    // methods
    private void Awake()
    {
        _map = (AbstractMap) FindObjectOfType(typeof(AbstractMap));
        _player = GameObject.Find("PlayerTarget");

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;

        _inRange = PollRange(_player); // Test whether the Player is within the range of the radius
        if (_inRange)
            OnEnteredArea(EventArgs.Empty);
   
    }

    // The RangeHandler is called every 2.0 seconds, instead of during every update frame.
    public void Start()
    {
        // TODO: Create materials for Friendly and Enemy RadialAreas
    }

    public void Update()
    {
        RangeHandler();
    }

    /* Method is run in regular intervals to determine if the player has entered or exited the radial area.
     * Is able to update the state variable _inRange and interact with the event system.
     */
    public void RangeHandler()
    {
        if(!_inRange && PollRange(_player)) // _inRange is false, PollRange returns true: player has entererd the area
        {   
            OnEnteredArea(EventArgs.Empty); // Signal to subscribers
        } else if(_inRange && !PollRange(_player)) // _inRange is true, PollRange returns false: player has exited the area
        {
            OnExitedArea(EventArgs.Empty); // Signal to subscribers
        }
    }

    // Signal to subscribers that the player has entered the radial area
    public void OnEnteredArea(EventArgs e)
    {
        EnteredArea?.Invoke(this, e);
        _inRange = true;
    }

    // Signal to subscribers that the player has exited the radial area
    public void OnExitedArea(EventArgs e)
    {
        ExitedArea?.Invoke(this, e);
        _inRange = false;
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

        if (distToTarget < Radius * 2)
        {
            return true;
        } else
        {
            return false;
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
        float xLoc = UnityEngine.Random.Range(-Radius, Radius);
        float zLoc = UnityEngine.Random.Range(-Radius, Radius);

        GameObject instance = Instantiate(obj);
        instance.transform.localPosition = new Vector3 (xLoc, 0.0f, zLoc);

        return instance;
    }

    /* Draw a circle indicating the area of effect of the radial area using a LineRenderer
     * Parameters:
     *    -> Material lineMaterial: Control the visual effects of the circle with this material
     */
    public void DrawAreaOfEffect()
    {
        _lineRenderer.widthMultiplier = 1.2f;

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

        transform.localPosition = new Vector3(0.0f, -2.0f, 0.0f);
    }
}
