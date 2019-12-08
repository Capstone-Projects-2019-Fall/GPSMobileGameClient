using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CardInventoryZone Description:
 * A CardInventoryZone is a flexible UI element used for displaying a graphical list of Cards. Similar to the CardScrollList, the main use cases
 * for a CardInventoryZone are the crafting and deckbuilding/homebase scenes, although theoretically the prefab should be encapsulated properly and
 * flexible enough to add to other scenes in the future. */
public class CardInventoryZone : MonoBehaviour
{
    // Data fields
    [SerializeField] private Deck _myCards;
    [SerializeField] private GameObject _cardImagePF;

    // UI-related fields
    [SerializeField] private List<GameObject> _cardImageList;
    [SerializeField] private GameObject _cardViewport;
    [SerializeField] private GameObject _cardListContent;

    private void Awake()
    {
        
    }
}
