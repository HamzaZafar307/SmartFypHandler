using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartFYPHandler.Data;
using SmartFYPHandler.Services.Interfaces;
using SmartFYPHandler.Services.Implementations;
using System.Text;
using SmartFYPHandler.Services.Interfaces;
using SmartFYPHandler.Services.Implementations.Embedding;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secret = jwtSettings["Secret"] ?? "super_secret_key_for_development_only_12345"; // Fallback for dev
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add Memory Cache
builder.Services.AddMemoryCache();

// Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IFYPProjectService, FYPProjectService>();
builder.Services.AddScoped<IProjectEvaluationService, ProjectEvaluationService>();
builder.Services.AddScoped<IRankingService, RankingService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
// Novelty Analyzer
builder.Services.AddScoped<ITextPreprocessor, TextPreprocessor>();
// Embedding provider selection
var noveltySection = builder.Configuration.GetSection("Novelty");
var embeddingProvider = noveltySection["EmbeddingProvider"] ?? "Hash";
if (string.Equals(embeddingProvider, "SBert", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddScoped<IEmbeddingProvider, SentenceBertEmbeddingProvider>();
}
else
{
    builder.Services.AddScoped<IEmbeddingProvider, SimpleHashEmbeddingProvider>();
}

builder.Services.AddHttpClient();
builder.Services.AddScoped<IDocumentIndexService, DocumentIndexService>();
// External providers (free sources)
builder.Services.AddScoped<IExternalDocumentProvider, SmartFYPHandler.Services.Implementations.External.GitHubSourceProvider>();
builder.Services.AddScoped<IExternalDocumentProvider, SmartFYPHandler.Services.Implementations.External.ArXivSourceProvider>();
builder.Services.AddScoped<INoveltyService, NoveltyService>();
builder.Services.Configure<SmartFYPHandler.Services.Implementations.NoveltyOptions>(
    builder.Configuration.GetSection("Novelty"));
builder.Services.AddScoped(sp => sp
    .GetRequiredService<Microsoft.Extensions.Options.IOptions<SmartFYPHandler.Services.Implementations.NoveltyOptions>>()
    .Value);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure database is created/updated for development/testing
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
