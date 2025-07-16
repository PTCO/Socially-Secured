using app.Client.Pages;
using app.Components;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Add essential services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// 🧠 App services
builder.Services.AddSingleton<Model>();
builder.Services.AddSingleton<Query>();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<UserService>();

// 🗂 Add session support
builder.Services.AddDistributedMemoryCache(); // Required for session backing store
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🌐 Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
// 🧵 Session must be added before routing
app.UseRouting(); // Needed for session routing context
app.UseSession();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(app.Client._Imports).Assembly);

app.Run();