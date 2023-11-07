using GameStoreLab.Domain.Interfaces.Repositories;
using GameStoreLab.Domain.Models;
using GameStoreLab.Infrastructure.Contexts;
using GameStoreLab.Infrastructure.Helpers;
using GameStoreLab.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GameStoreLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GameStoreContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("GameStoreCS")));
            builder.Services.AddMemoryCache();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddScoped<IRepository<Game>, BaseRepository<Game>>();
            builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();
            builder.Services.AddScoped<IRepository<Transaction>, BaseRepository<Transaction>>();

            var app = builder.Build();

            app.UseSession();

            app.Map("/searchform1", builder =>
            {
                builder.Run(async context =>
                {
                    var searchText = context.Request.Cookies["searchText"];
                    var searchOption = context.Request.Cookies["searchOption"];
                    var searchFormHtml = HtmlGenerationHelper.GenerateFormPage(1, searchText, searchOption);

                    await context.Response.WriteAsync(searchFormHtml);
                });
            });

            app.Map("/searchform2", builder =>
            {
                builder.Run(async context =>
                {
                    var sessionFormString = context.Session.GetString("form");
                    var serializedEmptyForm = JsonSerializer.Serialize(new SearchFormModel());

                    var form = context.Session.Keys.Contains("form")
                        ? JsonSerializer.Deserialize<SearchFormModel>(
                            string.IsNullOrEmpty(context.Session.GetString("form"))
                            ? serializedEmptyForm
                            : context.Session.GetString("form"))
                        : new SearchFormModel();

                    await context.Response.WriteAsync(HtmlGenerationHelper.GenerateFormPage(2, form));
                });
            });

            app.Map("/search1", builder =>
            {
                builder.Run(async context =>
                {
                    var formSearchText = context.Request.Form["searchText"].ToString();
                    var formSearchOption = context.Request.Form["searchType"].ToString();
                    var (searchOption, searchText) = (context.Request.Cookies["searchOption"],
                        context.Request.Cookies["searchText"]);

                    if (!string.IsNullOrEmpty(formSearchText) && formSearchText != searchText)
                    {
                        searchText = formSearchText;
                        context.Response.Cookies.Append("searchText", formSearchText);
                    }

                    if (!string.IsNullOrEmpty(formSearchOption) && formSearchOption != searchOption)
                    {
                        searchOption = formSearchOption;
                        context.Response.Cookies.Append("searchOption", formSearchOption);
                    }

                    var repository = context.RequestServices.GetService<IRepository<Game>>();

                    var list = repository.SearchByColumn(searchOption, searchText);
                    var htmlCode = HtmlGenerationHelper.GenerateTable(
                        headers: new List<string>()
                        {
                            "GameId", "Title", "Description", "DevId", "Release Year", "Price"
                        },
                        data: HtmlGenerationHelper.GenerateGamesData(list));

                    await context.Response.WriteAsync(htmlCode);
                });
            });

            app.Map("/search2", builder =>
            {
                builder.Run(async context =>
                {
                    var formSearchText = context.Request.Form["searchText"].ToString();
                    var formSearchOption = context.Request.Form["searchType"].ToString();
                    var sessionSavedForm = JsonSerializer.Deserialize<SearchFormModel>(context.Session.GetString("form")
                        ?? JsonSerializer.Serialize(new SearchFormModel()));
                    var (option, searchText) = (sessionSavedForm.Option, sessionSavedForm.Text);

                    if (!string.IsNullOrEmpty(formSearchText) && formSearchText != sessionSavedForm.Text)
                    {
                        searchText = formSearchText;
                    }

                    if (!string.IsNullOrEmpty(formSearchOption) && formSearchOption != sessionSavedForm.Option)
                    {
                        option = formSearchOption;
                    }

                    context.Session.SetString("form", JsonSerializer.Serialize(new SearchFormModel()
                    {
                        Option = option,
                        Text = searchText
                    }));

                    var repository = context.RequestServices.GetService<IRepository<Game>>();

                    var list = repository.SearchByColumn(option, searchText);
                    var htmlCode = HtmlGenerationHelper.GenerateTable(
                        headers: new List<string>()
                        {
                            "GameId", "Title", "Description", "DevId", "Release Year", "Price"
                        },
                        data: HtmlGenerationHelper.GenerateGamesData(list));
                    await context.Response.WriteAsync(htmlCode);

                });
            });

            app.Map("/about", builder =>
            {
                builder.Run(async context =>
                {
                    var htmlCode = HtmlGenerationHelper.GenerateAboutPage();
                    await context.Response.WriteAsync(htmlCode);
                });
            });

            app.Map("/games", builder =>
            {
                builder.Run(async context =>
                {
                    var repository = context.RequestServices.GetService<IRepository<Game>>();
                    var list = repository.GetCachedList();
                    var htmlCode = HtmlGenerationHelper.GenerateTable(
                        headers: new List<string>()
                        {
                            "GameId", "Title", "Description", "DevId", "Release Year", "Price"
                        },
                        data: HtmlGenerationHelper.GenerateGamesData(list));

                    await context.Response.WriteAsync(htmlCode);
                });
            });

            app.Map("/users", builder =>
            {
                builder.Run(async context =>
                {
                    var repository = context.RequestServices.GetService<IRepository<User>>();
                    var list = repository.GetCachedList();
                    var htmlCode = HtmlGenerationHelper.GenerateTable(
                        headers: new List<string>()
                        {
                            "Id", "Login", "Password", "Email", "Name", "Surname", "Date of Birth"
                        },
                        data: HtmlGenerationHelper.GenerateUsersData(list));
                    await context.Response.WriteAsync(htmlCode);
                });
            });

            app.Map("/transactions", builder =>
            {
                builder.Run(async context =>
                {
                    var repository = context.RequestServices.GetService<IRepository<Transaction>>();
                    var list = repository.GetCachedList();
                    var htmlCode = HtmlGenerationHelper.GenerateTable(
                        headers: new List<string>()
                        {
                            "Id", "Game Id", "Buyer Id", "Date", "Price"
                        },
                        data: HtmlGenerationHelper.GenerateTransactionsData(list));
                    await context.Response.WriteAsync(htmlCode);
                });
            });

            app.Run();
        }

    }
}