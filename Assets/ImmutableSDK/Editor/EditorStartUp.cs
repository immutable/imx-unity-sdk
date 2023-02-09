using UnityEditor;
using UnityEngine;

namespace ImmutableSDK.Editor
{
    [InitializeOnLoad]
    public class Startup
    {
        static Startup()
        {
            // Load the VSAttributionRegistration form state from EditorPrefs
            var data = EditorPrefs.GetString(VSAttributionRegistration.EDITOR_PREFS_KEY);
            VSAttributionRegistration.VSAttributionRegistrationState vsRegState = new();
            JsonUtility.FromJsonOverwrite(data, vsRegState);
            // If the form has not been submitted, initialize and pop up the editor window if not already open
            if (!vsRegState.submitted && !EditorWindow.HasOpenInstances<VSAttributionRegistration>())
            {
                VSAttributionRegistration.Initialize();
            }
        }
    }
}