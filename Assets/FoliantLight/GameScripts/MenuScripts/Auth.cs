using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Auth : MonoBehaviour {

    public static InputField login;
    public static InputField password;

    public static void submit()
    {        
        string responce = Utils.AES_decrypt(Utils.web(Utils.AES_encrypt("login=" + login + "&passwd=" + password)));
        if (responce.Contains("null"))
        {
            responce = "";
        }
    }

}
