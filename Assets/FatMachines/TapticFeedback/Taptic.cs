using System.Runtime.InteropServices;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Taptic : MonoBehaviour {


        public static bool tapticOn = true;

        public static void Warning() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.Warning);
#endif
        }
        public static void Failure() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.Failure);
#endif
        }
        public static void Success() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.Success);
#endif
        }
        public static void Light() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.LightImpact);
#endif
        }
        public static void Medium() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.MediumImpact);
#endif
        }
        public static void Heavy() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.HeavyImpact);
#endif
        }
        public static void Default() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                Handheld.Vibrate();
#endif
        }
        public static void Vibrate() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Vibrate();
#endif
        }
        public static void Selection() {
                if (!tapticOn || Application.isEditor) {
                        return;
                }
#if UNITY_ANDROID
                AndroidTaptic.Haptic(HapticTypes.Selection);
#endif
        }
        
}