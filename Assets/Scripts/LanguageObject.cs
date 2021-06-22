using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageObject : MonoBehaviour
{
    [Header("Language Key")]
    public string key;
    [Header("Affected Components")]
    public bool textUI;
    public bool buttonUI;
    public bool textMeshPro;
    public int keyIndex;
    public int keyTypeIndex;
    // Start is called before the first frame update
    void Start()
    {
        SetLanguageObject();
    }

    private void SetLanguageObject()
    {
        Text comp = null;
        if(textUI)
        {
            comp = GetComponent<Text>();
            comp.text = LanguageManager.instance.GetLanguageValue(key);
        }
        if(buttonUI)
        {
            comp = transform.GetChild(0).GetComponent<Text>();
            comp.text = LanguageManager.instance.GetLanguageValue(key);
        }
        TextMeshProUGUI meshComp = null;;
        if(textMeshPro)
        {
            meshComp = GetComponent<TextMeshProUGUI>();
            meshComp.text = LanguageManager.instance.GetLanguageValue(key);
        }
    }
}
