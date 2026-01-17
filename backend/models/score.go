package models

import "time"

// Score はランキングに保存されるスコアデータを表します
type Score struct {
	ID         string    `json:"id" dynamodbav:"id"`
	PlayerName string    `json:"player_name" dynamodbav:"player_name"`
	Score      int       `json:"score" dynamodbav:"score"`
	CreatedAt  time.Time `json:"created_at" dynamodbav:"created_at"`
}

// ScoreRequest はスコア登録リクエストの形式です
type ScoreRequest struct {
	PlayerName string `json:"player_name" binding:"required,min=1,max=20"`
	Score      int    `json:"score" binding:"required,min=0"`
}

// ScoreResponse はランキング取得レスポンスの形式です
type ScoreResponse struct {
	Rank       int       `json:"rank"`
	PlayerName string    `json:"player_name"`
	Score      int       `json:"score"`
	CreatedAt  time.Time `json:"created_at"`
}
