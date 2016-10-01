﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPerson : MonoBehaviour {
    const float checkGroundDiff = 0.05F;

    private float v = 0;//вертикальная ось (W,S or arrow down,arrow up)
    private float h = 0;//горизонтальная ось (A,D or arrow left,arrow right)

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Anim;//Аниматор
    private SpriteRenderer m_spriteRenderer;
    private BoxCollider2D m_boxCollider;

    /// <summary>Инвентарь</summary>
    private Inventory m_playerInventory;

    public Inventory inventory {
        get { return m_playerInventory; }
    }

    private GameObject m_inventoryCanvas;

    public GameObject inventoryCanvas {
        get { return m_inventoryCanvas; }
    }

    private System.Random rnd = new System.Random();
    /// <summary>Среднее количество кадров между анимацией топтания на месте</summary>
    private int m_averageFramesToTrample;

    /// <summary>Престиж (дружелюбность)</summary>
    public int prestige = 0;

    /// <summary>Номера выполненных квестов для получения награды за них у какого-либо НИПа при встрече с ним</summary>
    public List<int> doneQuests = new List<int>();

    /// <summary>Полученные, но еще не выполненные квесты</summary>
    public List<Quest> recievedQuests = new List<Quest>();

    /// <summary>Список встреченных НИПов, имена которых известны игроку</summary>
    public List<string> metNames = new List<string>();

    private void Awake() {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_boxCollider = GetComponent<BoxCollider2D>();

        m_inventoryCanvas = GameObject.Find(ObjectNames.InventoryCanvas);
    }

    void Start () {
        m_averageFramesToTrample = 500;

        if(m_inventoryCanvas == null) {
            Debug.Log("На сцене нет объекта InventoryCanvas");
        }

        InventoryPanel panel = m_inventoryCanvas.transform.GetChild(Inventories.PlayerInventory).GetComponent<InventoryPanel>();
        m_playerInventory = new Inventory(GameConsts.inventorySize, false);
        m_playerInventory.inventoryPanel = panel;
	}
	
	void Update () {
        if(CrossPlatformInputManager.GetButtonDown(Buttons.Inventory)) {
            m_inventoryCanvas.SetActive(!m_inventoryCanvas.activeSelf);
        }

        #region Поворот спрайта в направлении движения. Основное направление влево
        if(h > 0) {
            m_spriteRenderer.flipX = true;
        }
        else if(h < 0) {
            m_spriteRenderer.flipX = false;
        }
        #endregion

        GameObject[] platforms = GameObject.FindGameObjectsWithTag(Tags.Platform);
        for(int i = 0; i < platforms.Length; i++) {
            BoxCollider2D[] colliders = platforms[i].GetComponents<BoxCollider2D>();
            for(int j = 0; j < colliders.Length; j++) {
                float colliderY = platforms[i].transform.position.y + colliders[j].offset.y + colliders[j].size.y / 2;

                if(transform.position.y > colliderY) {
                    colliders[j].enabled = true;
                }
                else {
                    colliders[j].enabled = false;
                }
            }
        }
    }

    void FixedUpdate() {
        #region основа движения
        v = CrossPlatformInputManager.GetAxis(Axes.Vertical);
        h = CrossPlatformInputManager.GetAxis(Axes.Horizontal);

        float vSpeed = m_Rigidbody2D.velocity.y;

        m_Anim.SetFloat("vSpeed", vSpeed);
        m_Anim.SetBool("Grounded", checkGround());

        bool isMoving = (h != 0 || vSpeed != 0);

        m_Anim.SetBool("isMoving", isMoving);
        #endregion

        #region Прыжок
        if (checkGround()) {
            if (CrossPlatformInputManager.GetButtonDown(Buttons.Jump)) {                
                m_Anim.SetBool("Grounded", false);
                m_Anim.SetTrigger("Jmp");
                m_Rigidbody2D.AddForce(new Vector2(0, GameConsts.JumpForce));
            }
        }
        #endregion      

        #region Движение
        float hSpeed = h * GameConsts.PlayerSpeed;
        if (CrossPlatformInputManager.GetButton(Buttons.Run)) {
            hSpeed *= GameConsts.RunSpeedMultiplier;
        }
        
        m_Anim.SetFloat("hSpeed", hSpeed);
        
        transform.position += new Vector3(Time.fixedDeltaTime * hSpeed, 0);
        #endregion

        #region рандомное топтание на месте.
        if(rnd.Next(m_averageFramesToTrample) == 0) {
            m_Anim.SetTrigger("Trample");
        }
        #endregion

     
    }

    bool checkGround() {
        #region Вычисление середины нижней стороны коллидера
        float downPointXLeft = transform.position.x + m_boxCollider.offset.x - m_boxCollider.size.x / 2.0F;
        float downPointXRight = transform.position.x + m_boxCollider.offset.x + m_boxCollider.size.x / 2.0F;
        float downPointY = transform.position.y + m_boxCollider.offset.y - m_boxCollider.size.y / 2;

        Vector2 topLeft = new Vector2(downPointXLeft, downPointY - checkGroundDiff);
        Vector2 bottomRight = new Vector2(downPointXRight, downPointY + checkGroundDiff);
        #endregion

        Collider2D[] colliders = Physics2D.OverlapAreaAll(topLeft, bottomRight);
        for (int i = 0; i < colliders.Length; i++) {
            int layer = colliders[i].gameObject.layer;
            if(layer == Layers.Solid || layer == Layers.Platforms) {
                return true;
            }  
        }
        return false;
    }

    public static MainPerson getMainPersonScript() {
        return GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<MainPerson>();
    }
}
