using Microsoft.EntityFrameworkCore.Migrations;

namespace KH.Pepper.Core.Infra.DataBase
{
    public static class ScriptDbMigration
    {
        public static void ExecuteScript(this MigrationBuilder migrationBuilder, string script)
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, script);

            migrationBuilder.Sql($"PRINT 'Executing statements in : ... {file.Substring(AppDomain.CurrentDomain.BaseDirectory.Length)}'");
            migrationBuilder.Sql(File.ReadAllText(file));
             
        }
    }
}
