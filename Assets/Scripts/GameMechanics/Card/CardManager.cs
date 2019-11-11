using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    /*public Text title;
    public Text detail;
    public Text flavor;
    public Image art;*/

    public Card card;

    private ICardInterface cardHandler;

    private void Start()
    {
    }

    public void LoadCard(Card c)
    {
        if(c == null)
        {
            return;
        }
        card = c;
        /*title.text = card.Name;
        detail.text = card.Detail;
        flavor.text = card.Flavor;
        art.sprite = card.art;*/
    }

    public void OnMouseDown(Player p, Enemy e)
    {
        cardHandler.playCard(p, e);
        
        gameObject.Destroy();
    }

}
