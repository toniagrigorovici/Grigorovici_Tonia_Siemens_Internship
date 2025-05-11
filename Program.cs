using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Grigorovici_Tonia_Siemens_Internship.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Grigorovici_Tonia_Siemens_InternshipContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Grigorovici_Tonia_Siemens_InternshipContext") ?? throw new InvalidOperationException("Connection string 'Grigorovici_Tonia_Siemens_InternshipContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
