using UnityEngine;
using System.Collections;

public class TakeAction : ActionItem {
    public override void triggerAction() {
        bool isActive = m_mainPersonScript.inventoryCanvas.activeSelf; //не удалять
        m_mainPersonScript.inventoryCanvas.SetActive(true);            //не удалять

        if(m_mainPersonScript.inventory.addItem(GetComponent<InventoryItem>().info)) {
            Destroy(gameObject);
        }

        m_mainPersonScript.inventoryCanvas.SetActive(isActive);        //не удалять
    }
}
