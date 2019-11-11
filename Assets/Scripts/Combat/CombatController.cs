using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CombatContoller Description:
 * The CombatController is the main context for all of our clientside combat API. It is primarily responsible for:
 *    -> Constructing combat; i.e. loading prefabs, initializing static classes and unity singletons, etc.
 *    -> Managing the combat sequence; including making calls to the APIWrapper and UIController
 *    -> Cleaning up combat and transitioning back to the overworld
 */
public class CombatController : Singleton<CombatController>
{
    // fields
    private bool activeTurn;

    [SerializeField] private GameObject _playerPF;
    [SerializeField] private GameObject _playerGO;
    [SerializeField] private Player player;
    [SerializeField] private Vector3 playerSpawnPos;

    [SerializeField] private GameObject _enemyPF;
    [SerializeField] private GameObject _enemyGO;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Vector3 enemySpawnPos;

    // methods
    private void Awake()
    {
        _playerPF = Resources.Load<GameObject>("Prefabs/PlayerCombat");
        player = _playerPF.GetComponent<Player>();

        // TODO: Query enemy type from web API
        _enemyPF = Resources.Load<GameObject>("Prefabs/Enemies/Enemy");
        enemy = _enemyPF.GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TurnSystem");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator TurnSystem()
    {

    }

    private void SpawnCharacters()
    {
        _playerGO = Instantiate(_playerPF, playerSpawnPos, Quaternion.identity);
        _enemyGO = Instantiate(_enemyPF, enemySpawnPos, Quaternion.identity);
    }
}
