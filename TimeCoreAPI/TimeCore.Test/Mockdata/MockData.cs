using Model;
using System;

namespace TimeCore.Test
{
    public class MockData
    {
        public DateTime mockDate = new DateTime(2020, 5, 21, 15, 40, 0);

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

    }
}
