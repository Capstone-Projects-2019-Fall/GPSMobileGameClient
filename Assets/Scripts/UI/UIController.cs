using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text cardsInDeck;
    [SerializeField] private Image enemyHealth;
    [SerializeField] private Image playerHealth;
    [SerializeField] private GameObject runAway;
    [SerializeField] private GameObject items;
    [SerializeField] private Image memory;
    [SerializeField] private GameObject deck;

    private void Awake()
    {
        Assert.IsNotNull(cardsInDeck);
        Assert.IsNotNull(enemyHealth);
        Assert.IsNotNull(playerHealth);
        Assert.IsNotNull(runAway);
        Assert.IsNotNull(items);
        Assert.IsNotNull(memory);
        Assert.IsNotNull(deck);
    }

    public void updateCardsInDeck(int current, int total)
    {
        cardsInDeck.text = current.ToString() + " / " + total.ToString();
    }

    public void updateEnemyHealth(float n)
    {
        enemyHealth.fillAmount = n;
    }

    public void updatePlayerHealth(float n)
    {
        playerHealth.fillAmount = n;
    }

    public void updateMemory(float n)
    {
        memory.fillAmount = 1 - n;
    }
    
    public void Start()
    {
        //updateCardsInDeck(3, 30);
        //updateEnemyHealth(.8f);
        //updatePlayerHealth(.5f);
        //updateMemory(.6f);
    }

}