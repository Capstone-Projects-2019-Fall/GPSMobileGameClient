using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* DeckUpdate Description:
 * A simple utility script attached to the deck displayed on the Combat scene's user interface. It is referenced by the UIController, and
 * features a few simple methods to update the graphical/textual representation of the deck.
 * NOTE: This script should be attached to the same GameObject as the main deck in the combat scene
 */
 [RequireComponent(typeof(Deck))]
public class DeckUpdate : MonoBehaviour
{
	private Transform _deck;
	private Text _deckText;

	public int max;
	public int current;

    private void Awake()
	{
		_deck = transform.Find("deck");
		_deckText = _deck.Find("cards").GetComponent<Text>();
	}

    public void GetNumCards(Deck deck)
	{
		max = deck.MaxSize;
	}

	public void SetText(string text)
	{
		_deckText.text = text;
	}

    public void UpdateDeckText()
    {
        string newString = "";
        newString += current + "/" + max;
        SetText(newString);
    }
}
