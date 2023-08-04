using System;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using UnityEngine.Networking;

namespace Cafebazaar
{
    using Communicators;
    using Data;
    using Log;

    public static class MiniGame
    {
        private const string SCORE_URL = "https://minigames-api.cafebazaar.org/score";
        private static GameData gameData;

        #if UNITY_WEBGL && !UNITY_EDITOR

        public static void Initialize()
        {
            gameData = GetGameDataFromUrl();

            if (!IsGameDataFetched())
                Logger.LogError("Error in fetching process => The URL parameters are not correct.");

            GameData GetGameDataFromUrl()
            {
                Uri uri = new Uri(JavascriptCommunicator.GetUrl());
                NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

                return new GameData(
                    uid: query["uid"] ?? null,
                    slug: query["gid"] ?? null
                );
            }
        }

        public static IEnumerator SendScore(int score, Action onSuccess, Action onFail)
        {
            if (!IsGameDataFetched())
            {
                Logger.LogError("Error in fetching process => The URL parameters are not correct.");
                yield break;
            }

            UnityWebRequest request = new UnityWebRequest(url: SCORE_URL, method: "POST");
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(
                data: Encoding.UTF8.GetBytes(
                    "{\"game_slug\":\"" + gameData.Slug + 
                    "\", \"uid\":\"" + gameData.Uid + 
                    "\", \"score:\":" + score + 
                    "}"
                )
            );
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Logger.LogError($"Error in sending data to the server => {request.error}");
                onFail.Invoke();
            }
            else
            {
                onSuccess.Invoke();
            }
        }

        private static bool IsGameDataFetched() => gameData != null && gameData.Uid != null && gameData.Slug != null;

        #else

        public static void Initialize()
        {
            Logger.LogInfo("Initializing in the Editor mode.");
        }

        public static IEnumerator SendScore(int score, Action onSuccess, Action onFail)
        {
            Logger.LogInfo($"Sending score ({score}) in the Editor mode.");
            onSuccess.Invoke();
            yield return null;
        }

        #endif
    }
}