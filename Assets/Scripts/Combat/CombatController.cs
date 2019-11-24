using Colyseus.Schema;
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
    private bool _canPlayCards; // state variable used to limit user input (can be done better)

    [SerializeField] private GameObject _playerPF;
    [SerializeField] private GameObject _playerGO;
    [SerializeField] private Player player;
    [SerializeField] private Vector3 playerSpawnPos;

    [SerializeField] private GameObject _enemyPF;
    [SerializeField] private GameObject _enemyGO;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Vector3 enemySpawnPos;

    private Transform _handZone;
    private State state;
    private ColyseusClient colyseusClient;

    public bool CanPlayCards {
        get => _canPlayCards;
        set => _canPlayCards = value;
    }

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

    public MapSchema<ColyseusPlayer> ColyseusPlayers
    {
        get => state.players;
    }

    private Text _playerList;

    // methods
    private void Awake()
    {
        _canPlayCards = false;

        // Initialize static classes
        CardFactory.InitializeFactory();

        // UI references and initializations
        _handZone = GameObject.Find("Combat UI").transform.Find("HandZone").transform;

        // Instantitate player and enemy
        _playerPF = Resources.Load<GameObject>("Prefabs/PlayerCombat");
        player = _playerPF.GetComponent<Player>();
        _playerGO = Instantiate(_playerPF, playerSpawnPos, Quaternion.identity);

        // TODO: Query enemy type from web API
        _enemyPF = Resources.Load<GameObject>("Prefabs/Enemies/Enemy");
        enemy = _enemyPF.GetComponent<Enemy>();
        _enemyGO = Instantiate(_enemyPF, enemySpawnPos, Quaternion.identity);

        _playerList = GameObject.Find("Combat UI").transform.Find("PlayerList").gameObject.GetComponent<Text>();

        InitializeCombat();
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
        string newString = "";
        foreach (var player in players.Keys) {
            newString = newString + player.ToString() + '\n';
        }
        _playerList.text = newString;

        // Check player and enemy condition. 
        //Separating player and enemy because we might need to perform different requests to server.
        if(!player.IsAlive && player.InCombat && enemy.InCombat)
        {
            //update enemy health
            ExitCombat();
        } else if (!enemy.IsAlive && player.InCombat && enemy.InCombat){
            //turn node to friendly
            //delete enemy in db
            ExitCombat();
        }
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
        _canPlayCards = true;

        _canPlayCards = false;
    }

    private void EndPhase()
    {

    }

    private void InitializeCombat()
    {
        // Connect to combat instance        
        colyseusClient = new ColyseusClient();
        colyseusClient.JoinOrCreateRoom("Bob", "Node_1", OnStateChangeHandler);
            // Read combat state

        // Spawn monster and player prefabs with state data

            // Initialize clientside ui/system handlers

        // Start TurnSystem
        //return null;  
    }

    private void TerminateCombat()
    {
        if(colyseusClient != null)
        {
            colyseusClient.LeaveRoom();
            // Sync player state
            // Return to overworld
        }
    }

    private void ExitCombat()
    {
            // Loads back to map scene after death
            SceneManager.LoadScene(0);
            enemy.endCombat();
            player.endCombat();
            colyseusClient.LeaveRoom();
    }

    private void SpawnCharacters()
    {
        
        
    }

    /*
     * An example of a state callback function. It will most likely be more useful to pass in a 
     * custom stateHandler to JoinOrCreateRoom().
     */
    public void OnStateChangeHandler(State state, bool isFirstState)
    {
        this.state = state;
        Debug.Log("State has been updated!");
        updateCurrentPlayersTextField();
        Debug.LogFormat("MonsterHealth: {0}", state.monsterHealth);
    }

    private void updateCurrentPlayersTextField()
    {
        MapSchema<ColyseusPlayer> players = state.players;
        string newString = "";
        foreach (var key in players.Keys) {
            newString += ((ColyseusPlayer)players[key]).name + '\n';
        }
        _playerList.text = newString;
    }

}
