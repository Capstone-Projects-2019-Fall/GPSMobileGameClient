using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class CombatTester : MonoBehaviour
{
    [SerializeField] public CombatController _cc;

    // Update is called once per frame
    void Update()
    {
        // Test Player receiving damage
        _cc.Player.Health = 100;
        _cc.ChangePlayerHealth(5);
        Assert.AreEqual(95,_cc.Player.Health);

        // Test Enemy receiving damage
        _cc.Enemy.Health = 100;
        _cc.ChangeEnemyHealth(5);
        Assert.AreEqual(95, _cc.Enemy.Health);

        // Test Player Drawcards
        _cc.DrawCards(5);
        Assert.AreEqual(5,_cc.Player.DeckManager.Hand.Cards.Count);

        // Test Player losing memory.
        _cc.Player.Memory = 5;
        _cc.ChangeMemory(1);
        Assert.AreEqual(4, _cc.Player.Memory);
    }
}
