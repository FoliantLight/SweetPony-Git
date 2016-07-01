using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Net;

public class Utils : MonoBehaviour {

    private static string AES_Key = "VFlEcXJTenhgTF3Tgo0SnhZ6Q1dFMWdu";
    private static string AES_IV = "hYoBNju74322HiodEGPPhasnrHJJ04=";

    public static String AES_encrypt(String Input)
    { 
        var aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 256;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Convert.FromBase64String(AES_Key);
        aes.IV = Convert.FromBase64String(AES_IV);

        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Encoding.UTF8.GetBytes(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Convert.ToBase64String(xBuff);
        return Output;
    }

    public static String AES_decrypt(String Input)
    {
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Convert.FromBase64String(AES_Key);
        aes.IV = Convert.FromBase64String(AES_IV);

        var decrypt = aes.CreateDecryptor();
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Convert.FromBase64String(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Encoding.UTF8.GetString(xBuff);
        return Output;
    }

    public static string web(string par)
    {
        WebRequest request = WebRequest.Create("https://mq.sweetpony.ru/utils/remote.php");
        string postData = string.Concat(par);
        byte[] data = Encoding.UTF8.GetBytes(postData);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = (long)data.Length;
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(data, 0, data.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        string responseFromServer = new StreamReader(response.GetResponseStream()).ReadToEnd();
        return responseFromServer;
    }

}
