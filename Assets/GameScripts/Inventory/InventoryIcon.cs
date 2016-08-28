using UnityEngine;
using System.Collections;
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

    //Задать ItemInfo с размером и именем для префаба и иконки

	// Use this for initialization
	void Start () {
        m_inventoryCanvas = GameObject.Find(ObjectNames.InventoryCanvas).transform;
        if(m_inventoryCanvas == null) {
            Debug.Log("На сцене нет объекта InventoryCanvas");
        }
	}

    public void OnBeginDrag(PointerEventData eventData) {
        m_oldParent = transform.parent;
        m_inventoryPanel = m_oldParent.parent.GetComponent<InventoryPanel>();
        ItemPanel panel = m_oldParent.GetComponent<ItemPanel>();
        Vector2Int pos = m_inventoryPanel.panelCoordinates(panel);

        m_inventoryPanel.inventory.removeItem(pos);

        transform.SetParent(m_inventoryCanvas);
        GetComponent<Image>().raycastTarget = false;

        setItemPanelsRaycastTarget(true);
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
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
        Transform playerInventory = m_inventoryCanvas.GetChild(Inventories.PlayerInventory);
        for(int i = 0; i < playerInventory.childCount; i++) {
            playerInventory.GetChild(i).GetComponent<Image>().raycastTarget = value;
        }

        Transform othersInventory = m_inventoryCanvas.GetChild(Inventories.OthersInventory);
        if(othersInventory.gameObject.activeSelf) {
            for(int i = 0; i < othersInventory.childCount; i++) {
                othersInventory.GetChild(i).GetComponent<Image>().raycastTarget = value;
            }
        }
    }
}