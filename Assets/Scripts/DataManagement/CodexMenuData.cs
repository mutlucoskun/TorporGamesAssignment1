using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CodexMenuData
{
    public List<CodexMenuItem> CodexMenuItems = new List<CodexMenuItem>();
}

[System.Serializable]
public class CodexMenuItem
{
    public string name;
    public string icon;
    public string iconSelected;
    public string text;
    public int sorting;
    public List<CodexTopic> topicButtons = new List<CodexTopic>();
}

[System.Serializable]
public class CodexTopic
{
    public string name;
    public string text;
    public int sorting;
    public List<CodexEntry> entryButtons = new List<CodexEntry>();
}

[System.Serializable]
public class CodexEntry
{
    public string name;
    public string text;
    public int sorting;
    public string entryTitle;
    public string entryImage;
    public string entryText;
}
