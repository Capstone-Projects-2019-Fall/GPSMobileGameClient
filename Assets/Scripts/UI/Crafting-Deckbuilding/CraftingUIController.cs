using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUIController : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvas;  // The UI canvas; highest level UI object in the hierarchy

    [SerializeField] private Transform _upgradeZone;   // the screen region that holds cards when upgrading
    [SerializeField] private Transform _cardsZone;   // the screen region holds cards in player's deck

    
}
