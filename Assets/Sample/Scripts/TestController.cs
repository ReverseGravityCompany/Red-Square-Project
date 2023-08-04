using UnityEngine;
using UnityEngine.UI;

using Cafebazaar;

public class TestController : MonoBehaviour
{
    [SerializeField] private InputField inputScore;

    private void Start()
    {
        MiniGame.Initialize();
    }

    public void SendScoreToServer()
    {
        StartCoroutine(
            MiniGame.SendScore(
                score: int.Parse(inputScore.text), 
                onSuccess: OnSuccess,
                onFail: OnFail
            )
        );
    }

    private void OnSuccess()
    {
        Debug.Log("Request succeeded.");
    }

    private void OnFail()
    {
        Debug.Log("Request failed.");
    }
}
