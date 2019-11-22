using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CardHandler Description:
 * Wrapper class for a Card, allowing its behavior to be accessed by a MonoBehaviour in the CombatScene
 */
public class CardHandler : MonoBehaviour
{
    [SerializeField] private Card _myCard;

    public Card MyCard {
        get => _myCard;
        set => _myCard = value;
    }

    /* Wrapper method for each Card's playCard method
     * Will validate that the Player has enough Memory to play a card before actually invoking the method itself.
     * Parameters:
     *    -> Player p: The clientside representation of the player in the CombatScene
     *    -> Enemy e: The clientside representation of the enemy in the CombatScene
     */
    public void PlayCard(Player p, Enemy e)
    {
        if(p.Memory >= _myCard.MemoryCost)
        {
            _myCard.PlayCard(p, e);
            gameObject.Destroy();
        } else
        {
            Debug.Log("Not enough Memory to play that card!");
        }
    }
}
