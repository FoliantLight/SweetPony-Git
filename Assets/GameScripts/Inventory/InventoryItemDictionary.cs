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

        addItem("bottle", "Бутылка", new Vector2Int(1, 1));
        addItem("swab", "Бесячая\nшвабра", new Vector2Int(3, 2));
    }

    private void addItem(string name, Vector2Int size) {
        m_itemDictionary.Add(name, new InventoryItemInfo(name, size));
    }

    private void addItem(string name, string inGameName, Vector2Int size) {
        m_itemDictionary.Add(name, new InventoryItemInfo(name, inGameName, size));
    }

    public InventoryItemInfo getItem(string name) {
        return m_itemDictionary[name];
    }
}
