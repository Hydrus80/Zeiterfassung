using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;
using TimeCore.ModulService;

namespace TimeCore.Test
{
    [TestClass]
    public class CheckTimeCoreModulService_Mock
    {
        //Felder
        public static MockData mockData = new MockData();

        [TestInitialize]
        public void CheckTimeCoreModulServiceInitialize()
        {

        }

        #region Login
        /// <summary>
        /// Prueft ob gueltiger Account gefunden wird
        /// </summary>
        [TestMethod]
        public void Login_CheckExistingLogin_ResultIsFoundAccount()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = mockData.GetAccountOne().Password;
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.Login(accountUserName, accountPassword, workshopID)).Returns(mockData.GetAccountOne());

            //Act
            AccountModel MockResult = WorkshopModulService.Object.Login(accountUserName, accountPassword, workshopID);

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
        public void LoginAsync_CheckExistingLogin_ResultIsFoundAccount()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = mockData.GetAccountOne().Password;
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.LoginAsync(accountUserName, accountPassword, workshopID)).Returns(Task.FromResult<AccountModel>(mockData.GetAccountOne()));

            //Act
            AccountModel MockResult = WorkshopModulService.Object.LoginAsync(accountUserName, accountPassword, workshopID).GetAwaiter().GetResult();

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
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.Login(accountUserName, accountPassword, workshopID)).Returns(new AccountModel());

            //Act
            AccountModel MockResult = WorkshopModulService.Object.Login(accountUserName, accountPassword, workshopID);

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
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.LoginAsync(accountUserName, accountPassword, workshopID)).Returns(Task.FromResult<AccountModel>(new AccountModel()));

            //Act
            AccountModel MockResult = WorkshopModulService.Object.LoginAsync(accountUserName, accountPassword, workshopID).GetAwaiter().GetResult();

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
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.Login(accountUserName, accountPassword, workshopID)).Returns(new AccountModel());

            //Act
            AccountModel MockResult = WorkshopModulService.Object.Login(accountUserName, accountPassword, workshopID);

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
            int workshopID = mockData.GetAccountOne().Workshop.ID;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.LoginAsync(accountUserName, accountPassword, workshopID)).Returns(Task.FromResult<AccountModel>(new AccountModel()));

            //Act
            AccountModel MockResult = WorkshopModulService.Object.LoginAsync(accountUserName, accountPassword, workshopID).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Prueft ob nicht falsche Filiale abgefangen wird
        /// </summary>
        [TestMethod]
        public void Login_CheckExistingLoginWithWrongWorkshop_ResultIsEmpty()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = mockData.GetAccountOne().Password;
            int workshopID = 9;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.Login(accountUserName, accountPassword, workshopID)).Returns(new AccountModel());

            //Act
            AccountModel MockResult = WorkshopModulService.Object.Login(accountUserName, accountPassword, workshopID);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Prueft ob nicht falsche Filiale abgefangen wird
        /// </summary>
        [TestMethod]
        public void LoginAsync_CheckExistingLoginWithWrongWorkshop_ResultIsEmpty()
        {
            //Init
            string accountUserName = mockData.GetAccountOne().Username;
            string accountPassword = mockData.GetAccountOne().Password;
            int workshopID = 9;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.LoginAsync(accountUserName, accountPassword, workshopID)).Returns(Task.FromResult<AccountModel>(new AccountModel()));

            //Act
            AccountModel MockResult = WorkshopModulService.Object.LoginAsync(accountUserName, accountPassword, workshopID).GetAwaiter().GetResult();

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
            string userGUID = mockData.GetAccountOne();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampIn(userAccount)).Returns(mockData.GetStampInForAccountOne());

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampIn(userAccount);

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
            string userGUID = mockData.GetAccountOne();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampInAsync(userAccount)).Returns(Task.FromResult<TimeStampModel>(mockData.GetStampInForAccountOne()));

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampInAsync(userAccount).GetAwaiter().GetResult();

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
            string userGUID = new AccountModel();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampIn(userAccount)).Returns(new TimeStampModel());

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampIn(userAccount);

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
            string userGUID = new AccountModel();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampInAsync(userAccount)).Returns(Task.FromResult<TimeStampModel>(new TimeStampModel()));

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampInAsync(userAccount).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Zeiterfassung mit gueltigem Account aber falschem Passwort
        /// </summary>
        [TestMethod]
        public void StampIn_CheckExistingLoginWithWrongPassword_ResultIsEmpty()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            userAccount.Password = "XXX";

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampIn(userAccount)).Returns(new TimeStampModel());

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampIn(userAccount);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        ///  Zeiterfassung mit gueltigem Account aber falschem Passwort
        /// </summary>
        [TestMethod]
        public void StampInAsync_CheckExistingLoginWithWrongPassword_ResultIsEmpty()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            userAccount.Password = "XXX";

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampInAsync(userAccount)).Returns(Task.FromResult<TimeStampModel>(new TimeStampModel()));

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampInAsync(userAccount).GetAwaiter().GetResult();

            //Assert
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
            string userGUID = mockData.GetAccountOne();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampOut(userAccount)).Returns(mockData.GetStampOutOneHourForAccountOne());

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampOut(userAccount);

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetStampOutOneHourForAccountOne().ID);
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
            string userGUID = mockData.GetAccountOne();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampOutAsync(userAccount)).Returns(Task.FromResult<TimeStampModel>(mockData.GetStampOutOneHourForAccountOne()));

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampOutAsync(userAccount).GetAwaiter().GetResult();

            //Assert
            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetStampOutOneHourForAccountOne().ID);
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
            string userGUID = new AccountModel();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampOut(userAccount)).Returns(new TimeStampModel());

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampOut(userAccount);

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
            string userGUID = new AccountModel();

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampOutAsync(userAccount)).Returns(Task.FromResult<TimeStampModel>(new TimeStampModel()));

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampOutAsync(userAccount).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        /// Zeiterfassung mit gueltigem Account aber falschem Passwort
        /// </summary>
        [TestMethod]
        public void StampOut_CheckExistingLoginWithWrongPassword_ResultIsEmpty()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            userAccount.Password = "XXX";

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampOut(userAccount)).Returns(new TimeStampModel());

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampOut(userAccount);

            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }

        /// <summary>
        ///  Zeiterfassung mit gueltigem Account aber falschem Passwort
        /// </summary>
        [TestMethod]
        public void StampOutAsync_CheckExistingLoginWithWrongPassword_ResultIsEmpty()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            userAccount.Password = "XXX";

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.StampOutAsync(userAccount)).Returns(Task.FromResult<TimeStampModel>(new TimeStampModel()));

            //Act
            TimeStampModel MockResult = WorkshopModulService.Object.StampOutAsync(userAccount).GetAwaiter().GetResult();

            //Assert
            //Assert
            Assert.IsTrue(MockResult.ID == 0);
        }
        #endregion

        #region GetStampTimesList
        /// <summary>
        /// Zeitauflistung mit gueltigem Account und vorhandener Liste
        /// </summary>
        [TestMethod]
        public void GetStampTimesList_CheckExistingAccountAndValidMonth_ResultIsListOfTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

           //Arrange
           Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesList(userAccount, selectedYear, selectedMonth)).Returns(mockData.GetTimeStampsForAccountOne());

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesList(userAccount, selectedYear, selectedMonth);

            //Assert
            Assert.IsTrue(MockResult.Count == 2);
            Assert.IsTrue(MockResult[0].ID == mockData.GetStampInForAccountOne().ID);
            Assert.IsTrue(MockResult[1].ID == mockData.GetStampOutOneHourForAccountOne().ID);
        }

        /// <summary>
        /// Zeitauflistung mit gueltigem Account und vorhandener Liste
        /// </summary>
        [TestMethod]
        public void GetStampTimesListAsync_CheckExistingAccountAndValidMonth_ResultIsListOfTimeStamp()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesListAsync(userAccount, selectedYear, selectedMonth)).Returns(Task.FromResult<List<TimeStampModel>>(mockData.GetTimeStampsForAccountOne()));

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesListAsync(userAccount, selectedYear, selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 2);
            Assert.IsTrue(MockResult[0].ID == mockData.GetStampInForAccountOne().ID);
            Assert.IsTrue(MockResult[1].ID == mockData.GetStampOutOneHourForAccountOne().ID);
        }

        /// <summary>
        /// Zeitauflistung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void GetStampTimesList_CheckUnknownAccount_ResultIsEmptyList()
        {
            //Init
            string userGUID = new AccountModel();
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesList(userAccount, selectedYear, selectedMonth)).Returns(new List<TimeStampModel>());

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesList(userAccount, selectedYear, selectedMonth);

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        ///  Zeitauflistung mit ungueltigem Account
        /// </summary>
        [TestMethod]
        public void GetStampTimesListAsync_CheckUnknownAccount_ResultIsEmptyList()
        {
            //Init
            string userGUID = new AccountModel();
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesListAsync(userAccount, selectedYear, selectedMonth)).Returns(Task.FromResult<List<TimeStampModel>>(new List<TimeStampModel>()));

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesListAsync(userAccount, selectedYear, selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        /// Zeitauflistung mit gueltigem Account aber leerem Monat
        /// </summary>
        [TestMethod]
        public void GetStampTimesList_CheckAccountWithEmtpyMonth_ResultIsEmptyList()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            int selectedMonth = mockData.mockDate.Month - 1;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesList(userAccount, selectedYear, selectedMonth)).Returns(new List<TimeStampModel>());

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesList(userAccount, selectedYear, selectedMonth);

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        ///  Zeitauflistung mit gueltigem Account aber leerem Monat
        /// </summary>
        [TestMethod]
        public void GetStampTimesListAsync_CheckAccountWithEmtpyMonth_ResultIsEmptyList()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            int selectedMonth = mockData.mockDate.Month - 1;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesListAsync(userAccount, selectedYear, selectedMonth)).Returns(Task.FromResult<List<TimeStampModel>>(new List<TimeStampModel>()));

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesListAsync(userAccount, selectedYear,  selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        /// Zeitauflistung mit gueltigem Account aber falschem Passwort
        /// </summary>
        [TestMethod]
        public void GetStampTimesList_CheckExistingLoginWithWrongPassword_ResultIsEmptyList()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            userAccount.Password = "XXX";
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesList(userAccount, selectedYear, selectedMonth)).Returns(new List<TimeStampModel>());

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesList(userAccount, selectedYear, selectedMonth);
            
            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }

        /// <summary>
        ///  Zeitauflistung mit gueltigem Account aber falschem Passwort
        /// </summary>
        [TestMethod]
        public void GetStampTimesListAsync_CheckExistingLoginWithWrongPassword_ResultIsEmptyList()
        {
            //Init
            string userGUID = mockData.GetAccountOne();
            userAccount.Password = "XXX";
            int selectedMonth = mockData.mockDate.Month;
            int selectedYear = mockData.mockDate.Year;

            //Arrange
            Mock<ITimeCoreModulService> WorkshopModulService = new Mock<ITimeCoreModulService>();
            WorkshopModulService.Setup(x => x.GetStampTimesListAsync(userAccount, selectedYear,selectedMonth)).Returns(Task.FromResult<List<TimeStampModel>>(new List<TimeStampModel>()));

            //Act
            List<TimeStampModel> MockResult = WorkshopModulService.Object.GetStampTimesListAsync(userAccount, selectedYear, selectedMonth).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.Count == 0);
        }
        #endregion

    }
}
