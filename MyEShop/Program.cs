using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyEShop.Context;
using MyEShop.Contracts;
using MyEShop.Contracts.Repositories;
using MyEShop.Repositories;
using MyEShop.Utilities;
using System.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MyEShop.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.\


builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer<ShopDbContext>(builder.Configuration.GetConnectionString("ShopConnectionString"));

builder.Services.AddScoped<ShopDbContext>();
builder.Services.AddLogging(option =>
{
    option.AddConsole();
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserAddressRepository, UserAddressRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
builder.Services.AddScoped<ICommentWeakPointsRepository, CommentWeakPointsRepository>();
builder.Services.AddScoped<ICommentPositivePointsRepository, CommentPositivePointsRepository>();
builder.Services.AddScoped<IProductQuestionAndAnswerRepository, ProductQuestionAndAnswerRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseErrorHandlingMiddleware();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// map amdin area
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
