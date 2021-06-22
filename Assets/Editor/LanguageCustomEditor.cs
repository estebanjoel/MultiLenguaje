using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(Language))]
public class LanguageCustomEditor : Editor
{
    Language language;
    GUIStyle titleStyle;

    private void OnEnable()
    {
        language = target as Language;

        titleStyle = new GUIStyle();
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.fontSize = 20;
        titleStyle.fontStyle = FontStyle.Bold;
    }

    public override void OnInspectorGUI()
    {
        //Pregunta si el juego está andando para que no se hagan modificaciones y luego se pierdan
        if (Application.isPlaying)
        {
            EditorGUILayout.HelpBox("El juego está andando, no edites los valores.", MessageType.Warning);
        }

        //Imprimo el título del custom editor
        EditorGUILayout.LabelField("Language Custom Editor", titleStyle);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Muestro el nombre del idioma en cuestión.
        language.language = EditorGUILayout.TextField("Idioma", language.language);
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();

        SerializedObject so = new SerializedObject(language);
        SerializedProperty valuesProperty = so.FindProperty("values");
        SerializedProperty keysProperty = so.FindProperty("keys");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(valuesProperty, true);
        EditorGUILayout.PropertyField(keysProperty, true);
        so.ApplyModifiedProperties();

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        //if (GUI.changed)//Si hicimos algun cambio en el editor...
        //{
        //    EditorSceneManager.MarkSceneDirty(language.gameObject.scene); //marcamos la escena como guardable
        //}
    }

}
