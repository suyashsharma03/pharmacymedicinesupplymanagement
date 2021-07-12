using NUnit.Framework;
using System.IO;
using MedicalRepresentativeSchedule;
using MedicalRepresentativeSchedule.Services;
using System;
using MedicalRepresentativeSchedule.Model;

namespace MedicalRepresentativeScheduleTest
{
    [TestFixture]
    public class ScheduleRepoTest
    {
        IScheduleRepo<RepSchedule> repo;
        [SetUp]
        public void BaseWork()
        {
            repo = new ScheduleRepo();
        }

        [TearDown]
        public void EndWork()
        {
            repo = null;
        }
        [TestCase]
        public void GetList_Count_All()
        {
            int count = 0;
            try
            {
                using (StreamReader sr = new StreamReader(@"doctordetails.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                    }

                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            int actualCount = repo.GetList().Count;
            Assert.AreEqual(count,actualCount);
        }
        
        [TestCase("2021/07/31")]
        [TestCase("2021/07/30")]
        [TestCase("2021/07/02")]
        public void GetScheduleTest_Count_NotSunday(DateTime date)
        {
            int count = 0;
            try
            {
                using (StreamReader sr = new StreamReader(@"doctordetails.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if (values[6].Equals(date))
                            count++;
                    }

                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            int actualCount = repo.GetSchedule(date).Count;
        }

        [TestCase("2021/08/01")]
        public void GetScheduleTest_IsSunday(DateTime date)
        {
            var sundaySchedule = repo.GetSchedule(date);
            Assert.AreEqual(sundaySchedule, null);
        }
    }
}
