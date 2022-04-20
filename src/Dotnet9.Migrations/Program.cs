using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

Console.WriteLine(args[0]);
Console.WriteLine("开始迁移");
var optionsBuilder = new DbContextOptionsBuilder<Dotnet9DbContext>();
optionsBuilder.UseMySql(args[0], ServerVersion.AutoDetect(args[0]));
var context = new Dotnet9DbContext(optionsBuilder.Options);
await context.Database.MigrateAsync();
Console.WriteLine("迁移成功");