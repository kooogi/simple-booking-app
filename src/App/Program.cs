// Import required namespaces
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Build configuration from appsettings.json
var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

// Get the connection string from configuration
string? connectionString = config.GetConnectionString("DefaultConnection");





if (connectionString == null)
{
  Console.WriteLine("Connection string 'DefaultConnection' not found.");
}
else
{
  MainMenu.DisplayMenu();

}
