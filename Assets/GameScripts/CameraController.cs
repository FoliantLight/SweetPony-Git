using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public enum Scales {
        Small,
        Large
    }

    public float m_yDiff;

    private Scales m_zoom;

    private bool m_followPlayer;

    private MainPerson m_mainPersonScript;

	void Start () {
        m_followPlayer = true;
        m_mainPersonScript = MainPerson.getMainPersonScript();
        setScale(Scales.Small);
    }

	// Update is called once per frame
	void LateUpdate () {
        if(Input.GetKeyDown(KeyCode.P)) {
            if(m_zoom == Scales.Large) {
                setScale(Scales.Small);
            }
            else {
                setScale(Scales.Large);
            }
        }

        if(m_followPlayer) {
            transform.position = new Vector3(m_mainPersonScript.transform.position.x, m_mainPersonScript.transform.position.y + m_yDiff, -1);
        }
	}

    void setScale(Scales zoom) {
        float orthographicSize;
        switch(zoom) {
            case Scales.Small:
                orthographicSize = Screen.height / 128.0F;
                m_yDiff = GameConsts.yDiff;
                m_zoom = Scales.Small;
                break;
            case Scales.Large:
                orthographicSize = Screen.height / 192.0F;
                m_yDiff = GameConsts.yDiff / 1.5F;
                m_zoom = Scales.Large;
                break;
            default:
                orthographicSize = Screen.height / 128.0F;
                break;
        }
        GetComponent<Camera>().orthographicSize = orthographicSize;
    }
}
