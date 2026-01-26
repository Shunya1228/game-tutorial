using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI表示を管理するシングルトンクラス
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI要素")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button retryButton;

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

    void Start()
    {
        // スコア変更イベントを購読
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
        }

        // リトライボタンのイベント設定
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }

        // 初期状態
        UpdateScoreText(0);
        HideGameOver();
    }

    void OnDestroy()
    {
        // イベント購読解除
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
        }
    }

    /// <summary>
    /// スコア表示を更新する
    /// </summary>
    private void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    /// <summary>
    /// ゲームオーバー画面を表示する
    /// </summary>
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null && ScoreManager.Instance != null)
        {
            finalScoreText.text = $"Final Score: {ScoreManager.Instance.CurrentScore}";
        }
    }

    /// <summary>
    /// ゲームオーバー画面を非表示にする
    /// </summary>
    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// リトライボタンが押された時の処理
    /// </summary>
    private void OnRetryButtonClicked()
    {
        GameManager.Instance?.Retry();
    }
}
