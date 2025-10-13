using UserPresentation.Components;
using UserPresentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("SoukApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5156");
});

builder.Services.AddScoped<ApiProductService>();
builder.Services.AddScoped<ApiSupplierService>();
builder.Services.AddScoped<ApiWarehouseService>();
builder.Services.AddScoped<ApiPurchaseOrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();