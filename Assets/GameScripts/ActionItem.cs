using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class ActionItem : MonoBehaviour {

    private BoxCollider2D[] boxColliders;
    protected MainPerson m_mainPersonScript;

    // Use this for initialization
    protected virtual void Start () {
        boxColliders = GetComponents<BoxCollider2D>();
        for(int i = 0; i < boxColliders.Length; i++) {
            boxColliders[i].isTrigger = true;
        }
        m_mainPersonScript = MainPerson.getMainPersonScript();
    }

    void OnTriggerExit2D(Collider2D other) {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        if(mpac == null) {
            return;
        }

        if(mpac.actionItem == this) {
            exitAction();
            mpac.actionItem = null;
        }
    }

    public virtual bool actionCondition() {
        return CrossPlatformInputManager.GetButtonDown(Buttons.Use);
    }

    public abstract void triggerAction();

    public virtual void exitAction() {
        
    }
}
