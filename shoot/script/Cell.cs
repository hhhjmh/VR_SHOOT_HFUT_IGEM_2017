using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BulletData
{
    public float TimeDV;
    public Vector3 size;
    public Color color;
    public bullet type;
    public float LifeTime;
    public float damage;
    public float speed;

    public BulletData(float TimeDV, Vector3 size ,Color color,bullet type,float LifeTime,float damage,float speed)
    {
        this.TimeDV = TimeDV;
        this.size = size;
        this.color = color;
        this.type = type;
        this.LifeTime = LifeTime;
        this.damage = damage;
        this.speed = speed;
    }
}

public class Cell : MonoBehaviour
{
    //外部数据可改写
    public BulletData ndata = new BulletData(0.2f, new Vector3(0.05f, 0.05f, 0.05f), new Color(1f, 1f, 1f), bullet.normal, 2f, 20f, 10f);
    public BulletData sdata = new BulletData(0.5f, new Vector3(0.05f, 0.05f, 0.05f), new Color(0.0f, 0.0f, 0.0f), bullet.slow, 2f, 40f, 5f);
    public float maxblood = 100f;
    public float DZSubBlood = 20;
    public GameObject dztx;
    public GameObject hudun;

    public Transform firepos = null;
    public Transform linefin = null;
    public GameObject perb = null;//子弹的perb
    public GameObject TipUI = null;//提示ui
    public float ColorSpeed = 2.0f;
    public Sprite[] textures;
    //public Image m_logo;//logo对象
    //public Text m_name;//name对象

    private bool canshowUI = true;
    private float blood;
    public float Blood
    {
        get { return this.blood; }
    }
    public static bullet defaultbullet = bullet.normal;

    public float LineWidth = 0.01f;
    public bool ShowLine = false;
    public DZController dz1;
    public DZController dz2;
    public DZController dz3;
    public DZController dz4;

    private LineRenderer line;
    private Vector3 vect;
    private Transform father = null;
    private float TimeDV = 0.0f;
    private float timetemp;
    private ShakeController LeftShake;
    private ShakeController RightShake;

    void Start()
    {
        hudun.SetActive(false);
        changecolor = TipUI.transform.FindChild("changecolor").GetComponent<Image>();
        LeftShake = GameObject.Find(@"left_touch_controller_model_skel").GetComponent<ShakeController>();
        RightShake = GameObject.Find(@"right_touch_controller_model_skel").GetComponent<ShakeController>();
        defaultbullet = bullet.normal;
        timetemp = 0.0f;
        father = this.gameObject.transform.parent;
        line = this.gameObject.transform.FindChild("line").GetComponent<LineRenderer>();
        line.startWidth = LineWidth;
        line.endWidth = LineWidth;
        blood = maxblood;
    }

    public void dzfull()
    {
        if (GlobalData.choice != 0)
        {
            switch(GlobalData.choice)
            {
                case 1:
                    dz1.setfull();
                    break;
                case 2:
                    dz2.setfull();
                    break;
                case 3:
                    dz3.setfull();
                    break;
                case 4:
                    dz4.setfull();
                    break;
            }
            //makedz(4, GlobalData.choice);
        }
    }

