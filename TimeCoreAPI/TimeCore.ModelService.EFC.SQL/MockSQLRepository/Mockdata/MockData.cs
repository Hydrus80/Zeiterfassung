using Model;
using System;
using System.Collections.Generic;

namespace TimeCore.ModelService.EFC.SQL
{
    public class MockData
    {
        public DateTime mockDate = new DateTime(2020, 5, 21, 15, 40, 23);

        public FirmModel GetFirmOne()
        {
            return new FirmModel() { ID = 1, Name = "MockFirm", Number = 1, LastUpdate = mockDate };
        }

        public FirmModel GetFirmTwo()
        {
            return new FirmModel() { ID = 2, Name = "New", Number = 2, LastUpdate = mockDate };
        }

        public WorkshopModel GetWorkshopOne()
        {
            return new WorkshopModel() { ID = 1, Name = "MockWorkshop", Number = 1, LastUpdate = mockDate, Firm = GetFirmOne() };
        }
    
        public WorkshopModel GetWorkshopTwo()
        {
            return new WorkshopModel() { ID = 2, Name = "New", Number = 2, LastUpdate = mockDate, Firm = GetFirmOne() };
        }

        public AccountModel GetAccountOne()
        {
            return new AccountModel() { ID = 1, Username = "MockAccount", Password = "XXX", LastUpdate = mockDate, Workshop = GetWorkshopOne() };
        }

        public AccountModel GetAccountTwo()
        {
            return new AccountModel() { ID = 2, Username = "New", Password = "XXX", LastUpdate = mockDate, Workshop = GetWorkshopOne() };
        }

        public TimeStampModel GetStampInForAccountOne()
        {
            return new TimeStampModel() { 
                ID = 1, 
                Account = GetAccountOne(), 
                TimeStampYear = mockDate.Year,
                TimeStampMonth = mockDate.Month,
                TimeStampDay = mockDate.Day,
                TimeStampHour = mockDate.Hour,
                TimeStampMinute = mockDate.Minute,
                TimeStampSecond = mockDate.Second,
                LastUpdate = mockDate };
        }

        public TimeStampModel GetStampOutOneHourForAccountOne()
        {
            return new TimeStampModel() { 
                ID = 2, 
                Account = GetAccountOne(),
                TimeStampYear = mockDate.Year,
                TimeStampMonth = mockDate.Month,
                TimeStampDay = mockDate.Day,
                TimeStampHour = mockDate.Hour + 1,
                TimeStampMinute = mockDate.Minute,
                TimeStampSecond = mockDate.Second,
                LastUpdate = mockDate.AddHours(1) };
        }

        public List<TimeStampModel> GetTimeStampsForAccountOne()
        {
            List<TimeStampModel> returnList = new List<TimeStampModel>();
            returnList.Add(GetStampInForAccountOne());
            returnList.Add(GetStampOutOneHourForAccountOne());
            return returnList;
        }

        public AccountRightModel GetUserRightInForAccountOne()
        {
            return new AccountRightModel()
            {
                ID = 1,
                Account = GetAccountOne(),
                RightID = (int)eRights.Benutzer,
                LastUpdate = mockDate
            };
        }

        public AccountRightModel GetUserRightInForAccountTwo()
        {
            return new AccountRightModel()
            {
                ID = 2,
                Account = GetAccountTwo(),
                RightID = (int)eRights.Administrator,
                LastUpdate = mockDate
            };
        }

        public List<AccountRightModel> GetUserRights()
        {
            List<AccountRightModel> returnList = new List<AccountRightModel>();
            returnList.Add(GetUserRightInForAccountOne());
            returnList.Add(GetUserRightInForAccountTwo());
            return returnList;
        }

        public RightModel GetUserRight()
        {
            return new RightModel()
            {
                ID = 1,
                RightID = (int)eRights.Benutzer,
                LastUpdate = mockDate
            };
        }

        public RightModel GetAccountantRight()
        {
            return new RightModel()
            {
                ID = 2,
                RightID = (int)eRights.Buchhaltung,
                LastUpdate = mockDate
            };
        }

        public RightModel GetAdministratorRight()
        {
            return new RightModel()
            {
                ID = 3,
                RightID = (int)eRights.Administrator,
                LastUpdate = mockDate
            };
        }

        public List<RightModel> GetRights()
        {
            List<RightModel> returnList = new List<RightModel>();
            returnList.Add(GetUserRight());
            returnList.Add(GetAccountantRight());
            returnList.Add(GetAdministratorRight());
            return returnList;
        }

    }
}
