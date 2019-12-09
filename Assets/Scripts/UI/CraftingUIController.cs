using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CraftingUIController : MonoBehaviour
{
  //  private DeckManager _dm;                        // The DeckManager

    [SerializeField] private GameObject _craftingUI;  // The UI canvas; highest level UI object in the hierarchy

    [SerializeField] private Transform _upgradeZone;   // the screen region that holds cards when upgrading
    [SerializeField] private Transform _cardInventoryZone;   // the screen region holds cards in player's deck

    [SerializeField] private Button _upgrade;               // a button which allows players to upgrade the card
    [SerializeField] private Button _remove;            // a button which will remove the current card in the upgrade zone

    [SerializeField] private Image _playerGold;         // an image to indicate the player's gold ammount
    [SerializeField] private Text _playerGoldText;      // the text component of player gold

    [SerializeField] private Text _levelText;       // a text field to indicate the level of the card
    [SerializeField] private Text _levelUpDesc;     // text field to describe the bennefits of upgrading a card

    [SerializeField] private Image _goldCost;       // an image indicating the ammount of gold needed ot upgrade a card
    [SerializeField] private Text _goldCostText;    // text component of gold cost

    [SerializeField] private Button _leaveScene;    // a button that allows player to leave the scene;

    private List<Card> playerDeck;

    public Transform UpgradeZone
    {
        get => _upgradeZone;
    }

    public Transform CardInventoryZone
    {
        get => _cardInventoryZone;
    }


    private void Awake()
    {
        // _dm = GameObject.Find("CombatUtils").GetComponent<DeckManager>();
        _craftingUI = GameObject.Find("CraftingUI");

        _upgradeZone = _craftingUI.transform.Find("UpgradeZone");
        _cardInventoryZone = _craftingUI.transform.Find("CardInventoryZone");

        _upgrade = _craftingUI.transform.Find("Upgrade").GetComponent<Button>();
        _remove = _craftingUI.transform.Find("Remove").GetComponent<Button>();

        _playerGold = _craftingUI.transform.Find("PlayerGold").GetComponent<Image>();
        _playerGoldText = _playerGold.transform.Find("GoldAmmount").GetComponent<Text>();

        _levelText = _craftingUI.transform.Find("Level").GetComponent<Text>();
        _levelUpDesc = _levelText.transform.Find("UpgradeDescription").GetComponent<Text>();

        _goldCost = _craftingUI.transform.Find("Gold").GetComponent<Image>();
        _goldCostText = _goldCost.transform.Find("CostAmmount").GetComponent<Text>();

        _leaveScene = _craftingUI.transform.Find("Leave").GetComponent<Button>();


        Assert.IsNotNull(_craftingUI);
        Assert.IsNotNull(_upgradeZone);
        Assert.IsNotNull(_cardInventoryZone);
        Assert.IsNotNull(_upgrade);
        Assert.IsNotNull(_remove);
        Assert.IsNotNull(_playerGold);
        Assert.IsNotNull(_playerGoldText);
        Assert.IsNotNull(_levelText);
        Assert.IsNotNull(_levelUpDesc);
        Assert.IsNotNull(_goldCost);
        Assert.IsNotNull(_goldCostText);
        Assert.IsNotNull(_leaveScene);
    }

    public void PopulateCardInventory(Deck collection)
    {
        playerDeck = collection.Cards;


    }

    public void GetPlayerGold(Player player)
    {
       _playerGoldText.text = player.Gold.ToString();

    }

    public void GetUpgradeCost(Card card)
    {
        _goldCostText.text = card.UpgradeCost.ToString();

        if(_goldCostText.text.CompareTo(_playerGoldText.text) > 1)
        {
            _goldCostText.color = Color.gray;
        }
        else
        {
            _goldCostText.color = new Color(251 / 255f, 176 / 255f, 59 / 255f, 1f);
        }
       
    }

    public void UpdateCardLevel(Card card)
    {

        if (card.Level.ToString().Equals("1"))
        {
            _levelText.text = "Level 1 -> 2";
            _levelUpDesc.text = "+10%";
        }
        else if (card.Level.ToString().Equals("2"))
        {
            _levelText.text = "Level 2 -> 3";
            _levelUpDesc.text = "+20%";
        }
        else if (card.Level.ToString().Equals("3"))
        {
            _levelText.text = "Level 3 (MAX)";
            _levelUpDesc.text = "MAX";
        }

    }

    public void ExitScene()
    {
        SceneManager.LoadScene("GPSMobileGame");

    }

}
