using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
    public const int ColorsPanelChild = 0;
    public const int ItemsPanelChild = 1;

    /// <summary> Инвентарь, связанный с панелью </summary>
    private Inventory m_inventory;

    /// <summary> Панель с иконками предметов </summary>
    private ItemsPanel m_itemsPanel;
    /// <summary> Панель с цветовой индикацией инвентаря </summary>
    private ColorsPanel m_colorsPanel;

    public Inventory inventory {
        get { return m_inventory; }
        set {
            m_inventory = value;
            refillPanel();
        }
    }

    public ItemsPanel itemsPanel {
        get { return m_itemsPanel; }
    }

    public ColorsPanel colorsPanel {
        get { return m_colorsPanel; }
    }

    void Awake() {
        m_colorsPanel = transform.GetChild(ColorsPanelChild).GetComponent<ColorsPanel>();
        m_itemsPanel = transform.GetChild(ItemsPanelChild).GetComponent<ItemsPanel>();

        m_colorsPanel.fill(GameConsts.inventorySize);
        m_itemsPanel.fill(GameConsts.inventorySize);
    }

    public Vector2Int panelCoordinates(ItemPanel panel) {
        int number = panel.transform.GetSiblingIndex();
        Vector2Int size = m_inventory.size;
        int row = number / size.column;
        int column = number % size.column;
        return new Vector2Int(row, column);
    }

    public void refillPanel() {
        for(int i = 0; i < m_colorsPanel.transform.childCount; i++) {
            m_colorsPanel.setColorToPanel(i, ColorsPanel.noColor);
        }

        foreach(var item in m_inventory.items) {
            int number = m_inventory.childNumberFromVector(item.Key);
            addIcon(number, item.Value);
            setColorArea(item.Key, item.Value.size, ColorsPanel.occupiedColor);
        }
    }

    public void setColorArea(Vector2Int pos, Vector2Int size, Color color) {
        for(int i = pos.row; i < pos.row + size.row; i++) {
            for(int j = pos.column; j < pos.column + size.column; j++) {
                m_colorsPanel.setColorToPanel(m_inventory.childNumberFromVector(i, j), color);
            }
        }
    }

    public void addIcon(int number, InventoryItemInfo info) {
        m_itemsPanel.addIcon(number, info);
    }

    public void setColor(int number, Color color) {
        m_colorsPanel.setColorToPanel(number, color);
    }
}
