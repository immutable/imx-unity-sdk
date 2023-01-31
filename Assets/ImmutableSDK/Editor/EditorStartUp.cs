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
            var data = EditorPrefs.GetString("VSAttributionRegistration");
            VSAttributionRegistration.VSAttributionRegistrationState vsRegState = new();
            JsonUtility.FromJsonOverwrite(data, vsRegState);
            // If the form has not been submitted, initialize and pop up the editor window
            if (!vsRegState.submitted)
            {
                // Check if custom editor window is already open and act accordingly
                if (!vsRegState.isOpen)
                {
                    VSAttributionRegistration.Initialize();
                }
                else
                {
                    VSAttributionRegistration.FocusWindowIfItsOpen<VSAttributionRegistration>();
                }
            }
        }
    }
}