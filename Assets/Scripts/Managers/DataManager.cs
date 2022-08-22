using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Object = UnityEngine.Object;

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DataManager>();
            return _instance;
        }
    }
    #endregion

    #region Codex
    private CodexMenuData _codexMenu;
    public CodexMenuData CodexMenu
    {
        get
        {
            if (_codexMenu == null)
            {
                _codexMenu = FetchCodexData();
            }

            return _codexMenu;
        }
    }
    private CodexMenuData FetchCodexData()
    {
        string codexJsonData = ReadJsonToString("CodexMenuData.json");
        return JsonUtility.FromJson<CodexMenuData>(codexJsonData);
    }
    #endregion

    #region Notes
    private NotesMenuData _notesMenu;
    public NotesMenuData NotesMenu
    {
        get
        {
            if (_notesMenu == null)
            {
                _notesMenu = FetchNotesData();
            }

            return _notesMenu;
        }
    }
    private NotesMenuData FetchNotesData()
    {
        string notesJsonData = ReadJsonToString("NotesMenuData.json");
        return JsonUtility.FromJson<NotesMenuData>(notesJsonData);
    }

    public void SaveNotesData(NotesMenuData notesData)
    {
        WriteJson("NotesMenuData.json", JsonUtility.ToJson(notesData));
    }
    #endregion

    #region DataManagement

    private string ReadJsonToString(string fileName)
    {
        string fullPath = Path.Combine("Assets/Data", fileName);
        if (File.Exists(fullPath))
        {
            StreamReader sr = new StreamReader(fullPath);
            string contents = sr.ReadToEnd();
            sr.Close();
            return contents;
        }
        else
        {
            return null;
        }
    }
    
    private  void WriteJson( string fileName, string json)
    {
        StreamWriter sw;
        
        string fullPath = Path.Combine("Assets/Data", fileName);

        try
        {
            if (!File.Exists(fullPath))
            {
                sw = File.CreateText(fullPath);
                sw.WriteLine(json);
                sw.Close();
            }
            else
            {
                File.Delete(fullPath);
                sw = File.CreateText(fullPath);
                sw.WriteLine(json);
                sw.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    #endregion
}