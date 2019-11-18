using Colyseus.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 playerSpawnPos;
    private DeckManager _deckManager;

    [SerializeField] private GameObject _enemyPF;
    [SerializeField] private GameObject _enemyGO;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vector3 enemySpawnPos;
    [SerializeField] private ColyseusClient client;

    [SerializeField] private Text _playerList;
    private Transform _handZone;
    private MapSchema<Entity> players;
    private TurnTimer _timer;

    #region Combat Event System --------------------------------------------------------------------------------
    /* Brief Guide to the Combat Event System:
     * The goal behind the Combat Event System is to streamline and centralize all of the high level gameplay actions in one place,
     * so that smaller, utility scripts and GameObjects can all be notified when useful game events are triggered. In this context,
     * we would refer to CombatController.cs as the 'publisher' and any script who responds to the event triggers thrown here as the
     * 'subscribers'.   
     * 
     * TO ADD AN EVENT:
     * The publisher is responsible for creating 2 or 3 different actors in the event system:
     *    -> The event delegate itself: 
     *       EXAMPLE: public event EventHandler<OptionalEventData> EventTriggered;
     *       DESCRIPTION: This event is what the subscribers will reference in order to actually subscribe their own handling methods
     *                    to the event you are creating. The generic used in the example above, <OptionalEventData> would be a custom
     *                    class created by you if you want to send any data from the publisher to each of the event subscribers. Other-
     *                    -wise, you can simply use EventHandler (with no generic) to create an event that sends no event data (use
     *                    EventArgs.Empty() when necessary). Assume we have a custom class, CustomEventArgs, for the examples below.
     *                    
     *    -> The event signaller:
     *       EXAMPLE: public void OnEventTriggered(CustomEventArgs e) {
     *                    EventTriggered?.Invoke(this, e); }
     *       DESCRIPTION: This will be called by the publisher to actually signal to the subscribers that the event has fired off. The
     *                    EventArgs, e, will be sent to each of the subscribers, but exactly what is inside the EventArgs will be
     *                    determined by some updater method elsewhere in the publisher (read below for details).
     *                    
     *    -> The event updater/state checker:
     *       EXAMPLE: public void CheckIfEventTriggered(int number) {
     *                    _num += number;                                          // Update some state variable in the publisher
     *                    if(_num >= threshold) {                                  // If the event condition is satisfied
     *                          CustomEventArgs args = new CustomEventArgs();      // Create a new custom event args object
     *                          args.Payload = number;                             // Update fields of CustomEventArgs
     *                          OnEventTriggered(args); }                          // Signal to all subscribers
     *       DESCRIPTION: This method will check to see if the state of the publisher has changed such that the event need be fired off.
     *                    Checking the state of the publisher is only one possibility for events however; you could simply have some
     *                    public method which other scripts/classes can call to fire off an event (no if() statement required).
     *                          
     *                                
     * TO SUBSCRIBE / UNSUBSCRIBE TO AN EVENT:
     * The subscriber has a little bit less responsibility when subscribing to the event. It only needs 2 actos:
     *    -> The event handler:
     *       EXAMPLE: public void OnEventTriggeredAction(object sender, CustomEventArgs e) {
     *                    Debug.LogFormat("Received {0} from publisher", e.Payload); }
     *       DESCRIPTION: The event handler is where the actually 'useful' game logic will be executed. An indeterminate amount of
     *                    objects can subscibe to the publisher's event--the publisher itself need not maintain a reference to them.
     *                    The subscribers are responsible for listening for the event to fire off; this is the key to delegatio and
     *                    will allow us to flexibly add or remove functionality to high-level game events.
     *                    NOTE: That the signature of this method MUST match the signature of the delegate specified by the publisher.
     *                    
     *    -> To subscribe to an event:
     *       EXAMPLE: public void SubscribeToEvent() {
     *                    publisher.EventTriggerd += OnEventTriggeredAction; }
     *       DESCRIPTION: Subscribing to an event is generally as easy as this one-liner. Simply use the += operator on the EventHandler
     *                    to subscribe to the event. The only minor issue with this is that the subscriber will need to have some reference
     *                    to the publisher to access the EventHandler via the '.' operator, but this is to be expected. Also note that you
     *                    can unsubscribe from events using the '-=' operator.
     */

    public event EventHandler CardDrawn;
    public event EventHandler HealthChanged;

    public void OnCardDrawn(EventArgs e)
    {
        CardDrawn?.Invoke(this, e);
    }

    public void OnHealthChanged(EventArgs e)
    {
        CardDrawn?.Invoke(this, e);
    }
    #endregion

    #region Accessors --------------------------------------------------------------------------------------------

    public GameObject PlayerGO {
        get => _playerGO;
    }
    public GameObject EnemyGO {
        get => _enemyGO;
    }
    public Player Player
    {
        get => _player;
        set => _player = value;
    }
    public Enemy Enemy
    {
        get => _enemy;
        set => _enemy = value;
    }

    #endregion ---------------------------------------------------------------------------------------------------

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
        _playerList = GameObject.Find("Combat UI").transform.Find("PlayerList").GetComponent<Text>();
        _timer = gameObject.GetComponent<TurnTimer>(); // get reference to timer
        _timer.TimeExpired += OnTimeExpired; // subscribe to its TimeExpired event

        // Instantitate player and enemy
        _playerPF = Resources.Load<GameObject>("Prefabs/PlayerCombat");
        _playerGO = Instantiate(_playerPF, playerSpawnPos, Quaternion.identity);
        _player = _playerGO.GetComponent<Player>();
        _deckManager = _playerGO.GetComponent<DeckManager>();

        // TODO: Query enemy type from web API
        _enemyPF = Resources.Load<GameObject>("Prefabs/Enemies/HeavyVirus");
        _enemyGO = Instantiate(_enemyPF, enemySpawnPos, Quaternion.identity);
        _enemy = _enemyGO.GetComponent<VirusHeavy>();
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

        StartCoroutine(TurnSystem());
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
        if(!_player.IsAlive)
        {
            ExitCombat();
        } else if (!_enemy.IsAlive){
            ExitCombat();
        }
    }
    
    IEnumerator TurnSystem()
    {
        yield return new WaitForSeconds(0.5f);
        // Start phase

        // Action phase
        _deckManager.DrawStarterHand();
        Debug.Log(_deckManager.Hand.DisplayDeck());
        // End phase

        // Send Delta

        // Enemy phase
    }

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

    private void ExitCombat()
    {
            // Loads back to map scene after death
            SceneManager.LoadScene(0);
            _enemy.endCombat();
            _player.endCombat();
            client.LeaveRoom();
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
