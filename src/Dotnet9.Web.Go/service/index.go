package service

import (
	"html/template"

	"dotnet9.com/goweb/config"
	"dotnet9.com/goweb/dao"
	"dotnet9.com/goweb/models"
)

func GetAllIndexInfo(keywords string, page int, pageSize int) *models.HomeResponse {
	// 页面上涉及到的所有的数据，必须有定义
	var categories = dao.GetAllCategory()
	var blogPosts = dao.GetBlogPost(keywords, page, pageSize)
	var blogPostMores []models.BlogPostMore
	for _, blogpost := range blogPosts {
		categories := dao.GetCagegories(blogpost.Id)
		blogPostMore := models.BlogPostMore{
			Id:                   blogpost.Id,
			Title:                blogpost.Title,
			Slug:                 blogpost.Slug,
			Description:          blogpost.Description,
			Cover:                blogpost.Cover,
			Content:              template.HTML(blogpost.Content),
			CopyrightType:        blogpost.CopyrightType,
			Original:             blogpost.Original,
			OriginalAvatar:       blogpost.OriginalAvatar,
			OriginalTitle:        blogpost.OriginalTitle,
			OriginalLink:         blogpost.OriginalLink,
			Categories:           categories,
			ViewCount:            blogpost.ViewCount,
			Type:                 blogpost.Type,
			CreationTime:         models.DateDay(blogpost.CreationTime),
			LastModificationTime: models.DateDay(blogpost.LastModificationTime),
		}
		blogPostMores = append(blogPostMores, blogPostMore)
	}
	var hr = &models.HomeResponse{
		Viewer: config.Cfg.Viewer,
		Categories: categories,
		Posts: blogPostMores,
		Total: 1,
		Page: 1,
		Pages: []int{1},
		PageEnd: true,
	}
	return hr
}