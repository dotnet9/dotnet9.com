package config

import (
	"github.com/BurntSushi/toml"
	"os"
)

type TomlConfig struct {
	Viewer Viewer
	System SystemConfig
}

type AppSettings struct {
	Db DBConfig
	Site SiteConfig
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

var Cfg *TomlConfig
var AppCfg *AppSettings
func init() {
	Cfg = new(TomlConfig)
	AppCfg = new(AppSettings)
	currentDir, err := os.Getwd()
	if err != nil {
		panic(err)
	}
	Cfg.System.AppName = "Dotnet9"
	Cfg.System.Version = 0.1
	Cfg.System.CurrentDir = currentDir
	_, err = toml.DecodeFile("config/config.toml", &Cfg)
	if err != nil {
		panic(err)
	}
	_, err = toml.DecodeFile("appsettings.toml", &AppCfg)
	if err != nil {
		panic(err)
	}
}