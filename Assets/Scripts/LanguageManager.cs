using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    void Awake()
    {
        #region Singleton
        if(instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(instance);
        #endregion
    }
}
