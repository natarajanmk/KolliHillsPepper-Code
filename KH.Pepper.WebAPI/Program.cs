using KH.Pepper.Infra.DataBase;
using KH.Pepper.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//# 1: add DbContext
//builder.Services.AddScoped<AppDbContext>(dbContext => new AppDbContext(builder.Configuration.GetConnectionString("AppDbContext")));
builder.Services.AddScoped<AppDbContext>(dbContext => new AppDbContext());

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"),
    b => b.MigrationsAssembly("KH.Pepper.Infrastructure.DataBase")), ServiceLifetime.Singleton);


//# 2: added for Bearer Token popup 
builder.Services.AddSwaggerGen();

//# 3: JWT Token settings
builder.Services.AddJwtToken(builder);

//# 4: Register all services
builder.Services.AddServices();

    
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KolliHillsPepper v1");
    // c.RoutePrefix = String.Empty;
});
        
        
if (app.Environment.IsDevelopment())
{   
    app.UseDeveloperExceptionPage(); 
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //access only HTTPS request validtion, use Hsts and useHttpsRedirection
    app.UseHsts();
}
 

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(cors =>
    cors.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

//this is custom defined
//app.UseSwaggerDoc();

app.UseErrorHandlingMiddleware();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllers();

app.Run();
