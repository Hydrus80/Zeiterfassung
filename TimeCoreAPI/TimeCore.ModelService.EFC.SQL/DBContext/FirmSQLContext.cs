using ConfigurationService.JSON;
using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace TimeCore.ModelService.EFC.SQL
{
    public class FirmSQLContext : DbContext
    {
        //private readonly string _connString = "User ID=sa;Password=Dotnet123!;Server=ENT-NB-005\\WERBASWEB;Database=Artikel;Trusted_Connection=True;MultipleActiveResultSets=true";
        //Update der Datenbank
        //https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx
        /*Open the Package Manager Console from the menu Tools -> NuGet Package Manager -> Package Manager Console in Visual Studio and execute the following command to add a migration.  */
        //Add-Migration Firm
        //Update-Database

        /// <summary>
        /// Konstruktor ohne Connectrionstringuebergabe,
        /// diese wird intern geholt
        /// </summary>
        public FirmSQLContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Init
            string connString = string.Empty;

            //Connection holen
            ConfigurationModulService m_ConfigurationModulService = new ConfigurationModulService();
            if (Environment.MachineName == "DESKTOP-QVQKVKM")
                connString = m_ConfigurationModulService.GetConfigurationStringValue("Config\\databasesettingshome.json", "ConnectionString");
            else
                connString = m_ConfigurationModulService.GetConfigurationStringValue("Config\\databasesettingswork.json", "ConnectionString");

            //ConnectionString gefunden?
            if (string.IsNullOrEmpty(connString)) throw new System.Exception("ConnectionString für die SQL-Datenbank ist leer");

            //Verbindung initialisieren
            optionsBuilder.UseSqlServer(connString);
        }

        public virtual DbSet<FirmModel> Firm { get; set; }
    }
}
