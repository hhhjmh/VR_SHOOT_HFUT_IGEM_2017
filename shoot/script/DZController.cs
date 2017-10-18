using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalData
{
    public static int choice = 0;
    public static bool cancheck = false;
}
[RequireComponent(typeof(Image))]
public class DZController : MonoBehaviour//挂在到DZController
{
    //     [HideInInspector]
    //     public GameObject startobj;//已经存在的UI
    //UI预制件
    public GameObject start;
    public GameObject agen;
    public GameObject bgen;
    //public GameObject cgen;
    public GameObject end;
    public Text DZtip;

    //发射器
    public Cell maincell;
    [Range(2,8)]
    public int count;//随机生成的最大值
    [Range(1,4)]
    public int number;//第几条技能链
//     public Color basefirstcolor;
//     public Color basesecondcolor;
//     public Color basethirdcolor;
//    // public Color basecgencolor;
//     public Color basefourthcolor;
// 
//     public Color firstcolor;
//     public Color secondcolor;
//     public Color thirdcolor;
//     //public Color cgencolor;
//     public Color fourthcolor;

    [HideInInspector]
    public int point = -1;//当前指针
    private List<Groove> GrooveList = new List<Groove>();
    //private int maxcount=2;//当前生成的最大数量,生成一次变一次，至少为2

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
                        maincell.makedz(3, this.number);
                    ReProduce();
                    this.DZtip.gameObject.SetActive(false);
                    candz = false;
                }
            }
        }
    }

    public void setfull()
    {
        if (GlobalData.cancheck)
        {
            GrooveList[0].IsEmpty = false;
            GrooveList[1].IsEmpty = false;
            GrooveList[2].IsEmpty = false;
            GrooveList[3].IsEmpty = false;
            (GrooveList[0].GetPerb()).GetComponent<GenUI>().show = true;
            (GrooveList[1].GetPerb()).GetComponent<GenUI>().show = true;
            (GrooveList[2].GetPerb()).GetComponent<GenUI>().show = true;
            (GrooveList[3].GetPerb()).GetComponent<GenUI>().show = true;
            GlobalData.choice = this.number;
            candz = true;//可以释放大招
        }
    }

    public bool check(GameObject gen)//用于检测当前碰到的gen对象是否为groovelist下一个未激活对象，如果检测到startgroove==null返回true
    {
//         if (gen.GetComponent<Gen>().type == genlist.begin)//碰到红色之后才执行
//         {
//             if (GlobalData.startgroove==null)
//             {
//                 GlobalData.startgroove = new Groove(startobj, genlist.begin, basestartcolor, startcolor);
//                 GlobalData.startgroove.IsEmpty = false;
                   GlobalData.cancheck = true;
//                 //print(GlobalData.startgroove.ToString());
//                 return true;
//             }
//         }
        if (GlobalData.cancheck)
        {
            if ((point + 1) < 4)
            {
                if (GrooveList[point + 1]._GetType() == gen.GetComponent<Gen>().type&& GrooveList[point + 1].After_Color.r== gen.GetComponent<Gen>().color.r && GrooveList[point + 1].After_Color.g == gen.GetComponent<Gen>().color.g && GrooveList[point + 1].After_Color.b == gen.GetComponent<Gen>().color.b)
                {
                    GrooveList[point + 1].IsEmpty = false;
                    (GrooveList[point + 1].GetPerb()).GetComponent<GenUI>().show = true;
                    //gen.GetComponent<GenUI>().show = true;
                    point++;
                    if ((point + 1) >= 4)
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
        //maxcount = Random.Range(2, count + 1);

        Color color=new Color();
        Color basecolor=new Color();
        switch(number)
        {
            case 1:
                color = new Color(1,0,0);
                basecolor = new Color(0.5f,0,0);
                break;
            case 2:
                color = new Color(0,1,0);
                basecolor = new Color(0,0.5f,0);
                break;
            case 3:
                color = new Color(0,0,1);
                basecolor = new Color(0,0,0.5f);
                break;
            case 4:
                color = new Color(1,1,0);
                basecolor = new Color(0.5f,0.5f,0);
                break;
        }
        GameObject startobj = Instantiate(start, this.transform);
        Groove startgroove = new Groove(startobj, genlist.begin, basecolor, color);
        GrooveList.Add(startgroove);
//         for(int i=0;i<4;i++)
//         {
//             var temp = Random.Range(0, 3);//0,1,2
//             if (temp == 0)
//             {
                GameObject agenobj = Instantiate(agen, this.transform);
                Groove agengroove = new Groove(agenobj, genlist.agen, basecolor, color);
                GrooveList.Add(agengroove);
//             }
//             else
//             {
                GameObject bgenobj = Instantiate(bgen, this.transform);
                Groove bgengroove = new Groove(bgenobj, genlist.bgen, basecolor, color);
                GrooveList.Add(bgengroove);
//             }
//         }
        GameObject endobj = Instantiate(end, this.transform);
        Groove endgroove = new Groove(endobj, genlist.end, basecolor, color);
        GrooveList.Add(endgroove);
    }
}