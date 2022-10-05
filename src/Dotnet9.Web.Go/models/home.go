package models

import "dotnet9.com/goweb/config"

type HomeResponse struct {
	config.Viewer
	Categories []Category
	Posts []PostMore
	Total int
	Page int
	Pages []int
	PageEnd bool
}