    private float timeadd = 0.0f;
    private Image changecolor = null;
    void Update()
    {
        timeadd += Time.deltaTime*ColorSpeed;
        if (canshowUI)
        {
            TipUI.SetActive(true);
            TipUI.transform.LookAt(Camera.main.transform);
            changecolor.color = new Color(1, Mathf.Cos(timeadd) / 2 + 0.5f, Mathf.Cos(timeadd) / 2 + 0.5f);
            if (Controller.gamebegin)
                canshowUI = false;
        }
        else
            TipUI.SetActive(false);

        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight) || OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
            changebullet();
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickRight) || OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
            changebullet();
        timetemp += Time.deltaTime;
        if (ShowLine)
            line.enabled = true;
        else
            line.enabled = false;
        line.SetPosition(0, firepos.position);
        vect = Vector3.Normalize(linefin.position - firepos.position);
        Vector3 linefinpos = firepos.position + vect * 100;
        line.SetPosition(1, linefinpos);
    }

    public void makedz(int level,int number)//level包含0
    {
        if (number == 1)//爆照特效
        {
            List<GameObject> activelist = new List<GameObject>();
            GameObject[] temp = GameObject.FindGameObjectsWithTag("enemys");
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].GetComponent<enemy>().canshoot == true)
                    activelist.Add(temp[i]);
            }
            //特效
            Instantiate(dztx, this.transform.position, Quaternion.identity);
            if (activelist.Count > 0)
            {
                //释放大招
                foreach (var a in activelist)
                    a.GetComponent<enemy>().subblood((level + 1) * DZSubBlood);

            }
            activelist.Clear();
        }
        else if(number==2)//射速加快
        {
            StartCoroutine(shootfast(level));
        }
        else if (number == 3)//血量加满
        {
            addblood(100000);
        }
        else if (number == 4)//无敌状态
        {
            StartCoroutine(Invincible(level));
        }
    }

    private bool ShootFast = false;
    private IEnumerator shootfast(int level)
    {
        ShootFast = true;
        yield return new WaitForSeconds(level * 5);
        ShootFast = false;
    }

    private bool canInvincible = false;
    private IEnumerator Invincible(int level)
    {
        canInvincible = true;
        hudun.SetActive(true);
        yield return new WaitForSeconds(level * 4);
        canInvincible = false;
        hudun.SetActive(false);
    }

    public void shoot()
    {
        if (this.transform.parent.name == "left_touch_controller_model_skel")
            LeftShake.Vibrate(VibrationForce.Min);

        if (this.transform.parent.name == "right_touch_controller_model_skel")
            RightShake.Vibrate(VibrationForce.Min);

        if (timetemp >= TimeDV)
        {
            float ranz = Random.Range(-3, 0);
            float rany = Random.Range(-2, 0);

            Quaternion temp = Quaternion.Euler(this.transform.eulerAngles + new Vector3(90, rany, ranz));
            GameObject obj = (GameObject)Instantiate(perb, firepos.position, temp, father);
            Bullet bul = obj.GetComponent<Bullet>();
            bul.direction = vect;

            if (defaultbullet == bullet.normal)
            {
                if(ShootFast)
                    TimeDV = bul.TimeDV = 0.05f;
                else
                    TimeDV = bul.TimeDV = ndata.TimeDV;
                bul.size = ndata.size;
                bul.color = ndata.color;
                bul.type = ndata.type;
                bul.LifeTime = ndata.LifeTime;
                bul.damage = ndata.damage;
                bul.speed = ndata.speed;
            }
            else
            {
                if (ShootFast)
                    TimeDV = bul.TimeDV = 0.1f;
                else
                    TimeDV = bul.TimeDV = sdata.TimeDV;
                bul.size = sdata.size;
                bul.color = sdata.color;
                bul.type = sdata.type;
                bul.LifeTime = sdata.LifeTime;
                bul.damage = sdata.damage;
                bul.speed = sdata.speed;
            }
            timetemp = 0;
        }
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
        else
        {
            blood = maxblood;
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
            Controller.gameover = true;
        }
    }

    private void OnCollisionEnter(Collision collision)//被射中
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (!canInvincible)
                subblood(collision.gameObject.GetComponent<Bullet>().damage);
            if (this.transform.parent.name == "left_touch_controller_model_skel")
                LeftShake.Vibrate(VibrationForce.Hard);
            if (this.transform.parent.name == "right_touch_controller_model_skel")
                RightShake.Vibrate(VibrationForce.Hard);
            collision.gameObject.GetComponent<Bullet>().destory();
            if (Controller.gameover)
                print("you dead!");
        }
    }

    public void changebullet()
    {
        if (defaultbullet == bullet.normal)
        {
            defaultbullet = bullet.slow;
            print("change bullet to slow");
            return;
        }
        if (defaultbullet == bullet.slow)
        {
            defaultbullet = bullet.normal;
            print("change bullet to normal");
            return;
        }
    }
}