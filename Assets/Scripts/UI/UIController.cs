using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(CombatController))]
public class UIController : Singleton<UIController>
{
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

    #region Accessors --------------------------------------------------------------------------------------------------
    public Transform HandZone {
        get => _handZone;
    }
    public Transform PlayZone {
        get => _playZone;
    }

    #endregion ---------------------------------------------------------------------------------------------------------


    private void Awake()
    {
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
        _memBar = _eHealth = _uiCanvas.transform.Find("Memory").gameObject;
        _memBarFill = _eHealth.transform.Find("antifill").GetComponent<Image>();

        _runAway = _uiCanvas.transform.Find("Run").gameObject;


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

    public void UpdateCardsInDeck(int current, int total)
    {
        _cardsInDeck.text = current.ToString() + " / " + total.ToString();
    }

    public void UpdateEnemyHealth(float n)
    {
        _eHealthFill.fillAmount = n;
    }

    public void UpdatePlayerHealth(float n)
    {
        _pHealthFill.fillAmount = n;
    }

    public void UpdateMemory(float n)
    {
        _memBarFill.fillAmount = 1 - n;
    }
    
    public void Start()
    {
        //updateCardsInDeck(3, 30);
        //updateEnemyHealth(.8f);
        //updatePlayerHealth(.5f);
        //updateMemory(.6f);
    }

}