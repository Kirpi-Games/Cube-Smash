using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

namespace Aviator.Integrations.Internal
{
    internal class AviatorBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            #if UNITY_ANDROID

                InitFacebook();
                
                InitAnalytics();
            #endif
            
        }

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                #if UNITY_ANDROID
                    FB.Mobile.SetAdvertiserIDCollectionEnabled(true);
                #endif
                FB.Mobile.SetAutoLogAppEventsEnabled(true);
                
                // Signal an app activation App Event
                FB.ActivateApp();
                // Continue with Facebook SDK
                // ...
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }
        
        private void InitAnalytics()
        {
            GameAnalytics.Initialize();
        }
        private void InitFacebook()
        {
            if (!FB.IsInitialized) FB.Init(InitCallback, OnHideUnity);
            
            else InitCallback();         
        }
    }
}