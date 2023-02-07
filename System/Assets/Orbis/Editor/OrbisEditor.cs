using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace Orbis
{

    [InitializeOnLoad]
    public class OrbisEditor : Editor
    {
        private static bool isOrbis = false;        

        static OrbisEditor()
        {
            OrbisSetting();                        
        }

        private static void OrbisSetting()
        {
            var orbis = "Orbis.asset";
            var rootPath = "Assets/Resources/Metalive";
            var path = rootPath + "/" + orbis;
            var data = AssetDatabase.LoadAssetAtPath(path, typeof(OrbisData)) as OrbisData;
            if(data == null)
            {
                if(!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                data = CreateInstance<OrbisData>();
                AssetDatabase.CreateAsset(data, path);          
            }
            
            if(!isOrbis)
            {
                OrbisAccountWindow();
            }
        }

        private static void OrbisWindow()
        {

        }

        [MenuItem("Orbis/Account", priority = 1)]
        private static void OrbisAccountWindow()
        {
            OrbisEditorAccountWindow window = (OrbisEditorAccountWindow)EditorWindow.GetWindow(typeof(OrbisEditorAccountWindow), false, "OrbisAccount");
            window.ShowUtility();
        }
    }

    public class OrbisEditorWindow : EditorWindow
    {
        private void OnGUI()
        {
            
        }
    }

    public class OrbisEditorAccountWindow : EditorWindow
    {
        private void OnGUI()
        {
            if (Event.current.keyCode == KeyCode.Escape)
            {
                Close();
            }
        }
    }
}