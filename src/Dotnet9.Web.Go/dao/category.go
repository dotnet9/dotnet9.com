package dao

import "dotnet9.com/goweb/models"

func GetAllCategory() []models.Category {
	var categories []models.Category
	DB.Find(&categories)
	return categories
}

func GetCagegories(blogPostId string) []models.Category {
	var categories []models.Category
	DB.Select("c.\"Name\",c.\"Slug\"").Table("\"AppCategories\" as c").Joins("left join \"AppBlogPostCategories\" as bpc on bpc.\"CategoryId\" = c.\"Id\"").Where("bpc.\"BlogPostId\" = ?", blogPostId).Find(&categories)
	return categories
}