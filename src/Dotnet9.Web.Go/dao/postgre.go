package dao

import (
	"fmt"

	"dotnet9.com/goweb/config"
	"gorm.io/driver/postgres"
	"gorm.io/gorm"
	"gorm.io/gorm/logger"
)

type PqDb struct {
	*gorm.DB
}

var DB PqDb

func init() {
	ConnectionPg()
}

func ConnectionPg() {
	db, err := gorm.Open(postgres.Open(config.AppCfg.Db.ConnectionString), &gorm.Config{
		DisableForeignKeyConstraintWhenMigrating: true,
		Logger: 								 logger.Default.LogMode(logger.Info),
	})
	if err != nil {
		fmt.Printf("数据库连接失败-----err", err)
	}
	DB = PqDb{db}
}