using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services
{
    public interface IEncryptionService
    {
        string EncryptText(string plainText);
        string DecryptText(string plainText);
    }
}
