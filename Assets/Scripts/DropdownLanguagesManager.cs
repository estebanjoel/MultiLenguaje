using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownLanguagesManager : MonoBehaviour
{
    private List<Language> myLanguages;
    private TMP_Dropdown myDropDown;
    public LanguageManager myLanguageManager;

    // Start is called before the first frame update
    void Start()
    {
        myLanguages = new List<Language>();
        myDropDown = GetComponent<TMP_Dropdown>();

        myLanguages = LanguageList();
        FillDropDown(myLanguages, myDropDown);

        //Hago un for inicial que llena el dropdown con los lenguajes disponibles y asigna el que esté activo.
        SetValue(myLanguageManager.languageSetter.currentLanguage.language, myDropDown);

        //Hago un evento que detecta cuando se cambia el valor del dropdown para que llame a una función.
        myDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(myLanguages[myDropDown.value]);
        });
    }

    void Update()
    {
        if(myLanguageManager.languageSetter.currentLanguage.language != myDropDown.options[myDropDown.value].text)
        {
            SetValue(myLanguageManager.languageSetter.currentLanguage.language, myDropDown);
        }
    }

    private void DropdownValueChanged(Language newCurrentLanguage)
    {
        myLanguageManager.languageSetter.SetLanguage(newCurrentLanguage);
        LanguageObject[] languageObjects = GameObject.FindObjectsOfType<LanguageObject>();

        foreach (LanguageObject languageObject in languageObjects)
        {
            languageObject.SetLanguageObject(myLanguageManager);
        }
    }

    private List<Language> LanguageList()
    {
        List<Language> myLanguages = new List<Language>();
        string[] languagesGuids = AssetDatabase.FindAssets("", new[] { "Assets/Languages" });
        for (int i = 0; i < languagesGuids.Length; i++)
        {
            myLanguages.Add((Language)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(languagesGuids[i]), typeof(Language)));
        }
        return myLanguages;
    }

    private void FillDropDown(List<Language> languages, TMP_Dropdown dropDown)
    {
        dropDown.options.Clear();
        foreach (var language in languages)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = language.language });
        }
    }

    private void SetValue(string languageName, TMP_Dropdown dropDown)
    {
        for (int i = 0; i < dropDown.options.Count; i++)
        {
            if (languageName == dropDown.options[i].text)
            {
                dropDown.SetValueWithoutNotify(i);
            }
        }
    }
}
