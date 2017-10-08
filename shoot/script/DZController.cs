using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalData
{
    public static int choice = 0;
    public static bool cancheck = false;
    public static Groove startgroove = null;
}
[RequireComponent(typeof(Image))]
public class DZController : MonoBehaviour//挂在到DZController
{
    [HideInInspector]
    public GameObject startobj;//已经存在的UI
    //UI预制件
    public GameObject agen;
    public GameObject bgen;
    public GameObject cgen;
    public GameObject end;
    public Text DZtip;

    //发射器
    public Cell maincell;
    [Range(2,8)]
    public int count;//随机生成的最大值
    [Range(1,4)]
    public int number;//第几条技能链
    public Color basestartcolor;
    public Color baseagencolor;
    public Color basebgencolor;
    public Color basecgencolor;
    public Color baseendcolor;

    public Color startcolor;
    public Color agencolor;
    public Color bgencolor;
    public Color cgencolor;
    public Color endcolor;

    [HideInInspector]
    public int point = -1;//当前指针
    private List<Groove> GrooveList = new List<Groove>();
    private int maxcount=2;//当前生成的最大数量,生成一次变一次，至少为2

    private bool candz = false;//能否放大招


    private void Start()
    {
        this.GetComponent<Image>().enabled = false;
        ReProduce();
        this.DZtip.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Controller.gamebegin == true)
        {
            if (candz)
            {
                this.DZtip.gameObject.SetActive(true);
                if (OVRInput.GetDown(OVRInput.RawButton.Y) | OVRInput.GetDown(OVRInput.RawButton.B))
                {
                    //释放大招
                    if (GlobalData.choice == this.number)
                        maincell.makedz(maxcount - 1, this.number);
                    ReProduce();
                    this.DZtip.gameObject.SetActive(false);
                    candz = false;
                }
            }
        }
    }

    public bool check(GameObject gen)//用于检测当前碰到的gen对象是否为groovelist下一个未激活对象，如果检测到startgroove==null返回true
    {
        if (gen.GetComponent<Gen>().type == genlist.begin)//碰到红色之后才执行
        {
            if (GlobalData.startgroove==null)
            {
                GlobalData.startgroove = new Groove(startobj, genlist.begin, basestartcolor, startcolor);
                GlobalData.startgroove.IsEmpty = false;
                GlobalData.cancheck = true;
                //print(GlobalData.startgroove.ToString());
                return true;
            }
        }
        if (GlobalData.cancheck)
        {
            if ((point + 1) < maxcount)
            {
                if (GrooveList[point + 1]._GetType() == gen.GetComponent<Gen>().type)
                {
                    GrooveList[point + 1].IsEmpty = false;
                    point++;
                    if ((point + 1) >= maxcount)
                    {
                        GlobalData.choice = this.number;
                        candz = true;//可以释放大招
                    }
                }
                else
                    ReProduce();
            }
            else
            {
                GlobalData.choice = this.number;
                candz = true;//可以释放大招
            }
        }
        return false;
    }

    void ReProduce()//初始化，实例化预制件（生成链）
    {
        point = -1;
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(this.transform.GetChild(i).gameObject);
        GrooveList.Clear();
        maxcount = Random.Range(2, count + 1);

//         GameObject startobj = start;
//         Groove startgroove = new Groove(startobj, genlist.begin, basestartcolor, startcolor);
//         GrooveList.Add(startgroove);
        for(int i=0;i<maxcount-1;i++)
        {
            var temp = Random.Range(0, 3);//0,1,2
            if (temp == 0)
            {
                GameObject agenobj = Instantiate(agen, this.transform);
                Groove agengroove = new Groove(agenobj, genlist.agen, baseagencolor, agencolor);
                GrooveList.Add(agengroove);
            }
            else if (temp == 1)
            {
                GameObject bgenobj = Instantiate(bgen, this.transform);
                Groove bgengroove = new Groove(bgenobj, genlist.bgen, basebgencolor, bgencolor);
                GrooveList.Add(bgengroove);
            }
            else
            {
                GameObject cgenobj = Instantiate(cgen, this.transform);
                Groove cgengroove = new Groove(cgenobj, genlist.cgen, basecgencolor, cgencolor);
                GrooveList.Add(cgengroove);
            }
        }
        GameObject endobj = Instantiate(end, this.transform);
        Groove endgroove = new Groove(endobj, genlist.end, baseendcolor, endcolor);
        GrooveList.Add(endgroove);
    }
}