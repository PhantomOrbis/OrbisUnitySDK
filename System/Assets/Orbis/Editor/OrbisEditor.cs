#if UNITY_EDITOR

using CodiceApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Orbis
{

    [InitializeOnLoad]
    public class OrbisEditor : Editor
    {        
        static OrbisEditor()
        {
            Init();                        
        }

        #region UnityVariable
        
        public static OrbisData data;
        public static string click = "";

        #endregion



        #region UnityMethod


        public static bool IsClick([CallerMemberName] string memberName = "")
        {
            string member = memberName + DateTime.Now.ToString();

            if (click.Equals(member))
            {
                return true;
            }
            else
            {
                click = member;
                return false;
            }
        }

        public static void Init()
        {
            var orbis = "Orbis.asset";
            var rootPath = "Assets/Resources/Metalive";
            var path = rootPath + "/" + orbis;

            data = AssetDatabase.LoadAssetAtPath(path, typeof(OrbisData)) as OrbisData;
            if(data == null)
            {
                if(!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }

                data = CreateInstance<OrbisData>();
                AssetDatabase.CreateAsset(data, path);
            }

            // Manager only one count;
            if (data.account.Find(x => x.security == OrbisAccountSecurity.Manager) == null)
            {
                OrbisAccount account = new OrbisAccount()
                {
                    connect = OrbisAccountConnect.None,
                    security = OrbisAccountSecurity.Manager,
                    remember = OrbisAccountRemember.None,
                    user = "root",
                    password = "0000",
                    email = ""
                };
                data.account.Add(account);                 
                EditorUtility.SetDirty(data);                
            }            
        }

        [MenuItem("Orbis/Orbis Manager", priority = 1)]
        public static void Manager()
        {
            if(data.account.Find(x => x.connect == OrbisAccountConnect.Connect) == null)
            {
                Account();
            }
            else
            {

            }            
        }
        
        public static void Account()
        {
            //Resolution[] resolutions = Screen.resolutions;                        
            //foreach(var screen in resolutions)
            //{
            //    Debug.Log(screen.width + "x" + screen.height);
            //}

            var editor = CreateInstance<OrbisEditorAccountWindow>();                        
            var x = (Screen.currentResolution.width - editor.Size().x) * 0.5f;
            var y = (Screen.currentResolution.height - editor.Size().y) * 0.5f;
            
            var window = EditorWindow.GetWindow(typeof(OrbisEditorAccountWindow), false, "[Orbis] Account");                           
            window.ShowAsDropDown(new Rect(0, 0, x, y), new Vector2(editor.Size().x, editor.Size().y));            
        }
        
        public static void DashBoard()
        {

        }

        #endregion
    }

    public class OrbisEditorDashBoardWindow : EditorWindow
    {
        private void OnGUI()
        {
            if (Event.current.keyCode == KeyCode.Escape)
            {
                Close();
            }
        }
    }

    public class OrbisEditorAccountWindow : EditorWindow
    {

        #region UnityVariable
                      
        // User
        private string user = "User"; 
        
        // Password
        private string password = "Password";
        private string security = "Password";        

        // Option - remember
        private bool remember = false;

        public string test = "";

        #endregion;



        #region UnityMethod
        
        public void Login()
        {
            if (OrbisEditor.IsClick())
                return;
            
            if (OrbisEditor.data.account.Find(x => x.user == user.Trim()) != null)
            {
                OrbisAccount account = OrbisEditor.data.account.Find(x => x.user == user.Trim());
                if(account.password.Equals(security))
                {                    
                    account.connect = OrbisAccountConnect.Connect;
                    if (remember)
                    {
                        account.remember = OrbisAccountRemember.Remember;                        
                    }
                    
                    EditorUtility.SetDirty(OrbisEditor.data);
                    Close();
                }
                else
                {

                    //var lastRect = GUILayoutUtility.GetLastRect();
                    


                }
            }            
            else
            {
                Debug.Log("Login fail - user no search");
                // 회원이 존재하지않음
                // -> 관리자한테 요청필요
                // 
                var popup = new OrbisEditorAccountPopup();
                PopupWindow.Show(new Rect(0, 0, 200f, 50f), popup);
            }
        }
                
        public Vector2 Size()
        {
            return new Vector2(280, 420);
        }

        public Vector2 Margin()
        {
            return new Vector2(20, 20);
        }

        void OnGUI()
        {            
            // Escape => Account window close
            if (Event.current.keyCode == KeyCode.Escape)
            {
                Close();                
            }

            if (Event.current.keyCode == KeyCode.Return)
            {
                Login();
            }

            
            GUILayout.BeginArea(new Rect(Vector2.zero, Size()));
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.BeginHorizontal();
                    {
                        var titleStyle = new GUIStyle(GUI.skin.label);
                        titleStyle.fontSize = 28;
                        titleStyle.fontStyle = FontStyle.Bold;
                        titleStyle.alignment = TextAnchor.MiddleLeft;
                        GUILayout.Label("Welcome!", titleStyle, GUILayout.Width(280), GUILayout.Height(32));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        var contentStyle = new GUIStyle(GUI.skin.label);
                        contentStyle.fontSize = 12;
                        contentStyle.alignment = TextAnchor.UpperLeft;
                        contentStyle.padding = new RectOffset(4, 0, 0, 0);
                        GUILayout.Label("Sign in to continue", contentStyle, GUILayout.Width(280), GUILayout.Height(16));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(44);
                    GUILayout.BeginHorizontal();
                    {
                        // User text field
                        var focus = "user";
                        GUI.SetNextControlName(focus);

                        var userStyle = new GUIStyle(GUI.skin.textField);
                        userStyle.fontSize = 12;
                        userStyle.alignment = TextAnchor.MiddleLeft;
                        userStyle.padding = new RectOffset(12, 0, 0, 0);
                        user = GUILayout.TextField(user, userStyle, GUILayout.Width(280), GUILayout.Height(32));

                        if (GUI.GetNameOfFocusedControl().Equals(focus))
                        {
                            if (user.Equals("User"))
                            {
                                user = "";
                            }
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(4);
                    GUILayout.BeginHorizontal();
                    {
                        // Password text field
                        var focus = "password";
                        GUI.SetNextControlName(focus);

                        var passwordStyle = new GUIStyle(GUI.skin.textField);
                        passwordStyle.fontSize = 12;
                        passwordStyle.alignment = TextAnchor.MiddleLeft;
                        passwordStyle.padding = new RectOffset(12, 0, 0, 0);
                        password = GUILayout.TextField(password, passwordStyle, GUILayout.Width(280), GUILayout.Height(32));

                        if (GUI.GetNameOfFocusedControl().Equals(focus))
                        {
                            if (password.Equals("Password"))
                            {
                                password = "";
                                security = "";
                            }

                            if (password.Length != security.Length)
                            {
                                if (password.Length > security.Length)
                                {
                                    string str = password.Substring(password.Length - 1);
                                    security += str;
                                }
                                else if (password.Length == security.Length)
                                {
                                    return;
                                }
                                else
                                {
                                    string str = security.Substring(0, security.Length - 1);
                                    security = str;
                                }

                                password = "";
                                for (int i = 0; i < security.Length; i++)
                                {
                                    password += "*";
                                }
                            }
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        // Option
                        // Remember toggle
                        var rememberStyle = new GUIStyle(GUI.skin.toggle);
                        rememberStyle.fontSize = 8;
                        rememberStyle.fontStyle = FontStyle.Bold;
                        rememberStyle.alignment = TextAnchor.MiddleLeft;
                        rememberStyle.padding = new RectOffset(20, 0, 0, 2);

                        remember = GUILayout.Toggle(remember, "Remember Me?", rememberStyle, GUILayout.Width(280), GUILayout.Height(24));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(16);
                    GUILayout.BeginHorizontal();
                    {
                        // Login button
                        var loginStyle = new GUIStyle(GUI.skin.button);
                        loginStyle.fontSize = 12;
                        loginStyle.fontStyle = FontStyle.Bold;
                        loginStyle.alignment = TextAnchor.MiddleCenter;

                        if (GUILayout.Button("Login", loginStyle, GUILayout.Width(280), GUILayout.Height(32)))
                        {                            
                            Login();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.FlexibleSpace();
                    GUILayout.FlexibleSpace();
                    GUILayout.FlexibleSpace();
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }

        #endregion
    }

    public class OrbisEditorAccountPopup : PopupWindowContent
    {
        public override void OnGUI(Rect rect)
        {
            GUILayout.BeginArea(new Rect(5, 5, 200, 340));
            {
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(200), GUILayout.MaxHeight(340));
                {
                    GUILayout.Label("Test");
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();            
        }

        public override Vector2 GetWindowSize()
        {
            return base.GetWindowSize();
        }
    }

    public class LayerWindowSavePopup : PopupWindowContent
    {
        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("!Do you sure you want to clear it?");
            GUILayout.BeginArea(new Rect(116, 25, 40, 20));

            GUIStyle no = new GUIStyle(GUI.skin.button);
            no.fontStyle = FontStyle.Bold;

            if (GUILayout.Button("No", no))
            {
                editorWindow.Close();
            }

            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(158, 25, 40, 20));

            GUIStyle yes = new GUIStyle(GUI.skin.button);
            yes.normal.textColor = new Color(151 / 255f, 157 / 255f, 242 / 255f);
            yes.fontStyle = FontStyle.Bold;
            yes.alignment = TextAnchor.MiddleCenter;

            if (GUILayout.Button("Yes", yes))
            {                
                Debug.Log("Save user layers complete");
                editorWindow.Close();
            }
            GUILayout.EndArea();
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(200, 50);
        }
    }
}

#endif