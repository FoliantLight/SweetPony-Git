using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneLogic : MonoBehaviour {
    private GameObject m_UICanvas;

    void Awake() {
        m_UICanvas = GameObject.Find(ObjectNames.UICanvas);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject text = m_UICanvas.transform.GetChild(0).gameObject;

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hits.Length == 0) {
            text.SetActive(false);
            return;
        }

        for(int i = 0; i < hits.Length; i++) {
            INote note = hits[i].collider.GetComponent<INote>();
            if(note != null) {
                text.SetActive(true);
                text.transform.position = Camera.main.WorldToScreenPoint(hits[i].transform.position);
                //Посчитать корректнее где отображать текст
                text.GetComponent<Text>().text = note.getNoteString();
                return;
            }
        }
	}
}
