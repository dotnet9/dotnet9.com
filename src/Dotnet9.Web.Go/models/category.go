package models

type Category struct {
	Id string `gorm:"column:Id;primary_key" json:"id"`
	Name string
	Slug string
	CreationTime string
	LastModificationTime string
}

type CategoryResponse struct {
	*HomeResponse
	CategoryName string
}

func (v Category) TableName() string {
	return "AppCategories"
}