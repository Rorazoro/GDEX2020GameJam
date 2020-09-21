using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : SingletonBehaviour<DebugManager> {
    public bool showConsole;

    string input;

    public static DebugCommand TEST_COMMAND;
    public List<object> commandList;

    private void OnGUI () {
        if (!showConsole) { return; }

        float y = 0f;

        GUI.Box (new Rect (0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color (0, 0, 0, 0);

        GUI.SetNextControlName ("ConsoleTextField");
        input = GUI.TextField (new Rect (10f, y + 5f, Screen.width - 20f, 20f), input);
        GUI.FocusControl ("ConsoleTextField");
    }

    private void Awake () {
        TEST_COMMAND = new DebugCommand ("test", "Test command for testing the console", "test", () => {
            Debug.Log ("Test command has been run!");
        });

        commandList = new List<object> {
            TEST_COMMAND
        };
    }

    public void OnToggleConsole (InputAction.CallbackContext context) {
        showConsole = !showConsole;
    }
    public void OnReturn (InputAction.CallbackContext context) {
        if (showConsole) {
            HandleConsoleInput ();
            ClearConsoleInput ();
        }
    }

    public void HandleConsoleInput () {
        for (int i = 0; i < commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains (commandBase.commandId)) {
                if (commandList[i] as DebugCommand != null) {
                    (commandList[i] as DebugCommand).Invoke ();
                }
            }
        }
    }

    public void ClearConsoleInput () {
        input = "";
    }
}