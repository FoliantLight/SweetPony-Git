using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPersonActionController : MonoBehaviour {
    private ActionItem m_actionItem;
	
	// Update is called once per frame
	void Update () {
        if(m_actionItem != null) {
            if(m_actionItem.actionCondition()) {
                m_actionItem.triggerAction();
            }
        }
	}

    public void setActionItem(ActionItem item) {
        m_actionItem = item;
    }

    public void unsetActionItem() {
        m_actionItem = null;
    }
}
