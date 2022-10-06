package views

import (
	"errors"
	"net/http"
	"strings"
	"dotnet9.com/goweb/common"
	"dotnet9.com/goweb/service"
)

func (*HTMLApi) Detail(w http.ResponseWriter, r *http.Request) {
	detail := common.Template.Detail
	path := r.URL.Path
	slug := strings.TrimPrefix(path, "/p/")
	blogPostResponse, err := service.GetBlogPostDetail(slug)
	if err != nil {
		detail.WriteError(w, errors.New("文章不存在"))
		return
	}
	detail.WriteData(w, blogPostResponse)
}
