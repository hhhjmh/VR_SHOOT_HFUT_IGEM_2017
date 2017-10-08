using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Gen : MonoBehaviour
{
    //需要赋值的数据
    public genlist type = genlist.begin;
    public float life = 10.0f;
    public float speed = 1.0f;
    [HideInInspector]
    public DZController dzc1 = null;
    [HideInInspector]
    public DZController dzc2 = null;
    [HideInInspector]
    public DZController dzc3 = null;
    [HideInInspector]
    public DZController dzc4 = null;


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

    public void Sethightlight()
    {
        int[] array = new int[4] {
        dzc1.GetComponent<DZController>().point,
        dzc2.GetComponent<DZController>().point,
        dzc3.GetComponent<DZController>().point,
        dzc4.GetComponent<DZController>().point };
        int max = array[0];
        int temp = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (max < array[i])
            {
                max = array[i];
                temp = i;
            }
        }
        switch (temp)
        {
            case 0:
                GlobalData.choice = 1;
                GameObject.Find("explosion").GetComponent<Text>().color = new Color(1, 0, 0);
                GameObject.Find("shootfast").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("addblood").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("invincible").GetComponent<Text>().color = new Color(1, 1, 1);
                dzc1.GetComponent<Image>().enabled = true;
                dzc2.GetComponent<Image>().enabled = false;
                dzc3.GetComponent<Image>().enabled = false;
                dzc4.GetComponent<Image>().enabled = false;
                break;
            case 1:
                GlobalData.choice = 2;
                GameObject.Find("explosion").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("shootfast").GetComponent<Text>().color = new Color(1, 0, 0);
                GameObject.Find("addblood").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("invincible").GetComponent<Text>().color = new Color(1, 1, 1);
                dzc1.GetComponent<Image>().enabled = false;
                dzc2.GetComponent<Image>().enabled = true;
                dzc3.GetComponent<Image>().enabled = false;
                dzc4.GetComponent<Image>().enabled = false;
                break;
            case 2:
                GlobalData.choice =3;
                GameObject.Find("explosion").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("shootfast").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("addblood").GetComponent<Text>().color = new Color(1, 0, 0);
                GameObject.Find("invincible").GetComponent<Text>().color = new Color(1, 1, 1);
                dzc1.GetComponent<Image>().enabled = false;
                dzc2.GetComponent<Image>().enabled = false;
                dzc3.GetComponent<Image>().enabled = true;
                dzc4.GetComponent<Image>().enabled = false;
                break;
            case 3:
                GlobalData.choice = 4;
                GameObject.Find("explosion").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("shootfast").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("addblood").GetComponent<Text>().color = new Color(1, 1, 1);
                GameObject.Find("invincible").GetComponent<Text>().color = new Color(1, 0, 0);
                dzc1.GetComponent<Image>().enabled = false;
                dzc2.GetComponent<Image>().enabled = false;
                dzc3.GetComponent<Image>().enabled = false;
                dzc4.GetComponent<Image>().enabled = true;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "maincell")
        {
            if(!dzc1.check(this.gameObject))
            {
                dzc2.check(this.gameObject);
                dzc3.check(this.gameObject);
                dzc4.check(this.gameObject);
                this.Sethightlight();
            }
            Destroy(this.gameObject);
        }
    }
}