using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneLogic : MonoBehaviour {
    const int pixelsBetweenLines = 30;
    const int noteFontSize = 40;

    private GameObject m_UICanvas;

    void Awake() {
        m_UICanvas = GameObject.Find(ObjectNames.UICanvas);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject textObject = m_UICanvas.transform.GetChild(0).gameObject;

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        for(int i = 0; i < hits.Length; i++) {
            INote note = hits[i].collider.GetComponent<INote>();
            if(note != null) {
                textObject.SetActive(true);
                Text textComponent = textObject.GetComponent<Text>();
                textComponent.text = note.getNoteString();

                float xPoint = hits[i].collider.offset.x + hits[i].collider.transform.position.x;
                float yPoint = hits[i].collider.gameObject.transform.position.y;

                textObject.transform.position = Camera.main.WorldToScreenPoint(new Vector2(xPoint, yPoint));
                textObject.transform.position -= new Vector3(0, -textComponent.preferredHeight, 0);
                return;
            }
        }

        textObject.SetActive(false);
	}
}
