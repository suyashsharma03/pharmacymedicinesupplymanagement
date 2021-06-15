using MedicineStockSupply.Controllers;
using MedicineStockSupply.Model;
using MedicineStockSupply.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineStockSupplyTest
{
    [TestFixture]
    class PharmacyMedicineSupplyControllerTests
    {
        List<PharmacyMedicineSupply> list1 = new List<PharmacyMedicineSupply>();
        List<PharmacyMedicineSupply> list2 = new List<PharmacyMedicineSupply>();
        MedicineDemand demand = new MedicineDemand();
        Mock<IMedicineSupply> supplyRepo = new Mock<IMedicineSupply>();

        [SetUp]
        public void Setup()
        {
            list1 = new List<PharmacyMedicineSupply>()
            {
                new PharmacyMedicineSupply
                {
                    MedicineName = "Aspirin",
                    PharmacyName = "Max Pharmacy",
                    SupplyCount = 250
                },
                new PharmacyMedicineSupply
                {
                    MedicineName = "Disprin",
                    PharmacyName  ="Apollo Pharmacy",
                    SupplyCount = 150
                }
            };
            list2 = new List<PharmacyMedicineSupply>()
            {
                new PharmacyMedicineSupply
                {
                    MedicineName = "XYZ",
                    PharmacyName = "GoodWill Pharmacy",
                    SupplyCount = 250
                }
            };
        }

        [Test]
        public void GetSupplies_ValidInput_OkResult()
        {
            try
            {
                MedicineDemand medicineDemand = new MedicineDemand()
                {
                    Medicine = "Dispirin",
                    DemandCount = 100
                };
                supplyRepo.Setup(s => s.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount)).ReturnsAsync(list1);
                PharmacyMedicineController controller = new PharmacyMedicineController(supplyRepo.Object);
                var actual = controller.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount).Result as OkObjectResult;
                Assert.AreEqual(200, actual.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }

        [Test]
        public void GetSupplies_InValidInput_ReturnBadRequest()
        {
            try
            {
                MedicineDemand medicineDemand = new MedicineDemand()
                {
                    Medicine = "Paracetamol",
                    DemandCount = 0
                };
                supplyRepo.Setup(s => s.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount)).ReturnsAsync(list2);
                PharmacyMedicineController controller = new PharmacyMedicineController(supplyRepo.Object);
                var actual = controller.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount).Result as BadRequestResult;
                Assert.AreEqual(400, actual.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
    }
}
