//using KH.Pepper.Infra.DataBase;
//using KH.Pepper.Web;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using KH.Pepper.Web.Config;
//using Microsoft.Net.Http.Headers;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

////start: this is new code followed
//builder.Services.AddHttpContextAccessor();

////#1 Add Api Extenstions, which contains DbContext, Repository, application Config, Mediator, etc....
//builder.Services.AddApiExtensions(builder.Configuration, builder.Environment);
////end

////# 2: added for Bearer Token popup 
//builder.Services.AddSwaggerGen();

////# 3: JWT Token settings
//builder.Services.AddJwtToken(builder);

////# 4: Register all services
////builder.Services.AddServices();

    
//var app = builder.Build();

//// Configure the HTTP request pipeline.
        
//if (app.Environment.IsDevelopment())
//{   
//    app.UseDeveloperExceptionPage(); 
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    //access only HTTPS request validtion, use Hsts and useHttpsRedirection
//    app.UseHsts();
//}

////check -> middleware -> use exception handling
//app.UseHsts();
//app.UseSwagger();

//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KolliHillsPepper v1");
//    // c.RoutePrefix = String.Empty;
//});

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseDefaultFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    OnPrepareResponse = context =>
//    {
//        var path = context.File.Name;
//        if (path == "index.html")
//        {
//            context.Context.Response.Headers.TryAdd(HeaderNames.CacheControl, "no-cache, no-store, must-revalidate");
//            context.Context.Response.Headers.TryAdd(HeaderNames.Pragma, "no-cache");
//            context.Context.Response.Headers.TryAdd(HeaderNames.Expires, "0");
//        };
//    }
//});

////app.UseSession();
//app.UseAuthentication();
//app.UseAuthorization();

////Custom middleware
////app.UseExceptionHandling();

//app.UseCors(cors =>
//    cors.AllowAnyHeader()
//    .AllowAnyMethod()
//    .SetIsOriginAllowed(origin => true)
//    .AllowCredentials());


////this is custom defined
////app.UseSwaggerDoc();

////app.UseErrorHandlingMiddleware();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.Run();
