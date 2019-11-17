using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Linq;
using System;

public static class CardFactory
{
    private static Dictionary<int, Type> CardsById;
    private static bool _initialized => CardsById != null;

    // Initialize the NodeFactory
    public static void InitializeFactory()
    {
        // Ensures only one NodeFactory can exist
        if (_initialized)
            return;

        var cards = Assembly.GetAssembly(typeof(Card)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Card)));

        CardsById = new Dictionary<int, Type>();

        foreach (var card in cards)
        {
            var instantiatedCard = Activator.CreateInstance(card) as Card;
            CardsById.Add(instantiatedCard.Id, card);
        }
    }

    /*
     * Create a Card object with a given card id.
     */
    public static Card CreateCard(int cardId)
    {
        return Activator.CreateInstance(CardsById[cardId]) as Card;
    }

    /*
     * Create a List of Card objects with a given list of card ids.
     */
    public static List<Card> CreateCards(int[] cardIds)
    {
        List<Card> cards = new List<Card>(cardIds.Length);
        for(int i = 0; i < cardIds.Length; i++)
        {
            cards.Add(Activator.CreateInstance(CardsById[i]) as Card);
        }
        return cards;
    }

    /* Will return an instantiated Card GameObject with fields dynamically populated based on a Card object */
    public static GameObject CreateCardGameObject(Card card)
    {
        // Obtain template and instantiate
        GameObject cardPF = Resources.Load<GameObject>("Prefabs/UI/Card");
        GameObject cardGO = MonoBehaviour.Instantiate(cardPF);
        cardGO.GetComponent<CardManager>().LoadCard(card);
        Transform trans = cardGO.transform; // Used to look up child objects more easily

        // Populate elements
        trans.Find("card_name").GetComponent<Text>().text = card.Name; //name
        trans.Find("cost").Find("cost_number").GetComponent<Text>().text = card.MemoryCost.ToString(); //cost
        trans.Find("description").GetComponent<Text>().text = card.Detail;

        return cardGO;
    }

    /*
     * Create a List of Card Game Objects with a given list of card objects.
     */
    public static List<GameObject> CreateCardGameObjects(List<Card> cards)
    {
        List<GameObject> cardGameObjects = new List<GameObject>(cards.Count);
        foreach(Card card in cards)
        {
            cardGameObjects.Add(CreateCardGameObject(card));
        }
        return cardGameObjects;
    }

    /*
     * Get a List of all the Card Objects.
     */
    public static List<Card> CreateAllCards()
    {
        List<Card> cards = new List<Card>();
        foreach(Type type in CardsById.Values)
        {
            cards.Add(Activator.CreateInstance(type) as Card);
        }
        return cards;
    }

    /*
     * Get an array of card ids from a list of Card Objects.
     */
    public static int[] CardsToIdArray(List<Card> cards)
    {
        int[] cardIds = new int[cards.Count];
        for(int i = 0; i < cards.Count; i++)
        {
            cardIds[i] = cards[i].Id;
        }
        return cardIds;
    }
}
