using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    #region Singleton
    private static LanguageManager _instance;

    public static LanguageManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<LanguageManager>();
            return _instance;
        }
    }
    #endregion

    public string Translate(string translationKey)
    {
        //TODO: Implement localization through translation keys

        return translationKey;
    }
}
