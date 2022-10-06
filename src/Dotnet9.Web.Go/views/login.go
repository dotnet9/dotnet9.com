package views

import (
	"net/http"

	"dotnet9.com/goweb/common"
	"dotnet9.com/goweb/config"
)

func (*HTMLApi) Login(w http.ResponseWriter, r *http.Request) {
	login := common.Template.Login

	login.WriteData(w, config.Cfg.Viewer)
}