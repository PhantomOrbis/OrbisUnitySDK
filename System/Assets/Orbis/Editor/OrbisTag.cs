#if UNITY_EDITOR

using System.Collections.Generic;
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
    private static List<string> tag = new List<string>();


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

    private static void SetTag()
    {
        foreach(var code in InternalEditorUtility.tags)
        {
            tag.Add(code);
        }
    }

    private void OnGUI()
    {
        OrbisTagWindow window = (OrbisTagWindow)EditorWindow.GetWindow(typeof(OrbisTagWindow));
        //Debug.Log("x : " + window.position.size.x);
        //Debug.Log("y : " + window.position.size.y);
        GUILayout.BeginArea(new Rect(20, 20, 3960, 3960));
        GUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.MaxHeight((window.position.size.y - 40)));
        {
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            
            GUILayout.Label("Tag");
            {
                GUI.skin.label.fontSize = 16;
                GUI.skin.label.fontStyle = FontStyle.Bold;
            }
            GUILayout.FlexibleSpace();            
            GUILayout.EndHorizontal();            
            GUILayout.Space(10);
                        
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1f), Color.gray);

            if (tag.Count != InternalEditorUtility.tags.Count())
            {
                foreach (var code in InternalEditorUtility.tags)
                {
                    tag.Add(code);
                }
            }

            GUILayout.Space(10);

            for (int i = 0; i < tag.Count; i++)
            {
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(tag[i], GUILayout.Width(180F)))
                {
                    Debug.Log(tag[i]);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
        
        GUILayout.EndVertical();        
        GUILayout.EndArea();        
    }
}

#endif
