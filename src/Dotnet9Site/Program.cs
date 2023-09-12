SiteApp.Run(args,
    (services, config) =>
    {
        services.AddMyCaptcha(config);
        services.AddPgSql<Dotnet9DbContext>(config);
    });