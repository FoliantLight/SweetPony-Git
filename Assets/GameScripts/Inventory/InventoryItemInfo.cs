using System;
using UnityEngine;

[System.Serializable]
public class InventoryItemInfo {
    private string m_name;
    private string m_inGameName;
    private Vector2Int m_size;

    public InventoryItemInfo(string name, Vector2Int size) {
        m_name = name;
        m_inGameName = name;
        m_size = size;
    }

    public InventoryItemInfo(string name, string inGameName, Vector2Int size) {
        m_name = name;
        m_inGameName = inGameName;
        m_size = size;
    }

    public string name {
        get { return m_name; }
        set { m_name = value; }
    }

    public string inGameName {
        get { return m_inGameName; }
        set { m_inGameName = value; }
    }

    public Vector2Int size {
        get { return m_size; }
        set { m_size = value; }
    }
}
