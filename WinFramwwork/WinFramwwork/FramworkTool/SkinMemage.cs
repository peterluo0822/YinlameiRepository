using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FineEx.Control.Forms
{
    public class Skin
    {
        /// <summary>
        /// 保存自定义皮肤方案
        /// </summary>
        public void Save()
        {

        }

        /// <summary>
        /// 读取自定义皮肤方案
        /// </summary>
        public void Read()
        {

        }

        /// <summary>
        /// 获取所有皮肤方案
        /// </summary>
        /// <returns></returns>
        public SkinMemage[] GetSkins()
        {

            return new SkinMemage[1];
        }

        /// <summary>
        /// 获取指定名称的皮肤方案
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SkinMemage GetSkin(string name)
        {
            foreach (SkinMemage skin in GetSkins())
            {
                if (skin.Name == name)
                    return skin;
            }
            return null;
        }








        public static T Get<T>(string path)
        {
            FileStream fs = null;
            T t;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                t = (T)xs.Deserialize(fs);
                fs.Close();
                return t;
            }
            catch
            {
                if (fs != null)
                    fs.Close();
                throw new Exception("Xml deserialization failed!");
            }
        }



        public static void Set<T>(string path, T t)
        {
            if (t == null)
                throw new Exception("Parameter humanResource is null!");
            
            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                xs.Serialize(fs, t);
                fs.Close();
            }
            catch
            {
                if (fs != null)
                    fs.Close();
                throw new Exception("Xml serialization failed!");
            }
        }


    }

    [Serializable]
    public class SkinMemage
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上面工具颜色
        /// </summary>
        public static ColorMemage TopColor { get; set; }

        /// <summary>
        /// 右边导航颜色
        /// </summary>
        public static ColorMemage LeftColor { get; set; }
    }


    [Serializable]
    public class ColorMemage
    {

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

    }

}
