using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchPilot.Logic.Interfaces
{
    public interface IImageLogic
    {
        Task<String> UploadImage(byte[] fileData, string fileName);
    }
}
