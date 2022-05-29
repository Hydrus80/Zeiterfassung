using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class RightSQLRepository : IRightSQLRepository
    {
        public RightSQLRepository()
        { }

        public void DataSeeding()
        {
            //Context setzen
            using (SQLContext sqlContext = new SQLContext())
            {
                //Existiert Datenbank und ist der Zugriff gewährt?
                if (sqlContext.Database.CanConnect())
                {
                    //Zugriffsrechte setzen
                    var checkRight = sqlContext.Right.Where(b => b.RightID == (int)eRights.Administrator);
                    if (checkRight == null)
                        sqlContext.Right.Add(new RightModel { RightID = (int)eRights.Administrator, Description = Enum.GetName(typeof(eRights), eRights.Administrator) });
                    checkRight = sqlContext.Right.Where(b => b.RightID == (int)eRights.Buchhaltung);
                    if (checkRight == null)
                        sqlContext.Right.Add(new RightModel { RightID = (int)eRights.Buchhaltung, Description = Enum.GetName(typeof(eRights), eRights.Buchhaltung) });
                    checkRight = sqlContext.Right.Where(b => b.RightID == (int)eRights.Benutzer);
                    if (checkRight == null)
                        sqlContext.Right.Add(new RightModel { RightID = (int)eRights.Benutzer, Description = Enum.GetName(typeof(eRights), eRights.Benutzer) });

                    //Änderungen speichern
                    sqlContext.SaveChanges();
                }
                else
                {
                    ErrorHandlerLog.WriteError("RightSQLRepository.DataSeeding(): Keine Verbindung zur Datenbank möglich");
                }
            }
        }

      
    }
}
