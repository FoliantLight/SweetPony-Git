using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    /// <summary>Canvas для инвентарей</summary>
    private Transform m_inventoryCanvas;
    /// <summary>Переменная содержит ячейку, в которой предмет находился до начала перетаскивания</summary>
    private Transform m_oldParent;
    /// <summary>К какому инвентарю относился предмет до начала перетаскивания</summary>
    private InventoryPanel m_inventoryPanel;
    /// <summary>Информация о предмете предмета, по ней загружается его префаб и спрайт</summary>
    private InventoryItemInfo m_info;

    public Transform oldParent {
        get { return m_oldParent; }
    }

    public InventoryPanel inventoryPanel {
        get { return m_inventoryPanel; }
    }

    public InventoryItemInfo info {
        get { return m_info; }
        set { m_info = value; }
    }

	// Use this for initialization
	void Start () {
        m_inventoryCanvas = GameObject.Find(ObjectNames.InventoryCanvas).transform;
        if(m_inventoryCanvas == null) {
            Debug.Log("На сцене нет объекта InventoryCanvas");
        }
	}

    public void OnBeginDrag(PointerEventData eventData) {
        m_oldParent = transform.parent;
        m_inventoryPanel = ItemPanel.getInventoryPanel(m_oldParent);
        ItemPanel panel = m_oldParent.GetComponent<ItemPanel>();
        Vector2Int pos = m_inventoryPanel.panelCoordinates(panel);

        m_inventoryPanel.inventory.removeItem(pos);

        transform.SetParent(m_inventoryCanvas);
        GetComponent<Image>().raycastTarget = false;

        setItemPanelsRaycastTarget(true);
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
//        List<RaycastResult> list = new List<RaycastResult>();
//        m_inventoryCanvas.GetComponent<GraphicRaycaster>().Raycast(eventData, list);
//        for(int i = 0; i < list.Count; i++) {
//            ItemPanel panel = list[i].gameObject.GetComponent<ItemPanel>();
//            if(panel != null) {
//                
//            }
//        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        GetComponent<Image>().raycastTarget = true;
        setItemPanelsRaycastTarget(false);

        if(transform.parent == m_inventoryCanvas) {
            GameObject prefab = Resources.Load<GameObject>(Paths.PrefabPath + info.name);
            Vector3 diff = new Vector3(0.0F, GetComponent<Image>().sprite.bounds.size.y, 0.0F);
            Instantiate(prefab, MainPerson.getMainPersonScript().transform.position + diff, prefab.transform.rotation);
            Destroy(gameObject);
        }
    }

    void setItemPanelsRaycastTarget(bool value) {
        InventoryPanel playerInventory = m_inventoryCanvas.GetChild(Inventories.PlayerInventory).GetComponent<InventoryPanel>();
        Transform itemsPanel = playerInventory.itemsPanel.transform;
        for(int i = 0; i < itemsPanel.childCount; i++) {
            itemsPanel.GetChild(i).GetComponent<Image>().raycastTarget = value;
        }

        InventoryPanel othersInventory = m_inventoryCanvas.GetChild(Inventories.OthersInventory).GetComponent<InventoryPanel>();
        if(othersInventory.gameObject.activeSelf) {
            itemsPanel = othersInventory.itemsPanel.transform;
            for(int i = 0; i < itemsPanel.childCount; i++) {
                itemsPanel.GetChild(i).GetComponent<Image>().raycastTarget = value;
            }
        }
    }
}