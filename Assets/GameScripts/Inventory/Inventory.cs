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
    public Vector2Int m_size;

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
            if(addIcon && m_inventoryPanel != null) {
                int number = m_size.column * pos.row + pos.column;
                m_inventoryPanel.addIconToItemPanel(number, info);
            }
            m_items.Add(pos, info);
            setOccupiedCells(pos, info.size, true);
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

    public void setOccupiedCells(Vector2Int pos, Vector2Int size, bool value) {
        for(int i = pos.row; i < pos.row + size.row; i++) {
            for(int j = pos.column; j < pos.column + size.column; j++) {
                m_occupiedCells[i, j] = value;
            }
        }
    }

	/// <summary>Удаляет предмет по его позиции</summary>
    public void removeItem(Vector2Int pos) {
        setOccupiedCells(pos, m_items[pos].size, false);
        m_items.Remove(pos);
    }

	/// <summary>Удаляет предмет по его названию</summary>
	/// <returns><c>true</c>, if item was removed, <c>false</c> otherwise.</returns>
	public bool removeItem(InventoryItemInfo info) {
		{
			if (m_items.GetEnumerator ().Current.Value.Equals (info)) {
				removeItem (m_items.GetEnumerator ().Current.Key);
				return true;
			}
		} while (m_items.GetEnumerator ().MoveNext());
		return false;
	}

	/// <summary>Удаляет все, что может удалить из списка предметов</summary>
	public void removeItem(ItemSet it) {
			for(int j = 0; j < it.count; j++) 
				removeItem (InventoryItemDictionary.getInstance.getItem (it.name));
	}

	/// <summary>Удаляет набор одинаковых предметов. Удаление как транзакция</summary>
	/// <returns><c>true</c>, если может удалить все предметы <c>false</c> если не может удалить хотя бы один</returns>
	public bool removeItemTransaction(ItemSet it) {
		if (!have (it))
			return false;
		for(int j = 0; j < it.count; j++) {
			removeItem (InventoryItemDictionary.getInstance.getItem (it.name));
		}
		return true;
	}


	/// <summary>Проверка наличия по названию предмета</summary>
	/// <returns><c>true</c>, если есть хоть один такой предмет <c>false</c> если нет</returns>	
	public bool have(InventoryItemInfo info) {
		{
			if (m_items.GetEnumerator ().Current.Value.Equals (info)) {
				return true;
			}
		} while (m_items.GetEnumerator ().MoveNext());
		return false;
	}

	/// <summary>Проверка наличия</summary>
	/// <returns><c>true</c>, если все предметы есть в инвентаре <c>false</c> если нет хотя бы одного</returns>
	public bool have(ItemSet it)
	{
		int cnt = 0;
		InventoryItemInfo info = InventoryItemDictionary.getInstance.getItem (it.name);
		{
			if (m_items.GetEnumerator ().Current.Value.Equals (info)) {
				cnt++;
				if (cnt >= it.count)
					return true;
			}
		} while (m_items.GetEnumerator ().MoveNext());
		return false;
	}
}
