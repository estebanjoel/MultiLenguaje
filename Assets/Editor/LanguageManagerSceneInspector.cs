using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LanguageManager))]
public class LanguageManagerSceneInspector : Editor
{
    LanguageManager languageManager;
    LanguageObject[] sceneLanguageObjects;
    List<Language> myLanguages;
    int languageIndex;

    bool showSetLanguagePanel, showSetLanguageObjectsPanel;
    void OnEnable()
    {
        languageManager = target as LanguageManager;    
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        var sceneViewRect = EditorWindow.GetWindow<SceneView>().camera.pixelRect;
        GUILayout.BeginArea(new Rect(sceneViewRect.width / 2 - 250, sceneViewRect.height / 2.5f, 300, 500));

        var r = EditorGUILayout.BeginVertical();

        var original = GUI.color;
        GUI.color = new Color(0.7f, 0.7f, 0.7f);
        GUI.Box(r, GUIContent.none);
        GUI.color = original;

        GUILayout.Label("Language Manager Setter", GUItitleStyle);

        GUILayout.Space(3);

        // EditorGUILayout.HelpBox("On this window, you can select the current language and set the language on the LanguageObjects on scene. Be sure to set the LanguageObject script on every gameObject you want to modify it's language.", MessageType.Info);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(Screen.width/2));
        if(GUILayout.Button("Set Current Language", GUILayout.MaxWidth(Screen.width/2)))
        {
            showSetLanguagePanel = true;
            showSetLanguageObjectsPanel = false;
        }
        if(GUILayout.Button("Test Language on Scene", GUILayout.MaxWidth(Screen.width/2)))
        {
           showSetLanguagePanel = false;
           showSetLanguageObjectsPanel = true;
        }
        EditorGUILayout.EndHorizontal();
        
        if(showSetLanguagePanel) SetLanguagePanel();
        if(showSetLanguageObjectsPanel) SetLanguageObjects();

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        GUILayout.EndArea();
        Handles.EndGUI();    
    }

    private static GUIStyle GUItitleStyle
    {
        get
        {
            var titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.fontSize = 18;
            titleStyle.wordWrap = true;
            titleStyle.alignment = TextAnchor.UpperCenter;
            return titleStyle;
        }
    }

    private void SetLanguagePanel()
    {
        myLanguages = LanguageList();
        if(myLanguages.Count == 0 || myLanguages == null)
        {
            EditorGUILayout.HelpBox("You must have at least one Language in order to set a current language", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Current Language: " + languageManager.languageSetter.currentLanguage.name);
            languageIndex = EditorGUILayout.Popup("Select Language to set:", languageIndex, LanguageNameList(myLanguages));
            if(GUILayout.Button("Set New Language"))
            {
                languageManager.languageSetter.SetLanguage(myLanguages[languageIndex]);
            }
            EditorGUILayout.EndVertical();
        }
    }

    private void SetLanguageObjects()
    {
        LanguageObject[] languageObjects = GameObject.FindObjectsOfType<LanguageObject>();
        if(languageObjects.Length == 0)
        {
            EditorGUILayout.HelpBox("You must have at least one GameObject with the LanguageObject script in order to test the Language Setter.", MessageType.Warning);

        }
        else
        {
            EditorGUILayout.HelpBox("All LanguageObjects have been assigned.\nTouch the Game panel in order to see the changes.", MessageType.None);
            languageManager.languageSetter.SetLanguage(languageManager.languageSetter.currentLanguage);
            foreach(LanguageObject languageObject in languageObjects)
            {
                languageObject.SetLanguageObject(languageManager);
            }
        }
    }

    private List<Language> LanguageList()
    {
         List<Language> myLanguages = new List<Language>();
         string[] languagesGuids = AssetDatabase.FindAssets("", new[] {"Assets/Languages"});
         for (int i = 0; i < languagesGuids.Length; i++)
         {
             myLanguages.Add((Language)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(languagesGuids[i]), typeof(Language)));
         }
         return myLanguages;
    }

    private string[] LanguageNameList(List<Language> languages)
    {
        string[] languageNameList = new string[languages.Count];
        for(int i = 0; i < languageNameList.Length; i++)
        {
            languageNameList[i] = languages[i].name;
        }
        return languageNameList;
    }

}
