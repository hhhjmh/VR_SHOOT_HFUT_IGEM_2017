using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    public RectTransform blood;
    private GameObject round;
    public GameObject roundobj
    {
        get { return this.round; }
    }
    public Text times;
    public Text score;
    private Cell maincell;
    private float bloodmax;
    private float scale_x;
    private float scale_y;
    private float scale_z;
    private Text tblood;
    private float timer;
    public static float Times = 0.0f;

    private void Awake()
    {
        Times = 0.0f;
        timer = 0.0f;
        round = GameObject.Find("round");
    }

    void Start()
    {
        try
        {
            times.gameObject.SetActive(false);
            roundobj.SetActive(false);
            tblood = GameObject.Find("tblood").GetComponent<Text>();
            blood.gameObject.SetActive(false);
            scale_x = blood.localScale.x;
            scale_y = blood.localScale.y;
            scale_z = blood.localScale.z;
            tblood.text = "";
            score.text = "";
            maincell = GameObject.Find("maincell").GetComponent<Cell>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Blood Init error!");
        }
    }

    void Update()
    {
        bloodmax = maincell.maxblood;
        if(Controller.gamebegin==true)
        {
            blood.transform.FindChild("Image").GetComponent<Image>().color = new Color(1, 0, 0);
            times.gameObject.SetActive(true);
            blood.gameObject.SetActive(true);
            timer += Time.deltaTime;
            Times= timer;
            string temp = timer.ToString();
            if(!temp.Contains("."))//整数
                temp += ".0";
            string[] tempp = temp.Split('.');
            times.text = "Time:" + tempp[0] + '.' + tempp[1][0] + "s";//报错？？

            float bloodtemp= maincell.Blood;
            float scale = bloodtemp / bloodmax;
            blood.localScale = new Vector3(scale_x, scale*scale_y, scale_z);
            tblood.text = ((int)(scale * 100)).ToString()+"%";
            score.text = "Score : "+((int)Controller.Score).ToString();
        }
        else
        {
            blood.gameObject.SetActive(false);
            score.text = "";
            tblood.text = "";
        }
    }
}