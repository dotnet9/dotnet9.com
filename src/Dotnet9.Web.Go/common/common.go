package common

import (
	"sync"
	"dotnet9.com/goweb/config"
	"dotnet9.com/goweb/models"
)

var Template models.HtmlTemplate

func LoadTemplate() {
	w := sync.WaitGroup{}
	w.Add(1)
	go func() {
		// 耗时
		var err error
		Template, err = models.InitTemplate(config.AppCfg.System.CurrentDir + "/template/")
		if err != nil {
			panic(err)
		}
		w.Done()
	}()
	w.Wait()
}