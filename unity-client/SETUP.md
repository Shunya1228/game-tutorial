# Unity シューティングゲーム セットアップ手順

## 1. Unity プロジェクト作成

1. Unity Hub を開く
2. 「New Project」をクリック
3. 以下の設定で作成:
   - **Template**: 2D (Built-in Render Pipeline)
   - **Project Name**: SpaceShooter
   - **Location**: `game-tutorial/unity-client/`

## 2. プロジェクト初期設定

### 2.1 画面サイズ設定
1. Game ビューの「Free Aspect」をクリック
2. 「+」で新規追加: **9:16** (縦画面、スマホ向け)

### 2.2 フォルダ構成作成
Assets フォルダ内に以下を作成:
```
Assets/
├── Scripts/       # C#スクリプト
├── Prefabs/       # プレハブ
├── Sprites/       # 画像素材
└── Scenes/        # シーン
```

## 3. スクリプトの配置

`Scripts/` フォルダに以下のファイルをコピー:
- `PlayerController.cs` - プレイヤー操作
- `Bullet.cs` - 弾の挙動
- `Enemy.cs` - 敵の挙動
- `EnemySpawner.cs` - 敵の生成
- `GameManager.cs` - ゲーム全体の管理
- `ScoreManager.cs` - スコア管理
- `UIManager.cs` - UI制御
- `RankingAPI.cs` - バックエンドAPI連携

## 4. シーン構築手順

### 4.1 プレイヤー作成
1. Hierarchy で右クリック → 2D Object → Sprites → Square
2. 名前を「Player」に変更
3. Transform:
   - Position: (0, -4, 0)
   - Scale: (0.5, 0.5, 1)
4. Add Component:
   - `PlayerController` スクリプト
   - `Rigidbody2D` (Gravity Scale: 0)
   - `Box Collider 2D` (Is Trigger: ON)
5. Tag を「Player」に設定

### 4.2 弾のプレハブ作成
1. Hierarchy で Square を作成、名前を「Bullet」
2. Transform:
   - Scale: (0.1, 0.3, 1)
3. Add Component:
   - `Bullet` スクリプト
   - `Rigidbody2D` (Gravity Scale: 0)
   - `Box Collider 2D` (Is Trigger: ON)
4. Tag を「Bullet」に設定
5. Prefabs フォルダにドラッグしてプレハブ化
6. Hierarchy から削除

### 4.3 敵のプレハブ作成
1. Hierarchy で Square を作成、名前を「Enemy」
2. Transform:
   - Scale: (0.6, 0.6, 1)
3. 色を赤に変更 (Sprite Renderer → Color)
4. Add Component:
   - `Enemy` スクリプト
   - `Rigidbody2D` (Gravity Scale: 0)
   - `Box Collider 2D` (Is Trigger: ON)
5. Tag を「Enemy」に設定
6. Prefabs フォルダにドラッグしてプレハブ化
7. Hierarchy から削除

### 4.4 GameManager 作成
1. Hierarchy で Create Empty、名前を「GameManager」
2. Add Component:
   - `GameManager` スクリプト
   - `ScoreManager` スクリプト
   - `EnemySpawner` スクリプト

### 4.5 UI 作成
1. Hierarchy で右クリック → UI → Canvas
2. Canvas 内に以下を作成:
   - UI → Text - TextMeshPro: 「ScoreText」(左上に配置)
   - UI → Text - TextMeshPro: 「GameOverText」(中央、非アクティブに)
   - UI → Button - TextMeshPro: 「RetryButton」(中央下、非アクティブに)
3. 「UIManager」オブジェクトを作成し、UIManager スクリプトをアタッチ

### 4.6 参照の設定
Inspector で各コンポーネントの参照を設定:

**PlayerController:**
- Bullet Prefab: Bullet プレハブ

**EnemySpawner:**
- Enemy Prefab: Enemy プレハブ

**UIManager:**
- Score Text: ScoreText
- Game Over Text: GameOverText
- Retry Button: RetryButton

## 5. タグの作成
Edit → Project Settings → Tags and Layers で以下を追加:
- Player
- Bullet
- Enemy

## 6. ビルド設定
File → Build Settings:
- Platform: PC/WebGL/モバイル（お好みで）

## 動作確認
Play ボタンを押して:
1. 矢印キーでプレイヤーが動く
2. スペースキーで弾が出る
3. 敵が上から降ってくる
4. 敵に弾が当たるとスコア加算
5. 敵がプレイヤーに当たるとゲームオーバー
