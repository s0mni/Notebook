using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Areas.Identity.Data;

[assembly: HostingStartup(typeof(Notebook.Areas.Identity.IdentityHostingStartup))]
namespace Notebook.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<NotebookContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("NotebookContextConnection")));

                //services.AddDefaultIdentity<NotebookUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<NotebookContext>();
            });
        }
    }
}