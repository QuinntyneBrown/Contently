// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using DashboardService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
.Enrich.FromLogContext()
.CreateBootstrapLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCoreServices(builder.Environment, builder.Configuration);

    builder.Services.AddInfrastructureServices(builder.Configuration["ConnectionStrings:DefualtConnection"]!);

    builder.Services.AddApiServices();

    var app = builder.Build();

    app.UseSwagger(options => options.SerializeAsV2 = true);

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "DashboardService");
        options.RoutePrefix = string.Empty;
        options.DisplayOperationId();
    });

    app.UseCors("CorsPolicy");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    var services = (IServiceScopeFactory)app.Services.GetRequiredService(typeof(IServiceScopeFactory));

    using (var scope = services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<DashboardServiceDbContext>();

        if (args.Contains("ci"))
            args = new string[3] { "migratedb", "seeddb", "stop" };

        if (args.Contains("migratedb"))
        {
            context.Database.Migrate();
        }

        if (args.Contains("seeddb"))
        {
            context.Seed();
        }

        if (args.Contains("stop"))
            Environment.Exit(0);
    }

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}