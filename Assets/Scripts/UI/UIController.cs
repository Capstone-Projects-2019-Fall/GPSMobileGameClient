using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(CombatController))]    
public class UIController : Singleton<UIController>
{
    private CombatController _cc;                   // The CombatController
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
    [SerializeField] private Image _memBarFill;      // player's memory bar fil

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
        // Get references to UI elements
        _uiCanvas = GameObject.Find("CombatUI");
        _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();

        _handZone = _uiCanvas.transform.Find("HandZone");
        _playZone = _uiCanvas.transform.Find("PlayZone");

        _deck = _uiCanvas.transform.Find("Deck").gameObject;
        _cardsInDeck = _deck.transform.Find("Cards").GetComponent<Text>();

        _eHealth = _uiCanvas.transform.Find("eHealth").gameObject;
        _eHealthFill = _eHealth.transform.Find("fill").GetComponent<Image>();
        _pHealth = _eHealth = _uiCanvas.transform.Find("pHealth").gameObject;
        _pHealthFill = _eHealth.transform.Find("fill").GetComponent<Image>();
        _memBar = _eHealth = _uiCanvas.transform.Find("Memory").gameObject;
        _memBarFill = _eHealth.transform.Find("antifill").GetComponent<Image>();

        _runAway = _uiCanvas.transform.Find("Run").gameObject;


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

    }
    
    /*
     * 
     */
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