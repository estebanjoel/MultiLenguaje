using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using System.IO;

public class LanguageCreatorWindow : EditorWindow
{

    AnimBool animBool;
    bool showAddLanguagePanel, showDeleteLanguagePanel;
    string newScriptableName = "";
    bool isScriptableNameEmpty;
    List<Language> languages;
    bool[] languageCheckboxes;
    bool isACheckBoxSelected;
    
    [MenuItem("MultiLenguaje/Language Creator Window")]
    private static void ShowWindow() {
        var window = GetWindow<LanguageCreatorWindow>();
        window.titleContent = new GUIContent("LanguageCreatorWindow");
        window.Show();
    }

    private void OnEnable()
    {

    }

    private void OnGUI()
    {
        GUILayout.Label("Language Creator", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("On this window you can create or delete Language ScriptableObjects.", guiMessageStyle);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Add / Delete Language", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Add Language Scriptable Object"))
        {
            ShowHiddenPanels(true, false);
        }
        if(GUILayout.Button("Delete Language Scriptable Object"))
        {
            ShowHiddenPanels(false, true);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
        HiddenPanels();
        
    }

    //Estilo para que el texto de un LabelField se adapte al tamaño de la ventana
    public static GUIStyle guiMessageStyle
    {
        get{
            var messageStyle = new GUIStyle(GUI.skin.label);
            messageStyle.wordWrap = true;
            return messageStyle;
        }
    }

    private void ShowHiddenPanels(bool addPanel, bool deletePanel)
    {
        showAddLanguagePanel = addPanel;
        showDeleteLanguagePanel = deletePanel;
        languages = LanguageList();
        if(deletePanel) languageCheckboxes = new bool[languages.Count];
    }

    private void HiddenPanels()
    {
        if(showAddLanguagePanel)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Add Language Scriptable Object", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            newScriptableName = EditorGUILayout.TextField("New Language Name: ", newScriptableName);

            if(GUILayout.Button("Add Language"))
            {
                if(newScriptableName == "")
                {
                    isScriptableNameEmpty = true;
                }
                else
                {
                    isScriptableNameEmpty = false;
                    CreateScriptableButton<Language>("Languages", newScriptableName);
                }
            }
            if(isScriptableNameEmpty) EditorGUILayout.HelpBox("Cannot create a Language Scriptable Object without a name!", MessageType.Warning);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            if(languages.Count == 0)
            {
                EditorGUILayout.HelpBox("There must be at least one Language in order to show this panel!", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Available Language Scriptable Objects", EditorStyles.boldLabel);
                EditorGUILayout.Space();
                for (int i = 0; i < languages.Count; i++)
                {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();
                    if (languages[i] != null) languages[i] = (Language)EditorGUILayout.ObjectField(languages[i].name, languages[i], typeof(Language));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
            }   
            EditorGUILayout.Space();
        }

        if(showDeleteLanguagePanel)
        {
            if(languages.Count == 0)
            {
                EditorGUILayout.HelpBox("There must be at least one Language in order to show this panel!", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Delete Language Scriptable Object", EditorStyles.boldLabel);
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                for(int i = 0; i < languages.Count; i++)
                {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();
                    languageCheckboxes[i] = EditorGUILayout.BeginToggleGroup("", languageCheckboxes[i]);
                    if(languages[i]!= null) languages[i] = (Language)EditorGUILayout.ObjectField(languages[i].name, languages[i], typeof(Language));
                    EditorGUILayout.EndToggleGroup();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
                if(GUILayout.Button("Delete Selected Language"))
                {
                    if(CheckboxesChecker())
                    {
                        languages = LanguageList();
                        languageCheckboxes = new bool[languages.Count];
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }
    }


    private static T CreateScriptableButton<T>(string path, string fileName) where T : ScriptableObject
    {
        T save = ScriptableObject.CreateInstance<T>();

        if (!Directory.Exists(Application.dataPath + "/" + path))
        {
            Directory.CreateDirectory(Application.dataPath + "/" + path);
        }

        string finalPath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + path + "/" + fileName + ".asset");

        AssetDatabase.CreateAsset(save, finalPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return save;
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

    private bool CheckboxesChecker()
    {
        bool isACheckBoxSelected = false;
        for(int i = 0; i < languageCheckboxes.Length; i++)
        {
            if(languageCheckboxes[i])
            {
                isACheckBoxSelected = true;
                DeleteLanguage(languages[i].name);
            }
        }
        return isACheckBoxSelected;
    }

    private void DeleteLanguage(string fileName)
    {
        AssetDatabase.DeleteAsset("Assets/Languages/" + fileName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
