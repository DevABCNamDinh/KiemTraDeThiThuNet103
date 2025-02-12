﻿using KiemTraDeThiTHu1.models;
using KiemTraDeThiTHu1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromSeconds(15); // Set timeout = 5 giây
}); // Thêm session
builder.Services.AddSingleton<HoaService>();
builder.Services.AddSingleton<ThucVatDB2Context>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Khi báo có sử dụng session
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hoa}/{action=Index}/{id?}");

app.Run();
