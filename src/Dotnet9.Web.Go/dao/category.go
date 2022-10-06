package dao

import "dotnet9.com/goweb/models"

func GetAllCategory() []models.Category {
	var categories []models.Category
	DB.Find(&categories)
	return categories
}

func GetCagegoriesByBlogPostId(blogPostId string) []models.Category {
	var categories []models.Category
	DB.Select("c.\"Name\",c.\"Slug\"").Table("\"AppCategories\" as c").Joins("left join \"AppBlogPostCategories\" as bpc on bpc.\"CategoryId\" = c.\"Id\"").Where("bpc.\"BlogPostId\" = ?", blogPostId).Find(&categories)
	return categories
}

func GetCategoryNameBySlug(slug string) string {
	var category models.Category
	DB.Table("\"AppCategories\" as c").Where("c.\"Slug\" = ?", slug).Order("\"SequenceNumber\" asc").First(&category)
	return category.Name
}