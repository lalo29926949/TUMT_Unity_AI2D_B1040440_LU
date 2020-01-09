using UnityEngine;
using UnityEngine.Events;           // 引用 事件 API
using UnityEngine.UI;


public class Knight : MonoBehaviour
{

	#region 欄位

    public int speed = 0;              //整數
    public float jump = 2.5f;
    public string KnightName = "騎士";
    public bool pass = false;

    public UnityEvent onEat;

    private Rigidbody2D r2d;
    private Transform tra;
    private AudioSource aud;
    public bool isGround,isDie;
    #endregion


    [Header("血量"), Range(0, 200)]
    public float hp = 100;

    public Image hpBar;
    public GameObject final;

    private float hpMax;

    // 事件：在特定時間點會以指定頻率執行的方法
    // 開始事件：遊戲開始時執行一次
    private void Start()
    {
        // 泛型 <T>
        r2d = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
        //tra = GetComponent<Transform>();

        hpMax = hp;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
    }
    private void FixedUpdate()
    {
        Jump();
        Walk();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "地板")
        {
            isGround = true;
        }
        if (other.gameObject.name == "死亡區域")
        {
            isDie = true;
            if (isDie == true)
            {
                final.SetActive(true);
               
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "花")
        {
            Destroy(collision.gameObject);  // 刪除
            onEat.Invoke();                 // 呼叫事件
        }
    }

    /// <summary>
    /// 走路
    /// </summary>
    private void Walk()
    {
        if (r2d.velocity.magnitude < 10)
            r2d.AddForce(new Vector2(speed * Input.GetAxisRaw("Horizontal"), 0));
    }
    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="direction">方向 左轉為180 業轉為0</param>
    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
    }
    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;

        if (hp <= 0) final.SetActive(true);
    }

}

