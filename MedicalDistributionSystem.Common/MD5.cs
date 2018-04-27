//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/4/27 14:17:59
//  文件名：MD5
//  版本：V1.0.0 
//  说明：  
//==============================================================
using System;
using System.Text;
using System.Security.Cryptography;

namespace MedicalDistributionSystem.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MD5Helper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}