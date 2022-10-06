package models

import "dotnet9.com/goweb/config"

type HomeResponse struct {
	config.Viewer
	Categories []Category
	Posts []BlogPostMore
	Total int64
	Page int
	Pages []int
	PageEnd bool
}