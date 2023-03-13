using Microsoft.AspNetCore.ResponseCompression;
using BlazorWebAssemblySignalRApp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//SignalR Settings

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new []{"application/octet-stream"});
});

var app = builder.Build();

//SignalR

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//SignalR



app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

//SignalR

app.MapHub<ChatHub>("/mychat");

app.MapFallbackToFile("index.html");

app.Run();
