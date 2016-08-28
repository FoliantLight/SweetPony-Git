using UnityEngine;
using System.Collections.Generic;

public class Chest : ActionItem {
    public Sprite closedChest;
    public Sprite openedChest;

    private GameObject m_inventoryCanvas;

    [SerializeField]
    private List<ItemSet> items;

    private Inventory m_inventory;

    private bool m_isOpened;

    void Awake() {
        m_inventoryCanvas = GameObject.Find(ObjectNames.InventoryCanvas);
        m_inventory = new Inventory(GameConsts.inventorySize, false);
        m_inventory.addItems(items);

        m_isOpened = false;
    }
	
    public override void triggerAction() {
        if(!m_isOpened) {
            GetComponent<SpriteRenderer>().sprite = openedChest;

            m_inventoryCanvas.SetActive(true);
            m_inventoryCanvas.transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(true);

            InventoryPanel panel = m_inventoryCanvas.transform.GetChild(Inventories.OthersInventory).GetComponent<InventoryPanel>();
            m_inventory.inventoryPanel = panel;

            m_isOpened = true;
        }
        else {
            exitAction();
        }
    }

    public override void exitAction() {
        GetComponent<SpriteRenderer>().sprite = closedChest;
        m_inventoryCanvas.transform.GetChild(Inventories.OthersInventory).gameObject.SetActive(false);
        m_inventory.inventoryPanel = null;
        m_isOpened = false;
    }
}
