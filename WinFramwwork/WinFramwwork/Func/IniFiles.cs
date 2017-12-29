using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace FineEx.Func
{
    
    /// <summary> 
    /// IniFiles���� 
    /// </summary> 
    public class IniFiles
    {
        public string FileName; //INI�ļ��� 
        //������дINI�ļ���API���� 
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        //��Ĺ��캯��������INI�ļ��� 
        public IniFiles(string AFileName)
        {
            FileName = AFileName;
        }


        //дINI�ļ� 
        public void WriteString(string Section, string Ident, string Value)
        {
          
            try
            {
                WritePrivateProfileString(Section, Ident, Value, FileName);
            }
            catch { }
        }
        //��ȡINI�ļ�ָ�� 
        public string ReadString(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            //�����趨0��ϵͳĬ�ϵĴ���ҳ���ı��뷽ʽ�������޷�֧������ 
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);

            return s.Trim().Replace("\0", "");
        }

        //������ 
        public int ReadInteger(string Section, string Ident, int Default)
        {
            string intStr = ReadString(Section, Ident, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(intStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        //д���� 
        public void WriteInteger(string Section, string Ident, int Value)
        {
            WriteString(Section, Ident, Value.ToString());
        }

        //������ 
        public bool ReadBool(string Section, string Ident, bool Default)
        {
            try
            {
                return Convert.ToBoolean(ReadString(Section, Ident, Convert.ToString(Default)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        //дBool 
        public void WriteBool(string Section, string Ident, bool Value)
        {
            WriteString(Section, Ident, Convert.ToString(Value));
        }

        //��Ini�ļ��У���ָ����Section�����е�����Ident��ӵ��б��� 
        public void ReadSection(string Section, StringCollection Idents)
        {
            Byte[] Buffer = new Byte[16384];
            //Idents.Clear(); 

            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0),
              FileName);
            //��Section���н��� 
            GetStringsFromBuffer(Buffer, bufLen, Idents);
        }

        private void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }
        //��Ini�ļ��У���ȡ���е�Sections������ 
        public void ReadSections(StringCollection SectionList)
        {
            //Note:�������Bytes��ʵ�֣�StringBuilderֻ��ȡ����һ��Section 
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer,
              Buffer.GetUpperBound(0), FileName);
            GetStringsFromBuffer(Buffer, bufLen, SectionList);
        }
        //��ȡָ����Section������Value���б��� 
        public void ReadSectionValues(string Section, NameValueCollection Values)
        {
            StringCollection KeyList = new StringCollection();
            ReadSection(Section, KeyList);
            Values.Clear();
            foreach (string key in KeyList)
            {
                Values.Add(key, ReadString(Section, key, ""));

            }
        }
        /**/
        ////��ȡָ����Section������Value���б��У� 
        //public void ReadSectionValues(string Section, NameValueCollection Values,char splitString)
        //{   string sectionValue;
        //    string[] sectionValueSplit;
        //    StringCollection KeyList = new StringCollection();
        //    ReadSection(Section, KeyList);
        //    Values.Clear();
        //    foreach (string key in KeyList)
        //    {   
        //        sectionValue=ReadString(Section, key, "");
        //        sectionValueSplit=sectionValue.Split(splitString);
        //        Values.Add(key, sectionValueSplit[0].ToString(),sectionValueSplit[1].ToString());

        //    }
        //}
        //���ĳ��Section 
        public void EraseSection(string Section)
        {
            // 
            if (!WritePrivateProfileString(Section, null, null, FileName))
            {
                throw (new ApplicationException("�޷����Ini�ļ��е�Section"));
            }
        }
        //ɾ��ĳ��Section�µļ� 
        public void DeleteKey(string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, FileName);
        }
        //Note:����Win9X����˵��Ҫʵ��UpdateFile�����������е�����д���ļ� 
        //��Win NT, 2000��XP�ϣ�����ֱ��д�ļ���û�л��壬���ԣ�����ʵ��UpdateFile 
        //ִ�����Ini�ļ����޸�֮��Ӧ�õ��ñ��������»������� 
        public void UpdateFile()
        {
            WritePrivateProfileString(null, null, null, FileName);
        }

        //���ĳ��Section�µ�ĳ����ֵ�Ƿ���� 
        public bool ValueExists(string Section, string Ident)
        {
            // 
            StringCollection Idents = new StringCollection();
            ReadSection(Section, Idents);
            return Idents.IndexOf(Ident) > -1;
        }

        //ȷ����Դ���ͷ� 
        ~IniFiles()
        {
            UpdateFile();
        }
    }

}
