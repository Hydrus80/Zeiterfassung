using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Threading.Tasks;
using TimeCore.ModulService;

namespace TimeCore.Test
{
    [TestClass]
    public class CheckAccountModelService
    {
        //Felder
        public static MockData mockData = new MockData();

        [TestInitialize]
        public void CheckAccountModelServiceInitialize()
        {

        }

        /// <summary>
        /// Prueft ob Account 1 gefunden wird
        /// </summary>
        [TestMethod]
        public void GetAccountByNumber_Async_CheckExistingAccountOne_ResultIsFoundAccountOne()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.GetAccountByNumber_Async(1)).Returns(Task.FromResult<IAccountModel>(mockData.GetAccountOne()));

            //Act
            IAccountModel MockResult = AccountModulService.Object.GetAccountByNumber_Async(1).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountOne().ID);
            Assert.IsTrue(MockResult.Username == mockData.GetAccountOne().Username);

        }

        /// <summary>
        /// Prueft ob unbekannte Account nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetAccountByNumber_Async_CheckNonExistingAccount_ResultIsNULL()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.GetAccountByNumber_Async(2)).Returns(Task.FromResult<IAccountModel>(null));

            //Act
            IAccountModel MockResult = AccountModulService.Object.GetAccountByNumber_Async(2).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult is null);
         }

        /// <summary>
        /// Prueft ob Account 1 gefunden wird
        /// </summary>
        [TestMethod]
        public void GetAccountByNumber_CheckExistingAccountOne_ResultIsFoundAccountOne()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.GetAccountByNumber(1)).Returns(mockData.GetAccountOne());

            //Act
            IAccountModel MockResult = AccountModulService.Object.GetAccountByNumber(1);

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountOne().ID);
            Assert.IsTrue(MockResult.Username == mockData.GetAccountOne().Username);

        }

        /// <summary>
        /// Prueft ob unbekannte Account nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetAccountByNumber_CheckNonExistingAccount_ResultIsNULL()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.GetAccountByNumber(2));

            //Act
            IAccountModel MockResult = AccountModulService.Object.GetAccountByNumber(2);

            //Assert
            Assert.IsTrue(MockResult is null);
        }

        /// <summary>
        /// Prueft ob Account angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateAccount_Async_CreateNewAccount_ResultIsNewAccountWithID()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.CreateAccount_Async(It.IsAny<IAccountModel>())).Returns(Task.FromResult<IAccountModel>(mockData.GetAccountOne()));

            //Act
            IAccountModel MockResult = AccountModulService.Object.CreateAccount_Async(new AccountModel()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountOne().ID);

        }

        /// <summary>
        /// Prueft ob Account angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateAccount_CreateNewAccount_ResultIsNewAccountWithID()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.CreateAccount(It.IsAny<IAccountModel>())).Returns(mockData.GetAccountOne());

            //Act
            IAccountModel MockResult = AccountModulService.Object.CreateAccount(new AccountModel());

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountOne().ID);
        }

        /// <summary>
        /// Prueft ob Account angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void UpdateAccount_Async_CreateNewAccount_ResultIsNewAccountWithID()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.UpdateAccount_Async(It.IsAny<IAccountModel>())).Returns<IAccountModel>(x => Task.FromResult<IAccountModel>(x));

            //Act
            IAccountModel MockResult = AccountModulService.Object.UpdateAccount_Async(mockData.GetAccountTwo()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountTwo().ID);
            Assert.IsTrue(MockResult.Username == mockData.GetAccountTwo().Username);
            Assert.IsTrue(MockResult.LastUpdate == mockData.GetAccountTwo().LastUpdate);
        }

        /// <summary>
        /// Prueft ob Account aktualisiert wird, und diese dann zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void UpdateAccount_UpdateAccount_ResultIsUpdatedAccount()
        {
            //Arrange
            Mock<IAccountModulService> AccountModulService = new Mock<IAccountModulService>();
            AccountModulService.Setup(x => x.UpdateAccount(It.IsAny<IAccountModel>())).Returns<IAccountModel>(x => x);

            //Act
            IAccountModel MockResult = AccountModulService.Object.UpdateAccount(mockData.GetAccountTwo());

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetAccountTwo().ID);
            Assert.IsTrue(MockResult.Username == mockData.GetAccountTwo().Username);
            Assert.IsTrue(MockResult.LastUpdate == mockData.GetAccountTwo().LastUpdate);
        }
    }
}
