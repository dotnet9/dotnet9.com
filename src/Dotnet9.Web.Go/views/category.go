package views

import (
	"errors"
	"log"
	"net/http"
	"strconv"
	"strings"
	"dotnet9.com/goweb/common"
	"dotnet9.com/goweb/service"
)

func (*HTMLApi) Category(w http.ResponseWriter, r *http.Request) {
	categoryTemplate := common.Template.Category
	path := r.URL.Path
	slug := strings.TrimPrefix(path, "/c/")
	if len(slug) == 0 {
		categoryTemplate.WriteError(w, errors.New("分类不存在"))
	}
	if err := r.ParseForm(); err != nil {
		log.Println("表单获取失败：", err)
		categoryTemplate.WriteError(w, errors.New("系统错误，请联系管理员！！"))
		return
	}	
	currentStr := r.Form.Get("current")
	current := 1
	if currentStr != "" {
		current, _ = strconv.Atoi(currentStr)
	}
	pageSizeStr := r.Form.Get("pageSize")
	pageSize := 15
	if pageSizeStr != "" {
		pageSize, _ = strconv.Atoi(pageSizeStr)
	}

	categoryResponse, err := service.GetBlogPostByCategorySlug(slug, current, pageSize)
	if err != nil {
		categoryTemplate.WriteError(w, err)
		return
	}
	categoryTemplate.WriteData(w, categoryResponse)
}