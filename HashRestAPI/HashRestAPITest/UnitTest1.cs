
using HashRestAPI.Interfaces;
using HashRestAPI.Models;
using HashRestAPI.Utils;
using Moq;

namespace HashRestAPITest
{
    [TestClass]
    public class UnitTest1
    {
        private IDBServices _dbServices;
        private IHashServices _hashServices;

        public UnitTest1()
        {
            // set up DBServices
            var mockDBServices = new Mock<IDBServices>();
            mockDBServices.Setup(p => p.GetHashes()).Returns(new List<HashResult>(){
                new HashResult
                {
                    date = "2023-04-09",
                    count = 12345
                },
                new HashResult
                {
                    date = "2023-04-08",
                    count = 123
                }

            });

            _dbServices = mockDBServices.Object;



            //set up HashServices
            var mockHashServices = new Mock<IHashServices>();
            mockHashServices.Setup(p => p.GenerateHashes()).Returns(new List<HashItem>()
            {
                new HashItem
                {
                    Sha1 = "ASDASD@123123Sasd"
                },
                new HashItem
                {
                    Sha1 = "ASDASzhashdSasd"
                }
            });

            mockHashServices.Setup(p => p.GetHashes()).Returns(new List<HashResult>(){
                new HashResult
                {
                    date = "2023-04-09",
                    count = 12345
                },
                new HashResult
                {
                    date = "2023-04-08",
                    count = 123
                }

            } 
            );

            _hashServices = mockHashServices.Object;


        }

        [TestMethod]
        public void Test_DBServices_GetHashes()
        {
            
            var result = _dbServices.GetHashes();

            Assert.IsTrue(result.Count == 2);


        }

        [TestMethod]
        public void Test_HashServices_GenerateHashes()
        {

            var result = _hashServices.GenerateHashes();

            Assert.IsTrue(result.Count == 2);


        }

        [TestMethod]
        public void Test_HashServices_GetHashes()
        {

            var result = _hashServices.GetHashes();

            Assert.IsTrue(result.Count == 2);


        }
    }
}