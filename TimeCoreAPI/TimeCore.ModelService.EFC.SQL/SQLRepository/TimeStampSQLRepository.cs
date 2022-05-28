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

        public async Task<TimeStampModel> AddTimeStampToDataSource_Async(TimeStampModel newTimeStamp)
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
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.AddTimeStampToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.AddTimeStampToDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public TimeStampModel GetTimeStampByIDFromDataSource(int searchTimeStampID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.TimeStamp.Find(searchTimeStampID);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.GetTimeStampByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.GetTimeStampByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<TimeStampModel> GetTimeStampByIDFromDataSource_Async(int searchTimeStampID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult<TimeStampModel>(sqlContext.TimeStamp.Find(searchTimeStampID)).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.GetTimeStampByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.GetTimeStampByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public TimeStampModel UpdateTimeStampToDataSource(TimeStampModel updateTimeStamp)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        TimeStampModel existingTimeStamp = sqlContext.TimeStamp.Find(updateTimeStamp.ID);
                        if (existingTimeStamp != null)
                        {
                            existingTimeStamp.ID = updateTimeStamp.ID;
                            existingTimeStamp.TimeStampYear = updateTimeStamp.TimeStampYear;
                            existingTimeStamp.TimeStampMonth = updateTimeStamp.TimeStampMonth;
                            existingTimeStamp.TimeStampDay = updateTimeStamp.TimeStampDay;
                            existingTimeStamp.TimeStampHour = updateTimeStamp.TimeStampHour;
                            existingTimeStamp.TimeStampMinute = updateTimeStamp.TimeStampMinute;
                            existingTimeStamp.TimeStampSecond = updateTimeStamp.TimeStampSecond;
                            existingTimeStamp.Account = updateTimeStamp.Account;
                            existingTimeStamp.LastUpdate = updateTimeStamp.LastUpdate;
                            sqlContext.SaveChanges();
                            return existingTimeStamp;
                        }
                        else
                            return AddTimeStampToDataSource(updateTimeStamp);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.UpdateTimeStampToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.UpdateTimeStampToDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<TimeStampModel> UpdateTimeStampToDataSource_Async(TimeStampModel updateTimeStamp)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        TimeStampModel existingTimeStamp = sqlContext.TimeStamp.Find(updateTimeStamp.ID);
                        if (existingTimeStamp != null)
                        {
                            existingTimeStamp.ID = updateTimeStamp.ID;
                            existingTimeStamp.TimeStampYear = updateTimeStamp.TimeStampYear;
                            existingTimeStamp.TimeStampMonth = updateTimeStamp.TimeStampMonth;
                            existingTimeStamp.TimeStampDay = updateTimeStamp.TimeStampDay;
                            existingTimeStamp.TimeStampHour = updateTimeStamp.TimeStampHour;
                            existingTimeStamp.TimeStampMinute = updateTimeStamp.TimeStampMinute;
                            existingTimeStamp.TimeStampSecond = updateTimeStamp.TimeStampSecond;
                            existingTimeStamp.Account = updateTimeStamp.Account;
                            existingTimeStamp.LastUpdate = updateTimeStamp.LastUpdate;
                            sqlContext.SaveChanges();
                            return await Task.FromResult(existingTimeStamp).ConfigureAwait(false);
                        }
                        else
                            return await AddTimeStampToDataSource_Async(updateTimeStamp).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.UpdateTimeStampToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.UpdateTimeStampToDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public List<TimeStampModel> GetTimeStampListFromDataSource(AccountModel userAccount, int selectedMonth)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.TimeStamp.Where(s => s.Account.ID == userAccount.ID &&
                        s.TimeStampMonth == selectedMonth).ToList();
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

        public async Task<List<TimeStampModel>> GetTimeStampListFromDataSource_Async(AccountModel userAccount, int selectedMonth)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult(sqlContext.TimeStamp.Where(s => s.Account.ID == userAccount.ID &&
                         s.TimeStampMonth == selectedMonth).ToList()).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("TimeStampSQLRepository.GetTimeStampListToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"TimeStampSQLRepository.GetTimeStampListToDataSource_Async(): {ex.Message}");
                return null;
            }
        }
    }
}
