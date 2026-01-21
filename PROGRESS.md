# プロジェクト進捗状況

## Unity スクリプト

- [x] PlayerController.cs - プレイヤー操作・射撃
- [x] Bullet.cs - 弾の移動・敵への当たり判定
- [x] Enemy.cs - 敵の移動・ダメージ処理
- [ ] EnemySpawner.cs - 敵を定期的に生成
- [ ] ScoreManager.cs - スコア管理
- [ ] GameManager.cs - ゲーム全体の管理
- [ ] UIManager.cs - UI 表示
- [ ] RankingAPI.cs - バックエンド連携

## バックエンド (Go)

- [x] models/score.go - データモデル定義
- [ ] handlers/score.go - API ハンドラー
- [ ] main.go - エントリーポイント

## インフラ (AWS)

- [ ] API Gateway 設定
- [ ] Lambda 関数デプロイ
- [ ] DynamoDB テーブル作成

## Unity セットアップ

- [ ] Unity プロジェクト作成
- [ ] フォルダ構成作成
- [ ] プレイヤー GameObject 作成
- [ ] 弾のプレハブ作成
- [ ] 敵のプレハブ作成
- [ ] UI 作成
- [ ] タグ設定 (Player, Bullet, Enemy)
