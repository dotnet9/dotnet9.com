package views

import (
	"errors"
	"log"
	"net/http"
	"strconv"

	"dotnet9.com/goweb/common"
	"dotnet9.com/goweb/service"
)


type IndexData struct {
	Title string `json:"title"`
	Desc string `json:"desc"`
}

func (*HTMLApi) Index(w http.ResponseWriter, r *http.Request) {
	index := common.Template.Index
	if err := r.ParseForm(); err != nil {
		log.Println("表单获取失败：", err)
		index.WriteError(w, errors.New("系统错误，请联系管理员！！"))
		return
	}	
	keywords := r.Form.Get("keywords")
	currentStr := r.Form.Get("current")
	current := 1
	if currentStr != "" {
		current, _ = strconv.Atoi(currentStr)
	}
	pageSizeStr := r.Form.Get("pageSize")
	pageSize := 10
	if pageSizeStr != "" {
		pageSize, _ = strconv.Atoi(pageSizeStr)
	}

	hr := service.GetAllIndexInfo(keywords, current, pageSize)
	index.WriteData(w, hr)
}