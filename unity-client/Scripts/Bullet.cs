using UnityEngine;

/// <summary>
/// 弾の挙動を制御するクラス
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("弾の設定")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    void Start()
    {
        // 一定時間後に自動で削除
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 上方向に移動
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    /// <summary>
    /// 敵との衝突判定
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 敵を破壊してスコア加算
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }

            // 弾を削除
            Destroy(gameObject);
        }
    }
}
