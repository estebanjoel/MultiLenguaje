using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LanguageObject))]
[System.Serializable]
public class LanguageObjectEditor : Editor
{
   LanguageObject languageObject;
   bool[] itemsCheckers;
   private void OnEnable()
   {
       languageObject = target as LanguageObject;
   }

   public override void OnInspectorGUI()
   {
        EditorGUILayout.HelpBox("Choose the right key for this object in order to make his right language conversion", MessageType.Info);
        GUILayout.Label("Language Key", EditorStyles.boldLabel);
        serializedObject.FindProperty("keyIndex").intValue = EditorGUILayout.Popup(languageObject.keyIndex, LanguageKeys());
        serializedObject.FindProperty("key").stringValue = LanguageKeys()[languageObject.keyIndex];
        // languageObject.key = LanguageKeys()[keyIndex];
        EditorGUILayout.Space();
        GUILayout.Label("Language Object Type", EditorStyles.boldLabel);
        serializedObject.FindProperty("keyTypeIndex").intValue = EditorGUILayout.Popup(languageObject.keyTypeIndex, KeyTypes());
        SelectedItem(languageObject.keyTypeIndex, KeyTypes().Length);
        serializedObject.FindProperty("textUI").boolValue = itemsCheckers[0];
        serializedObject.FindProperty("buttonUI").boolValue = itemsCheckers[1];
        serializedObject.FindProperty("textMeshPro").boolValue = itemsCheckers[2];
        // languageObject.textUI = itemsCheckers[0];
        // languageObject.buttonUI = itemsCheckers[1];
        // languageObject.textMeshPro = itemsCheckers[2];
        if(GUI.changed)
        {
           EditorUtility.SetDirty(languageObject);
           EditorSceneManager.MarkSceneDirty(languageObject.gameObject.scene);
        }
        serializedObject.ApplyModifiedProperties();
   }

   private string[] LanguageKeys()
   {
        LanguageManager languageManager = (LanguageManager)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LanguageManager.prefab", typeof(LanguageManager));
        List<string> keyList = languageManager.languageSetter.currentLanguage.keys;
        string[] keys = new string[keyList.Count];
        for(int i = 0; i < keyList.Count; i++)
        {
            keys[i] = keyList[i];
        }
        return keys;
   }

   private string[] KeyTypes()
   {
       List<string> myKeys = new List<string>();
       myKeys.Add("textUI");
       myKeys.Add("buttonUI");
       myKeys.Add("textMeshPro");
       string[] myKeysArray = new string[myKeys.Count];
       for(int i = 0; i < myKeys.Count; i++)
       {
           myKeysArray[i] = myKeys[i];
       }
       return myKeysArray;
   }

   private bool[] SelectedItem(int itemIndex, int arrayLength)
   {
       itemsCheckers = new bool[arrayLength];
       for(int i = 0; i < itemsCheckers.Length; i++)
       {
           if(i == itemIndex) itemsCheckers[i] = true;
           else itemsCheckers[i] = false;
       }
       return itemsCheckers;
   }

}
