using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPersonActionController : MonoBehaviour {
    private ActionItem m_actionItem;

    public ActionItem actionItem {
        get { return m_actionItem; }
        set { m_actionItem = value; }
    }
	
	void Update () {
        if(m_actionItem == null) {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
            Vector2 topLeft = new Vector2(ColliderFunctions.colliderLeft(boxCollider), ColliderFunctions.colliderTop(boxCollider));
            Vector2 bottomRight = new Vector2(ColliderFunctions.colliderRight(boxCollider), ColliderFunctions.colliderBottom(boxCollider));

            Collider2D[] colliders = Physics2D.OverlapAreaAll(topLeft, bottomRight);
            for(int i = 0; i < colliders.Length; i++) {
                ActionItem actionItem = colliders[i].GetComponent<ActionItem>();
                if(actionItem != null) {
                    m_actionItem = actionItem;
                    break;
                }
            }
        }

        if(m_actionItem != null) {
            if(m_actionItem.actionCondition()) {
                m_actionItem.triggerAction();
            }
        }
	}
}
