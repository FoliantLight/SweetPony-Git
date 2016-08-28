using System;
using UnityEngine;

[System.Serializable]
public class InventoryItemInfo {
    [SerializeField]
    private string m_name;
    [SerializeField]
    private Vector2Int m_size;

    public InventoryItemInfo(string name, Vector2Int size) {
        m_name = name;
        m_size = size;
    }

    public string name {
        get { return m_name; }
        set { m_name = value; }
    }

    public Vector2Int size {
        get { return m_size; }
        set { m_size = value; }
    }
}
