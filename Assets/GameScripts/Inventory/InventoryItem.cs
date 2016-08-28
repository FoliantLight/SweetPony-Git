using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>Абстрактный класс предмета, который можно положить в инвентарь</summary>
public class InventoryItem : MonoBehaviour {
    /// <summary>Информация о предмете предмета, по ней загружается его префаб и спрайт</summary>
    [SerializeField]
    private string m_name;

    private InventoryItemInfo m_info;

    public InventoryItemInfo info {
        get { return m_info; }
        set { m_info = value; }
    }

    void Start() {
        m_info = InventoryItemDictionary.getInstance.getItem(m_name);
    }
}
