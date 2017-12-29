using System.Data;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FineEx.Func
{
    /// <summary>
    ///     DataSetProcess 的摘要说明
    /// </summary>
    public class DataSetProcess
    {
        public static byte[] GetBytesFromDataSet(DataSet ds)
        {
            byte[] bArrayResult = null;
            ds.RemotingFormat = SerializationFormat.Binary;
            var ms = new MemoryStream();
            IFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, ds);
            bArrayResult = Compress(ms.ToArray());
            ms.Close();
            ms.Dispose();
            return bArrayResult;
        }

        public static DataSet GetDataSetFromBytes(byte[] bArrayResult)
        {
            DataSet dsResult = null;
            var ms = new MemoryStream(Decompress(bArrayResult));
            ms.Position = 0;
            IFormatter bf = new BinaryFormatter();
            var obj = bf.Deserialize(ms);
            dsResult = (DataSet) obj;
            ms.Close();
            ms.Dispose();
            return dsResult;
        }

        public static DataTable GetDataTableFromBytes(byte[] bArrayResult)
        {
            var ds = GetDataSetFromBytes(bArrayResult);
            if (ds == null) return null;
            if (ds.Tables.Count > 0) return ds.Tables[0];
            return null;
        }

        public static byte[] Compress(byte[] buffer)
        {
            var ms = new MemoryStream();
            var gzipStrem = new GZipStream(ms, CompressionMode.Compress);
            gzipStrem.Write(buffer, 0, buffer.Length);
            gzipStrem.Close();
            gzipStrem.Dispose();
            return ms.ToArray();
        }

        public static byte[] Decompress(byte[] compressData)
        {
            var ms = new MemoryStream(compressData);
            var gzipStrem = new GZipStream(ms, CompressionMode.Decompress, true);
            var Retbuffer = new byte[1024];
            var temp = new MemoryStream();
            var buffer = new byte[1024];
            while (true)
            {
                var read = gzipStrem.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                temp.Write(buffer, 0, buffer.Length);
            }
            gzipStrem.Close();
            return temp.ToArray();
        }
    }
}