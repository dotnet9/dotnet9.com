package service

import (
	"html/template"

	"dotnet9.com/goweb/config"
	"dotnet9.com/goweb/dao"
	"dotnet9.com/goweb/models"
	"github.com/russross/blackfriday"
)

func GetBlogPostDetail(slug string) (*models.BlogPostRes, error) {
	blogpost, err := dao.GetBlogPostBySlug(slug)
	
	if err != nil {
		return nil, err
	}
	categoriesOfBlogPost := dao.GetCagegoriesByBlogPostId(blogpost.Id)
	contentBytes := []byte(blogpost.Markdown)
	contentStr := string(blackfriday.MarkdownCommon(contentBytes))
	blogPostMore := models.BlogPostMore{
		Id:                   blogpost.Id,
		Title:                blogpost.Title,
		Slug:                 blogpost.Slug,
		Description:          blogpost.Description,
		Cover:                blogpost.Cover,
		Markdown: 		   	  blogpost.Markdown,
		Content: 			  template.HTML(contentStr),
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
	var postRes = &models.BlogPostRes{
		Viewer: config.AppCfg.Viewer,
		SystemConfig: config.AppCfg.System,
		Article: blogPostMore,
	}
	return postRes, nil
}