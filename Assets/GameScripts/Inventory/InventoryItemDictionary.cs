using UnityEngine;
using System.Collections.Generic;

public class InventoryItemDictionary {
    private Dictionary<string, InventoryItemInfo> m_itemDictionary;

    private static InventoryItemDictionary m_instance;

    public static InventoryItemDictionary getInstance {
        get {
            if(m_instance == null) {
                m_instance = new InventoryItemDictionary();
            }
            return m_instance;
        }
    }

    private InventoryItemDictionary() {
        m_itemDictionary = new Dictionary<string, InventoryItemInfo>();

        addItem("star", new Vector2Int(1, 1));
        addItem("arrow", new Vector2Int(2, 2));
        addItem("rhombus", new Vector2Int(3, 2));
        addItem("gear", new Vector2Int(3, 3));

        addItem("bottle", new Vector2Int(1, 1));
        addItem("swab", new Vector2Int(3, 2));
    }

    private void addItem(string name, Vector2Int size) {
        m_itemDictionary.Add(name, new InventoryItemInfo(name, size));
    }

    public InventoryItemInfo getItem(string name) {
        return m_itemDictionary[name];
    }
}
