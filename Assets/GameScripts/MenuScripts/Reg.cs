using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Reg : MonoBehaviour {

    public InputField login;
    public InputField password;
    public InputField email;

    public void submit()
    {
        Debug.Log(Utils.AES_encrypt(login.text + "|" + password.text));
        string responce = Utils.AES_decrypt(Utils.web("action=" + Utils.AES_encrypt(login.text + "|" + password.text)));
        //Utils.AES_decrypt();
        Debug.Log(responce);
    }

    public void clear()
    {
        login.text = "";
        password.text = "";
        email.text = "";
    }
}
