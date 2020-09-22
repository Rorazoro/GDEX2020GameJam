using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : SingletonBehaviour<DebugManager> {
    private string input;
    private Vector2 scroll;

    public bool showConsole;
    public bool showHelp;
    public PlayerInput playerInput;
    public GameObject player;
    public List<object> commandList;

    public static DebugCommand HELP;
    public static DebugCommand TEST_COMMAND;
    public static DebugCommand<float> SET_LOOKSPEED;

    private void OnGUI () {
        if (!showConsole) { return; }

        float y = 0f;

        if (showHelp) {
            GUI.Box (new Rect (0, y, Screen.width, 100), "");

            Rect viewport = new Rect (0, 0, Screen.width - 30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView (new Rect (0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++) {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect (5, 20 * i, viewport.width - 100, 20);
                GUI.Label (labelRect, label);
            }
            GUI.EndScrollView ();

            y += 100;
        }

        GUI.Box (new Rect (0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color (0, 0, 0, 0);

        GUI.SetNextControlName ("ConsoleTextField");
        input = GUI.TextField (new Rect (10f, y + 5f, Screen.width - 20f, 20f), input);
        GUI.FocusControl ("ConsoleTextField");
    }

    private void Awake () {
        HELP = new DebugCommand ("help", "Shows a list of commands", "help", () => {
            showHelp = !showHelp;
        });
        TEST_COMMAND = new DebugCommand ("test", "Test command for testing the console", "test", () => {
            Debug.Log ("Test command has been run!");
        });
        SET_LOOKSPEED = new DebugCommand<float> ("set_lookspeed", "Sets the look speed of the player", "set_lookspeed <value>", (x) => {
            ThirdPersonController controller = player.GetComponent<ThirdPersonController> ();
            if (controller == null) { Debug.Log ("ThirdPersonController not found!"); return; }

            controller.LookSpeed = x;
        });

        commandList = new List<object> {
            HELP,
            TEST_COMMAND,
            SET_LOOKSPEED
        };
    }

    public void OnToggleConsole (InputAction.CallbackContext context) {
        showConsole = !showConsole;
        if (showConsole) {
            playerInput.actions.FindAction ("Move").Disable ();
            playerInput.actions.FindAction ("Look").Disable ();
        } else {
            playerInput.actions.FindAction ("Move").Enable ();
            playerInput.actions.FindAction ("Look").Enable ();
        }

        ClearConsoleInput ();
    }
    public void OnReturn (InputAction.CallbackContext context) {
        if (showConsole) {
            HandleConsoleInput ();
            ClearConsoleInput ();
        }
    }

    public void HandleConsoleInput () {
        string[] properties = input.Split (' ');

        for (int i = 0; i < commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains (commandBase.commandId)) {
                if (commandList[i] as DebugCommand != null) {
                    (commandList[i] as DebugCommand).Invoke ();
                } else if (commandList[i] as DebugCommand<float> != null) {
                    (commandList[i] as DebugCommand<float>).Invoke (float.Parse (properties[1]));
                } else if (commandList[i] as DebugCommand<int> != null) {
                    (commandList[i] as DebugCommand<int>).Invoke (int.Parse (properties[1]));
                } else if (commandList[i] as DebugCommand<string> != null) {
                    (commandList[i] as DebugCommand<string>).Invoke (properties[1]);
                } else if (commandList[i] as DebugCommand<bool> != null) {
                    (commandList[i] as DebugCommand<bool>).Invoke (bool.Parse (properties[1]));
                }
            }
        }
    }

    public void ClearConsoleInput () {
        input = "";
    }
}