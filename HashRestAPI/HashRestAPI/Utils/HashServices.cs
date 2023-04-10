using HashRestAPI.Interfaces;
using HashRestAPI.Models;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;


namespace HashRestAPI.Utils
{
    public class HashServices : IHashServices
    {
        private int _hashStringCountPerBatch = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HashStringCountPerBatch"]);
        private int _batchCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["BatchCount"]);

        private static IRabbitServices? _rabbitServices;
        private static IDBServices? _dbServices;

        public HashServices(IDBServices dbServices, IRabbitServices rabbitServices) 
        {
            _dbServices = dbServices;
            _rabbitServices = rabbitServices;
        }

        public List<HashItem> GenerateHashes()
        {
            var hashList = new List<HashItem>();

            for(int i = 0; i < _hashStringCountPerBatch; i++)
            {
                var randomString = Guid.NewGuid().ToString();
                var sha1 = SHA1.Create();
                var hashtemp = sha1.ComputeHash(Encoding.UTF8.GetBytes(randomString));

                var sb = new StringBuilder(hashtemp.Length * 2);

                foreach (byte b in hashtemp)
                {
                    sb.Append(b.ToString("X2"));
                }


                hashList.Add(new HashItem()
                {
                    Sha1 = sb.ToString()
                });
            }

            return hashList;
        }

        public void PublishHashes(List<HashItem> hashItems)
        {
            _rabbitServices.SendMessage(hashItems);
        }

        public void PostHashes()
        {
            for (int i = 0; i < _batchCount; i++)
            {
                var hashItems = GenerateHashes();
                if (hashItems.Count > 0)
                {
                    PublishHashes(hashItems);
                }
                Thread.Sleep(500);
            }
        }

        public List<HashResult> GetHashes()
        {
            return _dbServices.GetHashes();
        }
    }
}
