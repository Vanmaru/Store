using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
     public interface ICustomSerializable
    {
        void SetObjectData(BinaryReader stream);
        void GetObjectData(BinaryWriter stream);
    }
}
