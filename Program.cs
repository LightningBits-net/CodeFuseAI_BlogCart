﻿
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using CodeFuseAI_BlogCart;
using CodeFuseAI_BlogCart.Service;
using CodeFuseAI_BlogCart.Service.IService;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.JSInterop;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["BaseAPIUrl"]) });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IClientFrontendService, ClientFrontendService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
//builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
//builder.Services.AddSyncfusionBlazor();

builder.Services.AddSingleton<DomainLayoutService>();
builder.Services.AddHttpContextAccessor();




var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

