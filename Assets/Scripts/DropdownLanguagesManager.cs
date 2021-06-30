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
        fillDropDown(myLanguages, myDropDown);

        //Hago un for inicial que llena el dropdown con los lenguajes disponibles y asigna el que esté activo.
        for (int i = 0; i < myDropDown.options.Count; i++)
        {
            if(myLanguageManager.languageSetter.currentLanguage.language == myDropDown.options[i].text)
            {
                myDropDown.SetValueWithoutNotify(i);
            }
        }

        //Hago un evento que detecta cuando se cambia el valor del dropdown para que llame a una función.
        myDropDown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(myLanguages[myDropDown.value]);
        });
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

    private void fillDropDown(List<Language> languages, TMP_Dropdown dropDown)
    {
        dropDown.options.Clear();
        foreach (var language in languages)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = language.language });
        }
    }
}
