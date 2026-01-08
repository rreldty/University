using System.Reflection;
using System.IO;

namespace University.Dao.Base
{
    public class AssemblyExtender
    {
        public string ReadAssemblyString(string strPath)
        {
            string strFile = string.Empty;
            Assembly assem = this.GetType().Assembly;

            using (Stream _stream = assem.GetManifestResourceStream(strPath))
            {
                using (StreamReader sr = new StreamReader(_stream))
                {
                    strFile = sr.ReadToEnd();
                }
            }

            return strFile;
        }

        public void WriteAssemblyFile(string strFilePath, string strAssemblyPath)
        {
            string strFile = string.Empty;
            Assembly assem = this.GetType().Assembly;

            Stream _stream = assem.GetManifestResourceStream(strAssemblyPath);
            using (Stream s = File.Create(strFilePath))
            {
                _stream.CopyTo(s);
            }
        }
    }
}