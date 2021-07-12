using MedicineStockSupply.Controllers;
using MedicineStockSupply.Model;
using MedicineStockSupply.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MedicineStockSupplyTest
{
    [TestFixture]
    public class MedicineStockControllerTests
    {
        List<MedicineStock> list2 = new List<MedicineStock>();
        List<MedicineStock> list1 = new List<MedicineStock>();
        MedicineStock stock = new MedicineStock();

        [SetUp]
        public void Setup()
        {
            list1 = new List<MedicineStock>()
            {
                new MedicineStock
                {
                    MedicineName = "Dolo-650",
                    ChemicalComposition = "Acetaminophen",
                    TargetAilment = "General",
                    DateOfExpiry = "2021/09/21",
                    NumberOfTabletsInStock = 50
                },
                new MedicineStock
                {
                    MedicineName = "Cholecalciferol",
                    ChemicalComposition = "Carbonic Acid, Oxide, Vitamins",
                    TargetAilment = "Orthopaedics",
                    DateOfExpiry = "2022/01/05",
                    NumberOfTabletsInStock = 150
                },
                new MedicineStock
                {
                    MedicineName = "Gaviscon",
                    ChemicalComposition = "Aluminum, Oxide, Silicon, Sodium, " +"Carbonic Acid",
                    TargetAilment = "General",
                    DateOfExpiry = "2021/12/31",
                    NumberOfTabletsInStock = 120
                },
                new MedicineStock
                {
                    MedicineName = "Orthoherb",
                    ChemicalComposition = "ErandaVasa, Vilvam, Nimba, " +"Dusparsha, Amalaki, Manjishta, Nirgundi, Chithrakam, Kataka, Gokshurah, Shatavari, Pashanabheda",
                    TargetAilment = "Orthopaedics",
                    DateOfExpiry = "2022/04/05",
                    NumberOfTabletsInStock = 90
                },
                new MedicineStock
                {
                    MedicineName = "Hilact",
                    ChemicalComposition = "Vitamins, Calcium, Oxide",
                    TargetAilment = "Gynaecology",
                    DateOfExpiry = "2022/05/31",
                    NumberOfTabletsInStock = 340
                }
            };
            list2 = new List<MedicineStock>()
            {
                new MedicineStock
                {
                    MedicineName = "Cholecalciferol",
                    ChemicalComposition = "Carbonic Acid, Oxide, Vitamins",
                    TargetAilment = "XYZ",
                    DateOfExpiry = "2022/01/05",
                    NumberOfTabletsInStock = 1
                }
            };

        }

        [Test]
        public void MedicineStockInformation_ValidInput_OkRequest()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(p => p.GetAllMedicines()).Returns(list1);
            MedicineStockController controller = new MedicineStockController(mock.Object);
            var actual = controller.MedicineStockInformation() as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }


        [Test]
        public void MedicineStockInformation_InvalidInput_ReturnBadRequest()
        {
            try
            {
                Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
                mock.Setup(p => p.GetAllMedicines()).Returns(list2);
                MedicineStockController controller = new MedicineStockController(mock.Object);
                var actual = controller.MedicineStockInformation() as BadRequestResult;
                Assert.AreEqual(400, actual.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void MedicineStockInformation_NullInput_ThrowsException()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(p => p.GetAllMedicines()).Returns(list2);
            MedicineStockController controller = null;
            var ex = Assert.Throws<NullReferenceException>(() => controller.MedicineStockInformation());
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public void OneMedicineDetail_ValidInput_OkRequest()
        {
            try
            {
                Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
                mock.Setup(mp => mp.GetMedicineDetails(stock.MedicineName)).Returns(stock);
                MedicineStockController controller = new MedicineStockController(mock.Object);
                var actual = controller.MedicineStockInformation() as OkObjectResult;
                Assert.AreEqual(200, actual.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }

        [Test]
        public void OneMedicineDetail_InvalidInput_ReturnBadRequest()
        {
            try
            {
                Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
                mock.Setup(mp => mp.GetMedicineDetails(stock.MedicineName)).Returns(stock);
                MedicineStockController controller = new MedicineStockController(mock.Object);
                var actual = controller.MedicineStockInformation() as BadRequestResult;
                Assert.AreEqual(400, actual.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }

        [Test]
        public void OneMedicineDetail_NullInput_ThrowsException()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(mp => mp.GetMedicineDetails(stock.MedicineName)).Returns(stock);
            MedicineStockController controller = null;
            var ex = Assert.Throws<NullReferenceException>(() => controller.MedicineStockInformation());
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }



    }
}
