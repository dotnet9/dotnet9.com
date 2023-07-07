var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();

#region jwt

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtSection);
var jwtOptions = jwtSection.Get<JwtOptions>();

#endregion

builder.Services.AddSingleton((service) => new RedisClient(builder.Configuration["ConnectionStrings:Redis"])
{
    Serialize = obj=> JsonSerializer.Serialize(obj),
    Deserialize = (json, type) => JsonSerializer.Deserialize(json, type)
});

var siteSection = builder.Configuration.GetSection("Site");
builder.Services.Configure<SiteOptions>(siteSection);
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var corsPolicyName = "CorsPolicy";
var app = builder.Services
    .AddAuthorization()
    .AddMasaIdentity()
    .AddTransient<AuditMiddleware>()
    .AddTransient<AnomalyMiddleware>()
    .AddJwtBearerAuthentication(jwtOptions!)
    .AddCors(options =>
    {
        options.AddPolicy(corsPolicyName, corsBuilder =>
        {
            corsBuilder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader()
                .AllowCredentials();
        });
    })
    .AddMapster()
    .AddSequentialGuidGenerator()
    .Configure<AuditEntityOptions>(options => options.UserIdType = typeof(int))
    .AddMasaDbContext<Dotnet9DbContext>(dbContextBuilder =>
    {
        dbContextBuilder
            .UseNpgsql()
            .UseFilter();
    })
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1",
            new OpenApiInfo { Title = "Dotnet9", Version = "v1", Contact = new OpenApiContact { Name = "Dotnet9", } });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
        {
            options.IncludeXmlComments(item, true);
        }

        options.DocInclusionPredicate((_, _) => true);
    })
    .AddEventBus()
    .AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters()
    .AddDomainEventBus(dispatcherOptions =>
    {
        dispatcherOptions
            .UseIntegrationEventBus<IntegrationEventLogService>(options =>
                options.UseDapr().UseEventLog<Dotnet9DbContext>())
            .UseEventBus(eventBusBuilder =>
            {
                eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
                eventBusBuilder.UseMiddleware(typeof(LoggingMiddleware<>));
            })
            .UseUoW<Dotnet9DbContext>()
            .UseRepository<Dotnet9DbContext>();
    })
    .AddAutoInject()
    .AddServices(builder);

app.UseMiddleware<AuditMiddleware>();
app.UseMiddleware<AnomalyMiddleware>();

app.UseMasaExceptionHandler();
app.UseCors(corsPolicyName);

app.UseStaticFiles();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    ISeedService seedService = serviceScope.ServiceProvider.GetRequiredService<ISeedService>();
    await seedService.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCloudEvents();

await app.RunAsync();