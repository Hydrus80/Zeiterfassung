using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Threading.Tasks;
using TimeCore.FirmModulService;

namespace TimeCore.Test
{
    [TestClass]
    public class CheckFirmModelService
    {
        //Felder
        public static DateTime mockDate = new DateTime(2020,5,21,15,40,0 );

        [AssemblyInitialize]
        public static void CheckFirmModelServiceInitialize(TestContext testContext)
        {

        }

        /// <summary>
        /// Prueft ob Firma 1 gefunden wird
        /// </summary>
        [TestMethod]
        public void GetFirmByNumber_Async_CheckExistingFirmOne_ResultIsFoundFirmOne()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.GetFirmByNumber_Async(1)).Returns(Task.FromResult<IFirmModel>(new FirmModel() { ID = 1, Name = "MockFirm", Number = 1, LastUpdate = mockDate }));

            //Act
            IFirmModel MockResult = firmModulService.Object.GetFirmByNumber_Async(1).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 1);
            Assert.IsTrue(MockResult.Number == 1);

        }

        /// <summary>
        /// Prueft ob unbekannte Firma nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetFirmByNumber_Async_CheckNonExistingFirm_ResultIsNULL()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.GetFirmByNumber_Async(2)).Returns(Task.FromResult<IFirmModel>(null));

            //Act
            IFirmModel MockResult = firmModulService.Object.GetFirmByNumber_Async(2).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult is null);
         }

        /// <summary>
        /// Prueft ob Firma 1 gefunden wird
        /// </summary>
        [TestMethod]
        public void GetFirmByNumber_CheckExistingFirmOne_ResultIsFoundFirmOne()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.GetFirmByNumber(1)).Returns(new FirmModel() { ID = 1, Name = "MockFirm", Number = 1, LastUpdate = mockDate });

            //Act
            IFirmModel MockResult = firmModulService.Object.GetFirmByNumber(1);

            //Assert
            Assert.IsTrue(MockResult.ID == 1);
            Assert.IsTrue(MockResult.Number == 1);

        }

        /// <summary>
        /// Prueft ob unbekannte Firma nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetFirmByNumber_CheckNonExistingFirm_ResultIsNULL()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.GetFirmByNumber(2));

            //Act
            IFirmModel MockResult = firmModulService.Object.GetFirmByNumber(2);

            //Assert
            Assert.IsTrue(MockResult is null);
        }

        /// <summary>
        /// Prueft ob Firma angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateFirm_Async_CreateNewFirm_ResultIsNewFirmWithID()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.CreateFirm_Async(It.IsAny<IFirmModel>())).Returns(Task.FromResult<IFirmModel>(new FirmModel() { ID = 1 }));

            //Act
            IFirmModel MockResult = firmModulService.Object.CreateFirm_Async(new FirmModel()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == 1);

        }

        /// <summary>
        /// Prueft ob Firma angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateFirm_CreateNewFirm_ResultIsNewFirmWithID()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.CreateFirm(It.IsAny<IFirmModel>())).Returns(new FirmModel() { ID = 1 });

            //Act
            IFirmModel MockResult = firmModulService.Object.CreateFirm(new FirmModel());

            //Assert
            Assert.IsTrue(MockResult.ID == 1);
        }
    }
}
