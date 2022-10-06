package dao

import (
	"dotnet9.com/goweb/models"
	"fmt"
)

func GetBlogPost(keywords string, page int, pageSize int) (int64, []models.BlogPost, error) {
	var posts []models.BlogPost
	db := DB.Model(&models.BlogPost{})
	if len(keywords) != 0 {
		db = db.Where("Title like ?", "%"+keywords+"%")
	}
	var total int64
	err := db.Count(&total).Error
	if err != nil {
		fmt.Println("查询文章数量出错：", err)
		return 0, nil, err
	}
	fmt.Println("文章数量：", total)
	err = db.Order("\"CreationTime\" desc").Offset((page - 1) * pageSize).Limit(pageSize).Find(&posts).Error
	return total, posts, err
}

func GetBlogPostByCategorySlug(categorySlug string, page int, pageSize int) (int64, []models.BlogPost, error) {
	db := DB.Model(&models.BlogPost{})
	var posts []models.BlogPost
	var total int64
	db = db.Table("\"AppBlogPosts\" as b").Joins("left join \"AppBlogPostCategories\" as bcat on b.\"Id\" = bcat.\"BlogPostId\"").Joins("left join \"AppCategories\" as cat on bcat.\"CategoryId\"=cat.\"Id\"").Where("cat.\"Slug\" = ?", categorySlug)
	err := db.Count(&total).Error
	if err != nil {
		fmt.Println("查询文章数量出错：", err)
		return 0, nil, err
	}
	fmt.Println("文章数量：", total)
	err = db.Order("\"CreationTime\" desc").Offset((page - 1) * pageSize).Limit(pageSize).Find(&posts).Error
	return total, posts, err
}

func GetBlogPostBySlug(slug string) (*models.BlogPostDetail, error) {
	var post models.BlogPostDetail
	err := DB.Table("\"AppBlogPosts\" as b").Where("b.\"Slug\" = ?", slug).First(&post).Error
	return &post, err
}