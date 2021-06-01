using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSetter : MonoBehaviour
{
    public Language currentLanguage;
    
    public void SetLanguage(Language lang)
    {
        currentLanguage = lang;
    }
}
