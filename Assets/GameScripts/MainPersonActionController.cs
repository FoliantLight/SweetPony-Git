using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPersonActionController : MonoBehaviour {

    private GameObject actionItem;
	
	// Update is called once per frame
	void Update () {
	    if (actionItem != null)
        {
            if (CrossPlatformInputManager.GetButtonDown("Use"))
            Debug.Log(actionItem.transform.tag);
        }
	}

    public void isUseAroundEnter(GameObject go)
    {
        actionItem = go;
    }

    public void isUseAroundExit()
    {
        actionItem = null;
    }
}
