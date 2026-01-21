# 技術スタック

## アーキテクチャ

3層構成のモノレポ:
- **クライアント**: Unity 2Dゲーム (C#)
- **バックエンド**: Go サーバーレスAPI (AWS Lambda)
- **インフラ**: AWSサービス (API Gateway, DynamoDB)

## コア技術

### Unity クライアント
- **エンジン**: Unity (2D Built-in Render Pipeline)
- **言語**: C#
- **ターゲット**: モバイル/WebGL (9:16 縦画面)

### バックエンド
- **言語**: Go 1.21+
- **フレームワーク**: Gin (HTTPルーター)
- **ランタイム**: AWS Lambda

### インフラ
- **API**: AWS API Gateway
- **データベース**: AWS DynamoDB
- **コンピュート**: AWS Lambda

## 主要ライブラリ

### Go バックエンド
- `gin-gonic/gin`: HTTPルーティングとミドルウェア
- `aws-lambda-go`: Lambdaハンドラー統合
- `aws-sdk-go-v2`: DynamoDBクライアント

### Unity
- 標準Unity 2Dコンポーネント (Rigidbody2D, Collider2D)
- UnityEngine.Networking (API通信用)

## 開発標準

### C# (Unity)
- クラス、メソッド、プロパティは PascalCase
- Inspector公開のprivateフィールドには `[SerializeField]`
- 公開APIにはXMLドキュメントコメント

### Go
- 標準的なGoの規約に従う
- JSON/DynamoDBマッピング用の構造体タグ
- APIモデルはRequest/Responseパターン

## 主要な技術的決定

- **Singletonパターン**: GameManager, ScoreManagerでグローバルアクセス
- **コンポーネントベース**: Unity MonoBehaviour構成
- **サーバーレス**: コスト効率の良いスケーリングのためLambdaを採用
- **DynamoDB**: シンプルなスコアデータ用NoSQL、ランキングクエリ用GSI

---
_標準とパターンを文書化し、すべての依存関係は列挙しない_
