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

    public static Card CreateCard(int cardId)
    {
        return Activator.CreateInstance(CardsById[cardId]) as Card;
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
}
