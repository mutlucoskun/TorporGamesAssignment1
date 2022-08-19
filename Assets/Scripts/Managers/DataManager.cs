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

    private CodexMenuData FetchCodexData()
    {
        string codexJsonData = ReadJsonToString("CodexMenuData.json");
        return JsonUtility.FromJson<CodexMenuData>(codexJsonData);
    }
}