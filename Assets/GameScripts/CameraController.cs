using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public enum Scales {
        Small,
        Medium,
        Large
    }

    public float m_yDiff;
    [Range (0,2)]
    public int m_zoom;

    [System.NonSerialized]
    public bool followPlayer;

    private MainPerson m_mainPersonScript;

	void Start () {
        followPlayer = true;
        m_mainPersonScript = MainPerson.getMainPersonScript();
        setScale();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.P) && (Scales)m_zoom != Scales.Large) {
            m_zoom++;
            setScale();
        }
        if(Input.GetKeyDown(KeyCode.O) && (Scales)m_zoom != Scales.Small) {
            m_zoom--;
            setScale();
        }

        if(followPlayer) {
            transform.position = new Vector3(m_mainPersonScript.transform.position.x, m_mainPersonScript.transform.position.y + m_yDiff, -1);
        }
	}

    void setScale() {
        float orthographicSize;
        switch((Scales)m_zoom) {
            case Scales.Small:
                orthographicSize = Screen.height / 128.0F;
                m_yDiff = GameConsts.yDiff;
                break;
            case Scales.Medium:
                orthographicSize = Screen.height / 192.0F;
                m_yDiff = GameConsts.yDiff / 1.5F;
                break;
            case Scales.Large:
                orthographicSize = Screen.height / 256.0F;
                m_yDiff = GameConsts.yDiff / 2.0F;
                break; 
            default:
                orthographicSize = Screen.height / 128.0F;
                break;
        }
        GetComponent<Camera>().orthographicSize = orthographicSize;
    }
}
