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
                options.Binders.Add("setting-management", new() { Text = "����" });
                options.Binders.Add("text-templates", new() { Text = "�ı�ģ��" });
                options.Binders.Add("audit-logs", new() { Text = "�����־" });
                options.Binders.Add("language-management/languages", new() { Text = "����" });
                options.Binders.Add("language-management/texts", new() { Text = "�����ı�" });
                options.Binders.Add("openIddict/Applications", new() { Text = "Ӧ�ó���" });
                options.Binders.Add("openIddict/Scopes", new() { Text = "��Χ" });
                options.Binders.Add("identity/organization-units", new() { Text = "��֯����" });
                options.Binders.Add("identity/roles", new() { Text = "��ɫ" });
                options.Binders.Add("identity/users", new() { Text = "�û�" });
                options.Binders.Add("identity/claim-types", new() { Text = "��������" });
                options.Binders.Add("identity/security-logs", new() { Text = "��ȫ��־" });
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
