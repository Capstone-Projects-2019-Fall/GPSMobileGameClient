using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private int max_size = 40;
    private List<Card> cards;

    public int Max_Size { get => max_size; set => max_size = value; }

    public Deck()
    {

    }

    public Deck(int max_size)
    {
        Max_Size = max_size;
        //TODO: add card handling?
    }

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds card to list
    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    // Returns bool if card was removed or doesn't exist
    public bool RemoveCard(Card card)
    {
        return cards.Remove(card);
    }

    // Copies card list over
    public void CopyDeck(List<Card> cardList)
    {
        cards = new List<Card>(cardList);
    }
}
