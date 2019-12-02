using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(CombatController))]  
[RequireComponent(typeof(DeckManager))]
public class UIController : Singleton<UIController>
{
    private CombatController _cc;                   // The CombatController
    private DeckManager _dm;                        // The DeckManager
    [SerializeField] private GameObject _uiCanvas;  // The UI canvas; highest level UI object in the hierarchy

    [SerializeField] private Transform _handZone;   // the screen region that drawn cards will populate
    [SerializeField] private Transform _playZone;   // the screen region players drag cards onto to play them

    [SerializeField] private GameObject _deck;      // the deck game object
    [SerializeField] private Text _cardsInDeck;     // text objet on the UI deck representation

    [SerializeField] private GameObject _eHealth;   // enemy health bar
    [SerializeField] private Image _eHealthFill;    // the fill of the enemy health bar
    [SerializeField] private GameObject _pHealth;   // player health bar
    [SerializeField] private Image _pHealthFill;    // the fill of the player health bar
    [SerializeField] private GameObject _memBar;    // player memory bar
    [SerializeField] private Image _memBarFill;      // player's memory bar fill
    [SerializeField] private Text _memBarText;       // player's memory bar text

    [SerializeField] private GameObject _mpHealthZone;       // Root object for the multiplayer health grid
    [SerializeField] private GameObject _mpHealthGrid;       // Multiplayer health layout grid
    [SerializeField] private List<GameObject> _mpHealthList; // List of multiplayer health buttons
    [SerializeField] private List<Image> _mpHealthFills;     // List of multiplayer health fills
    [SerializeField] private GameObject _mpHealthPF;         // Multiplayer button prefab

    [SerializeField] private GameObject _runAway;   // run away button
    // [SerializeField] private GameObject _items;     // button that accesses inventory (TODO)

    // Useful local variables
    private int _currentNumCards;
    private int _totalNumCards;
    

    #region Accessors --------------------------------------------------------------------------------------------------
    public Transform HandZone {
        get => _handZone;
    }
    public Transform PlayZone {
        get => _playZone;
    }
    public int CurrentNumCards {
        get => _currentNumCards;
        set => _currentNumCards = value;
    }
    public int TotalNumCards {
        get => _totalNumCards;
        set => _totalNumCards = value;
    }
    
    #endregion ---------------------------------------------------------------------------------------------------------

    #region UIController Responsibilities ------------------------------------------------------------------------------

    private void Awake()
    {
        // Get references to important game objects
        _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();
        _dm = GameObject.Find("CombatUtils").GetComponent<DeckManager>();

        // Get references to UI elements
        _uiCanvas = GameObject.Find("CombatUI");

        _handZone = _uiCanvas.transform.Find("HandZone");
        _playZone = _uiCanvas.transform.Find("PlayZone");

        _deck = _uiCanvas.transform.Find("Deck").gameObject;
        _cardsInDeck = _deck.transform.Find("Cards").GetComponent<Text>();

        _eHealth = _uiCanvas.transform.Find("eHealth").gameObject;
        _eHealthFill = _eHealth.transform.Find("fill").GetComponent<Image>();
        _pHealth = _eHealth = _uiCanvas.transform.Find("pHealth").gameObject;
        _pHealthFill = _eHealth.transform.Find("fill").GetComponent<Image>();
        _memBar = _uiCanvas.transform.Find("Memory").gameObject;
        _memBarFill = _memBar.transform.Find("antifill").GetComponent<Image>();
        _memBarText = _memBar.transform.Find("number").GetComponent<Text>();

        _runAway = _uiCanvas.transform.Find("Run").gameObject;

        _mpHealthZone = _uiCanvas.transform.Find("mpHealthZone").gameObject;
        _mpHealthGrid = _mpHealthZone.transform.Find("mpHealthGrid").gameObject;
        _mpHealthList = new List<GameObject>(); // initialize empty list of game objects (populated dynamically)
        _mpHealthFills = new List<Image>(); // populated dynamically with corresponding GameObjects
        _mpHealthPF = Resources.Load<GameObject>("Prefabs/UI/mpHealth");

        // Subscribe to CombatController event system
        _cc.CardsDrawn += OnCardsDrawnAction;
        _cc.MemoryChanged += OnMemoryChangedAction;
        _cc.PlayerHealthChanged += OnPlayerHealthChange;
        _cc.EnemyHealthChanged += OnEnemyHealthChange;


        // Validate all UI resources are loaded properly
        Assert.IsNotNull(_uiCanvas);

        Assert.IsNotNull(_handZone);
        Assert.IsNotNull(_playZone);

        Assert.IsNotNull(_deck);
        Assert.IsNotNull(_cardsInDeck);

        Assert.IsNotNull(_eHealth);
        Assert.IsNotNull(_eHealthFill);
        Assert.IsNotNull(_pHealth);
        Assert.IsNotNull(_pHealthFill);
        Assert.IsNotNull(_memBar);
        Assert.IsNotNull(_memBarFill);

        Assert.IsNotNull(_runAway);

        Assert.IsNotNull(_mpHealthZone);
        Assert.IsNotNull(_mpHealthGrid);
        Assert.IsNotNull(_mpHealthList);
        Assert.IsNotNull(_mpHealthFills);
        Assert.IsNotNull(_mpHealthPF);

    }

