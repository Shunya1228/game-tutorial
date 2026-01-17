using UnityEngine;

/// <summary>
/// プレイヤー（自機）の操作を制御するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("射撃設定")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.2f;

    [Header("移動制限")]
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;
    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

    private float nextFireTime;
    private bool canControl = true;

    void Update()
    {
        if (!canControl) return;

        HandleMovement();
        HandleShooting();
    }

    /// <summary>
    /// キーボード入力による移動処理
    /// </summary>
    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // 画面外に出ないように制限
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    /// <summary>
    /// スペースキーによる射撃処理
    /// </summary>
    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    /// <summary>
    /// 弾を発射する
    /// </summary>
    private void Fire()
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// プレイヤーの操作を有効/無効にする
    /// </summary>
    public void SetCanControl(bool value)
    {
        canControl = value;
    }

    /// <summary>
    /// 敵との衝突判定
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // ゲームオーバー処理を呼び出す
            GameManager.Instance?.GameOver();
            gameObject.SetActive(false);
        }
    }
}
