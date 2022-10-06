package router

import (
	"net/http"
	"dotnet9.com/goweb/views"
	"dotnet9.com/goweb/api"
)

func Router() {
	http.HandleFunc("/", views.HTML.Index)
	http.HandleFunc("/cat/", views.HTML.Category)
	http.HandleFunc("/login", views.HTML.Login)
	http.HandleFunc("/api/v1/post", api.API.SaveAndUpdatePost)
	http.Handle("/resource/", http.StripPrefix("/resource/", http.FileServer(http.Dir("public/resource/"))))
}