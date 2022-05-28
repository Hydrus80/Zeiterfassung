using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Threading.Tasks;
using TimeCore.ModulService;

namespace TimeCore.Test
{
    [TestClass]
    public class CheckFirmModelService
    {
        //Felder
        public static MockData mockData = new MockData();

        [TestInitialize]
        public void CheckFirmModelServiceInitialize()
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
            firmModulService.Setup(x => x.GetFirmByNumber_Async(1)).Returns(Task.FromResult<FirmModel>(mockData.GetFirmOne()));

            //Act
            FirmModel MockResult = firmModulService.Object.GetFirmByNumber_Async(1).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetFirmOne().ID);
            Assert.IsTrue(MockResult.Number == mockData.GetFirmOne().Number);

        }

        /// <summary>
        /// Prueft ob unbekannte Firma nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetFirmByNumber_Async_CheckNonExistingFirm_ResultIsNULL()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.GetFirmByNumber_Async(2)).Returns(Task.FromResult<FirmModel>(null));

            //Act
            FirmModel MockResult = firmModulService.Object.GetFirmByNumber_Async(2).GetAwaiter().GetResult();

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
            firmModulService.Setup(x => x.GetFirmByNumber(1)).Returns(mockData.GetFirmOne());

            //Act
            FirmModel MockResult = firmModulService.Object.GetFirmByNumber(1);

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetFirmOne().ID);
            Assert.IsTrue(MockResult.Number == mockData.GetFirmOne().Number);

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
            FirmModel MockResult = firmModulService.Object.GetFirmByNumber(2);

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
            firmModulService.Setup(x => x.CreateFirm_Async(It.IsAny<FirmModel>())).Returns(Task.FromResult<FirmModel>(mockData.GetFirmOne()));

            //Act
            FirmModel MockResult = firmModulService.Object.CreateFirm_Async(new FirmModel()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetFirmOne().ID);

        }

        /// <summary>
        /// Prueft ob Firma angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateFirm_CreateNewFirm_ResultIsNewFirmWithID()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.CreateFirm(It.IsAny<FirmModel>())).Returns(mockData.GetFirmOne());

            //Act
            FirmModel MockResult = firmModulService.Object.CreateFirm(new FirmModel());

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetFirmOne().ID);
        }

        /// <summary>
        /// Prueft ob Firma angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void UpdateFirm_Async_CreateNewFirm_ResultIsNewFirmWithID()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.UpdateFirm_Async(It.IsAny<FirmModel>())).Returns<FirmModel>(x => Task.FromResult<FirmModel>(x));

            //Act
            FirmModel MockResult = firmModulService.Object.UpdateFirm_Async(mockData.GetFirmTwo()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetFirmTwo().ID);
            Assert.IsTrue(MockResult.Name == mockData.GetFirmTwo().Name);
            Assert.IsTrue(MockResult.Number == mockData.GetFirmTwo().Number);
            Assert.IsTrue(MockResult.LastUpdate == mockData.GetFirmTwo().LastUpdate);
        }

        /// <summary>
        /// Prueft ob Firma aktualisiert wird, und diese dann zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void UpdateFirm_UpdateFirm_ResultIsUpdatedFirm()
        {
            //Arrange
            Mock<IFirmModulService> firmModulService = new Mock<IFirmModulService>();
            firmModulService.Setup(x => x.UpdateFirm(It.IsAny<FirmModel>())).Returns<FirmModel>(x => x);

            //Act
            FirmModel MockResult = firmModulService.Object.UpdateFirm(mockData.GetFirmTwo());

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetFirmTwo().ID);
            Assert.IsTrue(MockResult.Name == mockData.GetFirmTwo().Name);
            Assert.IsTrue(MockResult.Number == mockData.GetFirmTwo().Number);
            Assert.IsTrue(MockResult.LastUpdate == mockData.GetFirmTwo().LastUpdate);
        }
    }
}
