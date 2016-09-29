using UnityEngine;
using UnityEngine.UI;

public class ItemsPanel : MonoBehaviour {
    private GameObject m_inventoryIcon;
    private GameObject m_itemPanel;

    void Awake() {
        m_itemPanel = Resources.Load<GameObject>("InventoryRes/ItemPanel");
        m_inventoryIcon = Resources.Load<GameObject>("InventoryRes/InventoryIcon");
    }

    public void addIcon(int number, InventoryItemInfo info) {
        Transform panel = transform.GetChild(number);

        GameObject icon = Instantiate(m_inventoryIcon) as GameObject;
        icon.transform.SetParent(panel, false);
        icon.GetComponent<Image>().sprite = Resources.Load<Sprite>(Paths.IconPath + info.name);
        icon.GetComponent<Image>().SetNativeSize();
        icon.GetComponent<RectTransform>().localPosition = new Vector3(1.0F, -1.0F, 0.0F);

        icon.GetComponent<InventoryIcon>().info = info;
    }

    public void fill(Vector2Int size) {
        for(int i = 0; i < size.row; i++) {
            for(int j = 0; j < size.column; j++) {            
                addItemPanel();
            }
        }
    }

    void addItemPanel() {
        GameObject panel = Instantiate(m_itemPanel) as GameObject;
        panel.transform.SetParent(transform, false);
    }
}
