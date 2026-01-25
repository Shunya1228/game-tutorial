using UnityEngine;
using System;

/// <summary>
/// スコアを管理するシングルトンクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    /// <summary>
    /// スコアが変更された時に発火するイベント
    /// </summary>
    public event Action<int> OnScoreChanged;

    private int currentScore;

    /// <summary>
    /// 現在のスコア
    /// </summary>
    public int CurrentScore => currentScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreChanged?.Invoke(currentScore);
    }

    /// <summary>
    /// スコアをリセットする
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }
}
