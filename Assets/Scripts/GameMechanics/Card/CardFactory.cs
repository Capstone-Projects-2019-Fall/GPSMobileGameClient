using System.Collections.Generic;
using UnityEngine;
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
}
