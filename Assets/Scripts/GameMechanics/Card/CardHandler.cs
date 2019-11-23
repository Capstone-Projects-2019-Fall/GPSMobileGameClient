using UnityEngine;

/* CardHandler Description:
 * Wrapper class for a Card, allowing its behavior to be accessed by a MonoBehaviour in the CombatScene
 */
public class CardHandler : MonoBehaviour
{
    // Fields
    [SerializeField] private Card _myCard;
    private CombatController _cc;

    public Card MyCard {
        get => _myCard;
        set => _myCard = value;
    }

    private void Awake()
    {
        _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();
    }

    /* Wrapper method for each Card's playCard method
     * Will validate that the Player has enough Memory to play a card before actually invoking the method itself.
     * Parameters:
     *    -> Player p: The clientside representation of the player in the CombatScene
     *    -> Enemy e: The clientside representation of the enemy in the CombatScene
     */
    public void PlayCard(Player p, Enemy e)
    {
        if(p.Memory >= _myCard.MemoryCost) // If the player has enough memory, then play the card
        {
            _cc.CardPlayed += OnCardPlayedAction; // Listen for OK from CombatController
            _cc.PlayCard(gameObject);
        } else
        {
            Debug.Log("Not enough Memory to play that card!");
            gameObject.transform.SetParent(GameObject.Find("CombatUI").transform.Find("HandZone"));
        }
    }

    /* Event handler for CombatController's CardPlayed Event
     * Used in CardHandler as a confirmation mechanism: if this event is being received by the CardHandler, then
     * the card must have been successfully played.
     */
    private void OnCardPlayedAction(object sender, CardPlayedArgs e)
    {
        gameObject.Destroy();
        _cc.CardPlayed -= OnCardPlayedAction; // unsubscribe from the event
    }
}
