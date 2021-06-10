using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    public class Serializer
    {
        public static void Save(string filePath, ICustomSerializable objToSerialize)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Save(writer, objToSerialize);
                }
            }
        }
        public static void Save(BinaryWriter writer, ICustomSerializable objToSerialize)
        {
            writer.Write(objToSerialize.GetType().FullName);
            objToSerialize.GetObjectData(writer);
        }
        public static ICustomSerializable Load(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return Load(reader);
                }
            }
        }
        public static ICustomSerializable Load(BinaryReader reader)
        {
            string FullName = reader.ReadString();
            Type newT = Type.GetType(FullName);
            ICustomSerializable c = (ICustomSerializable)Activator.CreateInstance(newT, null);
            c.SetObjectData(reader);
            return c;
        }
    }
}
