using MedicineStockSupply.Model;
using MedicineStockSupply.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineStockSupplyTest
{
    [TestFixture]
    class MedicineRepoTests
    {
        List<MedicineStock> list2 = new List<MedicineStock>();
        List<MedicineStock> list1 = new List<MedicineStock>();
        MedicineStock stock = new MedicineStock();
        string medicine = "";


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
                    MedicineName = "Cyclopam",
                    ChemicalComposition = "Paracetamol, Dicyclomine",
                    TargetAilment = "Gynaecology",
                    DateOfExpiry = "2021/07/01",
                    NumberOfTabletsInStock = 40
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
        public void MedicineStockDetails_ValidInput_OkRequest()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(mp => mp.GetAllMedicines()).Returns(list1);
            MedicineRepo repo = new MedicineRepo();
            List<MedicineStock> result = repo.GetAllMedicines();
            Assert.AreEqual(list1.Count, result.Count);
        }

        [Test]
        public void MedicineStockDetails_InvalidInput_ReturnBadRequest()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(mp => mp.GetAllMedicines()).Returns(list2);
            MedicineRepo repo = new MedicineRepo();
            List<MedicineStock> result = repo.GetAllMedicines();
            Assert.AreNotEqual(list2.Count, result.Count);
        }

        [Test]
        public void OneMedicineDetails_ValidInput_OkRequest()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(mp => mp.GetMedicineDetails(stock.MedicineName)).Returns(stock);
            MedicineRepo repo = new MedicineRepo();
            var actual = repo.GetMedicineDetails(stock.MedicineName);
            Assert.AreEqual(stock.MedicineName, actual);
        }

        [Test]
        public void OneMedicineDetails_InvalidInput_ReturnBadRequest()
        {
            Mock<IMedicineRepo<MedicineStock>> mock = new Mock<IMedicineRepo<MedicineStock>>();
            mock.Setup(mp => mp.GetMedicineDetails(stock.MedicineName)).Returns(stock);
            MedicineRepo repo = new MedicineRepo();
            var actual = repo.GetMedicineDetails(stock.MedicineName);
            Assert.AreNotEqual(medicine, actual);
        }

    }
}
