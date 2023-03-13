using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Components.Web.LeptonXTheme.Components.ApplicationLayout.Common;
using Volo.Abp.AspNetCore.Components.Web.Theming.Layout;
using Volo.Abp.LeptonX.Shared;

namespace TABS.Blazor.Shared
{
    public partial class SideMenuLayout
    {
        [Inject]
        protected IAbpUtilsService UtilsService { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }


        //[Inject] 
        //protected PageLayout PageLayout { get; set; }

        [Inject]
        protected IOptions<LeptonXThemeOptions> Options { get; set; }


        public Assembly[] otherAssemblys;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var fileNames = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).Where(x => x.Contains(".Blazor.dll") && !x.Contains(".config"));
            var assemblys = new List<Assembly>();
            foreach (var file in fileNames)
            {
                //assemblys.Add(Assembly.LoadFrom(file));
            }
            assemblys.Add(Assembly.GetExecutingAssembly());
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.SettingManagement.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.TextTemplateManagement.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.OpenIddict.Pro.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.AuditLogging.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.Identity.Pro.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Abp.LanguageManagement.Blazor.dll")));
            assemblys.Add(Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Volo.Saas.Host.Blazor.dll"))); 
             otherAssemblys = assemblys.ToArray();


            //otherAssemblys = new Assembly[]
            //{
            //Assembly.LoadFrom(  Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"")),
            //GetType().Assembly
            //};
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await UtilsService.AddClassToTagAsync("body", GetBodyClassName());
                await JSRuntime.InvokeVoidAsync("initLeptonX", new[] { "side-menu", Options.Value.DefaultStyle });
                await JSRuntime.InvokeVoidAsync("afterLeptonXInitialization", new[] { "side-menu", Options.Value.DefaultStyle });

            }
        }

        //public async Task OnTabClick() {
        //    PageLayout.Title = "123";
        //    await InvokeAsync(StateHasChanged);
        //}
        private Task OnClickTabItemAsync(TabItem item)
        {
            StateHasChanged();
            return Task.CompletedTask;
        }
        private string GetBodyClassName()
        {
            return "lpx-theme-" + Options.Value.DefaultStyle;
        }
    }
}
