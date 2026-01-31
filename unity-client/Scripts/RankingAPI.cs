using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// バックエンドAPIと通信するクラス
/// </summary>
public class RankingAPI : MonoBehaviour
{
    public static RankingAPI Instance { get; private set; }

    [Header("API設定")]
    [SerializeField] private string apiBaseUrl = "https://your-api-gateway-url.amazonaws.com";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// スコアを送信する
    /// </summary>
    public void SubmitScore(string playerName, int score, Action<bool> onComplete)
    {
        StartCoroutine(SubmitScoreCoroutine(playerName, score, onComplete));
    }

    /// <summary>
    /// ランキングを取得する
    /// </summary>
    public void GetRanking(int limit, Action<List<RankingEntry>> onComplete)
    {
        StartCoroutine(GetRankingCoroutine(limit, onComplete));
    }

    private IEnumerator SubmitScoreCoroutine(string playerName, int score, Action<bool> onComplete)
    {
        var request = new ScoreSubmitRequest
        {
            player_name = playerName,
            score = score
        };
        string jsonData = JsonUtility.ToJson(request);

        using (UnityWebRequest www = new UnityWebRequest($"{apiBaseUrl}/scores", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            bool success = www.result == UnityWebRequest.Result.Success;
            if (!success)
            {
                Debug.LogError($"スコア送信エラー: {www.error}");
            }
            onComplete?.Invoke(success);
        }
    }

    private IEnumerator GetRankingCoroutine(int limit, Action<List<RankingEntry>> onComplete)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{apiBaseUrl}/ranking?limit={limit}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<RankingResponse>($"{{\"entries\":{www.downloadHandler.text}}}");
                onComplete?.Invoke(response.entries ?? new List<RankingEntry>());
            }
            else
            {
                Debug.LogError($"ランキング取得エラー: {www.error}");
                onComplete?.Invoke(new List<RankingEntry>());
            }
        }
    }

    /// <summary>
    /// スコア送信リクエスト
    /// </summary>
    [Serializable]
    private class ScoreSubmitRequest
    {
        public string player_name;
        public int score;
    }

    /// <summary>
    /// ランキングレスポンス
    /// </summary>
    [Serializable]
    private class RankingResponse
    {
        public List<RankingEntry> entries;
    }
}

/// <summary>
/// ランキングエントリ
/// </summary>
[Serializable]
public class RankingEntry
{
    public int rank;
    public string player_name;
    public int score;
    public string created_at;
}
