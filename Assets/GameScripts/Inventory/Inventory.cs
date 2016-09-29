using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Инвентарь игрока.</summary>
public class Inventory {
    private InventoryPanel m_inventoryPanel;
    private bool m_takeOnly;
    /// <summary>Список предметов, находящихся в инвентаре</summary>
    private Dictionary<Vector2Int, InventoryItemInfo> m_items;

    public bool takeOnly {
        get { return m_takeOnly; }
        set { m_takeOnly = value; }
    }

    public InventoryPanel inventoryPanel {
        get { return m_inventoryPanel; }
        set {
            m_inventoryPanel = value;
            if(value != null) {
                m_inventoryPanel.inventory = this;
            }
        }
    }

    public Dictionary<Vector2Int, InventoryItemInfo> items  {
        get { return m_items; }
    }

    /// <summary>Занятые ячейки (для простоты алгоритмов размещения и проверок)</summary>
    private bool[,] m_occupiedCells;

    /// <summary>Размеры инвентаря в ячейках</summary>
    private Vector2Int m_size;

    public Vector2Int size {
        get { return m_size; }
        set { m_size = value; }
    }

    /// <summary>Создание инвентаря</summary>
    public Inventory(Vector2Int size, bool takeOnly) {
        m_size = size;       
        m_takeOnly = takeOnly;
        m_items = new Dictionary<Vector2Int, InventoryItemInfo>();

        m_occupiedCells = new bool[m_size.row, m_size.column];
        for(int i = 0; i < m_size.row; i++) {
            for(int j = 0; j < m_size.column; j++) {            
                m_occupiedCells[i, j] = false;
            }
        }
    }

    /// <summary>Загрузка предметов инвентаря из XML</summary>
    /// <param name="xml">Узел, содержащий инвентарьL</param>
    public void loadFromXml(XmlNode xml) {
        // TODO: XML parser
    }

    /// <summary>Cохранение списка предметов инвентаря в XML</summary>
    /// <param name="path">Путь к файлу XML</param>
    public void saveToXML(string path) {
        // TODO: serializer
    }

    /// <summary>Добавляет предмет в инвентарь</summary>
    public bool addItem(Vector2Int pos, InventoryItemInfo info, bool addIcon) {
        if(checkPosition(pos, info.size)) {
            m_items.Add(pos, info);
            setOccupiedCells(pos, info.size, true);

            if(m_inventoryPanel != null) {
                m_inventoryPanel.setColorArea(pos, info.size, ColorsPanel.occupiedColor);
                if(addIcon) {                
                    int number = childNumberFromVector(pos);
                    m_inventoryPanel.addIcon(number, info);
                }
            }

            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>Добавляет предмет в инвентарь автоматически</summary>
    public bool addItem(InventoryItemInfo info) {
        for(int i = 0; i < m_size.row; i++) {
            for(int j = 0; j < m_size.column; j++) {            
                Vector2Int pos = new Vector2Int(i, j);
                if(addItem(pos, info, true)) {
                    return true;
                }
            }
        }
        return false;
    }

    public void addItems(List<ItemSet> items) {
        for(int i = 0; i < items.Count; i++) {
            for(int j = 0; j < items[i].count; j++) {
                addItem(InventoryItemDictionary.getInstance.getItem(items[i].name));
            }
        }
    }

    public void removeItem(Vector2Int pos) {
        setOccupiedCells(pos, m_items[pos].size, false);
        m_items.Remove(pos);
    }

    public void setOccupiedCells(Vector2Int pos, Vector2Int size, bool value) {
        for(int i = pos.row; i < pos.row + size.row; i++) {
            for(int j = pos.column; j < pos.column + size.column; j++) {
                m_occupiedCells[i, j] = value;
            }
        }
    }

    public bool checkPosition(Vector2Int pos, Vector2Int size) {
        if(pos.row + size.row > m_size.row) {
            return false;
        }
        if(pos.column + size.column > m_size.column) {
            return false;
        }
        for(int i = pos.row; i < pos.row + size.row; i++) {
            for(int j = pos.column; j < pos.column + size.column; j++) {
                if(m_occupiedCells[i, j]) {
                    return false;
                }
            }
        }
        return true;
    }

    public int childNumberFromVector(Vector2Int pos) {        
        return m_size.column * pos.row + pos.column;
    }

    public int childNumberFromVector(int i, int j) {        
        return m_size.column * i + j;
    }

    public Vector2Int vectorFromChildNumber(int number) {
        int row = number / m_size.column;
        int column = number % m_size.column;
        return new Vector2Int(row, column);
    }
}
