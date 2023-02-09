using System;
using System.Net.Mail;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

namespace ImmutableSDK.Editor
{
    public class VSAttributionRegistration : EditorWindow
    {
        private static readonly int MAX_HEIGHT = 460;
        private static readonly int MAX_WIDTH = 500;
        private static readonly Vector2 s_WindowSize = new(MAX_WIDTH, MAX_HEIGHT);
        private readonly string RELATIVE_EDITOR_DIR = "Assets/ImmutableSDK/Editor/";
        private readonly string PACKAGED_RELATIVE_EDITOR_DIR = "Editor/";
        private readonly string IMX_LOGO_FILENAME = "ImmutableXLogo.png";
        private static readonly string PRODUCT_GUID = PlayerSettings.productGUID != null ? PlayerSettings.productGUID.ToString() : "";
        private static readonly string PRODUCT_VERSION = "0.1.2";
        public static readonly string EDITOR_PREFS_KEY = "ImxVSRegAttr-" + PRODUCT_VERSION + "-" + PRODUCT_GUID;

        private readonly VSAttributionRegistrationState vsRegState = new();

        protected void OnEnable()
        {
            // Retrieve existing data if already registered to prevent sending multiple events
            
            LoadVSRegState();
        }

        protected void OnDestroy()
        {
            // Save entered data on exit
            SaveVSRegState();
        }

        public void OnGUI()
        {
            // Simple custom EditorWindow for users to register with VS based on the Unity VSAttribution example
            var banner = (Texture)AssetDatabase.LoadAssetAtPath(RELATIVE_EDITOR_DIR + "Art/" + IMX_LOGO_FILENAME, typeof(Texture));
            // Fallback for package .tar.gz installations
            if (banner == null) banner = (Texture)AssetDatabase.LoadAssetAtPath(PACKAGED_RELATIVE_EDITOR_DIR + "Art/" + IMX_LOGO_FILENAME, typeof(Texture));

            GUILayout.Box(banner, GUILayout.MaxHeight(MAX_HEIGHT), GUILayout.MaxWidth(MAX_WIDTH));

            DrawLine(Color.gray);

            DrawHelpBox("<b><size=12>Welcome to the Immutable Unity SDK!</size></b>\n\n" +
                        "<size=11>This process manually registers your SDK instance with the Unity Verified Solutions team via " +
                        " the Unity Editor Analytics API. It's highly recommended in case you require any support.</size>\n\n" +
                        "<b><size=12>Get Started:</size></b>\n\n" +
                        "<size=11>1. Register with your email address at the <a href=\"https://hub.immutable.com/\">Immutable Developer Hub</a>" +
                        " to access the ability to create projects on Immutable via the SDKs, Public API or the CLI.\n" +
                        "2. Enter the registered email address in the relevant field below and click Register.\n\n" +
                        "More information on using the Immutable Unity SDK and APIs can be found at the <a href=\"https://docs.x.immutable.com/\">" +
                        "Immutable Docs</a>.</size>", 14);

            DrawLine(Color.gray);

            // VS Attribution API
            GUILayout.Label("Immutable Unity SDK Registration", EditorStyles.boldLabel);

            vsRegState.customerUid = EditorGUILayout.TextField("Registered Email:", vsRegState.customerUid);

            GUILayout.Space(20f);

            if (GUILayout.Button("Register"))
            {
                if (vsRegState.customerUid == null || vsRegState.customerUid.Length == 0 ||
                    !IsValidEmail(vsRegState.customerUid))
                {
                    EditorUtility.DisplayDialog("Validation Error!", "Please enter a valid email address!", "OK");
                    return;
                }

                if (vsRegState.submitted)
                {
                    EditorUtility.DisplayDialog("Already registered!",
                        "This instance of the Immutable Unity SDK has already been registered!", "Close");
                    GetWindow<VSAttributionRegistration>().Close(); // this will trigger OnDestroy()
                    return;
                }

                var result = VSAttribution.SendAttributionEvent("ImmutableUnitySDKRegistration", "Immutable",
                    vsRegState.customerUid);
                Debug.Log("[Immutable Unity SDK] Registration Successful!");
                if (result == AnalyticsResult.Ok)
                {
                    vsRegState.submitted = true;
                    GetWindow<VSAttributionRegistration>().Close(); // this will trigger OnDestroy()
                }
            }
        }

        /// <summary>
        ///     Saves the current state of VSAttributionRegistrationState to EditorPrefs
        /// </summary>
        private void SaveVSRegState()
        {
            EditorPrefs.SetString(EDITOR_PREFS_KEY, JsonUtility.ToJson(vsRegState, false));
        }

        /// <summary>
        ///     Load the saved state of VSAttributionRegistrationState from EditorPrefs
        /// </summary>
        private void LoadVSRegState()
        {
            var data = EditorPrefs.GetString(EDITOR_PREFS_KEY, JsonUtility.ToJson(vsRegState, false));
            JsonUtility.FromJsonOverwrite(data, vsRegState);
        }

        [MenuItem("Immutable/Immutable Unity SDK Registration")]
        public static void Initialize()
        {
            var window = GetWindow<VSAttributionRegistration>();

            window.titleContent = new GUIContent("Immutable Unity SDK Registration");
            window.minSize = s_WindowSize;
            window.maxSize = s_WindowSize;
        }

        [Serializable]
        public class VSAttributionRegistrationState
        {
            [SerializeField] public string customerUid;
            [SerializeField] public bool submitted;
        }

        #region Utilities

        private void DrawHelpBox(string text, int linesCount)
        {
            var helpBox = new GUIStyle(EditorStyles.helpBox) { richText = true };
            EditorGUILayout.SelectableLabel(text, helpBox, GUILayout.Height(linesCount * 14f));
        }

        private void DrawLine(Color color, int thickness = 2, int padding = 7)
        {
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding * 2 + thickness));
            r.height = thickness;
            r.y += padding;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        /// <summary>
        ///     Basic email validation function using System.Net.Mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool IsValidEmail(string email)
        {
            var valid = true;
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        #endregion
    }
}