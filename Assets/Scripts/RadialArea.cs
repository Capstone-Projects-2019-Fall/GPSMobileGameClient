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
    // fields

    [SerializeField] private AbstractMap _map;
    private float _radius;

    private int _vertexCount = 40; // used to control smoothness of drawn circle, default 40
    private float _lineWidth = 1.0f;
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

    // methods
    private void Awake()
    {
        _map = (AbstractMap) FindObjectOfType(typeof(AbstractMap));
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {

    }

    /* Returns true if another GameObject is within range of the RadialArea
     * Parameters:
     *    -> GameObject other: Test if this game object is within range
     * Returns:
     *    -> bool: True if the other object is within range
     */ 
    public bool WithinRange(GameObject other)
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
