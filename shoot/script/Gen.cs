using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Gen : MonoBehaviour
{
    //需要赋值的数据
    public genlist type = genlist.begin;
    public float life = 10.0f;
    public float speed = 1.0f;
    [HideInInspector]
    public DZController dzc = null;
    
    private Vector3 pos;
    private float timetemp;

    void Start()
    {
        timetemp = 0;
        this.GetComponent<BoxCollider>().isTrigger = false;
        pos = this.transform.position;
    }

    void Update()
    {
        timetemp += Time.deltaTime;
        if (timetemp >= life)
            Destroy(this.gameObject);
        this.transform.position = pos;
        this.transform.Rotate(new Vector3(0, 5f*speed, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "maincell")
        {
            dzc.check(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}