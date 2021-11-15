using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tools
{
    class SHA256
    {
        public static string Encryption(string content)
        {
            string result = "";            
            try
            {
                System.Security.Cryptography.SHA256 sha256 = new SHA256CryptoServiceProvider();
                result = Convert.ToBase64String(sha256.ComputeHash(Encoding.Default.GetBytes(content)));

                //SHA1 hash = SHA1.Create();
                //ASCIIEncoding encoder = new ASCIIEncoding();
                //byte[] combined = encoder.GetBytes(content);
                //hash.ComputeHash(combined);
                //result = Convert.ToBase64String(hash.Hash);

                //SHA1 sha1 = new SHA1CryptoServiceProvider();
                //byte[] bytes_in = encode.GetBytes(content);
                //byte[] bytes_out = sha1.ComputeHash(bytes_in);
                //sha1.Dispose();
                //string result = BitConverter.ToString(bytes_out);
                //result = result.Replace("-", "");
                //return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出錯：" + ex.Message);
            }
            return result;
        }
    }
}
