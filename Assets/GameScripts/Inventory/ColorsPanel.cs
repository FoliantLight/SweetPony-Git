using UnityEngine;
using UnityEngine.UI;

public class ColorsPanel : MonoBehaviour {
    public static readonly Color occupiedColor = new Color(0, 0, 1, 0.25F);
    public static readonly Color canPlaceColor = new Color(0, 1, 0, 0.25F);
    public static readonly Color cantPlaceColor = new Color(1, 0, 0, 0.25F);
    public static readonly Color noColor = new Color(1, 1, 1, 0.0F);

    private GameObject m_colorPanel;

    void Awake() {
        m_colorPanel = Resources.Load<GameObject>("InventoryRes/ColorPanel");
    }
	
    public void setColorToPanel(int number, Color color) {
        Transform panel = transform.GetChild(number);
        panel.GetComponent<Image>().color = color;
    }

    public void fill(Vector2Int size) {
        for(int i = 0; i < size.row; i++) {
            for(int j = 0; j < size.column; j++) {            
                addColorPanel();
            }
        }
    }

    void addColorPanel() {
        GameObject panel = Instantiate(m_colorPanel) as GameObject;
        panel.transform.SetParent(transform, false);
    }
}
