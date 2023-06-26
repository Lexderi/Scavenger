using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Hotkeys
{
    private static readonly Dictionary<string, KeyCode> nameToHotkey = new()
    {
        { "Primary", KeyCode.Alpha1 }
    };

    public static KeyCode GetKeyCode(string name)
    {
        if (nameToHotkey.TryGetValue(name, out KeyCode keyCode))
        {
            return keyCode;
        }
        throw new($"Hotkey named {name} does not exist");
    }

    public static bool GetKey(string name) => Input.GetKey(GetKeyCode(name));
    public static bool GetKeyDown(string name) => Input.GetKeyDown(GetKeyCode(name));
    public static bool GetKeyUp(string name) => Input.GetKeyUp(GetKeyCode(name));
}
