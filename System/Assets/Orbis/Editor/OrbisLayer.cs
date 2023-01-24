using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OrbisLayer : Editor
{
    private static int layerCount = 31;

    public static readonly string[] layer =
    {
        "Character",
        "Obstruction",
        "Point",
        "Interactive",
        "Map"
    };

    [MenuItem("Orbis/Layer/Add Layer", priority = 201)]
    public static void InitLayer()
    {
        foreach (var location in layer)
        {
            if (location.Length == 0)
                continue;

            AddLayer(location);
        }
    }

    [MenuItem("Orbis/Layer/Remove Layer", priority = 201)]
    public static void RemoveLayer()
    {
        foreach(var location in layer)
        {
            if (location.Length == 0)
                continue;

            DeleteLayer(location);
        }
    }

    public static void AddLayer(string name)
    {
        CreateLayer(name);
    }

    public static void DeleteLayer(string name)
    {
        RemoveLayer(name);
    }

    public static bool CreateLayer(string layerName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        // Layers Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        if (!PropertyExists(layersProp, 0, layerCount, layerName))
        {
            SerializedProperty sp;
            // Start at layer 9th index -> 8 (zero based) => first 8 reserved for unity / greyed out
            for (int i = 8, j = layerCount; i < j; i++)
            {
                sp = layersProp.GetArrayElementAtIndex(i);
                if (sp.stringValue == "")
                {
                    // Assign string value to layer
                    sp.stringValue = layerName;
                    Debug.Log("Layer: " + layerName + " has been added");
                    // Save settings
                    tagManager.ApplyModifiedProperties();
                    return true;
                }
                if (i == j)
                    Debug.Log("All allowed layers have been filled");
            }
        }
        else
        {
            //Debug.Log ("Layer: " + layerName + " already exists");
        }
        return false;
    }

    public static string NewLayer(string name)
    {
        if (name != null || name != "")
        {
            CreateLayer(name);
        }

        return name;
    }

    public static bool RemoveLayer(string layerName)
    {

        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Tags Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        if (PropertyExists(layersProp, 0, layersProp.arraySize, layerName))
        {
            SerializedProperty sp;

            for (int i = 0, j = layersProp.arraySize; i < j; i++)
            {

                sp = layersProp.GetArrayElementAtIndex(i);

                if (sp.stringValue == layerName)
                {
                    sp.stringValue = "";
                    Debug.Log("Layer: " + layerName + " has been removed");
                    // Save settings
                    tagManager.ApplyModifiedProperties();
                    return true;
                }

            }
        }

        return false;

    }

    public static bool LayerExists(string layerName)
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        // Layers Property
        SerializedProperty layersProp = tagManager.FindProperty("layers");
        return PropertyExists(layersProp, 0, layerCount, layerName);
    }

    private static bool PropertyExists(SerializedProperty property, int start, int end, string value)
    {
        for (int i = start; i < end; i++)
        {
            SerializedProperty t = property.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(value))
            {
                return true;
            }
        }
        return false;
    }
}
