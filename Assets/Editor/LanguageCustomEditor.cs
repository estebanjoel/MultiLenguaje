using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Language))]
public class LanguageCustomEditor : Editor
{
    Language language;
    GUIStyle titleStyle;
    GUIStyle labelStyle;
    bool displayError = false;

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
            EditorGUILayout.HelpBox("The game is in Play, modify fields with caution.", MessageType.Warning);
        }

        //Imprimo el título del custom editor
        EditorGUILayout.LabelField("Language Custom Editor", titleStyle);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Muestro el nombre del idioma en cuestión.
        language.language = EditorGUILayout.TextField("Idioma", language.language);

        //Pregunto si el idioma no tiene keys o values y en caso de ser verdadero le agrego 1 valor vacio a cada uno.
        if(language.keys == null && language.values == null)
        {
            language.keys = new List<string>();
            language.values = new List<string>();
            language.keys.Add("");
            language.values.Add("");
        }

        //Asigno el valor de tamaño de las listas al keysAndValues.
        keysAndValuesSize = language.keys.Count;
        EditorGUILayout.Space();
        
        EditorGUILayout.BeginVertical();
        EditorGUI.BeginChangeCheck();//Chequeo si modificaron el size para aumentar o disminuir la lista.

        if (displayError)
        {
            EditorGUILayout.HelpBox("Please write a value greater than 0.", MessageType.Error);
        }

        keysAndValuesSize = EditorGUILayout.IntField("Size", keysAndValuesSize);//Muestro el size de las listas.

        if (EditorGUI.EndChangeCheck())
        {
            if (keysAndValuesSize <= 0)//En caso de cambiar el valor a numeros menores que 1, indico que no es posible cambiar el tamaño.
            {
                displayError = true;
            }
            else if (keysAndValuesSize > language.keys.Count)//Si el valor es mayor, agrego los elementos necesarios en las listas.
            {
                displayError = false;
                int extraValue = keysAndValuesSize - language.keys.Count;
                for (int i = 0; i < extraValue; i++)
                {
                    language.keys.Add("");
                    language.values.Add("");
                }
            }
            else if (keysAndValuesSize < language.keys.Count)//Si el valor es menor, remuevo los ultimos elementos necesarios en las listas.
            {
                displayError = false;
                int extraValue = language.keys.Count - keysAndValuesSize;
                for (int i = 0; i < extraValue; i++)
                {
                    language.keys.Remove("");
                    language.values.Remove("");
                }
            }
        }

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
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            string key = EditorGUILayout.TextField("", language.keys[i]);

            if (EditorGUI.EndChangeCheck())
            {
                if(key != language.keys[i])
                {
                    language.keys[i] = key;
                }
            }

            EditorGUI.BeginChangeCheck();
            string value = EditorGUILayout.TextField("", language.values[i]);

            if (EditorGUI.EndChangeCheck())
            {
                if (value != language.values[i])
                {
                    language.values[i] = value;
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(language);
        }

        so.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();
    }

}
