using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class BackgroundParallax : MonoBehaviour
{
    public List<Material> m_materials;
    public List<float> m_selfSpeeds;
    public GameObject m_backgroundQuad;

    private List<GameObject> m_backgroundQuads;

    Vector3 lastPos;

    void Start() {
        m_backgroundQuads = new List<GameObject>(m_materials.Count);
        for(int i = 0; i < m_materials.Count; i++) {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, i + 1);
            GameObject quad = Instantiate(m_backgroundQuad, position, transform.rotation) as GameObject;
            quad.transform.SetParent(this.transform);
            m_backgroundQuads.Add(quad);
            m_backgroundQuads[i].GetComponent<Renderer>().material = m_materials[i];
        }

        lastPos = transform.position;
    }

    void Update() {
        Vector3 cameraOffset = transform.position - lastPos;
        for(int i = 0; i < m_backgroundQuads.Count; i++) {
            Material mat = m_backgroundQuads[i].GetComponent<Renderer>().material;
            //150 и 10 отвечают за скорость параллакса. Чем больше число, тем медленнее перемещается фон.
            float textureOffsetX = mat.GetTextureOffset("_MainTex").x;
            float offsetX = m_selfSpeeds[i] / 1000.0F + cameraOffset.x / (150 * (i + 1));
            float offsetY = cameraOffset.y / (10 * (i + 1));
            mat.SetTextureOffset("_MainTex", new Vector2(textureOffsetX + offsetX, 0.0F));
            m_backgroundQuads[i].transform.position += new Vector3(0.0F, -offsetY, 0.0F); 
        }

        lastPos = transform.position;
    }
}