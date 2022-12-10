using KH.Pepper.Web;
using KH.Pepper.Web.Config;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddExtensionBindings(builder.Configuration, builder.Environment);

//TODO:  Yet to check
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHsts();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KolliHillsPepper v1");
    // c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        var path = context.File.Name;
        if (path == "index.html")
        {
            context.Context.Response.Headers.TryAdd(HeaderNames.CacheControl, "no-cache, no-store, must-revalidate");
            context.Context.Response.Headers.TryAdd(HeaderNames.Pragma, "no-cache");
            context.Context.Response.Headers.TryAdd(HeaderNames.Expires, "0");
        };
    }
});


app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(cors =>
    cors.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

//use middleware
app.UseExceptionHandling();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();