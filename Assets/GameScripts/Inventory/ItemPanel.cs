﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour, IDropHandler {
    private InventoryPanel m_inventoryPanel;

    void Start() {
        m_inventoryPanel = transform.parent.GetComponent<InventoryPanel>();
    }

    public void OnDrop(PointerEventData eventData) {
        InventoryIcon icon = eventData.pointerDrag.GetComponent<InventoryIcon>();
        Inventory oldInventory = icon.inventoryPanel.inventory;
        Inventory newInventory = m_inventoryPanel.inventory;
        Vector2Int oldCoordinates = oldInventory.inventoryPanel.panelCoordinates(icon.oldParent.GetComponent<ItemPanel>());
        Vector2Int newCoordinates = m_inventoryPanel.panelCoordinates(this);

        if(!newInventory.takeOnly && newInventory.addItem(newCoordinates, icon.info, false)) {
            icon.transform.SetParent(transform);
        }
        else {
            oldInventory.addItem(oldCoordinates, icon.info, false);
            icon.transform.SetParent(icon.oldParent);
        }

        icon.GetComponent<RectTransform>().localPosition = new Vector3(0.0F, 0.0F, 0.0F);
    }
}