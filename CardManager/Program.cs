using CardManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Correct SQLite registration
builder.Services.AddDbContext<CardCollectionContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CardCollectionContext")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();