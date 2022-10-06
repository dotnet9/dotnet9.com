package dao

import "dotnet9.com/goweb/models"

func GetBlogPost(keywords string, page int, pageSize int) []models.BlogPost {
	var posts []models.BlogPost
	if keywords != "" {
		DB.Where("Title like ?", "%"+keywords+"%")
	}
	DB.Order("\"CreationTime\" desc").Offset((page - 1) * pageSize).Limit(pageSize).Find(&posts)
	return posts
}