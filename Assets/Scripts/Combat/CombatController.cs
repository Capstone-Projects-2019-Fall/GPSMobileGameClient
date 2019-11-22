using Colyseus.Schema;
using System;
using System.Collections;
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
    private UIController _uiCont;

    [SerializeField] private GameObject _playerPF;
    [SerializeField] private GameObject _playerGO;
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 playerSpawnPos;
    [SerializeField] private int _startingHandSize;
    private DeckManager _deckManager;

    [SerializeField] private GameObject _enemyPF;
    [SerializeField] private GameObject _enemyGO;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vector3 enemySpawnPos;
    [SerializeField] private ColyseusClient client;

    [SerializeField] private Text _playerList;
    private Transform _handZone;
    private State state;
    private TurnTimer _timer;

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
    public int StartingHandSize {
        get => _startingHandSize;
        set => _startingHandSize = value;
    }

    public MapSchema<ColyseusPlayer> players
    {
        get => state.players;
    }

    #endregion ---------------------------------------------------------------------------------------------------
        
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

    // delegates
    public event EventHandler<DrawEventArgs> CardsDrawn;
    public event EventHandler HealthChanged;

    // signallers
    public void OnCardsDrawn(DrawEventArgs e)
    {
        CardsDrawn?.Invoke(this, e);
    }

    public void OnHealthChanged(EventArgs e)
    {
        HealthChanged?.Invoke(this, e);
    }

    // updaters & state checkers

    /* DrawCard Description:
     * Simple wrapper method for the OnCardsDrawn event that can be called externally by other scripts (such as cards or items)
     * Parameters:
     *    -> int numCards: The number of cards each subscriber will be notified it must draw
     */
    public void DrawCards(int numCards)
    {
        DrawEventArgs args = new DrawEventArgs { NumCards = numCards };
        _uiCont.UpdateCardsInDeck(_deckManager.Deck.CurrentLength - numCards, _deckManager.Deck.MaxLength);
        OnCardsDrawn(args);
    }
    #endregion ---------------------------------------------------------------------------------------------------

    // Enum representing all of the important states that the client can occupy during the runtime of the TurnSystem.
    public cState clientState;
    public enum cState
    {
        Active, Busy, WaitingForServer
    }

    // methods
    private void Awake()
    {
        clientState = cState.Busy;

        // Initialize/obtain references to static classes and unity singletons
        CardFactory.InitializeFactory();
        _uiCont = gameObject.GetComponent<UIController>(); // reference to UIController
        _timer = gameObject.GetComponent<TurnTimer>(); // reference to timer
        _deckManager = gameObject.GetComponent<DeckManager>(); // reference to deck manager

        // Send data to static classes and singletons
        //_uiCont.TotalNumCards = _deckManager.Deck.MaxLength;

        // Event subscriptions
        _timer.TimeExpired += OnTimeExpired; // subscribe to the timer's TimeExpired event

        // Set local private variables
        _startingHandSize = 5;
        _playerList = GameObject.Find("CombatUI").transform.Find("PlayerList").gameObject.GetComponent<Text>();

        // Instantitate player and enemy
        _playerPF = Resources.Load<GameObject>("Prefabs/PlayerCombat");
        _playerGO = Instantiate(_playerPF, playerSpawnPos, Quaternion.identity);
        _player = _playerGO.GetComponent<Player>();

        // TODO: Query enemy type from web API
        _enemyPF = Resources.Load<GameObject>("Prefabs/Enemies/HeavyVirus");
        _enemyGO = Instantiate(_enemyPF, enemySpawnPos, Quaternion.identity);
        _enemy = _enemyGO.GetComponent<VirusHeavy>();
        
        InitializeCombat();        
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("TurnSystem");
        Card cardEX = CardFactory.CreateCard(0);
        GameObject cardEXgo = CardFactory.CreateCardGameObject(cardEX);

        cardEXgo.transform.SetParent(_uiCont.HandZone);
        cardEXgo.transform.localPosition = new Vector3(0, 0, 0);
        cardEXgo.transform.localScale = new Vector3(1, 1, 1);

        Card cardEX1 = CardFactory.CreateCard(2);
        GameObject cardEX1go = CardFactory.CreateCardGameObject(cardEX1);

        cardEX1go.transform.SetParent(_uiCont.HandZone);
        cardEX1go.transform.localPosition = new Vector3(0, 0, 0);
        cardEX1go.transform.localScale = new Vector3(1, 1, 1);

        StartCoroutine(TurnSystem());
    }

    // Update is called once per frame
    void Update()
    {
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
        yield return new WaitForSeconds(0.2f);
        StartPhase(); 
        
        // Action phase
        Debug.Log(_deckManager.Hand.DisplayDeck());
        // End phase

        // Send Delta

        // Enemy phase
    }

    private void StartPhase()
    {
        Delta.Reset();
        DrawCards(_startingHandSize);
        _timer.StartTimer();
    }

    private void ActionPhase()
    {
        clientState = cState.Active;
    }

    private void EndPhase()
    {

    }

    private void InitializeCombat()
    {
        Debug.LogFormat("Player: {0}\nRoom:{1}", _player.Username, Node.getLastClickedNodename());
        // Connect to combat instance        
        //client = new ColyseusClient();
        //client.JoinOrCreateRoom(_player.Username, Node.getLastClickedNodename(), OnStateChangeHandler);
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
        clientState = cState.Active;
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
        string newString = "";
        foreach (var key in players.Keys) {
            newString += ((ColyseusPlayer)players[key]).name + '\n';
        }
        _playerList.text = newString;
    }
}
