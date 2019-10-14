using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.GameEngine
{
	/// <summary>
    /// The container that holds UI elements and element behavior for the crafting system
    /// </summary>
    /// <description>
    /// This single screen supports two separate but closely related pieces of functionality: editing Cards and editing Components. In order to make both of these features fit within the
    /// same screen a lot of the functionality of this class revolves around altering the UI elements' scale and position in response to the user's input. Cards and Components can be "staged",
    /// which means they are brought into the center of the canvas for editing purposes.
    /// </description>
	public class CraftingCanvas : MonoBehaviour
    { 
		/// <summary>
        /// A list of each card in the player's collection
        /// </summary>
        public List<Card> cardCollection;

		/// <summary>
        /// The UI screen element that holds the card collection
        /// </summary>
		public GameObject cardScrollView;

		/// <summary>
        /// The currently staged card within the crafting system.
        /// </summary>
        /// <description>
        /// The player can only make changes to a single card at any given time. While one card is staged, the cardScrollView is hidden with hideCardView
        /// </description>
        public Card stagedCard;

		/// <summary>
        /// A list of each component in the player's component collection
        /// </summary>
        public List<Component> componentCollection;

		/// <summary>
        /// The UI screen element that holds the component collection
        /// </summary>
		public GameObject componentScrollView;

		/// <summary>
        /// The component currently being edited
        /// </summary>
        public Component stagedComponent;

		/// <summary>
        /// Stage one card so the player can edit it
        /// </summary>
        /// <description>
        /// This method is responsible for "snapping in" the inboundCard into the staging area and making other function calls to manipulate the user interface in response to the player
        /// starting the editing process. Importantly, it also is responsible for checking which card/component is currently staged and for removing it from the staging area.
        /// </description>
        /// <param name="inboundCard">The card to be staged</param>
		public void stageCard(Card inboundCard) { }
        /// <summary>
        /// Stage one component so the player can edit it
        /// </summary>
        /// <description>
        /// This method is responsible for "snapping in" the inboundComponent into the staging area and making other function calls to manipulate the user interface in response to the player
        /// starting the editing process. Importantly, it also is responsible for checking which card/component is currently staged and for removing it from the staging area.
        /// </description>
        /// <param name="inboundComponent">The component to be staged</param>
        public void stageComponent(Component inboundComponent) { }

		/// <summary>
        /// Hide the cardCollection so the player has more room to edit the staged card
        /// </summary>
        public void hideCardView() { }

		/// <summary>
        /// Hide the componentCollection so the player has more room to edit the staged component
        /// </summary>
        public void hideComponentView() { }
    }
}