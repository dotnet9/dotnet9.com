package models

import (
	"html/template"
	"dotnet9.com/goweb/config"
	"time"
)

type Post struct {
	Pid 		int			`json:"pid"`
	Title 		string		`json:"title"`
	Slug 		string		`json:"slug"`
	Content 	string 		`json:"content"`
	Markdown 	string 		`json:"markdown"`
	CategoryId 	int 		`json:"categoryId"`
	UserId 		int 		`json:"userId"`
	ViewCount 	int 		`json:"viewCount"`
	Type 		int 		`json:"type"`
	CreateAt 	time.Time 	`json:"createAt"`
	UpdateAt 	time.Time 	`json:"updateAt"`
}

type PostMore struct {
	Pid 			int 			`json:"pid"`
	Title 			string 			`json:"title"`
	Slug 			string 			`json:"slug"`
	Content 		template.HTML 	`json:"content"`	// 文章的html
	CategoryId 		int 			`json:"CategoryId"`
	CategoryName 	string 			`json:"categoryName"`
	UserId 			int 			`json:"userId"`
	UserName 		string 			`json:"userName"`
	ViewCount 		int 			`json:"viewCount"`
	Type 			int 			`json:"type"`
	CreateAt 		string 		`json:"createAt"`
	UpdateAt 		string 		`json:"updateAt"`
}

type PostReq struct {
	Pid 		int		`json:"pid"`
	Title 		string	`json:"title"`
	Slug 		string	`json:"slug"`
	Content 	string 	`json:"content"`
	Markdown 	string 	`json:"markdown"`
	CategoryId 	int 	`json:"categoryId"`
	UserId		int 	`json:"userId"`
	Type 		int 	`json:"type"`
}

type SearchResp struct {
	Pid 	int 	`orm:"pid" json:"pid"`		// 文章ID
	Title 	string 	`orm:"title" json:"title"`	// 文章标题
}

type PostResp struct {
	config.Viewer
	config.SystemConfig
	Article PostMore
}