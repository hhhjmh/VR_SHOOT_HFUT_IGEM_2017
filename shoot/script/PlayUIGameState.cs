using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum UIState
{
    GameOver,
    GameWin
}

public class PlayUIGameState : MonoBehaviour
{
    private Text score;
    private Text time;
    public UIState state;

    void Start()
    {
        try
        {
            score = this.transform.FindChild("score").GetComponent<Text>();
            time = this.transform.FindChild("time").GetComponent<Text>();
            score.text = "";
            time.text = "";
        }
        catch (System.Exception ex)
        {
            Debug.LogError("cant find obj!");
        }
    }

    private bool flag = false;
    void Update()
    {
        if (flag == false)//执行一次
        {
            string temp = PlayUI.Times.ToString();
            if (!temp.Contains("."))//整数
                temp += ".0";
            string[] tempp = temp.Split('.');
            if(SimpleData.getInstance().isbest(Controller.Score, tempp[0] + '.' + tempp[1][0]))
            {
                score.color = new Color(1, 1, 0);
                score.text = "New Best Score ! " + Controller.Score+"!";
                time.color = new Color(1, 1, 0);
                time.text = "Your Time is " + tempp[0] + '.' + tempp[1][0] + "s";
            }
            else
            {
                score.text = "Your Score is " + Controller.Score;
                time.text = "Your Time is " + tempp[0] + '.' + tempp[1][0] + "s";
            }
            flag = true;
        }
        if(state==UIState.GameOver)
        {
            if(OVRInput.GetDown(OVRInput.RawButton.X))//按x键从新开始,a键回到主界面
            {
                print("restart");
                //传递数据
                SceneManager.LoadScene("1");
            }
            if(OVRInput.GetDown(OVRInput.RawButton.A))
            {
                print("backtomain");
                //传递数据
                SceneManager.LoadScene("0");
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))//a键回到主界面
            {
                print("backtomain");
                //传递数据
                SceneManager.LoadScene("0");
            }
        }
    }
}