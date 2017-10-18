using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenUI : MonoBehaviour
{
    [HideInInspector]
    public bool show = false;
    private Image kuang = null;

    void Start()
    {
        show = false;
        kuang = this.transform.FindChild("kuang").GetComponent<Image>();
        kuang.enabled = false;
    }

    private bool doonce=false;
    void Update()
    {
        if(show)
        {
            if(!doonce)
            {
                kuang.enabled = true;
                doonce = true;
            }
        }
    }
}