using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace TABS.Blazor;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<TABSBlazorModule>();
            builder.Services.AddBootstrapBlazor();

            builder.Services.ConfigureTabItemMenuBindOptions(options => {
                options.Binders.Add("setting-management", new() { Text = "设置" });
                options.Binders.Add("text-templates", new() { Text = "文本模板" });
                options.Binders.Add("audit-logs", new() { Text = "审计日志" });
                options.Binders.Add("language-management/languages", new() { Text = "语言" });
                options.Binders.Add("language-management/texts", new() { Text = "语言文本" });
                options.Binders.Add("openIddict/Applications", new() { Text = "应用程序" });
                options.Binders.Add("openIddict/Scopes", new() { Text = "范围" });
                options.Binders.Add("identity/organization-units", new() { Text = "组织机构" });
                options.Binders.Add("identity/roles", new() { Text = "角色" });
                options.Binders.Add("identity/users", new() { Text = "用户" });
                options.Binders.Add("identity/claim-types", new() { Text = "声明类型" });
                options.Binders.Add("identity/security-logs", new() { Text = "安全日志" });
            });
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
