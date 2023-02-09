#if UNITY_EDITOR

using CodiceApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.VersionControl;
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

        // Click check
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
                Debug.Log("Account");
                Account();
            }
            else
            {
                Debug.Log("DashBoard");
                DashBoard();
            }            
        }
        
        public static void Account()
        {
            //Resolution[] resolutions = Screen.resolutions;                        
            //foreach(var screen in resolutions)
            //{
            //    Debug.Log(screen.width + "x" + screen.height);
            //}

            var window = EditorWindow.GetWindow<OrbisEditorAccountWindow>();
            var x = (Screen.currentResolution.width - window.Area().x) * 0.5f;
            var y = (Screen.currentResolution.height - window.Area().y) * 0.5f;

            window.position = new Rect(x, y, window.Area().x, window.Area().y);
            window.titleContent = new GUIContent("[Orbis] Account");
            window.minSize = window.maxSize = new Vector2(window.Area().x, window.Area().y);
            window.Show();                                    
        }
        
        public static void DashBoard()
        {
            var window = CreateInstance<OrbisEditorDashBoardWindow>();
            window.titleContent = new GUIContent("[Orbis] DashBoard");
            window.Show();
        }

        #endregion
    }

    public class OrbisEditorDashBoardWindow : EditorWindow
    {
        #region UnityVariable

        public Texture2D category;

        #endregion



        #region UnityReturn

        public Vector2 Area()
        {
            var window = GetWindow<OrbisEditorDashBoardWindow>();
            return new Vector2(window.maxSize.x, window.maxSize.y);
        }

        public Vector2 Margin()
        {
            return new Vector2(5, 5);
        }

        #endregion



        #region UnityMethod

        private void OnGUI()
        {            
            GUILayout.BeginArea(new Rect(0, 0, 4000, 4000));
            {
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = MakeBackgroundTexture(2, 2, OrbisColor.editor);

                GUILayout.BeginHorizontal("box", boxStyle, GUILayout.MaxWidth(4000), GUILayout.MaxHeight(40));
                {
                    GUIStyle categoryStyle = new GUIStyle(GUI.skin.button);
                    categoryStyle.normal.background = MakeBackgroundTexture(2, 2, OrbisColor.editor);

                    //if (category == null)
                    //{
                    //    category = EditorGUIUtility.Load("Assets/Orbis/Editor/Src/Icon/category.png") as Texture2D;
                    //}

                    if (GUILayout.Button("+", categoryStyle, GUILayout.Width(60), GUILayout.Height(40)))
                    {
                        Debug.Log("Button");
                    }
                }                
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        private Texture2D MakeBackgroundTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D backgroundTexture = new Texture2D(width, height);

            backgroundTexture.SetPixels(pixels);
            backgroundTexture.Apply();

            return backgroundTexture;
        }

        #endregion
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

        #endregion;



        #region UnityReturn

        public Vector2 Area()
        {
            return new Vector2(280, 420);
        }

        public Vector2 Field()
        {
            return new Vector2(240, 4);
        }

        public Vector2 Margin()
        {
            return new Vector2(20, 20);
        }

        #endregion



        #region UnityMethod

        public void Login()
        {
            if (OrbisEditor.IsClick())
                return;

            if (string.IsNullOrEmpty(user) || user.Equals("User"))
            {
                Popup("Please enter your ID!");
            }

            if (string.IsNullOrEmpty(security) || password.Equals("Password"))
            {
                Popup("Please enter your Password!");
            }

            if (OrbisEditor.data.account.Find(x => x.user == user.Trim()) != null)
            {
                OrbisAccount account = OrbisEditor.data.account.Find(x => x.user == user.Trim());
                if (account.password.Equals(security))
                {
                    account.connect = OrbisAccountConnect.Connect;
                    if (remember)
                    {
                        account.remember = OrbisAccountRemember.Remember;
                    }

                    EditorUtility.SetDirty(OrbisEditor.data);
                    Close();


                    // Login success open manager
                    OrbisEditor.Manager();
                }
                else
                {
                    Popup("Accounts do not match!");
                }
            }
            else
            {
                Popup("Account does not exist!");
            }
        }

        public void Popup(string message)
        {
            var popup = new OrbisEditorAccountPopup();
            popup.message = message;
            PopupWindow.Show(new Rect(new Vector2(-10, -50), popup.GetWindowSize()), popup);
        }

        #endregion



        #region UnityLifecycle

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


            GUILayout.BeginArea(new Rect(Margin(), Area()));
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
                        GUILayout.Label("Welcome!", titleStyle, GUILayout.Width(Field().x), GUILayout.Height(Field().y * 8f));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        var contentStyle = new GUIStyle(GUI.skin.label);
                        contentStyle.fontSize = 12;
                        contentStyle.alignment = TextAnchor.UpperLeft;
                        contentStyle.padding = new RectOffset(4, 0, 0, 0);
                        GUILayout.Label("Sign in to continue", contentStyle, GUILayout.Width(Field().x), GUILayout.Height(Field().y * 4f));
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
                        user = GUILayout.TextField(user, userStyle, GUILayout.Width(Field().x), GUILayout.Height(Field().y * 8f));

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
                        password = GUILayout.TextField(password, passwordStyle, GUILayout.Width(Field().x), GUILayout.Height(Field().y * 8f));

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

                        remember = GUILayout.Toggle(remember, "Remember Me?", rememberStyle, GUILayout.Width(Field().x), GUILayout.Height(Field().y * 6f));
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

                        if (GUILayout.Button("Login", loginStyle, GUILayout.Width(Field().x), GUILayout.Height(Field().y * 8f)))
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
        public string message = "";  
        
        public override void OnGUI(Rect rect)
        {
            GUILayout.BeginArea(new Rect(new Vector2(0, 0), GetWindowSize()));
            {
                GUILayout.BeginVertical("box", GUILayout.MaxWidth(GetWindowSize().x), GUILayout.MaxHeight(GetWindowSize().y));
                {                    
                    GUILayout.FlexibleSpace();

                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.fontSize = 12;
                    style.fontStyle = FontStyle.Bold;
                    style.alignment = TextAnchor.MiddleCenter;
                    style.padding = new RectOffset(0, 0, 0, 2);
                    GUILayout.Label(message, style);

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndVertical();                
            }
            GUILayout.EndArea();            
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(260, 40);
        }
    }
}

#endif