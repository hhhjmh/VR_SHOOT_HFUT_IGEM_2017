using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LorR
{
    LeftHand,
    RightHand
}

[RequireComponent(typeof(BoxCollider))]
public class Controller : MonoBehaviour//数据传输
{
    public LorR Hand;
    public Transform cell;
    public static bool getthecell = false;//记录能否拿到细胞
    public static string WhichHand = "";//全局记录拿到细胞的是哪只手
    public static GameObject maincell = null;//全局记录maincell对象
    public static bool gameover = false;
    public static bool gamewin = false;
    public static bool gamebegin = false;
    public static float Score = 0;
    private bool isthecell = false;
    private Vector3 maincell_pos;
    private Vector3 maincell_euler;
    private Transform father;//maincell的父对象
    public static float AllTime;

    void Start()
    {
        AllTime = 0.0f;
        Score = 0.0f;
        Time.timeScale = 1.0f;
        getthecell = false;
        WhichHand = "";
        gameover = false;
        gamewin = false;
        gamebegin = false;//重要
        father = cell.parent;
        maincell_pos = cell.position;
        maincell_euler = cell.eulerAngles;
        //print(Hand == LorR.LeftHand ? "Left " : "Right " + "maincell_pos:" + maincell_pos);
        //print(Hand == LorR.LeftHand ? "Left " : "Right " + "maincell_euler:" + maincell_euler);
        try
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
        }
        catch (System.Exception ex)
        {
            string temp = Hand == LorR.LeftHand ? "Left" : "Right";
            Debug.LogError(temp + " no BoxCollider!");
        }
    }
    private bool canaddtimes = false;
    void Update()
    {
        if (canaddtimes == true)
            AllTime += Time.deltaTime;
        if (isthecell == true)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger) || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
            {
                //print("get the cell");
                getthecell = true;
            }
        }

        if(Hand == LorR.RightHand)
        {
            if (gameover == true)
            {
                gameOver();
            }

            if (gamewin == true)
            {
                gameWin();
            }
        }

        if (getthecell == true)
        {
            if (Hand == LorR.LeftHand)
                this.gameObject.transform.FindChild(@"lctrl:geometry_null").gameObject.SetActive(false);
            else
            {
                this.gameObject.transform.Find(@"rctrl:geometry_null").gameObject.SetActive(false);
            }
            CtrlCell();
        }
        else
        {
            if (Hand == LorR.LeftHand)
                this.gameObject.transform.FindChild(@"lctrl:geometry_null").gameObject.SetActive(true);
            else
            {
                this.gameObject.transform.Find(@"rctrl:geometry_null").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name == "maincell")
        {
            isthecell = true;
            maincell = other.transform.gameObject;
            if (this.Hand == LorR.LeftHand)
                WhichHand = "Left";
            else
                WhichHand = "Right";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.name == "maincell")
        {
            isthecell = false;
            //WhichHand = "";
        }
    }

    public void CtrlCell()
    {
        canaddtimes = true;
        if (Hand == LorR.LeftHand && WhichHand == "Left")
        {
            maincell.transform.SetParent(this.transform);
            maincell.transform.localPosition = Vector3.zero;
            maincell.transform.localEulerAngles = Vector3.zero;
            gamebegin = true;
            shoot(Hand);
        }
        if (Hand == LorR.RightHand && WhichHand == "Right")
        {
            maincell.transform.SetParent(this.transform);
            maincell.transform.localPosition = Vector3.zero;
            maincell.transform.localEulerAngles = Vector3.zero;
            gamebegin = true;
            shoot(Hand);
        }
    }

    private void SetBack()
    {
        getthecell = false;
        WhichHand = "";
        maincell = null;
        isthecell = false;
    }

    public void UnCtrlCell()
    {
        canaddtimes = false;
        cell.transform.SetParent(father);
        cell.gameObject.transform.position = this.maincell_pos;
        cell.gameObject.transform.eulerAngles = this.maincell_euler;
        SetBack();
    }

    private bool flag = false;
    private bool flag2 = false;
    public void shoot(LorR Hand)
    {
        if (Hand == LorR.LeftHand)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
                flag = true;
            if(flag)
                cell.GetComponent<Cell>().shoot();
            if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
                flag = false;
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                flag2 = true;
            if (flag2)
                cell.GetComponent<Cell>().shoot();
            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
                flag2 = false;
        }
    }

    private bool doonce1 = false;
    public void gameOver()
    {
        UnCtrlCell();
        if (doonce1 == false)
        {
            print("score: " + Score);
            print("times: " + AllTime);
            doonce1 = true;
            //弹出来UI
            GameObject.Find("playUI").SetActive(false);
            GameInit.playUI_gameover.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    private bool doonce2 = false;
    public void gameWin()
    {
        if (doonce2 == false)
        {
            print("win!");
            print("score: " + Score);
            print("times: " + AllTime);
            doonce2 = true;
            //弹出来UI
            GameObject.Find("playUI").SetActive(false);
            GameInit.playUI_gamewin.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}