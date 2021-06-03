using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New language", menuName = "Language Info")]
public class Language : ScriptableObject
{
    //Nombre del lenguaje ex: español.
    public string language;
    //Diccionario con los textos y sus keys correspondientes.
    public Dictionary<string, string> texts;
    //Lista de las keys de los textos.
    public List<string> keys;
    //Lista de los textos.
    public List<string> values;

    //Cree una función que llena un diccionario porque los diccionarios no se muestran en Editor.
    public Dictionary<string, string> FillDictionary(List<string> llaves, List<string> valores)
    {
        Dictionary<string, string> textos = new Dictionary<string, string>();

        if(llaves != null || valores != null)
        {
            for (int i = 0; i < llaves.Count -1; i++)
            {
                textos.Add(llaves[i], valores[i]);
            }
        }

        return textos;
    }

    //Acá comenté un ejemplo usando la función de arriba, esto teniendo en cuenta que las listas estaban previamente llenas.
    //public void Start()
    //{
    //    texts = new Dictionary<string, string>();

    //    texts = FillDictionary(keys, values);
    //}
}
