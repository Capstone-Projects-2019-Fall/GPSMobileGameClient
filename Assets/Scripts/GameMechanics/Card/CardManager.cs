using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Text title;
    public Text detail;
    public Text flavor;
    public Image art;

    public Card card;

    private void Start()
    {
        LoadCard(card);
    }

    public void LoadCard(Card c)
    {
        if(c == null)
        {
            return;
        }
        card = c;
        title.text = c.Name;
        detail.text = c.Detail;
        flavor.text = c.Flavor;
        art.sprite = c.art;
    }
    
}
