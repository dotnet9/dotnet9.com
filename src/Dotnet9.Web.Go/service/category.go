package service

import (
	"fmt"
	"dotnet9.com/goweb/config"
	"dotnet9.com/goweb/dao"
	"dotnet9.com/goweb/models"
)

func GetBlogPostByCategorySlug(slug string, current int, pageSize int) (*models.CategoryResponse, error) {
	var categories = dao.GetAllCategory()
	total, blogPosts, err := dao.GetBlogPostByCategorySlug(slug, current, pageSize)
	fmt.Println("查旬文章总数：", total)
	if err != nil {
		fmt.Println("查询文章出错：", err)
		return nil, err
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
		Page: current,
		Pages: pages,
		PageEnd: current != pageCount,
	}
	categoryName := dao.GetCategoryNameBySlug(slug)
	categoryResponse := &models.CategoryResponse{
		HomeResponse: hr,
		CategoryName: categoryName,
	}
	return categoryResponse, nil
}