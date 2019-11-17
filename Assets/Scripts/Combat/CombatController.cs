using Colyseus.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* CombatContoller Description:
 * The CombatController is the main context for all of our clientside combat API. It is primarily responsible for:
 *    -> Constructing combat; i.e. loading prefabs, initializing static classes and unity singletons, etc.
 *    -> Managing the combat sequence; including making calls to the APIWrapper and UIController
 *    -> Cleaning up combat and transitioning back to the overworld
 */
public class CombatController : Singleton<CombatController>
{
    // fields
    [SerializeField] private GameObject _playerPF;
    [SerializeField] private GameObject _playerGO;
    [SerializeField] private Player player;
    [SerializeField] private Vector3 playerSpawnPos;

    [SerializeField] private GameObject _enemyPF;
    [SerializeField] private GameObject _enemyGO;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Vector3 enemySpawnPos;

    private Transform _handZone;
    private MapSchema<Entity> players;
    private TurnTimer _timer;

    public GameObject PlayerGO {
        get => _playerGO;
    }
    public GameObject EnemyGO {
        get => _enemyGO;
    }
    public Player Player
    {
        get => player;
        set => player = value;
    }
    public Enemy Enemy
    {
        get => enemy;
        set => enemy = value;
    }

    public cState clientState;
    public enum cState
    {
        CanTakeActions, Busy
    }

    // methods
    private void Awake()
    {
        clientState = cState.Busy;

        // Initialize static classes
        CardFactory.InitializeFactory();

        // UI references and initializations
        _handZone = GameObject.Find("Combat UI").transform.Find("HandZone").transform;
        _timer = gameObject.GetComponent<TurnTimer>(); // get reference to timer
        _timer.TimeExpired += OnTimeExpired; // subscribe to its TimeExpired event

        // Instantitate player and enemy
        _playerPF = Resources.Load<GameObject>("Prefabs/PlayerCombat");
        player = _playerPF.GetComponent<Player>();
        _playerGO = Instantiate(_playerPF, playerSpawnPos, Quaternion.identity);

        // TODO: Query enemy type from web API
        _enemyPF = Resources.Load<GameObject>("Prefabs/Enemies/Enemy");
        enemy = _enemyPF.GetComponent<Enemy>();
        _enemyGO = Instantiate(_enemyPF, enemySpawnPos, Quaternion.identity);

    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("TurnSystem");
        Card cardEX = CardFactory.CreateCard(0);
        GameObject cardEXgo = CardFactory.CreateCardGameObject(cardEX);

        cardEXgo.transform.SetParent(_handZone);
        cardEXgo.transform.localPosition = new Vector3(0, 0, 0);
        cardEXgo.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /*IEnumerator TurnSystem()
    {
        // Start phase

        // Action phase

        // End phase

        // Send Delta

        // Enemy phase
    }*/

    private void StartPhase()
    {

    }

    private void ActionPhase()
    {
        clientState = cState.CanTakeActions;
    }

    private void EndPhase()
    {

    }

    private void InitializeCombat()
    {
        // Connect to combat instance
        
            // Read combat state

        // Spawn monster and player prefabs with state data

            // Initialize clientside ui/system handlers

        // Start TurnSystem
        //return null;  
    }

    private void SpawnCharacters()
    {
        
        
    }

    public void OnTimeExpired(object sender, EventArgs e)
    {

    }

    /*
     * An example of a state callback function. It will most likely be more useful to pass in a 
     * custom stateHandler to JoinOrCreateRoom().
     */
    public void OnStateChangeHandler(State state, bool isFirstState)
    {
        players = state.players;
        Debug.Log("State has been updated!");
        Debug.LogFormat("MonsterHealth: {0}", state.monsterHealth);
    }

}
