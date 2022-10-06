package service

import (
	"fmt"
	"dotnet9.com/goweb/config"
	"dotnet9.com/goweb/dao"
	"dotnet9.com/goweb/models"
)

func GetAllIndexInfo(keywords string, page int, pageSize int) *models.HomeResponse {
	// 页面上涉及到的所有的数据，必须有定义
	var categories = dao.GetAllCategory()
	total, blogPosts, err := dao.GetBlogPost(keywords, page, pageSize)
	if err != nil {
		fmt.Println("查询文章出错：", err)
		return nil
	}
	pageCount := int(total-1)/pageSize+1
	var pages []int
	for i := 1; i <= pageCount; i++ {
		pages = append(pages, i)
	}
	var blogPostMores []models.BlogPostMore
	for _, blogpost := range blogPosts {
		categoriesOfBlogPost := dao.GetCagegoriesByBlogPostId(blogpost.Id)
		blogPostMore := models.BlogPostMore{
			Id:                   blogpost.Id,
			Title:                blogpost.Title,
			Slug:                 blogpost.Slug,
			Description:          blogpost.Description,
			Cover:                blogpost.Cover,
			CopyrightType:        blogpost.CopyrightType,
			OriginalAvatar:       blogpost.OriginalAvatar,
			OriginalTitle:        blogpost.OriginalTitle,
			OriginalLink:         blogpost.OriginalLink,
			Categories:           categoriesOfBlogPost,
			ViewCount:            blogpost.ViewCount,
			CreationTime:         models.DateDay(blogpost.CreationTime),
			LastModificationTime: models.DateDay(blogpost.LastModificationTime),
		}
		if len(blogpost.Original) == 0 {
			blogPostMore.Original = config.AppCfg.Site.DefaultAuthor
		} else {
			blogPostMore.Original = blogpost.Original
		}
		blogPostMores = append(blogPostMores, blogPostMore)
	}
	var hr = &models.HomeResponse{
		Viewer: config.Cfg.Viewer,
		Categories: categories,
		Posts: blogPostMores,
		Total: total,
		Page: page,
		Pages: pages,
		PageEnd: page != pageCount,
	}
	return hr
}