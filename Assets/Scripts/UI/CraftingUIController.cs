using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIController : MonoBehaviour
{
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


    
}
