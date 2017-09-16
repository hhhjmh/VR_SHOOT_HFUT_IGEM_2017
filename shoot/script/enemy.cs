using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    normal,
    fast,
    boss,
    hard,
    fool
}

public class enemy : MonoBehaviour
{
    public EnemyType type = EnemyType.normal;
    public Transform firpos;
    public Transform endpos;
    public GameObject perb = null;
    public bool canshoot = false;
    public bool canrandmove = false;
    public static bool isnoob = false;
    public float DeadLine = -30f;
    public GameObject Particle;

    private GameObject model = null;
    private bool canshowUI = false;
    private GameObject enemyUI = null;
    private Text enemyUI_text = null;
    private GameObject enemyUI_blood = null;
    private bool canrot = true;
    private GameObject player = null;
    private bullet defaultbullet = bullet.enormal;
    private float maxblood = 100f;
    private int id;
    private float m_radius;
    private Vector3 vect;
    private float TimeDV = 0.0f;
    private float speed = 1f;
    private float blood = 100f;
    private bool islife = true;
    private float[] timetemp;
    private Vector3 beginpos;
    private float score;

    float vel_x, vel_y, vel_z;
    public float minPos_x = -3;
    public float maxPos_x = 3;
    public float minPos_y = -3;
    public float maxPos_y = 3;
    public float minPos_z = -3;
    public float maxPos_z = 3;

    public float Score
    {
        get
        {
            return this.score;
        }
    }

    public int ID
    {
        set
        {
            id = value;
        }
        get
        {
            return this.id;
        }
    }

    public float Speed
    {
        get
        {
            return this.speed;
        }
    }

    public float Blood
    {
        get
        {
            return this.blood;
        }
    }

    public bool IsLife
    {
        get
        {
            return this.islife;
        }
    }

    void Start()
    {
        this.model = this.transform.FindChild("model").gameObject;
        enemyUI = this.transform.FindChild("enemyUI").gameObject;
        enemyUI_text = this.transform.FindChild("enemyUI").FindChild("Text").GetComponent<Text>();
        enemyUI_blood = this.transform.FindChild("enemyUI").FindChild("Slider").FindChild("blood_base").gameObject;
        enemyUI.SetActive(false);
        player = GameObject.Find("maincell");
        switch (this.type)
        {
            case EnemyType.normal:
                canshowUI = true;
                enemyUI_text.text = "Normal";
                score = 100;
                this.canrot = true;
                speed = vel_x = vel_y = vel_z = 0.003f;
                timetemp = new float[1];
                defaultbullet = bullet.enormal;
                maxblood = blood = (isnoob == true ? 20 : 80);
                break;
            case EnemyType.fast:
                canshowUI = true;
                enemyUI_text.text = "Fast";
                score = 200;
                this.canrot = true;
                speed = vel_x = vel_y = vel_z = 0.006f;
                timetemp = new float[2];
                defaultbullet = bullet.efast;
                maxblood = blood = (isnoob == true ? 10 : 40);
                break;
            case EnemyType.hard:
                canshowUI = true;
                enemyUI_text.text = "Hard";
                score = 400;
                this.canrot = true;
                speed = vel_x = vel_y = vel_z = 0.005f;
                timetemp = new float[4];
                defaultbullet = bullet.ebig;
                maxblood = blood = (isnoob == true ? 50 : 150);
                break;
            case EnemyType.boss:
                canshowUI = true;
                enemyUI_text.text = "Boss";
                score = 800;
                this.canrot = true;
                speed = vel_x = vel_y = vel_z = 0.008f;
                timetemp = new float[10];
                defaultbullet = bullet.enormal;
                maxblood = blood = (isnoob == true ? 500 : 800);
                break;
            case EnemyType.fool:
                canshowUI = true;
                enemyUI_text.text = "Fool";
                score = 50;
                this.canrot = false;
                speed = vel_x = vel_y = vel_z = 0.004f;
                timetemp = new float[1];
                defaultbullet = bullet.efast;
                maxblood = blood = (isnoob == true ? 30 : 60);
                break;
        }
        vect = Vector3.Normalize(endpos.position - firpos.position);
    }

