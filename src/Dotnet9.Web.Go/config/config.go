package config

import (
	"github.com/BurntSushi/toml"
	"os"
)

type AppSettings struct {
	Viewer Viewer
	System SystemConfig
	Db DBConfig
	Site SiteConfig
	Admin AdminConfig
}

type Viewer struct {
	Title string
	Description string
	Logo string
	Navigation []string
	Bilibili string
	Avatar string
	UserName string
	UserDesc string
}

type SystemConfig struct {
	AppName string
	Version float32
	CurrentDir	string
	CdnURL string
	QiniuAccessKey string
	QiniuSecretKey string
	Valine bool
	ValineAppId string
	ValineAppKey string
	ValineServerURL string
}

type DBConfig struct {
	ConnectionString string
}

type SiteConfig struct {
	DefaultAuthor string
}

type AdminConfig struct {
	Api string
}

var AppCfg *AppSettings
func init() {
	AppCfg = new(AppSettings)
	currentDir, err := os.Getwd()
	if err != nil {
		panic(err)
	}
	AppCfg.System.AppName = "Dotnet9"
	AppCfg.System.Version = 0.1
	AppCfg.System.CurrentDir = currentDir
	_, err = toml.DecodeFile("appsettings.toml", &AppCfg)
	if err != nil {
		panic(err)
	}
}