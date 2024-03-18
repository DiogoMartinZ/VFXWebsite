var builder = WebApplication.CreateBuilder(args);
IronXL.License.LicenseKey = "IRONSUITE.DIOGO.WORMS.HOTMAIL.COM.12293-9185639754-BXIGKWYA3HY47F-UNOF7XH7J7LH-EACNHX2RE3HN-QSOIKHZ5ZO65-4DKMHI5NWW63-54TFYTYUI37U-VWJNUG-TCG6UJCA3QCMEA-DEPLOYMENT.TRIAL-BDPZUN.TRIAL.EXPIRES.17.APR.2024";
// Add services to the container.
builder.Services.AddRazorPages();

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
