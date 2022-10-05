package config

import (
	"github.com/BurntSushi/toml"
	"os"
)

type TomlConfig struct {
	Viewer Viewer
	System SystemConfig
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

var Cfg *TomlConfig
func init() {
	Cfg = new(TomlConfig)
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
}