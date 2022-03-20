using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileDirectory.Utils
{
    public interface IHexByteConverter
    {
        byte[] ConvertHexStringToByteArray(string hex);
        string ConvertByteArrayToHexString(byte[] buffer);
    }
}
