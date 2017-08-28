using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject Help;
    public GameObject Player;
    public GameObject mRank;
    public GameObject Exit;
    public GameObject setting;
    public GameObject easy;
    public GameObject classic;
    public GameObject music;
    public GameObject sound;
    public AudioSource audiosource;

    void Start()
    {
        audiosource.loop = true;
        audiosource.volume = SimpleData.getInstance().volume;
        audiosource.Play();

        music.GetComponent<Slider>().value = 1.0f;
        sound.GetComponent<Slider>().value = 1.0f;
        Time.timeScale = 1.0f;
        Help.SetActive(true);
        Player.SetActive(true);
        Exit.SetActive(true);
        setting.SetActive(false);
    }

    void Update()
    {
        GameObject.Find("LeftHandAnchor").transform.position = GameObject.Find("controller_left").transform.position;
        GameObject.Find("LeftHandAnchor").transform.eulerAngles = GameObject.Find("controller_left").transform.eulerAngles;
        GameObject.Find("RightHandAnchor").transform.position = GameObject.Find("controller_right").transform.position;
        GameObject.Find("RightHandAnchor").transform.eulerAngles = GameObject.Find("controller_right").transform.eulerAngles;
    }

    public void BeginGame()
    {
        Help.SetActive(false);
        Player.SetActive(false);
        mRank.SetActive(false);
        Exit.SetActive(false);
        setting.SetActive(true);
    }

    public void closesetting()
    {
        changemode();
        changenoob();
        Help.SetActive(true);
        Player.SetActive(true);
        mRank.SetActive(true);
        Exit.SetActive(true);
        setting.SetActive(false);
    }

    public void Play()
    {
        changemode();
        changenoob();
        SceneManager.LoadScene("1");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void changemode()//c i
    {
        if (classic.name == "classic")
        {
            if (classic.GetComponent<Toggle>().isOn == true)
            {
                SimpleData.getInstance().GameType = state.classic;
                print("classic");
            }
            else
            {
                SimpleData.getInstance().GameType = state.infinite;
                print("infinite");
            }
        }
    }

    public void changemusic()
    {
        SimpleData.getInstance().volume = audiosource.volume = music.GetComponent<Slider>().value * sound.GetComponent<Slider>().value;
    }

    public void changesound()
    {
        SimpleData.getInstance().volume = audiosource.volume = music.GetComponent<Slider>().value * sound.GetComponent<Slider>().value;
    }

    public void changenoob()//e n
    {
        if (easy.name == "easy")
        {
            if (easy.GetComponent<Toggle>().isOn == true)
            {
                SimpleData.getInstance().Noob = EasyOrHard.easy;
                print("easy");
            }
            else
            {
                SimpleData.getInstance().Noob = EasyOrHard.hard;
                print("normal");
            }
        }
    }
}