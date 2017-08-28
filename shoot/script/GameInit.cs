using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EasyOrHard
{
    easy,
    hard
}

public class GameInit : MonoBehaviour
{
    private Cell maincell;

    public state GameType;
    public EasyOrHard Noob;
    public static GameObject playUI_gamewin;
    public GameObject gamewin;
    public static GameObject playUI_gameover;
    public GameObject gameover;
    public AudioSource audiosource;

    [HideInInspector]
    public string mname;
    [HideInInspector]
    public Vector3 color;
    [HideInInspector]
    public int maxblood;
    [HideInInspector]
    public int number;

    private void SetGame(BulletData ndata, BulletData sdata, string name, Vector3 maincolor, EasyOrHard noob = EasyOrHard.easy, state type = state.classic, int maxBlood = 100, int number = 5)//随机外观
    {
        maincell = GameObject.Find("maincell").GetComponent<Cell>();
        maincell.m_name.text = name.ToString();
        int temp = (int)number % maincell.textures.Length;
        maincell.m_logo.sprite = maincell.textures[temp];
        maincell.transform.FindChild("model").GetComponent<MeshRenderer>().materials[0].color = new Color(maincolor.x, maincolor.y, maincolor.z);

        GameObject.Find("enemycontroller").GetComponent<EnemyController>().GameType = type;
        BulletData endata = new BulletData(ndata.TimeDV * 0.6f, ndata.size, ndata.color, ndata.type, ndata.LifeTime, ndata.damage, ndata.speed * 1.3f);
        BulletData hsdata = new BulletData(sdata.TimeDV * 0.6f, sdata.size, sdata.color, sdata.type, sdata.LifeTime, sdata.damage, sdata.speed * 1.3f);
        maincell.ndata = (noob == EasyOrHard.easy ? endata : ndata);
        maincell.sdata = (noob == EasyOrHard.easy ? hsdata : sdata);
        maincell.maxblood = maxBlood;
        maincell.ShowLine = (noob == EasyOrHard.easy ? true : false);
        enemy.isnoob = (noob == EasyOrHard.easy ? true : false);
    }

    private void Awake()
    {
        //赋值
        //this.name = xxx;
        this.Noob = SimpleData.getInstance().Noob;
        this.GameType = SimpleData.getInstance().GameType;
        playUI_gameover = gameover;
        playUI_gamewin = gamewin;
        playUI_gameover.SetActive(false);
        playUI_gamewin.SetActive(false);

        audiosource.loop = true;
        audiosource.volume = SimpleData.getInstance().volume;

        SetGame(
            new BulletData(
                0.2f,
                new Vector3(0.05f, 0.05f, 0.05f),
                new Color(0.34f, 0.56f, 0.68f),
                bullet.normal,
                2f,
                20f,
                10f),
            new BulletData(
                0.5f,
                new Vector3(0.05f, 0.05f, 0.05f),
                new Color(0.0f, 0.0f, 0.0f),
                bullet.slow,
                2f,
                40f,
                5f),
            "BioBrick-A" + Random.Range((int)100, (int)151).ToString(),//改成name
            new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
            Noob,
            GameType, Random.Range((int)90, (int)151), Random.Range((int)0, (int)201));//重要
    }
}