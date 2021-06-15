using MedicineStockSupply.Model;
using MedicineStockSupply.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicineStockSupplyTest
{
    [TestFixture]
    class MedicineSupplyRepoTests
    {
        List<PharmacyMedicineSupply> list1 = new List<PharmacyMedicineSupply>();
        MedicineDemand demand = new MedicineDemand();
        Mock<IMedicineSupply> supplyRepo;

        [SetUp]
        public void Setup()
        {
            list1 = new List<PharmacyMedicineSupply>()
            {
                new PharmacyMedicineSupply
                {
                    MedicineName="Aspirin",
                    PharmacyName="Apollo Pharmacy",
                    SupplyCount=50
                },
                new PharmacyMedicineSupply
                {
                    MedicineName="Aspirin",
                    PharmacyName="Max Pharmacy",
                    SupplyCount=50
                }
            };

            demand = new MedicineDemand()
            {
                Medicine = "Aspirin",
                DemandCount = 100
            };
            supplyRepo = new Mock<IMedicineSupply>();
            supplyRepo.Setup(s => s.GetSupplies(demand.Medicine, demand.DemandCount)).ReturnsAsync(list1);
        }

        [Test]
        public async Task GetSupplies_ValidInput_OkRequest()
        {
            MedicineDemand medicineDemand = new MedicineDemand()
            {
                Medicine = "Aspirin",
                DemandCount = 100
            };
            IMedicineSupply repo = supplyRepo.Object;
            var actual = await repo.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount);
            Assert.IsNotNull(actual);
        }

        [Test]
        public async Task GetSupplies_InvalidDemand_ReturnBadRequest()
        {
            MedicineDemand medicineDemand = new MedicineDemand()
            {
                Medicine = "Aspirin",
                DemandCount = 0
            };
            IMedicineSupply repo = supplyRepo.Object;
            var actual = await repo.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount);
            Assert.IsNull(actual);
        }

        [Test]
        public async Task GetSupplies_InvalidMedicineName_ReturnBadRequest()
        {
            MedicineDemand medicineDemand = new MedicineDemand()
            {
                Medicine = "ABC",
                DemandCount = 100
            };
            IMedicineSupply repo = supplyRepo.Object;
            var actual = await repo.GetSupplies(medicineDemand.Medicine, medicineDemand.DemandCount);
            Assert.IsNull(actual);
        }
    }
}
