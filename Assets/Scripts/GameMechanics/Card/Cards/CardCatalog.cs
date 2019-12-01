using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Contains a catalog of all cards. Easier to track and implement new cards.
// This only really works if we track Card id within the PlayCard function.
public static class CardCatalog
{
    // 
    public static int cardCount { get; set; }

    // Array to contain all defined cards.
    private static Card[] cardDefs = Array.Empty<Card>();

    private static void Init()
    {
        CardCatalog.DefineCards();
    }

    // Defines cards.
    private static void DefineCards()
    {
        CardCatalog.cardDefs = new Card[9];
        CardCatalog.cardCount = CardCatalog.cardDefs.Length;
        /*
         * Example of what it would look like.
        CardCatalog.RegisterCard(0, new Card
        {
            Name = "Strike 1";
            Detail = "Deal 20 damage to the enemy.";
            Flavor = "Simple solutions to complicated problems.";
            Level = 1;
            MemoryCost = 2;
            UpgradeCost = 50;
        })
        */
    }

    // Registers a card
    private static void RegisterCard(int cardIndex, Card card)
    {
        CardCatalog.cardDefs[cardIndex] = card;
        /*
        if(card.Name == null)
        {
            card.name = cardIndex.ToString();
        }
        if(card.Detail == null)
        {
            card.Detail = "Null description.";
        }
        
         
         */
    }
}

