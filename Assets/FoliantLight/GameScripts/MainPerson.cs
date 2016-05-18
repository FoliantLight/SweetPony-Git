using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MainPerson : MonoBehaviour {
    [SerializeField] private float playerSpeed = 2;//Скорость игрока
    [SerializeField] private float jumpForce = 500;//Сила прыжка
    [Range(1, 3)][SerializeField] private float runSpeed = 1.5f;//Скорость бега
    private float v;//вертикальная ось (W,S or arrow down,arrow up)
    private float h;//вертикальная ось (A,D or arrow left,arrow right)
    private bool isRight = false;//Переключатель для настроки направления спрайта
    [SerializeField] private LayerMask m_WhatIsGround;//Что считается землей для функции checkGround()

    private Transform m_GroundCheck;//Объект проверки столкновения с землей для функции checkGround()
    private Rigidbody2D m_Rigidbody2D;
    //private Transform m_CeilingCheck;//Объект проверки столкновения башки с потолком или другой хренью сверху для функции checkGround()
    //private Animator m_Anim;//Аниматор (его пока нет)

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        //m_CeilingCheck = transform.Find("CeilingCheck");//Пока совсем ненужная переменная
        //m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start () { 
	
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

    }

    void FixedUpdate()
    {
        v = CrossPlatformInputManager.GetAxis("Vertical");
        h = CrossPlatformInputManager.GetAxis("Horizontal");

        if (h != 0)
        {
            transform.position += new Vector3(playerSpeed * Time.fixedDeltaTime * h, 0);
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump") && checkGround())
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }        
    }

    bool checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.2f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                return true;    
        }
        return false;
    }
}
