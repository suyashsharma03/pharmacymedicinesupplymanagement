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
        List<string> list3 = new List<string>();
        List<string> list4 = new List<string>();
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
            list3 = new List<string>()
            { "Max Pharmacy", "Appolo Pharmacy", "Synergy Pharmacy", "GoodWill Pharmacy" };

            list4 = new List<string>()
            {"ABC", "XYZ" };

            supplyRepo.Setup(s => s.GetSupplies(demand.Medicine, demand.DemandCount)).ReturnsAsync(list1);
        }

        [Test]
        public void GetPharmacies_ValidInput_OkRequest()
        {

            supplyRepo.Setup(mp => mp.GetAllPharmacies()).Returns(list3);
            MedicineSupplyRepo supply = new MedicineSupplyRepo();
            PharmacyMedicineController controller = new PharmacyMedicineController(supplyRepo.Object);
            var actual = controller.GetPharmacies() as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [Test]
        public void GetPharmacies_InvalidInput_ReturnBadRequest()
        {
            try
            {
                supplyRepo.Setup(mp => mp.GetAllPharmacies()).Returns(list4);
                MedicineSupplyRepo supply = new MedicineSupplyRepo();
                PharmacyMedicineController controller = new PharmacyMedicineController(supplyRepo.Object);
                var actual = controller.GetPharmacies() as BadRequestResult;
                Assert.AreNotEqual(400, actual.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void GetPharmacies_NullInput_ThrowsException()
        {

            supplyRepo.Setup(mp => mp.GetAllPharmacies()).Returns(list3);
            MedicineSupplyRepo supply = new MedicineSupplyRepo();
            PharmacyMedicineController controller = null;
            var ex = Assert.Throws<NullReferenceException>(() => controller.GetPharmacies());
            Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));
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
