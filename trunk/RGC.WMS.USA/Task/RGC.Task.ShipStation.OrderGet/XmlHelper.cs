using System;
using System.IO;
using System.Xml.Serialization;

namespace RGC.Task.ShipStation.OrderGet
{
    /// <summary>
    /// shane 2020/4/20 11:57:42
    /// </summary>
    public class XmlHelper
    {
        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="xml">XML字符串</param>
        /// <returns></returns> 
        public static object Deserialize(Type type, string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="type"></param> 
        /// <param name="xml"></param> 
        /// <returns></returns> 

        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
    }
}
