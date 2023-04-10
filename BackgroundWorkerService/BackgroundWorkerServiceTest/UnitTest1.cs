using BackgroundWorkerService.Interfaces;
using BackgroundWorkerService.Models;
using Moq;

namespace BackgroundWorkerServiceTest
{
    [TestClass]
    public class UnitTest1
    {
        private IDBServices _dbServices;

        public UnitTest1()
        {
            // set up DBServices
            var hashResults = new List<HashItem>
            {
                new HashItem
                {
                    Sha1 = "AKJSHD@#!@JHKJHASDASDD123123"
                },

                new HashItem
                {
                    Sha1 = "ZXZXA@#!@JHKJHASDASDD123123"
                }
            };

            var mockDBServices = new Mock<IDBServices>();
            mockDBServices.Setup(p => p.GetHashesInDB()).Returns(hashResults);

            _dbServices = mockDBServices.Object;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = _dbServices.GetHashesInDB();

            Assert.IsTrue(result.Count == 2);
        }
    }
}