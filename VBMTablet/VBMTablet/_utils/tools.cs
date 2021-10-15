using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using VBMTablet._objs._menuObjs;
using VBMTablet._objs._promoObjs;
using VBMTablet._objs._storeObjs;
using VBMTablet._process;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace VBMTablet._utils
{
    public class tools
    {
        public static bool isConn()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }

        public static string GetJArrayValue(JObject yourJArray, string key)
        {
            foreach (KeyValuePair<string, JToken> keyValuePair in yourJArray)
            {
                if (key == keyValuePair.Key)
                {
                    return keyValuePair.Value.ToString();
                }
            }
            return "";
        }

        public static string getVNName(Dictionary<string, string> sources)
        {
            try
            {
                return sources["vi"];
            }
            catch { }
            return null;
        }

        public static string NonUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                                        "đ",
                                                        "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                                        "í","ì","ỉ","ĩ","ị",
                                                        "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                                        "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                                        "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                                        "d",
                                                        "e","e","e","e","e","e","e","e","e","e","e",
                                                        "i","i","i","i","i",
                                                        "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                                        "u","u","u","u","u","u","u","u","u","u","u",
                                                        "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static bool compareText(string sBase, string s2)
        {
            sBase = NonUnicode(sBase).ToLower();
            sBase = sBase.Replace(" ", "");
            return sBase.Contains(s2) ? true : false;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string EncodePassword(string value)
        {
            MD5 algorithm = MD5.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }
        public static string EncryptAES(string text)
        {
            string keyString = "VnVQaG9uZ01haVRodXk@ppcafe890vbm"; //replace with your key
            string ivString = "I@mL3g3nd@ary020"; //replace with your iv

            byte[] key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = Encoding.UTF8.GetBytes(ivString);

            RijndaelManaged rijndaelManaged = new RijndaelManaged();

            rijndaelManaged.Key = key;
            rijndaelManaged.IV = iv;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.KeySize = 256;

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(text);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }

        public static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string HtmlToText(string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }

        public static double convertFToCTemperate(double f)
        {
            return (f - 32) * 5 / 9;
        }

        public static HttpClient createHttpClient()
        {
            var cl = new HttpClient();
            cl.Timeout = TimeSpan.FromSeconds(10);
            return cl;
        }
    }
}
