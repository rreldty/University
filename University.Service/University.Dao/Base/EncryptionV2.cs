using System;
using System.Text;
using System.Threading;

namespace University.Dao.Base
{
    public class EncryptionV2
    {
        #region Private
        private string ByteArrayToString(byte[] bytData)
        {
            string str;
            try
            {
                int num2 = bytData.Length - 1;
                StringBuilder builder = new StringBuilder();
                int index = 0;
                while (index <= num2)
                {
                    builder.Append(Convert.ToChar(bytData[index]));
                    Math.Min(Interlocked.Increment(ref index), index - 1);
                }
                str = builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }

        private byte[] StringToByteArray(string strData)
        {
            byte[] buffer;
            try
            {
                int num2 = strData.Length - 1;
                byte[] buffer2 = new byte[num2 + 1];
                int index = 0;
                while (index <= num2)
                {
                    buffer2[index] = Convert.ToByte(Convert.ToChar(strData.Substring(index, 1)));
                    Math.Min(Interlocked.Increment(ref index), index - 1);
                }
                buffer = buffer2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }
        #endregion

        #region Public
        public string Decrypt(string strRawText)
        {
            string str;
            try
            {
                byte[] bytData = Convert.FromBase64String(strRawText);
                str = this.ByteArrayToString(bytData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }

        public string Encrypt(string strRawText)
        {
            string str;
            try
            {
                str = Convert.ToBase64String(this.StringToByteArray(strRawText));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        #endregion
    }
}
