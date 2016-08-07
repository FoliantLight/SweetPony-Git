using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPersonActionController : MonoBehaviour {

    private ActionItem actionItem;
	
	// Update is called once per frame
	void FixedUpdate () {
        if(actionItem != null) {
            if(CrossPlatformInputManager.GetButtonDown(Buttons.Use)) {
                actionItem.triggerAction();
            }
        }
	}

    public void setActionItem(ActionItem item) {
        actionItem = item;
    }

    public void unsetActionItem() {
        actionItem = null;
    }
}
