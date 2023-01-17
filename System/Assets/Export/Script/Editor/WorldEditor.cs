#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditorInternal;
using System.Linq;

public class WorldEditor : Editor
{
    // [ Init ] =================================================
    public static readonly string[] TagArray =
    {
        "GameManager",
        "NetworkManager",
        "User",
        "Npc",
    };

    public static readonly string[] LayerArray =
    {
        "Character",
        "Obstruction",
        "Point",
        "Interactive",
        "Map"
    };

    [MenuItem("World/Tag And Layer/Init Tag", priority = 100)]
    public static void InitTag()
    {
        foreach (var tag in TagArray)
        {
            if (tag.Length == 0)
                continue;

            if (!InternalEditorUtility.tags.Contains(tag))
            {
                InternalEditorUtility.AddTag(tag);
            }
        }
    }

    [MenuItem("World/Tag And Layer/Init Layer", priority = 101)]
    public static void InitLayer()
    {
        foreach (var layer in LayerArray)
        {
            if (layer.Length == 0)
                continue;

            AddNewLayer(layer);
        }
    }


    private static int maxLayers = 31;
    public static void AddNewLayer(string name)
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
        if (!PropertyExists(layersProp, 0, maxLayers, layerName))
        {
            SerializedProperty sp;
            // Start at layer 9th index -> 8 (zero based) => first 8 reserved for unity / greyed out
            for (int i = 8, j = maxLayers; i < j; i++)
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
        return PropertyExists(layersProp, 0, maxLayers, layerName);
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


    // [ Select ] =================================================    
    private static EditorMode editorMode;
    private enum EditorMode
    {        
        Phantom = 0,
        Dev = 1,
        QA = 2,
        Release = 3,
    }

    [MenuItem("World/EditorMode/Phantom", priority = 0)]
    private static void Phantom()
    {
        editorMode = EditorMode.Phantom;
        SetAddressableProfile(editorMode.ToString());

        SetEditorModeCheckMark();
    }

    [MenuItem("World/EditorMode/Dev", priority = 1)]
    private static void Dev()
    {
        editorMode = EditorMode.Dev;
        SetAddressableProfile(editorMode.ToString());

        SetEditorModeCheckMark();
    }

    [MenuItem("World/EditorMode/QA", priority = 1)]
    private static void QA()
    {
        editorMode = EditorMode.QA;
        SetAddressableProfile(editorMode.ToString());

        SetEditorModeCheckMark();
    }

    [MenuItem("World/EditorMode/Release", priority = 1)]
    private static void Release()
    {
        editorMode = EditorMode.Release;
        SetAddressableProfile(editorMode.ToString());

        SetEditorModeCheckMark();
    }

    public static readonly string[] LabelArray =
    {
        "map",
        "video",
        "data",
        "image",
        "sound",
        "world",
        "character",
        "skybox",
        "avatar",
        "all",
        "acc",
        "bottoms",
        "outfit",
        "top",
        "hair",
        "shoes",
    };

    [MenuItem("World/Addressable/Init Label", priority = 200)]
    private static void InitLabel()
    {
        foreach (var label in LabelArray)
        {
            if (label.Length == 0)
                continue;

            AddressableAssetSettingsDefaultObject.Settings.AddLabel(label, true);
        }
    }

    [MenuItem("World/Addressable/Remove Label", priority = 201)]
    private static void RemoveLabel()
    {
        foreach (var label in LabelArray)
        {
            if (label.Length == 0)
                continue;

            AddressableAssetSettingsDefaultObject.Settings.RemoveLabel(label, true);
        }
    }

    private static void SetAddressableProfile(string profile)
    {
        string profileId = AddressableAssetSettingsDefaultObject.Settings.profileSettings.GetProfileId(profile);
        if (string.IsNullOrEmpty(profileId))
        {
            Debug.LogWarning($"Couldn't find a profile named, {profile}, " +
                 $"using current profile instead.");
        }
        else
        {
            AddressableAssetSettingsDefaultObject.Settings.activeProfileId = profileId;

            switch (editorMode)
            {
                case EditorMode.Phantom:
                    AddressableAssetSettingsDefaultObject.Settings.OverridePlayerVersion = "phantom";
                    break;
                case EditorMode.Dev:
                    AddressableAssetSettingsDefaultObject.Settings.OverridePlayerVersion = "awesomepia";
                    break;
                case EditorMode.QA:
                    AddressableAssetSettingsDefaultObject.Settings.OverridePlayerVersion = "awesomepia";
                    break;
                case EditorMode.Release:
                    AddressableAssetSettingsDefaultObject.Settings.OverridePlayerVersion = "awesomepia";
                    break;
            }

            AddressableAssetSettingsDefaultObject.Settings.ContentStateBuildPath = $"Assets/AddressableAssetsData/{profile}";
            Debug.Log($"Change Addressable Group {profile}");
        }
    }

    private static void SetEditorModeCheckMark()
    {        
        Menu.SetChecked("World/EditorMode/Phantom", editorMode == EditorMode.Phantom);
        Menu.SetChecked("World/EditorMode/Dev", editorMode == EditorMode.Dev);
        Menu.SetChecked("World/EditorMode/QA", editorMode == EditorMode.QA);
        Menu.SetChecked("World/EditorMode/Release", editorMode == EditorMode.Release);
    }
}
#endif
