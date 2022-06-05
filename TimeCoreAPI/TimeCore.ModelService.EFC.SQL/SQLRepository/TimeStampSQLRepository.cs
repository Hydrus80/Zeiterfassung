using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class TimeStampSQLRepository : ITimeStampSQLRepository
    {
        public TimeStampSQLRepository()
        { }

        public TimeStampModel AddTimeStampToDataSource(TimeStampModel newTimeStamp)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        TimeStampModel insertEntry = new TimeStampModel()
                        {
                            TimeStampYear = newTimeStamp.TimeStampYear,
                            TimeStampMonth = newTimeStamp.TimeStampMonth,
                            TimeStampDay = newTimeStamp.TimeStampDay,
                            TimeStampHour = newTimeStamp.TimeStampHour,
                            TimeStampMinute = newTimeStamp.TimeStampMinute,
                            TimeStampSecond = newTimeStamp.TimeStampSecond,
                            Account = newTimeStamp.Account,
                            LastUpdate = newTimeStamp.LastUpdate,
                        };
                        sqlContext.TimeStamp.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return insertEntry;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.AddTimeStampToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.AddTimeStampToDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<TimeStampModel> AddTimeStampToDataSourceAsync(TimeStampModel newTimeStamp)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        TimeStampModel insertEntry = new TimeStampModel()
                        {
                            TimeStampYear = newTimeStamp.TimeStampYear,
                            TimeStampMonth = newTimeStamp.TimeStampMonth,
                            TimeStampDay = newTimeStamp.TimeStampDay,
                            TimeStampHour = newTimeStamp.TimeStampHour,
                            TimeStampMinute = newTimeStamp.TimeStampMinute,
                            TimeStampSecond = newTimeStamp.TimeStampSecond,
                            Account = newTimeStamp.Account,
                            LastUpdate = newTimeStamp.LastUpdate,
                        };
                        sqlContext.TimeStamp.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return await Task.FromResult<TimeStampModel>(insertEntry).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.AddTimeStampToDataSourceAsync(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.AddTimeStampToDataSourceAsync(): {ex.Message}");
                return null;
            }
        }

        public List<TimeStampModel> GetTimeStampListFromDataSource(string userGUID, int selectedYear, int selectedMonth)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        //Init
                        List<TimeStampModel> returnValue = new List<TimeStampModel>();

                        //Liste holen
                        returnValue = sqlContext.TimeStamp.Where(s => s.Account.GUID == userGUID &&
                        s.TimeStampMonth == selectedMonth &&
                        s.TimeStampYear == selectedYear).ToList();

                        //Accounts ergänzen
                        for (int i = 0; i < returnValue.Count; i++)
                        {
                            TimeStampModel updateTimeStamp = returnValue[i];
                            updateTimeStamp.Account = sqlContext.Account.Find(updateTimeStamp.AccountID);
                        }
                        return returnValue;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.GetTimeStampListFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.GetTimeStampListFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<List<TimeStampModel>> GetTimeStampListFromDataSourceAsync(string userGUID, int selectedYear, int selectedMonth)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult(sqlContext.TimeStamp.Where(s => s.Account.GUID == userGUID &&
                         s.TimeStampMonth == selectedMonth &&
                        s.TimeStampYear == selectedYear).ToList()).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.GetTimeStampListToDataSourceAsync(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.GetTimeStampListToDataSourceAsync(): {ex.Message}");
                return null;
            }
        }
    }
}
