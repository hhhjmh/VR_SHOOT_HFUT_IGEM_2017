using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SearchController : MonoBehaviour
{
    public float DeltaTime = 5;
    public float Threshold = 2;//阈值
    public float Count = 1;
    public GameObject perb = null;

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
        if (Controller.gamebegin)
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
        for (int i = 0; i < Count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(min_x, max_x), Random.Range(min_y, max_y), Random.Range(min_z, max_z));
            perba.Add(pos);
        }
        foreach (var a in perba)
        {
            Quaternion temp = Quaternion.Euler(new Vector3(0, 0, 0));
            GameObject obj = Instantiate(perb, a, temp);
        }
    }
}