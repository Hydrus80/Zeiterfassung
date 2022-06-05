using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModelService
{
    [TestClass]
    public class CheckTimeCoreModelService
    {
        //Felder
        public static MockData mockData = new MockData();
        public static ITimeStampModelService timeStampModelService;
        public static IAccountModelService accountModelService;
        public static ITimeCoreModelService timeCoreModelService;


        [AssemblyInitialize]
        public static void CheckTimeCoreModelServiceInitialize(TestContext testContext)
        {
            if (timeStampModelService is null)
                timeStampModelService = new TimeStampModelService(eDatabaseType.SQL, new MockTimeStampSQLRepository());
            if (accountModelService is null)
                accountModelService = new AccountModelService(eDatabaseType.SQL, new MockAccountSQLRepository());
            if (timeCoreModelService is null)
                timeCoreModelService = new TimeCoreModelService(eDatabaseType.SQL);
        }

        #region Login
        /// <summary>
        /// Prueft ob gueltiger Account gefunden wird
        /// </summary>
        [TestMethod]
        public void Authenticate_CheckExistingLogin_ResultIsFoundAccount()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = mockData.GetAccountOne().Password;

            //Act
            AccountModel MockResult = timeCoreModelService.Authenticate(accountUserName, accountPassword);

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountOne().ID);
            Assert.IsTrue(MockResult.Username == mockData.GetAccountOne().Username);
            Assert.IsTrue(MockResult.Password == mockData.GetAccountOne().Password);
            Assert.IsTrue(MockResult.Workshop.ID == mockData.GetAccountOne().Workshop.ID);
        }

        /// <summary>
        /// Prueft ob gueltiger Account gefunden wird
        /// </summary>
        [TestMethod]
        public void AuthenticateAsync_CheckExistingLogin_ResultIsFoundAccount()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = mockData.GetAccountOne().Password;
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Act
            AccountModel MockResult = timeCoreModelService.AuthenticateAsync(accountUserName, accountPassword).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountOne().ID);
            Assert.IsTrue(MockResult.Username == mockData.GetAccountOne().Username);
            Assert.IsTrue(MockResult.Password == mockData.GetAccountOne().Password);
            Assert.IsTrue(MockResult.Workshop.ID == mockData.GetAccountOne().Workshop.ID);

        }

        /// <summary>
        /// Prueft ob falsches Passwort abgefangen wird
        /// </summary>
        [TestMethod]
        public void Login_CheckExistingLoginWithWrongPassword_ResultIsEmtpy()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = "XXX";

            //Act
            AccountModel MockResult = timeCoreModelService.Authenticate(accountUserName, accountPassword);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Prueft ob falsches Passwort abgefangen wird
        /// </summary>
        [TestMethod]
        public void LoginAsync_CheckExistingLoginWithWrongPassword_ResultIsEmtpy()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = "XXX";

            //Act
            AccountModel MockResult = timeCoreModelService.AuthenticateAsync(accountUserName, accountPassword).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Prueft ob nicht bekannter Benutzer abgefangen wird
        /// </summary>
        [TestMethod]
        public void Login_CheckNonExistingLogin_ResultIsEmtpy()
        {
            //Init
            string accountUserName = "Uknown";
            string accountPassword = "XXX";

            //Act
            AccountModel MockResult = timeCoreModelService.Authenticate(accountUserName, accountPassword);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Prueft ob nicht bekannter Benutzer abgefangen wird
        /// </summary>
        [TestMethod]
        public void LoginAsync_CheckNonExistingLogin_ResultIsEmtpy()
        {
            //Init
            string accountUserName = "Uknown";
            string accountPassword = "XXX";

            //Act
            AccountModel MockResult = timeCoreModelService.AuthenticateAsync(accountUserName, accountPassword).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }
        #endregion

        #region StampIn
        /// <summary>
        /// Zeiterfassung mit gueltigem Account
        /// </summary>
        [TestMethod]
        public void StampIn_CheckExistingAccount_ResultIsTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int timeStampYear = mockData.GetStampInForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampInForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampInForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampInForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampInForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampInForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampIn(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetStampInForAccountOne().ID);
            Assert.IsTrue(MockResult.TimeStampYear == mockData.GetStampInForAccountOne().TimeStampYear);
            Assert.IsTrue(MockResult.TimeStampMonth == mockData.GetStampInForAccountOne().TimeStampMonth);
            Assert.IsTrue(MockResult.TimeStampDay == mockData.GetStampInForAccountOne().TimeStampDay);
            Assert.IsTrue(MockResult.TimeStampHour == mockData.GetStampInForAccountOne().TimeStampHour);
            Assert.IsTrue(MockResult.TimeStampMinute == mockData.GetStampInForAccountOne().TimeStampMinute);
            Assert.IsTrue(MockResult.TimeStampSecond == mockData.GetStampInForAccountOne().TimeStampSecond);
            Assert.IsTrue(MockResult.Account.ID == mockData.GetStampInForAccountOne().Account.ID);
        }

        /// <summary>
        ///  Zeiterfassung mit gueltigem Account
        /// </summary>
        [TestMethod]
        public void StampInAsync_CheckExistingAccount_ResultIsTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int timeStampYear = mockData.GetStampInForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampInForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampInForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampInForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampInForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampInForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampInAsync(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).GetAwaiter().GetResult();

            //Assert
            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetStampInForAccountOne().ID);
            Assert.IsTrue(MockResult.TimeStampYear == mockData.GetStampInForAccountOne().TimeStampYear);
            Assert.IsTrue(MockResult.TimeStampMonth == mockData.GetStampInForAccountOne().TimeStampMonth);
            Assert.IsTrue(MockResult.TimeStampDay == mockData.GetStampInForAccountOne().TimeStampDay);
            Assert.IsTrue(MockResult.TimeStampHour == mockData.GetStampInForAccountOne().TimeStampHour);
            Assert.IsTrue(MockResult.TimeStampMinute == mockData.GetStampInForAccountOne().TimeStampMinute);
            Assert.IsTrue(MockResult.TimeStampSecond == mockData.GetStampInForAccountOne().TimeStampSecond);
            Assert.IsTrue(MockResult.Account.ID == mockData.GetStampInForAccountOne().Account.ID);
        }

        /// <summary>
        /// Zeiterfassung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void StampIn_CheckUnknownAccount_ResultIsEmpty()
        {
            //Init
            string userGUID = string.Empty;
            int timeStampYear = mockData.GetStampInForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampInForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampInForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampInForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampInForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampInForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampIn(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        ///  Zeiterfassung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void StampInAsync_CheckUnknownAccount_ResultIsEmpty()
        {
            //Init
            string userGUID = string.Empty;
            int timeStampYear = mockData.GetStampInForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampInForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampInForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampInForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampInForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampInForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampInAsync(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }
        #endregion

        #region StampOut
        /// <summary>
        /// Zeiterfassung mit gueltigem Account
        /// </summary>
        [TestMethod]
        public void StampOut_CheckExistingAccount_ResultIsTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int timeStampYear = mockData.GetStampOutOneHourForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampOutOneHourForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampOutOneHourForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampOutOneHourForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampOutOneHourForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampOutOneHourForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampOut(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);

            //Assert
            Assert.IsTrue(MockResult.ID == 1);
            Assert.IsTrue(MockResult.TimeStampYear == mockData.GetStampOutOneHourForAccountOne().TimeStampYear);
            Assert.IsTrue(MockResult.TimeStampMonth == mockData.GetStampOutOneHourForAccountOne().TimeStampMonth);
            Assert.IsTrue(MockResult.TimeStampDay == mockData.GetStampOutOneHourForAccountOne().TimeStampDay);
            Assert.IsTrue(MockResult.TimeStampHour == mockData.GetStampOutOneHourForAccountOne().TimeStampHour);
            Assert.IsTrue(MockResult.TimeStampMinute == mockData.GetStampOutOneHourForAccountOne().TimeStampMinute);
            Assert.IsTrue(MockResult.TimeStampSecond == mockData.GetStampOutOneHourForAccountOne().TimeStampSecond);
            Assert.IsTrue(MockResult.Account.ID == mockData.GetStampOutOneHourForAccountOne().Account.ID);
        }

        /// <summary>
        ///  Zeiterfassung mit gueltigem Account
        /// </summary>
        [TestMethod]
        public void StampOutAsync_CheckExistingAccount_ResultIsTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int timeStampYear = mockData.GetStampOutOneHourForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampOutOneHourForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampOutOneHourForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampOutOneHourForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampOutOneHourForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampOutOneHourForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampOutAsync(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).GetAwaiter().GetResult();

            //Assert
            //Assert
            Assert.IsTrue(MockResult.ID == 1);
            Assert.IsTrue(MockResult.TimeStampYear == mockData.GetStampOutOneHourForAccountOne().TimeStampYear);
            Assert.IsTrue(MockResult.TimeStampMonth == mockData.GetStampOutOneHourForAccountOne().TimeStampMonth);
            Assert.IsTrue(MockResult.TimeStampDay == mockData.GetStampOutOneHourForAccountOne().TimeStampDay);
            Assert.IsTrue(MockResult.TimeStampHour == mockData.GetStampOutOneHourForAccountOne().TimeStampHour);
            Assert.IsTrue(MockResult.TimeStampMinute == mockData.GetStampOutOneHourForAccountOne().TimeStampMinute);
            Assert.IsTrue(MockResult.TimeStampSecond == mockData.GetStampOutOneHourForAccountOne().TimeStampSecond);
            Assert.IsTrue(MockResult.Account.ID == mockData.GetStampOutOneHourForAccountOne().Account.ID);
        }

        /// <summary>
        /// Zeiterfassung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void StampOut_CheckUnknownAccount_ResultIsEmpty()
        {
            //Init
            string userGUID = string.Empty;
            int timeStampYear = mockData.GetStampInForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampInForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampInForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampInForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampInForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampInForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampOut(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        ///  Zeiterfassung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void StampOutAsync_CheckUnknownAccount_ResultIsEmpty()
        {
            //Init
            string userGUID = string.Empty;
            int timeStampYear = mockData.GetStampInForAccountOne().TimeStampYear;
            int timeStampMonth = mockData.GetStampInForAccountOne().TimeStampMonth;
            int timeStampDay = mockData.GetStampInForAccountOne().TimeStampDay;
            int timeStampHour = mockData.GetStampInForAccountOne().TimeStampHour;
            int timeStampMinute = mockData.GetStampInForAccountOne().TimeStampMinute;
            int timeStampSecond = mockData.GetStampInForAccountOne().TimeStampSecond;

            //Act
            TimeStampModel MockResult = timeCoreModelService.StampOutAsync(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }
        #endregion

        #region GetStampTimesMonthList
        /// <summary>
        /// Zeitauflistung mit gueltigem Account und vorhandener Liste
        /// </summary>
        [TestMethod]
        public void GetStampTimesMonthList_CheckExistingAccountAndValidMonth_ResultIsListOfTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Act
            List<TimeStampModel> MockResult = timeCoreModelService.GetStampTimesMonthList(userGUID, selectedYear, selectedMonth);

            //Assert
            Assert.IsTrue(MockResult.Count == 2);
            Assert.IsTrue(MockResult[0].ID == mockData.GetStampInForAccountOne().ID);
            Assert.IsTrue(MockResult[1].ID == mockData.GetStampOutOneHourForAccountOne().ID);
        }

        /// <summary>
        /// Zeitauflistung mit gueltigem Account und vorhandener Liste
        /// </summary>
        [TestMethod]
        public void GetStampTimesMonthListAsync_CheckExistingAccountAndValidMonth_ResultIsListOfTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Act
            List<TimeStampModel> MockResult = timeCoreModelService.GetStampTimesMonthListAsync(userGUID, selectedYear, selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 2);
            Assert.IsTrue(MockResult[0].ID == mockData.GetStampInForAccountOne().ID);
            Assert.IsTrue(MockResult[1].ID == mockData.GetStampOutOneHourForAccountOne().ID);
        }

        /// <summary>
        /// Zeitauflistung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void GetStampTimesMonthList_CheckUnknownAccount_ResultIsEmptyList()
        {
            //Init
            string userGUID = string.Empty;
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Act
            List<TimeStampModel> MockResult = timeCoreModelService.GetStampTimesMonthList(userGUID, selectedYear, selectedMonth);

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        ///  Zeitauflistung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void GetStampTimesMonthListAsync_CheckUnknownAccount_ResultIsEmptyList()
        {
            //Init
            string userGUID = string.Empty;
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Act
            List<TimeStampModel> MockResult = timeCoreModelService.GetStampTimesMonthListAsync(userGUID, selectedYear, selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        /// Zeitauflistung mit gueltigem Account aber leerem Monat
        /// </summary>
        [TestMethod]
        public void GetStampTimesMonthList_CheckAccountWithEmtpyMonth_ResultIsEmptyList()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int selectedMonth = mockData.mockDate.Month - 1;
            int selectedYear = mockData.mockDate.Year;

            //Act
            List<TimeStampModel> MockResult = timeCoreModelService.GetStampTimesMonthList(userGUID, selectedYear, selectedMonth);

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        ///  Zeitauflistung mit gueltigem Account aber leerem Monat
        /// </summary>
        [TestMethod]
        public void GetStampTimesMonthListAsync_CheckAccountWithEmtpyMonth_ResultIsEmptyList()
        {
            //Init
            string userGUID = mockData.GetAccountOne().GUID;
            int selectedMonth = mockData.mockDate.Month - 1;
            int selectedYear = mockData.mockDate.Year;

            //Act
            List<TimeStampModel> MockResult = timeCoreModelService.GetStampTimesMonthListAsync(userGUID, selectedYear, selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }
        #endregion
    }
}
