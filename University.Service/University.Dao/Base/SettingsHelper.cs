using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace University.Dao.Base
{
    public class SettingsHelper : IDisposable
    {
        #region Constructor
        public SettingsHelper()
        {
            CheckUniversityFolder();
        }
        #endregion

        #region Public Methods
        public bool FileExists(string strFileName)
        {
            string strPath = Path.Combine(UniversityFolderPath, strFileName);

            return File.Exists(strPath);
        }

        public StreamReader ReadSettings(string strFileName)
        {
            string strPath = Path.Combine(UniversityFolderPath, strFileName);

            return new StreamReader(strPath);
        }

        public void DeleteFile(string strFileName)
        {
            string strPath = Path.Combine(UniversityFolderPath, strFileName);

            if (File.Exists(strPath))
            {
                File.SetAttributes(strPath, FileAttributes.Normal);
                File.Delete(strPath);
            }
        }

        public string GetFilePath(string strFileName)
        {
            string strPath = Path.Combine(UniversityFolderPath, strFileName);
            return strPath;
        }
        #endregion

        #region Private Methods
        void CheckUniversityFolder()
        {
            if (!Directory.Exists(UniversityFolderPath))
            {
                Directory.CreateDirectory(UniversityFolderPath);
            }
        }

        public void Dispose()
        {
            //NOTHING TO DO
        }

        string UniversityFolderPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"University");
            }
        }
        #endregion
    }
}
