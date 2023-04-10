using HashRestAPI.Models;

namespace HashRestAPI.Interfaces
{
    public interface IDBServices
    {
        public List<HashResult> GetHashes();
    }
}
