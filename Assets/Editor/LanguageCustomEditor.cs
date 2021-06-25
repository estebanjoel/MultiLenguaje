using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Language))]
public class LanguageCustomEditor : Editor
{
    Language language;
    GUIStyle titleStyle;
    GUIStyle labelStyle;

    private void OnEnable()
    {
        language = target as Language;

        titleStyle = new GUIStyle();
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.fontSize = 20;
        titleStyle.fontStyle = FontStyle.Bold;

        labelStyle = new GUIStyle();
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.fontStyle = FontStyle.Bold;
    }

    int keysAndValuesSize;

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

        //Muestro el nombre del idioma en cuestión e igualo el tamaño de las listas al keysAndValuesSize.
        language.language = EditorGUILayout.TextField("Idioma", language.language);
        keysAndValuesSize = language.keys.Count;
        EditorGUILayout.Space();

        //Muestro el size de las listas.
        EditorGUILayout.BeginVertical();
        keysAndValuesSize = EditorGUILayout.IntField("Size", keysAndValuesSize);
        EditorGUILayout.Space();

        //Serializo el scriptableObject
        SerializedObject so = new SerializedObject(language);
        SerializedProperty valuesProperty = so.FindProperty("values");
        SerializedProperty keysProperty = so.FindProperty("keys");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Keys", labelStyle);
        EditorGUILayout.LabelField("Values", labelStyle);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        //Muestro los strings uno al lado del otro con textfield.
        for (int i = 0; i < keysAndValuesSize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("", language.keys[i]);
            EditorGUILayout.TextField("", language.values[i]);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();

        //EditorGUILayout.PropertyField(valuesProperty, true);
        //EditorGUILayout.PropertyField(keysProperty, true);
        so.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();
    }

}
