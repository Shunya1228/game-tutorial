# プロジェクト構成

## 構成方針

レイヤー分離型モノレポ: クライアント、バックエンド、インフラは明確な境界を持ち独立。各レイヤーは個別にデプロイ可能。

## ディレクトリパターン

### Unity クライアント
**場所**: `/unity-client/`
**目的**: ゲームクライアントのソースとUnityプロジェクトファイル
**例**: `Scripts/PlayerController.cs`, `Prefabs/Bullet.prefab`

### バックエンドAPI
**場所**: `/backend/`
**目的**: ランキングシステム用Goサーバーレス API
**例**: `handlers/score.go`, `models/score.go`

### インフラ
**場所**: `/infrastructure/`
**目的**: AWSリソース定義 (Terraform/CloudFormation)
**例**: API Gateway、Lambda、DynamoDB設定

## 命名規則

### Unity (C#)
- **スクリプト**: PascalCase (`PlayerController.cs`, `EnemySpawner.cs`)
- **クラス**: PascalCase、名詞ベース (`Bullet`, `Enemy`)
- **メソッド**: PascalCase、動詞ベース (`HandleMovement`, `TakeDamage`)
- **privateフィールド**: camelCase、プレフィックスなし (`moveSpeed`, `bulletPrefab`)

### Go
- **ファイル**: snake_case (`score.go`, `score_handler.go`)
- **パッケージ**: 小文字単語 (`models`, `handlers`)
- **構造体**: PascalCase (`Score`, `ScoreRequest`)
- **JSONフィールド**: snake_case (`player_name`, `created_at`)

## Unity スクリプトパターン

```csharp
// 標準的なMonoBehaviourパターン
public class ComponentName : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private float setting = 1f;

    void Update() { /* フレーム毎の処理 */ }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TagName")) { /* 衝突処理 */ }
    }
}
```

## Go API パターン

```go
// JSONとDynamoDBタグ付きモデル
type Model struct {
    Field string `json:"field" dynamodbav:"field"`
}

// Request/Response分離
type ModelRequest struct { /* 入力検証 */ }
type ModelResponse struct { /* 出力フォーマット */ }
```

## コード構成原則

- **Unity**: 1ファイル1MonoBehaviour、クラス名と同じファイル名
- **Go**: 責務でグループ化 (models, handlers, middleware)
- **Prefabs**: 再利用可能なGameObjectは `/Prefabs/` フォルダに配置
- **Tags**: 衝突判定にUnityタグを使用 (Player, Bullet, Enemy)

---
_パターンを文書化し、ファイルツリーは列挙しない。パターンに従う新規ファイルは更新不要_
