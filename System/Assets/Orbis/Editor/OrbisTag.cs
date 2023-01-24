#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class OrbisTag
{
    public static readonly string[] tag =
    {        
        "GameManager",
        "NetworkManager"
    };
}

public class OrbisTagMenu : Editor
{
    [MenuItem("Orbis/Tag/Add Tag", priority = 121)]
    private static void AddTag()
    {
        foreach (var location in OrbisTag.tag)
        {
            if (location.Length == 0)
                continue;

            if (!InternalEditorUtility.tags.Contains(location))
            {
                InternalEditorUtility.AddTag(location);
            }
        }
    }

    [MenuItem("Orbis/Tag/Remove Tag", priority = 122)]
    private static void RemoveTag()
    {
        foreach (var location in OrbisTag.tag)
        {
            if (location.Length == 0)
                continue;

            if (InternalEditorUtility.tags.Contains(location))
            {
                InternalEditorUtility.RemoveTag(location);
            }
        }
    }
}

[InitializeOnLoad]
public class OrbisTagWindow : EditorWindow
{  
    [MenuItem("Orbis/Tag/Tag Manager", priority = 101)]
    private static void TagWindow()
    {
        /*
         * utility[bool]
         * [true] = floating utility window
         * [false ] = normal window
         */
        OrbisTagWindow window = (OrbisTagWindow)EditorWindow.GetWindow(typeof(OrbisTagWindow), false, "Tag Manager");
        window.Show();        
    }

    private void OnGUI()
    {
        OrbisTagWindow window = (OrbisTagWindow)EditorWindow.GetWindow(typeof(OrbisTagWindow));
        //Debug.Log("x : " + window.position.size.x);
        //Debug.Log("y : " + window.position.size.y);
        GUILayout.BeginArea(new Rect(20, 20, 4000, 4000));

        GUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.MaxHeight((window.position.size.y - 20)));             
        GUILayout.Label("Side NavBar", EditorStyles.boldLabel);        
        GUILayout.EndVertical();
        
        GUILayout.EndArea();        
    }
}

#endif
