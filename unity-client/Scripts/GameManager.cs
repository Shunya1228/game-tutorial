using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を管理するシングルトンクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("参照")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerController player;

    private bool isGameOver;

    /// <summary>
    /// ゲームオーバー状態かどうか
    /// </summary>
    public bool IsGameOver => isGameOver;

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
        StartGame();
    }

    /// <summary>
    /// ゲームを開始する
    /// </summary>
    public void StartGame()
    {
        isGameOver = false;
        ScoreManager.Instance?.ResetScore();
        enemySpawner?.StartSpawning();
        player?.SetCanControl(true);
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        enemySpawner?.StopSpawning();
        player?.SetCanControl(false);

        // 画面上の敵を全て削除
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        // UIManagerにゲームオーバーを通知
        UIManager.Instance?.ShowGameOver();
    }

    /// <summary>
    /// ゲームをリトライする
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