    public void UpdateCardsInDeck(int current, int total)
    {
        _currentNumCards = Mathf.Max(0, current);
        _cardsInDeck.text = _currentNumCards.ToString() + " / " + total.ToString();
    }

    public void UpdateEnemyHealth(float n)
    {
        _eHealthFill.fillAmount = n;
    }

    public void UpdatePlayerHealth(float n)
    {
        _pHealthFill.fillAmount = n;
    }

    public void UpdateMemory(int n)
    {
        float memDiff = -((float)n / (float)_cc.Player.MaxMemory);
        _memBarFill.fillAmount += memDiff;
        _memBarText.text = _cc.Player.Memory.ToString();
    }

    /* GetHandGameObjects Description:
     * A helper method that returns a list containing every GameObject that is a child of the HandZone transform
     */
    public List<GameObject> GetHandGameObjects()
    {
        int count = _handZone.childCount;
        Assert.AreEqual(count, _dm.Hand.CurrentLength); // Should be the same

        List<GameObject> GOs = new List<GameObject>();
        for(int i = 0; i < count; i++)
        {
            GameObject cardGO = _handZone.GetChild(i).gameObject;
            GOs.Add(cardGO);
        }

        return GOs;
    }

    /* Resets the parent of each active Card GameObject in the scene to the HandZone using the "Card" GameObject tag.
     * This is primarily called during the EndPhase to make sure all Cards are in one place before cleaning them up.
     */
    public void ResetCardGameObjects()
    {
        GameObject[] cardGOs = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject go in cardGOs)
        {
            go.transform.SetParent(_handZone);
        }
    }

    /* Called when a remote player connects to a combat instance to add a multiplayer health button to the multiplayer health grid.
     * Parameters:
     *   -> string playerName: The name of the client joining the game
     */
    public void AddRemotePlayerToUI(string playerName)
    {
        GameObject mpHpButton = MonoBehaviour.Instantiate(_mpHealthPF);
        MpButtonHandler handler = mpHpButton.GetComponent<MpButtonHandler>();

        handler.NameString = playerName;
        mpHpButton.transform.SetParent(_mpHealthGrid.transform);
        mpHpButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        _mpHealthList.Add(mpHpButton);
    }
    
    /* Clears all of the current player buttons from the player button grid
     * Primarily called from CombatController's OnStateChanged handler
     */
    public void ClearRemotePlayerUI()
    {
        foreach(GameObject button in _mpHealthList)
        {
            button.Destroy();
        }
    }

    public void Start()
    {
        //updateCardsInDeck(3, 30);
        //updateEnemyHealth(.8f);
        //updatePlayerHealth(.5f);
        //updateMemory(.6f);
    }

    #endregion ----------------------------------------------------------------------------------------------------------

    #region Event Handlers/Subscribers ----------------------------------------------------------------------------------

    private void OnCardsDrawnAction(object sender, DrawEventArgs e)
    {
        UpdateCardsInDeck((_currentNumCards - e.NumCards), _totalNumCards);
    }

    private void OnMemoryChangedAction(object sender, MemEventArgs e)
    {
        UpdateMemory(e.MemDiff);
    }

    private void OnPlayerHealthChange(object sender, HealthEventArgs e)
    {
        UpdatePlayerHealth(_cc.Player.Health / _cc.Player.MaxHealth);
    }

    private void OnEnemyHealthChange(object sender, HealthEventArgs e)
    {
        UpdateEnemyHealth(_cc.Enemy.Health / _cc.Enemy.MaxHealth);
    }


    #endregion ----------------------------------------------------------------------------------------------------------

}