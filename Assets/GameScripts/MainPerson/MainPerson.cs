﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPerson : MonoBehaviour {
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

    private System.Random rnd = new System.Random();
    /// <summary>Среднее количество кадров между анимацией топтания на месте</summary>
    private int m_averageFramesToTrample;

    /// <summary>Престиж (дружелюбность)</summary>
    private int prestige = 0;

    /// <summary>Номера выполненных квестов, которые нельзя получить второй раз</summary>
    private List<int> doneQuests = new List<int>();

    /// <summary>Все полученные квесты</summary>
    private List<Quest> quests = new List<Quest>();

    /// <summary>Список встреченных НИПов, имена которых известны игроку</summary>
    private List<string> knownNPCNames = new List<string>();

    private void Awake() {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start () {
        m_averageFramesToTrample = 500;

        m_inventoryCanvas = GameObject.Find(ObjectNames.InventoryCanvas);
		if (m_inventoryCanvas == null) {
			Debug.Log ("На сцене нет объекта InventoryCanvas");
		} else {
			InventoryPanel panel = m_inventoryCanvas.transform.GetChild (Inventories.PlayerInventory).GetComponent<InventoryPanel> ();
			m_playerInventory = new Inventory (GameConsts.inventorySize, false);
			m_playerInventory.inventoryPanel = panel;

			//m_inventoryCanvas.transform.GetChild (Inventories.PlayerInventory).gameObject.SetActive (false);
		}
	}
	
	void Update () {
        if(CrossPlatformInputManager.GetButtonDown(Buttons.Inventory)) { 
            m_inventoryCanvas.SetActive(!m_inventoryCanvas.activeSelf);
		//	m_inventoryCanvas.transform.GetChild (Inventories.PlayerInventory).gameObject.SetActive (!m_inventoryCanvas.transform.GetChild (Inventories.PlayerInventory).gameObject.activeSelf);

        }

        #region Поворот спрайта в направлении движения. Основное направление влево
        if(h > 0) {
            m_spriteRenderer.flipX = true;
        }
        else if(h < 0) {
            m_spriteRenderer.flipX = false;
        }
        #endregion
    }

    void FixedUpdate() {
        #region основа движения
        v = CrossPlatformInputManager.GetAxis(Axes.Vertical);
        h = CrossPlatformInputManager.GetAxis(Axes.Horizontal);

        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        m_Anim.SetBool("Grounded", checkGround());
        #endregion
        #region Прыжок
        if (checkGround()) {
            if (CrossPlatformInputManager.GetButtonDown(Buttons.Jump)) {
                m_Rigidbody2D.AddForce(new Vector2(0, GameConsts.JumpForce));
                m_Anim.SetBool("Grounded", false);
                m_Anim.SetTrigger("Jmp");
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
        if(h == 0 && checkGround() && rnd.Next(m_averageFramesToTrample) == 0) {
            m_Anim.SetTrigger("Trample");
        }
        #endregion
    }

    bool checkGround() {
        #region Вычисление середины нижней стороны коллидера
        float downPointX = transform.position.x + m_boxCollider.offset.x;
        float downPointY = transform.position.y + m_boxCollider.offset.y - m_boxCollider.size.y / 2;
        Vector2 downPoint = new Vector2(downPointX, downPointY);
        #endregion

        Collider2D[] colliders = Physics2D.OverlapCircleAll(downPoint, 0.1F);
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

	/// <summary>Удаляет все предметы из инвентаря без проверки их наличия в инвентаре</summary>
	public void drop(List<ItemSet> it)
	{
		for (int i = 0; i < it.Count; i++) 
			m_playerInventory.removeItem(it[i]);
	}
		
	/// <summary>Проверяет наличие ВСЕХ предметов в инвентаре</summary>
	public bool haveInInventory(List<ItemSet> it)
	{
		bool res = true;
		for (int i = 0; i < it.Count; i++)
			if (!m_playerInventory.have (it [i]))
				res = false;
		return res;
	}

	/// <summary>Запоминает, что НИП представился игроку</summary>
	public void addKnownNPCName(String name)
	{
		knownNPCNames.Add (name);
	}

	/// <summary>Если НИП уже представился игроку, то в следующий раз при встрече его имя высвечивается рядом с НИПом</summary>
	public bool isKnownNPCname(String name)
	{
		return knownNPCNames.FindIndex (x => x.Equals (name)) != -1;
	}

	/// <summary>НИП принимает квест с таким-то номером. Проверить выполнение</summary>
	public void checkQuest(int q_id)
	{
		int id = quests.FindIndex (x => x.id == q_id);
		if (id == -1)
			return; // игрок не взял этот квест
		
		if (haveInInventory(quests [id].drop)) { // если эти предметы найдены
			quests [id].done = true;
			m_playerInventory.addItems (quests [id].drag);
			prestige += quests [id].prestige;
		}
	}



}
