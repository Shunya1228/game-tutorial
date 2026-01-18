using UnityEngine;

/// <summary>
/// 敵の挙動を制御するクラス
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("敵の設定")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private int scoreValue = 100;

    void Update()
    {
        // 下方向に移動
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // 画面外に出たら削除
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 弾に当たった時の処理
    /// </summary>
    public void TakeDamage()
    {
        // スコアを加算
        ScoreManager.Instance?.AddScore(scoreValue);

        // 敵を削除
        Destroy(gameObject);
    }
}
