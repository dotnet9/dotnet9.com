package views

import (
	"net/http"
	"dotnet9.com/goweb/common"
	"dotnet9.com/goweb/config"
	"dotnet9.com/goweb/models"
)


type IndexData struct {
	Title string `json:"title"`
	Desc string `json:"desc"`
}

func (*HTMLApi) Index(w http.ResponseWriter, r *http.Request) {
	index := common.Template.Index

	// 页面上涉及到的所有的数据，必须有定义
	var categories = []models.Category{
		{
			Cid: 1,
			Name: "Dotnet", 
		},
		{
			Cid: 2,
			Name: "GoLang", 
		},
	}
	var posts = []models.PostMore{
		{
			Pid: 1, 
			Title: "go博客", 
			Slug: "go-blog", 
			Content: "This is a simple example of GoLang", 
			CategoryId: 1, 
			CategoryName: "GoLang", 
			UserId: 1, 
			UserName: "dotnet9", 
			ViewCount: 1, 
			Type: 0, 
			CreateAt: "2022-10-05",
		},
	}

	var hr = &models.HomeResponse{
		Viewer: config.Cfg.Viewer,
		Categories: categories,
		Posts: posts,
		Total: 1,
		Page: 1,
		Pages: []int{1},
		PageEnd: true,
	}
	index.WriteData(w, hr)
}