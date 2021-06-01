using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class LanguageCreatorWindow : EditorWindow
{

    [MenuItem("MultiLenguaje/Language Creator Window")]
    private static void ShowWindow() {
        var window = GetWindow<LanguageCreatorWindow>();
        window.titleContent = new GUIContent("LanguageCreatorWindow");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Language Creator", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("On this window you can create, modify or delete Language ScriptableObjects.", guiMessageStyle);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Add / Modify / Delete Language", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Create New Language"))
        {
            // Aquí habrá una función que creará un nuevo ScriptableObject de tipo Language
        }
        if(GUILayout.Button("Modify Language"))
        {
            // Aquí habrá una función que modificará un ScriptableObject de tipo Language existente
        }
        if(GUILayout.Button("Delete Language"))
        {
            // Aquí habrá una función que eliminará un ScriptableObject de tipo Language existente
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
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
}