    private bool flag2 = false;
    private bool flag3 = false;//控制transport执行
    public void Transport(float radius)
    {
        m_radius = radius;
        flag3 = true;
        if (Vector3.Magnitude(transform.position) <= radius-3f)
        {
            flag3 = false;
            this.canshoot = true;
            this.canrandmove = true;
        }
        else
        {
            this.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speed * 450), Space.World);
        }
    }

    void simplemove()
    {
        if (flag2 == false)
        {
            vel_x = Random.Range(0, 1) == 0 ? Random.Range(-vel_x, -vel_x / 2) : Random.Range(vel_x / 2, vel_x);
            vel_y = Random.Range(0, 1) == 0 ? Random.Range(-vel_y, -vel_y / 2) : Random.Range(vel_y / 2, vel_y);
            vel_z = Random.Range(0, 1) == 0 ? Random.Range(-vel_z, -vel_z / 2) : Random.Range(vel_z / 2, vel_z);
            flag2 = true;
        }
        transform.Translate(vel_x, vel_y, vel_z, Space.World);
        if (transform.position.x - this.beginpos.x > maxPos_x)
        {
            vel_x = -vel_x;
            transform.position = new Vector3(this.beginpos.x + maxPos_x, transform.position.y, transform.position.z);
        }
        if (transform.position.x - this.beginpos.x < minPos_x)
        {
            vel_x = -vel_x;
            transform.position = new Vector3(this.beginpos.x + minPos_x, transform.position.y, transform.position.z);
        }
        if (transform.position.y - this.beginpos.y > maxPos_y)
        {
            vel_y = -vel_y;
            transform.position = new Vector3(transform.position.x, this.beginpos.y + maxPos_y, transform.position.z);
        }
        if (transform.position.y - this.beginpos.y < minPos_y)
        {
            vel_y = -vel_y;
            transform.position = new Vector3(transform.position.x, this.beginpos.y + minPos_y, transform.position.z);
        }
        if (transform.position.z - this.beginpos.z > maxPos_z)
        {
            vel_z = -vel_z;
            transform.position = new Vector3(transform.position.x, transform.position.y, this.beginpos.z + maxPos_z);
        }
        if (transform.position.z - this.beginpos.z < minPos_z)
        {
            vel_z = -vel_z;
            transform.position = new Vector3(transform.position.x, transform.position.y, this.beginpos.z + minPos_z);
        }
    }

    private bool flag = false;
    void Update()
    {
        if (this.transform.position.z <= DeadLine)
            Destroy(this.gameObject);

        vect = Vector3.Normalize(endpos.position - firpos.position);
        if (canrot)
        {
            Vector3 direction = player.transform.position - transform.position;// 这是获取物体指向player的一个向量 
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * speed * (this.type == EnemyType.boss ? 10000 : (this.type == EnemyType.fast ?6000:3000)));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //this.gameObject.transform.LookAt(player.transform);
        for (int i = 0; i < timetemp.Length; i++)
        {
            timetemp[i] += Time.deltaTime;
        }

        if (canshoot)
            Shoot();

        if (flag3)
            Transport(m_radius);

        if (canrandmove)
        {
            model.transform.Rotate(0,Time.deltaTime*50,0,Space.Self);
            if(canshowUI)
            {
                enemyUI.SetActive(true);
                enemyUI_blood.transform.localScale = new Vector3(this.blood / this.maxblood, 1, 1);
            }
            if (flag == false)
            {
                this.beginpos = this.transform.position;
                //print(this.beginpos);
                flag = true;
            }
            if(!(Controller.gameover||Controller.gamewin))
                move();
        }
        else
        {
            this.blood = maxblood;
        }
    }

    public void Shoot()
    {
        switch (this.type)
        {
            case EnemyType.normal:
                {
                    LineShoot(ref timetemp[0], firpos.position, Vector3.zero);
                    break;
                }
            case EnemyType.fast:
                {
                    LineShoot(ref timetemp[0], firpos.position + new Vector3(0.05f, 0, 0), Vector3.zero);
                    LineShoot(ref timetemp[1], firpos.position + new Vector3(-0.05f, 0, 0), Vector3.zero);
                    break;
                }
            case EnemyType.hard:
                {
                    LineShoot(ref timetemp[0], firpos.position + new Vector3(0.1f, 0.1f, 0), new Vector3(0.1f,0.1f,0));
                    LineShoot(ref timetemp[1], firpos.position + new Vector3(-0.1f, 0.1f, 0), new Vector3(-0.1f, 0.1f, 0));
                    LineShoot(ref timetemp[2], firpos.position + new Vector3(0.1f, -0.1f, 0), new Vector3(0.1f, -0.1f, 0));
                    LineShoot(ref timetemp[3], firpos.position + new Vector3(-0.1f, -0.1f, 0), new Vector3(-0.1f, -0.1f, 0));
                    break;
                }
            case EnemyType.boss:
                {
                    for(int i=0;i<10;i++)
                    {
                        Transform temp=this.transform.GetChild(i);
                        if(temp.name.Contains("shoot"))
                        {
                            //print(i+" "+ temp.name);
                            LineShoot(ref timetemp[i], temp.position, temp.position-this.transform.position);
                        }
                    }
                    break;
                }
            case EnemyType.fool:
                {
                    LineShoot(ref timetemp[0], firpos.position, Vector3.zero);
                    break;
                }
        }
    }

    public void LineShoot(ref float timetemp, Vector3 pos, Vector3 rot)//rot是偏移
    {
        if (timetemp >= TimeDV)
        {
            switch (defaultbullet)
            {
                case bullet.enormal:
                    {
                        float ranz = Random.Range(-3, 0);
                        float rany = Random.Range(-2, 0);
                        Quaternion temp = Quaternion.Euler(this.transform.eulerAngles + new Vector3(90, rany, ranz));
                        GameObject obj = (GameObject)Instantiate(perb, pos, temp);
                        Bullet bul = obj.GetComponent<Bullet>();

                        bul.direction = (this.type == EnemyType.boss ? rot : vect + rot);
                        TimeDV = bul.TimeDV = (isnoob == true ? 1.2f : 0.6f);
                        bul.size =( isnoob == true ? new Vector3(0.03f, 0.03f, 0.03f) : new Vector3(0.06f, 0.06f, 0.06f));
                        bul.color = (this.type == EnemyType.boss ? new Color(0, 1f, 0) : new Color(0.6f, 0, 0));
                        bul.type = bullet.enormal;
                        bul.LifeTime = 5f;
                        bul.damage = (isnoob == true ? 10 : 20);
                        bul.speed = 1.5f;
                        break;
                    }
                case bullet.efast:
                    {
                        float ranz = Random.Range(-3, -2);
                        float rany = Random.Range(-2, -1);
                        Quaternion temp = Quaternion.Euler(this.transform.eulerAngles + new Vector3(90, rany, ranz));
                        GameObject obj = (GameObject)Instantiate(perb, pos, temp);
                        Bullet bul = obj.GetComponent<Bullet>();

                        bul.direction = vect + rot;
                        TimeDV = bul.TimeDV = (isnoob == true ? 0.8f : 0.4f);
                        bul.size =(isnoob == true ? new Vector3(0.015f, 0.015f, 0.015f) : new Vector3(0.03f, 0.03f, 0.03f));
                        bul.color = new Color(0, 0, 1);
                        bul.type = bullet.efast;
                        bul.LifeTime = 3f;
                        bul.damage = (isnoob == true ? 10 : 10);
                        bul.speed = 3f;
                        break;
                    }
                case bullet.ebig:
                    {
                        float ranz = Random.Range(-3, 3);
                        float rany = Random.Range(-2, 2);
                        Quaternion temp = Quaternion.Euler(this.transform.eulerAngles + new Vector3(90, rany, ranz));
                        GameObject obj = (GameObject)Instantiate(perb, pos, temp);
                        Bullet bul = obj.GetComponent<Bullet>();

                        bul.direction = vect + rot;
                        TimeDV = bul.TimeDV = (isnoob == true ? 2f : 1f) ;
                        bul.size = (isnoob == true ? new Vector3(0.05f, 0.05f, 0.05f) : new Vector3(0.1f, 0.1f, 0.1f));
                        bul.color = new Color(1, 1, 0);
                        bul.type = bullet.ebig;
                        bul.LifeTime = 5f;
                        bul.damage = (isnoob == true ? 40 : 60);
                        bul.speed = 1.3f;
                        break;
                    }
                default:
                    {
                        float ranz = Random.Range(-3, 0);
                        float rany = Random.Range(-2, 0);
                        Quaternion temp = Quaternion.Euler(this.transform.eulerAngles + new Vector3(90, rany, ranz));
                        GameObject obj = (GameObject)Instantiate(perb, firpos.position, temp);
                        Bullet bul = obj.GetComponent<Bullet>();
                        bul.direction = vect;
                        TimeDV = bul.TimeDV = 0.2f;
                        bul.size = new Vector3(0.05f, 0.05f, 0.05f);
                        bul.color = new Color(1, 0, 0);
                        bul.type = bullet.normal;
                        bul.LifeTime = 2f;
                        bul.damage = (isnoob == true ? 10 : 20);
                        bul.speed = 10f;
                        break;
                    }
            }
            timetemp = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)//被射中
    {
        if (collision.gameObject.tag == "player")
        {
            subblood(collision.gameObject.GetComponent<Bullet>().damage);
            collision.gameObject.GetComponent<Bullet>().destory();
            if (islife == false)
                destroy();
        }
    }

    public void destroy()
    {
        print(this.gameObject.name + " " + this.id + " destory!");
        Controller.Score += this.score;
        GameObject obj = Instantiate(Particle, this.transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(this.gameObject);
    }

    public void addblood(float per)
    {
        if (per < 0.0f)
            per = 0.0f;
        float temp = blood + per;
        if (temp <= maxblood)
        {
            blood = temp;
        }
    }

    public void subblood(float per)
    {
        if (per < 0.0f)
            per = 0.0f;
        float temp = blood - per;
        if (temp > 0.0f)
        {
            blood = temp;
        }
        else
        {
            blood = 0.0f;
            islife = false;
            destroy();
        }
    }

    public void move()
    {
        switch (this.type)
        {
            //敌人移动逻辑
            case EnemyType.normal:
                {
                    simplemove();
                    break;
                }
            case EnemyType.fast:
                {
                    simplemove();
                    break;
                }
            case EnemyType.hard:
                {
                    simplemove();
                    break;
                }
            case EnemyType.boss:
                {
                    simplemove();
                    break;
                }
            case EnemyType.fool:
                {
                    simplemove();
                    break;
                }
        }
    }
}