using System;

public class DebugCommandBase {
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    public string commandId { get => _commandId; }
    public string commandDescription { get => _commandDescription; }
    public string commandFormat { get => _commandFormat; }

    public DebugCommandBase (string id, string description, string format) {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase {
    private Action command;

    public DebugCommand (string id, string description, string format, Action command) : base (id, description, format) {
        this.command = command;
    }

    public void Invoke () {
        command.Invoke ();
    }
}