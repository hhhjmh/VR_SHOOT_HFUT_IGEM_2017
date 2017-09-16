using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DZController : MonoBehaviour//挂在到DZController
{
    //UI预制件
    public GameObject start;
    public GameObject agen;
    public GameObject bgen;
    public GameObject end;
    public Text DZtip;

    //发射器
    public Cell maincell;
    [Range(2,8)]
    public int count;//随机生成的最大值
    public Color basestartcolor;
    public Color baseagencolor;
    public Color basebgencolor;
    public Color baseendcolor;

    public Color startcolor;
    public Color agencolor;
    public Color bgencolor;
    public Color endcolor;

    private int point = -1;//当前指针
    private List<Groove> GrooveList = new List<Groove>();
    private int maxcount=2;//当前生成的最大数量,生成一次变一次，至少为2

    private bool candz = false;//能否放大招


    private void Start()
    {
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
                    maincell.makedz(maxcount-2);
                    ReProduce();
                    this.DZtip.gameObject.SetActive(false);
                    candz = false;
                }
            }
        }
    }

    public void check(GameObject gen)//用于检测当前碰到的gen对象是否为groovelist下一个未激活对象
    {
        if ((point + 1) < maxcount)
        {
            if (GrooveList[point + 1]._GetType() == gen.GetComponent<Gen>().type)
            {
                GrooveList[point + 1].IsEmpty = false;
                point++;
                if((point+1) >= maxcount)
                    candz = true;//可以释放大招
            }
            else
                ReProduce();
        }
        else
        {
            candz = true;//可以释放大招
        }
    }

    void ReProduce()//初始化，实例化预制件（生成链）
    {
        point = -1;
        for (int i = 0; i < this.transform.childCount; i++)
            Destroy(this.transform.GetChild(i).gameObject);
        GrooveList.Clear();
        maxcount = Random.Range(2, count + 1);

        GameObject startobj = Instantiate(start, this.transform);
        Groove startgroove = new Groove(startobj, genlist.begin, basestartcolor, startcolor);
        GrooveList.Add(startgroove);
        for(int i=0;i<maxcount-2;i++)
        {
            if(Random.Range(0,2)==0)
            {
                GameObject agenobj = Instantiate(agen, this.transform);
                Groove agengroove = new Groove(agenobj, genlist.agen, baseagencolor, agencolor);
                GrooveList.Add(agengroove);
            }
            else
            {
                GameObject bgenobj = Instantiate(bgen, this.transform);
                Groove bgengroove = new Groove(bgenobj, genlist.bgen, basebgencolor, bgencolor);
                GrooveList.Add(bgengroove);
            }
        }
        GameObject endobj = Instantiate(end, this.transform);
        Groove endgroove = new Groove(endobj, genlist.end, baseendcolor, endcolor);
        GrooveList.Add(endgroove);
    }
}