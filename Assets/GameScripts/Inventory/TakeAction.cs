using UnityEngine;
using System.Collections;

public class TakeAction : ActionItem {
    public override void triggerAction() {
        if(m_mainPersonScript.inventory.addItem(GetComponent<InventoryItem>().info)) {
            Destroy(gameObject);
        }
    }
}
