using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum genlist
{
    begin,
    agen,
    bgen,
    cgen,
    end
}

public class Groove
{
    private GameObject perb;//槽的预制件
    private genlist type;//种类
    private Color base_color;//原始颜色，不要改
    private bool isempty = true;
    private Color after_color;//传入的颜色
    public bool IsEmpty
    {
        get
        {
            return this.isempty;
        }
        set
        {
            if (value)
            {
                this.perb.GetComponent<Image>().color =this.base_color;
                this.isempty = true;
            }
            else
            {
                this.perb.GetComponent<Image>().color = this.after_color;
                this.isempty = false;
            }
        }
    }
    public Groove(GameObject perb, genlist type,Color base_color, Color after_color)
    {
        this.perb = perb;
        this.type = type;
        this.base_color = base_color;
        this.after_color = after_color;
        this.IsEmpty = true;
    }
    public GameObject GetPerb()//获取预制件
    {
        return this.perb;
    }
    public genlist _GetType()
    {
        return this.type;
    }
    public override string ToString()
    {
        return "name:"+perb.name+"\n"+"after_color:" + this.after_color;
    }
}