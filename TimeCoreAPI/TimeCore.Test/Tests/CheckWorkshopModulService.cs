using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Threading.Tasks;
using TimeCore.ModulService;

namespace TimeCore.Test
{
    [TestClass]
    public class CheckWorkshopModulService
    {
        //Felder
        public static MockData mockData = new MockData();

        [TestInitialize]
        public void CheckWorkshopModelServiceInitialize()
        {

        }

        /// <summary>
        /// Prueft ob Filiale 1 gefunden wird
        /// </summary>
        [TestMethod]
        public void GetWorkshopByNumber_Async_CheckExistingWorkshopOne_ResultIsFoundWorkshopOne()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.GetWorkshopByNumber_Async(1)).Returns(Task.FromResult<WorkshopModel>(mockData.GetWorkshopOne()));

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.GetWorkshopByNumber_Async(1).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetWorkshopOne().ID);
            Assert.IsTrue(MockResult.Number == mockData.GetWorkshopOne().Number);

        }

        /// <summary>
        /// Prueft ob unbekannte Filiale nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetWorkshopByNumber_Async_CheckNonExistingWorkshop_ResultIsNULL()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.GetWorkshopByNumber_Async(2)).Returns(Task.FromResult<WorkshopModel>(null));

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.GetWorkshopByNumber_Async(2).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult is null);
         }

        /// <summary>
        /// Prueft ob Filiale 1 gefunden wird
        /// </summary>
        [TestMethod]
        public void GetWorkshopByNumber_CheckExistingWorkshopOne_ResultIsFoundWorkshopOne()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.GetWorkshopByNumber(1)).Returns(mockData.GetWorkshopOne());

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.GetWorkshopByNumber(1);

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetWorkshopOne().ID);
            Assert.IsTrue(MockResult.Number == mockData.GetWorkshopOne().Number);

        }

        /// <summary>
        /// Prueft ob unbekannte Filiale nicht gefunden wird 
        /// </summary>
        [TestMethod]
        public void GetWorkshopByNumber_CheckNonExistingWorkshop_ResultIsNULL()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.GetWorkshopByNumber(2));

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.GetWorkshopByNumber(2);

            //Assert
            Assert.IsTrue(MockResult is null);
        }

        /// <summary>
        /// Prueft ob Filiale angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateWorkshop_Async_CreateNewWorkshop_ResultIsNewWorkshopWithID()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.CreateWorkshop_Async(It.IsAny<WorkshopModel>())).Returns(Task.FromResult<WorkshopModel>(mockData.GetWorkshopOne()));

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.CreateWorkshop_Async(new WorkshopModel()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetWorkshopOne().ID);

        }

        /// <summary>
        /// Prueft ob Filiale angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void CreateWorkshop_CreateNewWorkshop_ResultIsNewWorkshopWithID()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.CreateWorkshop(It.IsAny<WorkshopModel>())).Returns(mockData.GetWorkshopOne());

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.CreateWorkshop(new WorkshopModel());

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetWorkshopOne().ID);
        }

        /// <summary>
        /// Prueft ob Filiale angelegt, und die ID zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void UpdateWorkshop_Async_CreateNewWorkshop_ResultIsNewWorkshopWithID()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.UpdateWorkshop_Async(It.IsAny<WorkshopModel>())).Returns<WorkshopModel>(x => Task.FromResult<WorkshopModel>(x));

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.UpdateWorkshop_Async(mockData.GetWorkshopTwo()).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetWorkshopTwo().ID);
            Assert.IsTrue(MockResult.Name == mockData.GetWorkshopTwo().Name);
            Assert.IsTrue(MockResult.Number == mockData.GetWorkshopTwo().Number);
            Assert.IsTrue(MockResult.LastUpdate == mockData.GetWorkshopTwo().LastUpdate);
        }

        /// <summary>
        /// Prueft ob Filiale aktualisiert wird, und diese dann zurueck gegeben wird
        /// </summary>
        [TestMethod]
        public void UpdateWorkshop_UpdateWorkshop_ResultIsUpdatedWorkshop()
        {
            //Arrange
            Mock<IWorkshopModulService> WorkshopModulService = new Mock<IWorkshopModulService>();
            WorkshopModulService.Setup(x => x.UpdateWorkshop(It.IsAny<WorkshopModel>())).Returns<WorkshopModel>(x => x);

            //Act
            WorkshopModel MockResult = WorkshopModulService.Object.UpdateWorkshop(mockData.GetWorkshopTwo());

            //Assert
            Assert.IsTrue(MockResult.ID == mockData.GetWorkshopTwo().ID);
            Assert.IsTrue(MockResult.Name == mockData.GetWorkshopTwo().Name);
            Assert.IsTrue(MockResult.Number == mockData.GetWorkshopTwo().Number);
            Assert.IsTrue(MockResult.LastUpdate == mockData.GetWorkshopTwo().LastUpdate);
        }
    }
}
