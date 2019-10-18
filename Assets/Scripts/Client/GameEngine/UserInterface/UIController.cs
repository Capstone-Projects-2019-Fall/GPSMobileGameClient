/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client.GameEngine
{

    /// <summary>
    /// Provides functionality for manipulating Unity UI objects via script
    /// </summary>
    /// <description>
    /// A lot of the "heavy lifting" for making a functional user interface is handled by Unity's built-in UI system. The UIController will maintain references to each of the Unity GameObjects that comprise
    /// the User Interface, as well as providing a number of useful methods for manipulating them graphically. The UIController can dynamically swap between different UI views depending on where has
    /// navigated to within the menu system. As an example, the player navigating from the Crafting System to the Deckbuilding system would be handled by the UIController, including the deletion and creation
    /// of GameObjects necessary in order to make that transition.
    /// </description>
    public class UIController : MonoBehaviour
    {
        /// <summary>
        /// Handles input from the user
        /// </summary>
        /// <description>
        /// EventSystems are a built-in Unity feature that determines what and how the user is clicking on gameplay objects.
        /// </description>
        public EventSystem eventSystem;

        /// <summary>
        /// Root UI object containing all interactive UI elements
        /// </summary>
        /// <description>
        /// Each individual interactive element on the user interface is a child object of the UI Canvas. This is a reference to a UI canvas that can change at runtime depending on which sub-menu
        /// the user has navigated to.
        /// </description>
        public GameObject canvas;
        

        /// <summary>
        /// Initialize the UI controller with a default Canvas
        /// </summary>
        public void Init()
        {

        }

        /// <summary>
        /// Handle switiching between the current UI canvas and another one.
        /// </summary>
        /// <param name="nextCanvas">The next UI canvas to be switched to via the UIController</param>
        public void switchCanvas(GameObject nextCanvas)
        {

        }

    }
}

*/