package models

import (
	"html/template"
	"dotnet9.com/goweb/config"
	"time"
)

type BlogPost struct {
	Id 				string		`json:"id"`
	Title 			string		`json:"title"`
	Slug 			string		`json:"slug"`
	Description 	string		`json:"description"`
	Cover 			string		`json:"cover"`
	Content 		string 		`json:"content"`
	CopyrightType 	int			`json:"copyrightType"`
	Original 		string		`json:"original"`
	OriginalAvatar 	string		`json:"originalAvatar"`
	OriginalTitle 	string		`json:"originalTitle"`
	OriginalLink 	string		`json:"originalLink"`
	UserId 			int 		`json:"userId"`
	ViewCount 		int 		`json:"viewCount"`
	Type 			int 		`json:"type"`
	CreationTime 	time.Time 	`json:"creationTime"`
	LastModificationTime 	time.Time 	`json:"lastModificationTime"`
}

func (v BlogPost) TableName() string {
	return "AppBlogPosts"
}

type BlogPostMore struct {
	Id 				string		`json:"id"`
	Title 			string		`json:"title"`
	Slug 			string		`json:"slug"`
	Description 	string		`json:"description"`
	Cover 			string		`json:"cover"`
	Content 		template.HTML 	`json:"content"`	// 文章的html
	CopyrightType 	int			`json:"copyrightType"`
	Original 		string		`json:"original"`
	OriginalAvatar 	string		`json:"originalAvatar"`
	OriginalTitle 	string		`json:"originalTitle"`
	OriginalLink 	string		`json:"originalLink"`
	Categories 		[]Category 			`json:"categories"`
	ViewCount 		int 			`json:"viewCount"`
	Type 			int 			`json:"type"`
	CreationTime 	string 	`json:"creationTime"`
	LastModificationTime 	string 	`json:"lastModificationTime"`
}

type BlogPostReq struct {
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

type BlogPostResp struct {
	config.Viewer
	config.SystemConfig
	Article BlogPostMore
}