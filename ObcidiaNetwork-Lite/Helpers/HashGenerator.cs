using System.Text;

namespace ObcidiaNetwork.Helpers
{
    public class HashGenerator
    {
        public static string GenerateString(string key)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create ())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes (key);
                byte[] hashBytes = md5.ComputeHash (inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder ();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append (hashBytes[i].ToString ("X2"));
                }
                return sb.ToString ();
            }
        }
    }
}