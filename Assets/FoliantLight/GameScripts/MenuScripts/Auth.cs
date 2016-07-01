using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Auth : MonoBehaviour {

    public InputField login;
    public InputField password;

    public void submit()
    {
        string responce = Utils.AES_decrypt(Utils.web("action=" + Utils.AES_encrypt(login.text + "|" + password.text)));
        Debug.Log(responce);
    }

    public void clear()
    {
        login.text = "";
        password.text = "";
    }

}
