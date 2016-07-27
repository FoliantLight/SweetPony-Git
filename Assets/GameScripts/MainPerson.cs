using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPerson : MonoBehaviour {
    [SerializeField] private float playerSpeed = 2;//Скорость игрока
    [SerializeField] private float jumpForce = 500;//Сила прыжка
    private float runSpeed = 2;//Скорость бега *****Константа*****
    private float v;//вертикальная ось (W,S or arrow down,arrow up)
    private float h;//вертикальная ось (A,D or arrow left,arrow right)
    private bool isRight = true;//Переключатель для настроки направления спрайта
    [SerializeField] private LayerMask m_WhatIsGround;//Что считается землей для функции checkGround()

    private Transform m_GroundCheck;//Объект проверки столкновения с землей для функции checkGround()
    private Rigidbody2D m_Rigidbody2D;
    //private Transform m_CeilingCheck;//Объект проверки столкновения башки с потолком или другой хренью сверху для функции checkGround()
    private Animator m_Anim;//Аниматор

    private DateTime lastTrample;
    private int secToTrample;
    private System.Random rndSec = new System.Random();

    /// <summary>Престиж (дружелюбность)</summary>
    public int prestige = 0;

    /// <summary>Номера выполненных квестов для получения награды за них у какого-либо НИПа при встрече с ним</summary>
    public List<int> doneQuests = new List<int>();

    /// <summary>Полученные, но еще не выполненные квесты</summary>
    public List<Quest> recievedQuests = new List<Quest>();

    /// <summary>Инвентарь</summary>
    public Inventory inventory = new Inventory();

    /// <summary>Список встреченных НИПов, имена которыхх известны игроку</summary>
    public List<string> metNames = new List<string>();

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        //m_CeilingCheck = transform.Find("CeilingCheck");//Пока совсем ненужная переменная
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start () {
        lastTrample = DateTime.Now;
        secToTrample = rndSec.Next(5, 15);
	}
	
	void Update () {
        #region Поворот спрайта в направлении движения. Основное направление влево
        float tempScaleX = transform.localScale.x;
        if (h != 0)
        {            
            if (h > 0)
            {
                if (isRight)
                {
                    if (tempScaleX > 0)
                    {
                        tempScaleX = -tempScaleX;
                    }
                    transform.localScale = new Vector3(tempScaleX, transform.localScale.y);
                    isRight = false;
                }
            }
            else
            {
                if (!isRight)
                {
                    if (tempScaleX < 0)
                    {
                        tempScaleX = -tempScaleX;
                    }
                    transform.localScale = new Vector3(tempScaleX, transform.localScale.y);
                    isRight = true;
                }
            }           
        }
        #endregion
        #region топтание на месте. Время в рандоме от 30 до 60 секунд
        if ((DateTime.Now - lastTrample).TotalSeconds > secToTrample)
        {
            if (m_Anim.GetFloat("hSpeed") == 0)
            {
                m_Anim.SetTrigger("Trample");
                lastTrample = DateTime.Now;
                secToTrample = rndSec.Next(5, 15);
            }
            else
            {
                lastTrample = DateTime.Now;
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region основа движения
        v = CrossPlatformInputManager.GetAxis("Vertical");
        h = CrossPlatformInputManager.GetAxis("Horizontal");

        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        m_Anim.SetBool("Grounded", checkGround());
        #endregion
        #region Прыжок
        if (checkGround())
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                m_Rigidbody2D.AddForce(new Vector2(0, jumpForce));
                m_Anim.SetBool("Grounded", false);
                m_Anim.SetBool("jmp", true);
            }

        }

        #endregion
        #region Движение
        if (CrossPlatformInputManager.GetButton("Run"))
        {
            h *= runSpeed;
        }
        m_Anim.SetFloat("hSpeed", h);
        transform.position += new Vector3(playerSpeed * Time.fixedDeltaTime * h, 0);
        #endregion
    }

    bool checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.1f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                return true;    
        }
        return false;
    }

}
