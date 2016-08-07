using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class ActionItem : MonoBehaviour {

    private BoxCollider2D boxCollider;
    protected MainPerson m_mainPersonScript;

    // Use this for initialization
    protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        m_mainPersonScript = MainPerson.getMainPersonScript();
    }

    void OnTriggerEnter2D(Collider2D other) {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        mpac.setActionItem(this);
    }

    void OnTriggerExit2D(Collider2D other) {
        MainPersonActionController mpac = other.gameObject.GetComponent<MainPersonActionController>();
        exitAction();
        mpac.unsetActionItem();
    }

    public abstract void triggerAction();
    public abstract void exitAction();
}
