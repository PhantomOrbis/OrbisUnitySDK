#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEditorInternal;

public class OrbisTag : Editor
{
    public static readonly string[] tag =
    {        
        "GameManager",
        "NetworkManager"
    };

    [MenuItem("Orbis/Tag/Add Tag", priority = 101)]
    public static void AddTag()
    {
        foreach(var location in tag)
        {
            if (location.Length == 0)
                continue;

            if (!InternalEditorUtility.tags.Contains(location))
            {
                InternalEditorUtility.AddTag(location);
            }
        }
    }

    [MenuItem("Orbis/Tag/Remove Tag", priority = 102)]
    public static void RemoveTag()
    {
        foreach (var location in tag)
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

#endif
