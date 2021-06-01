using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableUtility
{
    public static T CreateScriptable<T>(string path, string fileName) where T : ScriptableObject
    {
        T save = ScriptableObject.CreateInstance<T>();


        //Debug.Log(Application.dataPath + "/" + path);

        if (!Directory.Exists(Application.dataPath + "/" + path))
        {
            Directory.CreateDirectory(Application.dataPath + "/" + path);
        }

        string finalPath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + path + "/" + fileName + ".asset");

        AssetDatabase.CreateAsset(save, finalPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();

        return save;
    }
}
