using BackgroundWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgroundWorkerService.Interfaces;

namespace BackgroundWorkerService.Interfaces
{
    public interface IDBServices
    {
        public void SaveHashesInDB(List<HashItem> hashItems);

        public List<HashItem> GetHashesInDB();
    }
}
