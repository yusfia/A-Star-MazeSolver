using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Runtime.Serialization;

namespace TheLostPawn
{
    class SaveandLoad
    {
        public void Serialize(Map lst,String filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    IFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, lst);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public Map Deserialize(String filename)
        {
            Map lst = null ;
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open,FileAccess.Read,FileShare.Read))
                {
                    IFormatter bin = new BinaryFormatter();
                    stream.Position = 0;
                    lst = (Map)bin.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return lst;
        }
    }
}
