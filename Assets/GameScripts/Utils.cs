using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class Utils : MonoBehaviour {

    private static string AES_Key = "UVNtcnAwUmxyQ0YweVFaSzg3Zlg2RUxUTTVMcGhyWGY=";
    private static string AES_IV = "T1lWYlppUmp5ekoqMk1JJXlDJXhSRX5pSnpZbC9SfXA=";

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

    public static string web(string postedData)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        var bytes = encoding.GetBytes(postedData);

        ServicePointManager.ServerCertificateValidationCallback += (s, ce, ca, p) => true;
        WebClient webClient = new WebClient();
        webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

        byte[] byteResult = webClient.UploadData("https://mq.sweetpony.ru/utils/remote.php", "POST", bytes);
        string responceText = Encoding.UTF8.GetString(byteResult);
        return responceText;
    }    
   
}
