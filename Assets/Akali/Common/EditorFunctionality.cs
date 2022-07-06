using UnityEditor;
using UnityEngine;

namespace Akali.Common
{
    public static class EditorFunctionality
    {   
#if UNITY_EDITOR
        private static int _screenshotIndex;
        
        [MenuItem(App.AppName + "/Functionality/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debugger.Log("Player Prefs cleared.");
        }
#endif
    }
}