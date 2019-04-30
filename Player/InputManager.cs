using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
/// Handles input for a single player character using ReWired.
/// Should be placed on an animal.
/// </summary>
public class InputManager : MonoBehaviour
{
    [Tooltip("The controller index of the player. Index starts at zero.")]
    public int playerIndex;

    [Header("Bindings")]
    [Tooltip("ReWired button name for standard attacks.")]
    public string attackButton;

    [Tooltip("ReWired button name for special attacks.")]
    public string specialButton;

    [Tooltip("ReWired button name for evasion moves.")]
    public string evadeButton;

    [Tooltip("ReWired button name for ultimate moves.")]
    public string ultButton;

    [Header("Keyboard")]
    [Tooltip("Check this to override controller input with keyboard input.")]
    public bool useKeyboard;

    public KeyCode keyboardAttack;
    public KeyCode keyboardSpecial;
    public KeyCode keyboardEvade;
    public KeyCode keyboardUlt;

    [Tooltip("Name of the Unity axis that should be used for horizontal input.")]
    public string keyboardHorizontalAxis;

    [Tooltip("Name of the Unity axis that should be used for vertical input.")]
    public string keyboardVerticalAxis;

    private Player player;// my ReWired player instance

    // Set vibration in all Joysticks assigned to the Player
    private int motorIndex = 0; // the first motor
    private float motorLevel = 1.0f; // full motor speed
    private float duration = 0.5f; // 0.5 seconds

    private void Start()
    {
        //print(playerIndex);
        player = ReInput.players.GetPlayer(playerIndex - 1);// get our ReWired player instance from ReWired, indexed at 0
        //print(player.name);
    }

    /// <summary>
    /// The current direction of player input.
    /// </summary>
    public Vector2 InputVector
    {
        get
        {
            if (!useKeyboard) return new Vector2(player.GetAxis("Horizontal"), player.GetAxis("Vertical")).normalized;
            else return new Vector2(Input.GetAxis(keyboardHorizontalAxis), Input.GetAxis(keyboardVerticalAxis)).normalized;

        }
    }

    /// <summary>
    /// Returns whether the player has pressed the standard attack button this frame.
    /// </summary>
    public bool GetAttackButtonDown()
    {
        if (!useKeyboard) return player.GetButtonDown(attackButton);
        else return Input.GetKeyDown(keyboardAttack);
    }

    /// <summary>
    ///  Returns whether the player has pressed the special attack button this frame.
    /// </summary>
    public bool GetSpecialButtonDown()
    {
        if (!useKeyboard) return player.GetButtonDown(specialButton);
        else return Input.GetKeyDown(keyboardSpecial);
    }

    /// <summary>
    /// Returns whether the player has pressed the evasion button this frame.
    /// </summary>
    public bool GetEvadeButtonDown()
    {
        if (!useKeyboard) return player.GetButtonDown(evadeButton);
        else return Input.GetKeyDown(keyboardEvade);
    }

    /// <summary>
    /// Returns whether the player has pressed the evasion button this frame.
    /// </summary>
    public bool GetUltButtonDown()
    {
        if (!useKeyboard) return player.GetButtonDown(ultButton);
        else return Input.GetKeyDown(keyboardUlt);
    }

    /// <summary>
    /// Returns whether the player is currently holding the standard attack button this frame.
    /// </summary>
    public bool GetAttackButton()
    {
        if (!useKeyboard) return player.GetButton(attackButton);
        else return Input.GetKey(keyboardAttack);
    }

    /// <summary>
    /// Returns whether the player is currently holding the special attack button this frame.
    /// </summary>
    public bool GetSpecialButton()
    {
        if (!useKeyboard) return player.GetButton(specialButton);
        else return Input.GetKey(keyboardSpecial);
    }

    /// <summary>
    /// Returns whether the player is currently holding the evasion button this frame.
    /// </summary>
    public bool GetEvadeButton()
    {
        if (!useKeyboard) return player.GetButton(evadeButton);
        else return Input.GetKey(keyboardEvade);
    }

    /// <summary>
    /// Returns whether the player is currently holding the ultimate button this frame.
    /// </summary>
    public bool GetUltButton()
    {
        if (!useKeyboard)
        {
            print("Ult button pressed");
            return player.GetButton(ultButton);
        }
        else return Input.GetKey(keyboardUlt);
    }

    public void Rumble() {
        player.SetVibration(motorIndex, motorLevel, duration);
    }

    /// <summary>
    ///  Checks that every player character in the scene has a controller and prints an error message if there is not.
    /// </summary>
    private void CheckConnectivity()
    {
        //Checking to ensure the number of controllers matches the number of players
        int numPlayers = GameObject.Find("GameController").GetComponent<GameController>().numberOfPlayers;
        //print(numPlayers);
        string[] connectedControllers = Input.GetJoystickNames();

        //Rename PS4 controllers, if any
        for (int i = 0; i < connectedControllers.Length; i++)
        {
            if (connectedControllers[i].Contains("Sony") || connectedControllers[i].Contains("Unknown"))
            {
                connectedControllers[i] = "PS4";
            }
        }

        if (numPlayers > connectedControllers.Length)
        {
            print("Disconnected Controller!");
            //            Bool for displaying an error message if needed
            //            disconnected = true;
        }
    }
}
