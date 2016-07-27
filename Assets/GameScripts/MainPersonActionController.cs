using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPersonActionController : MonoBehaviour {

    private GameObject actionItem;
    public GameObject hint;
    private float changeScale;
	
	// Update is called once per frame
	void Update () {
	    if (actionItem != null)
        {
            hint.SetActive(true);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (h > 0)
                changeScale = 1;
            else
                if (h < 0)
                changeScale = -1;
            hint.transform.localScale = new Vector3(changeScale * -1.5f, 1.5f, 1.5f);
            if (CrossPlatformInputManager.GetButtonDown("Use"))
            {
                switch(actionItem.tag)
                {
                    case "door":
                        teleport tlp = actionItem.GetComponent<teleport>();
                        tlp.goToHouse(gameObject);
                        break;
                    case "NPC":
                        var nac = actionItem.GetComponent<NPCActionController>();
                        nac.startDialog(actionItem.name);
                        break;
                }
            }
        }
        else
        {
            hint.SetActive(false);
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
