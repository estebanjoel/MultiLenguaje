using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;
    public LanguageSetter languageSetter;

    void Awake()
    {
        #region Singleton
        if(instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(instance);
        #endregion
    }

    private void Start()
    {
        languageSetter.SetLanguage(languageSetter.currentLanguage);
    }

    public string GetLanguageValue(string key)
    {
        string result = "< Key is Missing! >";
        if(languageSetter.currentLanguage.texts.ContainsKey(key)) result = languageSetter.currentLanguage.texts[key];
        return result;
    }
}
