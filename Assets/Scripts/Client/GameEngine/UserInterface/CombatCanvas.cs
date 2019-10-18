/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.GameEngine
{

    /// <summary>
    /// The container that holds UI elements and element behavior for the combat screen
    /// </summary>
    /// <description>
    /// The Combat Canvas is a high level container for several UI objects that each have their own lower level UI elements. Importantly, game logic itself is not handled here. Other scripts that deal
    /// with game logic in combat are able to make calls to the public methods here to alter what is displayed on the UI.
    /// </description>
    public class CombatCanvas : MonoBehaviour
    {
        /// <summary>
        /// A small UI canvas for the player's status
        /// </summary>
        public GameObject playerStatus;

        /// <summary>
        /// A small UI canvas for the enemy status
        /// </summary>
        public GameObject enemyStatus;

        /// <summary>
        /// A location within the CombatCanvas for the player's hand of Cards
        /// </summary>
        public Transform handRegion;

        /// <summary>
        /// Update a given entities' health bar with damage
        /// </summary>
        /// <param name="dmg"></param>
        /// <param name="entity"></param>
        public void dealDamage(int dmg, GameObject entity) { }

        /// <summary>
        /// Update a given entities' health bar with healing
        /// </summary>
        /// <param name="health"></param>
        /// <param name="entity"></param>
        public void healDamage(int health, GameObject entity) { }

        /// <summary>
        /// Draw cards from the player's deck 
        /// </summary>
        /// <param name="cards">A list of cards selected from the deck to add to the hand</param>
        public void drawCards(List<Card> cards) { }

        /// <summary>
        /// Discard cards from the player's hand
        /// </summary>
        /// <param name="cards">A list of cards currently in hand to send to the discard pile</param>
        public void discardCards(List<Card> cards) { }

        /// <summary>
        /// When the player would draw but their deck is empty, reshuffle
        /// </summary>
        public void reshuffule() { }

        /// <summary>
        /// Add a status icon to an entity
        /// </summary>
        public void addStatus(GameObject buff, GameObject entity) { }

        /// <summary>
        /// Remove a status icon from an entity
        /// </summary>
        public void remStatus(GameObject buff, GameObject entity) { }
    }
}
*/