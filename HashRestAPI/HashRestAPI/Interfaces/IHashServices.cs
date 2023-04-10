using HashRestAPI.Models;

namespace HashRestAPI.Interfaces
{
    public interface IHashServices
    {
        public List<HashItem> GenerateHashes();
        public void PublishHashes(List<HashItem> hashItems);
        public List<HashResult> GetHashes();
        public void PostHashes();
    }
}
