using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryPanel : MonoBehaviour {
    /// <summary> Инвентарь, связанный с панелью </summary>
    private Inventory m_inventory;
    private GameObject m_inventoryIcon;
    private GameObject m_itemPanel;

    /// <summary>Хранение координат для каждой ячейки</summary>
    private Dictionary<ItemPanel, Vector2Int> m_itemPanels;

    public Inventory inventory {
        get { return m_inventory; }
        set {
            m_inventory = value;
            refillPanel();
        }
    }

    void Awake() {
        m_itemPanels = new Dictionary<ItemPanel, Vector2Int>();
        m_itemPanel = Resources.Load<GameObject>("InventoryRes/ItemPanel");
        m_inventoryIcon = Resources.Load<GameObject>("InventoryRes/InventoryIcon");

        for(int i = 0; i < GameConsts.inventorySize.row; i++) {
            for(int j = 0; j < GameConsts.inventorySize.column; j++) {            
                addItemPanel(new Vector2Int(i, j));
            }
        }
    }

    public Vector2Int panelCoordinates(ItemPanel panel) {
        return m_itemPanels[panel];
    }

    public void addItemPanel(Vector2Int pos) {
        GameObject panel = Instantiate(m_itemPanel) as GameObject;
        panel.transform.SetParent(transform, false);
        m_itemPanels.Add(panel.GetComponent<ItemPanel>(), pos);
    }

    public void addIconToItemPanel(int number, InventoryItemInfo info) {
        Transform panel = transform.GetChild(number);

        GameObject icon = Instantiate(m_inventoryIcon) as GameObject;
        icon.transform.SetParent(panel, false);
        icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(Paths.IconPath + info.name);
        icon.GetComponent<Image>().SetNativeSize();
        icon.GetComponent<RectTransform>().localPosition = new Vector3(0.0F, 0.0F, 0.0F);

        icon.GetComponent<InventoryIcon>().info = info;
    }

    public void refillPanel() {
        foreach(var panel in m_itemPanels.Keys) {
            if(panel.transform.childCount != 0) {
                Destroy(panel.transform.GetChild(0).gameObject);
            }
        }

        foreach(var item in m_inventory.items) {
            int number = GameConsts.inventorySize.column * item.Key.row + item.Key.column;
            addIconToItemPanel(number, item.Value);
        }
    }
}
