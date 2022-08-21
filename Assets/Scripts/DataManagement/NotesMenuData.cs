using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NotesMenuData
{
    public List<NotesMenuItem> NotesMenuItems = new List<NotesMenuItem>();
}
    
[System.Serializable]
public class NotesMenuItem
{
    public string name;
    public string text;
    public int sorting;
    public List<NotesEntry> entryButtons = new List<NotesEntry>();
}

[System.Serializable]
public class NotesEntry
{
    public string text;
}
