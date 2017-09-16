using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GenController : MonoBehaviour
{
    public float DeltaTime = 5;
    public float Threshold = 2;//DeltaTime+-阈值
    public int CountMax = 4;
    public bool CanProduce = true;//能否生成gen
    public DZController dzc;
    //预制件
    public GameObject begin = null;
    public GameObject agen = null;
    public GameObject bgen = null;
    public GameObject end = null;

    private BoxCollider boder;
    private float min_x;
    private float max_x;
    private float min_y;
    private float max_y;
    private float min_z;
    private float max_z;
    private float timetemp = 0;
    private float timedx;

    void Start()
    {
        timedx = DeltaTime;
        boder = this.transform.GetComponent<BoxCollider>();
        boder.isTrigger = true;
        max_x = boder.center.x + boder.size.x / 2;
        min_x = boder.center.x - boder.size.x / 2;
        max_y = boder.center.y + boder.size.y / 2;
        min_y = boder.center.y - boder.size.y / 2;
        max_z = boder.center.z + boder.size.z / 2;
        min_z = boder.center.z - boder.size.z / 2;
    }

    void Update()
    {
        if (Controller.gamebegin&&this.CanProduce)
        {
            timetemp += Time.deltaTime;
            if (timetemp >= DeltaTime)
            {
                InitPerb();
                timetemp = 0.0f;
            }
        }
    }

    private void InitPerb()
    {
        DeltaTime = Random.Range(timedx - Threshold, timedx + Threshold);
        List<Vector3> perba = new List<Vector3>();
        for (int i = 0; i < Random.Range(1, CountMax + 1); i++)
        {
            Vector3 pos = new Vector3(Random.Range(min_x, max_x), Random.Range(min_y, max_y), Random.Range(min_z, max_z));
            perba.Add(pos);
        }
        foreach (var a in perba)
        {
            Quaternion temp = Quaternion.Euler(new Vector3(0, 0, 0));
            GameObject obj = null;
            switch (Random.Range(1,5))
            {
                case 1:
                    obj = Instantiate(begin, a, temp);
                    obj.GetComponent<Gen>().type = genlist.begin;
                    break;
                case 2:
                    obj = Instantiate(agen, a, temp);
                    obj.GetComponent<Gen>().type = genlist.agen;
                    break;
                case 3:
                    obj = Instantiate(bgen, a, temp);
                    obj.GetComponent<Gen>().type = genlist.bgen;
                    break;
                case 4:
                    obj = Instantiate(end, a, temp);
                    obj.GetComponent<Gen>().type = genlist.end;
                    break;
            }
            obj.GetComponent<Gen>().speed = Random.Range(0.1f, 0.2f);//0.1f;
            obj.GetComponent<Gen>().life = Random.Range(5.0f, 10.0f);//10f;
            obj.GetComponent<Gen>().dzc = dzc;
        }
    }
}