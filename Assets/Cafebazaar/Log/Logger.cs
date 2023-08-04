using UnityEngine;

namespace Cafebazaar.Log
{
    public static class Logger
    {
        public static void LogError(string message)
        {
            Debug.LogError($"ERROR | {message}");
        }

        public static void LogInfo(string message)
        {
            Debug.Log($"INFO | {message}");
        }
    }
}