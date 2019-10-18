using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client.GameEngine
{
    /*
    /// <summary>
    /// This is the Game Mechanics API for Ground War. 
    /// </summary>
    /// <description>
    /// These are classes related to game mechanics
    /// </description>
    public class GameMechanics : MonoBehaviour
    {
        /// <summary>
        /// This is the Battle System class
        /// </summary>
        /// <description>
        /// These are functions related to the battle system 
        /// </description>
        public class BattleSystem
        {

            /// <summary>
            /// Starts the battle and initializes variables of the battle, like player and enemu health, cards in the player deck, checks the number of players int he battle, etc
            /// </summary>
            public void startBattle();

            /// <summary>
            /// Gets the current round of the battle
            /// </summary>
            public int getRound();

            /// <summary>
            /// Sets the round of the battle
            /// </summary>
            public int setRound();

            /// <summary>
            /// Gets the current round of the battle
            /// </summary>
            public int getRound();


            /// <summary>
            /// Gets the enemy health from server
            /// </summary>
            /// <returns>
            /// Returns a float representing the enemy health
            /// </returns>
            public float getEnemyHealth();

            /// <summary>
            /// Gets the player health from server
            /// </summary>
            /// <returns>
            /// Returns a float representing the player health
            /// </returns>
            public float getPlayerHealth();

            /// <summary>
            /// Sends the damage taken by the enemy to the server
            /// </summary>
            /// <param name="enemyDamageDelta">This is a value of the difference between the enemy's health at the start and end of the round </param>
            public  setEnemyHealth(float damageDelta, int round);

            /// <summary>
            /// Sends the damage taken by the player to the server
            /// </summary>
            /// <param name="playerDamageDelta">The purpose of this is to send how much damage the player took </param>
            public setPlayerHealth(float playerDamageDelta);

            /// <summary>
            /// Sends the type of buff given to the player to the server
            /// </summary>
            /// <param name="buff">This will make the player stronger based on the buff passed </param>
            public void buffPlayer(string buff);

            /// <summary>
            /// Sends the type of buff given to the player to the server
            /// </summary>
            /// <param name="buff">This will make the enemy stronger based on the buff </param>
            public void buffEnemy(string buff);

            /// <summary>
            /// Sends the type of buff given to the player to the server
            /// </summary>
            /// <param name="debuff">This will make the player weaker based on the debuff passed </param>
            public void debuffPlayer(string debuff);

            /// <summary>
            /// Sends the type of buff given to the player to the server
            /// </summary>
            /// <param name="debuff">This will make the enemy weaker based on the debuff passed </param>
            public void debuffEnemy(string debuff);

            /// <summary>
            /// Draws a card from the user's deck
            /// </summary>
            public void drawCard();

            /// <summary>
            /// Plays a card from the user's hand
            /// </summary>
            public void playCard();



        }

        /// <summary>
        /// This is the Crafting class
        /// </summary>
        /// <description>
        /// It contains functions related to the crafting system
        /// </description>
        public class Crafting
        {
            /// <summary>
            /// Gets the information of a card given the card id
            /// </summary>
            /// <param name="cardId">This is the identifier of the card </param>
            /// <returns>
            /// An array of strings from the server of the card information
            /// </returns>
            public ArrayList<string> getCardInfo(int cardId);

            /// <summary>
            /// Combines resources to strengthen them
            /// </summary>
            /// <param name="resourceId">This is the identifier of the resource to combine</param>
            /// <returns>
            /// A bool value to check if the combination worked
            /// </returns>
            public bool combineResources(List<int> resourceId);

            /// <summary>
            /// Combines resources to strengthen them
            /// </summary>
            /// <param name="playerid">This is the identifier of the player </param>
            /// <param name="resourceId">This is the identifier of the resource that was removed </param>
            public void updateInventory(int palyerId, List<int> resourceId);

            /// <summary>
            /// Creates a new resource once a combination is successful
            /// </summary>
            /// <returns>
            /// An integer value of the new resource id
            /// </returns>
            public int createResource();

            /// <summary>
            /// Upgrades cards to strengthen them
            /// </summary>
            /// <param name="cardId">This is the identifier of the card to upgrade</param>
            /// <returns>
            /// A bool value to check if the upgrade worked
            /// </returns>
            public bool upgradeCard(List<int> cardId);

            /// <summary>
            /// Updates the card info after upgrading it
            /// </summary>
            /// <param name="cardId">This is the identifier of the card to update</param>
            /// <param name="updatedInfo">This is a list of strings of the card infor to update</param>
            /// <returns>
            /// A bool value to check if the update worked
            /// </returns>
            public bool updateCard(int cardId, List<string> updatedInfo);
        }

        /// <summary>
        /// This is the Loot System class
        /// </summary>
        /// <description>
        /// It contains functions related to the loot system
        /// </description>
        public class Loot
        {
            /// <summary>
            /// Creates an item
            /// </summary>
            /// <returns>
            /// An integer of the item id
            /// </returns>
            public int generateItem();

            /// <summary>
            /// Adds the item to the player's inventory
            /// </summary>
            /// <param name="playerId">This is the identifier for the player </param>
            /// /// <param name="itemId">This is the identifier for the item </param>
            /// <returns>
            /// A bool value to check if the update worked
            /// </returns>
            public bool updateInventory(int playerId, int itemId);
        }

        /// <summary>
        /// This is the Mapping class
        /// </summary>
        /// <description>
        /// It contains functions related to the mapping system
        /// </description>
        public class Mapping
        {
            /// <summary>
            /// Loads the tiles for the ground layer based on the map api data
            /// </summary>
            public void loadGround();

            /// <summary>
            /// Loads the tiles for the buildings layer based on the map aipi data
            /// </summary>
            public void loadBuildings();

            /// <summary>
            /// Loads the tiles for the street layer based on the map api data
            /// </summary>
            public void loadStreets();

            /// <summary>
            /// Loads the markers for points of interes from the map api which will later become nodes
            /// </summary>
            public void loadPointsOfInterest();

        }
    }
    */    
}
