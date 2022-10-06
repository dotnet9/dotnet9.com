package models

type Category struct {
	Id string
	Name string
	Slug string
	CreationTime string
	LastModificationTime string
}

func (v Category) TableName() string {
	return "AppCategories"
}