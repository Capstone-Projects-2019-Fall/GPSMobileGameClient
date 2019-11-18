using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUpdate : MonoBehaviour
{
	private Button deck;
	private Text deckText;
	public int max;
	public int current;

    private void Awake()
	{
		deck = transform.Find("deck").GetComponent<Button>();
		deckText = deck.Find("cards").GetComponent<deckText>();
	}

    public void getNumCards(Deck deckObj)
	{
		max = deckObj.length();
	}

	public void setText(string text)
	{
		deckText.text = text;
	}
}
