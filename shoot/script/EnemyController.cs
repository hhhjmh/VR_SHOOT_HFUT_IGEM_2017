using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum state
{
    classic,
    infinite
}

public class EnemyController : MonoBehaviour
{
    public float Radius = 5.0f;
    public GameObject EnemyNormal;
    public GameObject EnemyFast;
    public GameObject EnemyHard;
    public GameObject EnemyBoss;
    public GameObject EnemyFool;

    private int id = 0;
    private state gametype;
    private List<GameObject> EnemyNormalA = new List<GameObject>();
    private List<GameObject> EnemyFastA = new List<GameObject>();
    private List<GameObject> EnemyHardA = new List<GameObject>();
    private List<GameObject> EnemyBossA = new List<GameObject>();
    private List<GameObject> EnemyFoolA = new List<GameObject>();
    private GameObject roundobj;

    public state GameType
    {
        set { this.gametype = value; }
        get { return this.gametype; }
    }

    private void Start()
    {
        roundobj = GameObject.Find("playUI").GetComponent<PlayUI>().roundobj;
    }
    private float timetemp;
    private int roundtime = 1;
    void Update()
    {
        if (Controller.gamebegin == true)
        {
            if (this.gametype == state.classic)
            {
                timetemp += Time.deltaTime;

                if (timetemp <= 2f)
                {
                    roundobj.SetActive(true);
                    roundobj.GetComponent<Text>().text = "Round " + roundtime.ToString();
                }
                else
                    roundobj.SetActive(false);

                if (ClassicFlow_1_flag == false)//只做一次
                {
                    ClassicFlow_1();
                    timetemp = 0.0f;
                    roundtime = 1;
                }
                if (EnemyNormalA[0] == null &&
                    EnemyNormalA[1] == null &&

                    EnemyFoolA[0] == null)
                {
                    if (ClassicFlow_2_flag == false)//只做一次
                    {
                        ClassicFlow_2();
                        timetemp = 0.0f;
                        roundtime = 2;
                    }
                    if (EnemyNormalA[2] == null &&
                        EnemyNormalA[3] == null &&
                        EnemyNormalA[4] == null &&
                        EnemyNormalA[5] == null &&
                        EnemyNormalA[6] == null &&

                        EnemyFastA[0] == null &&

                        EnemyFoolA[0] == null &&
                        EnemyFoolA[1] == null)
                    {
                        if (ClassicFlow_3_flag == false)//只做一次
                        {
                            ClassicFlow_3();
                            timetemp = 0.0f;
                            roundtime = 3;
                        }
                        if (EnemyNormalA[7] == null &&
                            EnemyNormalA[8] == null &&
                            EnemyNormalA[9] == null &&

                            EnemyFastA[1] == null &&
                            EnemyFastA[2] == null &&
                            EnemyFastA[3] == null &&
                            EnemyFastA[4] == null &&
                            EnemyFastA[5] == null &&
                            EnemyFastA[6] == null &&

                            EnemyFoolA[2] == null &&
                            EnemyFoolA[3] == null &&
                            EnemyFoolA[4] == null &&
                            EnemyFoolA[5] == null &&
                            EnemyFoolA[6] == null &&
                            EnemyFoolA[7] == null &&
                            EnemyFoolA[8] == null &&
                            EnemyFoolA[9] == null)
                        {
                            if (ClassicFlow_4_flag == false)//只做一次
                            {
                                ClassicFlow_4();
                                timetemp = 0.0f;
                                roundtime = 4;
                            }
                            if (EnemyNormalA[10] == null &&
                                EnemyNormalA[11] == null &&
                                EnemyNormalA[12] == null &&
                                EnemyNormalA[13] == null &&
                                EnemyNormalA[14] == null &&
                                EnemyNormalA[15] == null &&
                                EnemyNormalA[16] == null &&
                                EnemyNormalA[17] == null &&

                                EnemyFastA[7] == null &&
                                EnemyFastA[8] == null &&
                                EnemyFastA[9] == null &&
                                EnemyFastA[10] == null &&
                                EnemyFastA[11] == null &&
                                EnemyFastA[12] == null &&
                                EnemyFastA[13] == null &&

                                EnemyHardA[0] == null &&
                                EnemyHardA[1] == null &&
                                EnemyHardA[2] == null &&
                                EnemyHardA[3] == null &&
                                EnemyHardA[4] == null &&
                                EnemyHardA[5] == null &&
                                EnemyHardA[6] == null &&

                                EnemyFoolA[10] == null &&
                                EnemyFoolA[11] == null &&
                                EnemyFoolA[12] == null &&
                                EnemyFoolA[13] == null &&
                                EnemyFoolA[14] == null &&
                                EnemyFoolA[15] == null &&
                                EnemyFoolA[16] == null &&
                                EnemyFoolA[17] == null &&
                                EnemyFoolA[18] == null &&
                                EnemyFoolA[19] == null &&
                                EnemyFoolA[20] == null)
                            {
                                if (ClassicFlow_5_flag == false)//只做一次
                                {
                                    ClassicFlow_5();
                                    timetemp = 0.0f;
                                    roundtime = 5;
                                }
                                if (EnemyNormalA[18] == null &&
                                    EnemyNormalA[19] == null &&
                                    EnemyNormalA[20] == null &&

                                    EnemyFastA[14] == null &&
                                    EnemyFastA[15] == null &&
                                    EnemyFastA[16] == null &&
                                    EnemyFastA[17] == null &&
                                    EnemyFastA[18] == null &&
                                    EnemyFastA[19] == null &&
                                    EnemyFastA[20] == null &&
                                    EnemyFastA[21] == null &&
                                    EnemyFastA[22] == null &&
                                    EnemyFastA[23] == null &&
                                    EnemyFastA[24] == null &&
                                    EnemyFastA[25] == null &&
                                    EnemyFastA[26] == null &&

                                    EnemyBossA[0] == null &&
                                    EnemyBossA[1] == null &&
                                    EnemyBossA[2] == null &&
                                    EnemyBossA[3] == null &&

                                    EnemyHardA[7] == null &&
                                    EnemyHardA[8] == null &&

                                    EnemyFoolA[21] == null &&
                                    EnemyFoolA[22] == null &&
                                    EnemyFoolA[23] == null &&
                                    EnemyFoolA[24] == null &&
                                    EnemyFoolA[25] == null &&
                                    EnemyFoolA[26] == null &&
                                    EnemyFoolA[27] == null &&
                                    EnemyFoolA[28] == null &&
                                    EnemyFoolA[29] == null &&
                                    EnemyFoolA[30] == null &&
                                    EnemyFoolA[31] == null &&
                                    EnemyFoolA[32] == null &&
                                    EnemyFoolA[33] == null &&
                                    EnemyFoolA[34] == null &&
                                    EnemyFoolA[35] == null)
                                {
                                    Controller.gamewin = true;
                                    Controller.gamebegin = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (doonce == false)
                {
                    DeltaCall(2, 2, 2, 2, 2);
                    doonce = true;
                }
                lasttime += Time.deltaTime;
                if (lasttime >= Random.Range(30f, 40f))
                {
                    DeltaCall(5, 3, 6, 4, 2);
                    lasttime = 0.0f;
                }
            }
        }
    }

    private bool doonce = false;
    private float lasttime = 0.0f;
    private void DeltaCall(int normal, int fast, int fool, int hard, int boss)//参数都是最大值
    {
        for (int i = 0; i < Random.Range(0, normal + 1); i++)
        {
            InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        }
        for (int i = 0; i < Random.Range(0, fast + 1); i++)
        {
            InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        }
        for (int i = 0; i < Random.Range(0, fool + 1); i++)
        {
            InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        }
        for (int i = 0; i < Random.Range(0, hard + 1); i++)
        {
            InitEnemy(EnemyType.hard).GetComponent<enemy>().Transport(Radius);
        }
        for (int i = 0; i < Random.Range(0, boss + 1); i++)
        {
            InitEnemy(EnemyType.boss).GetComponent<enemy>().Transport(Radius);
        }
    }

    private GameObject InitEnemy(EnemyType type, float offset = 0.0f)
    {
        GameObject perb = null;
        Vector3 pos = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 2.0f), Random.Range(15, 20));
        if (Vector3.Magnitude(pos) <= Radius)
        {
            pos = new Vector3(Random.Range(-2f, 2f), Random.Range(0.0f, 2.0f), Radius + 4);
        }
        switch (type)
        {
            case EnemyType.normal:
                {
                    perb = this.EnemyNormal;
                    break;
                }
            case EnemyType.fast:
                {
                    perb = this.EnemyFast;
                    break;
                }
            case EnemyType.hard:
                {
                    perb = this.EnemyHard;
                    break;
                }
            case EnemyType.boss:
                {
                    perb = this.EnemyBoss;
                    break;
                }
            case EnemyType.fool:
                {
                    perb = this.EnemyFool;
                    break;
                }
        }
        Quaternion temp = Quaternion.Euler(Vector3.zero);
        GameObject obj = (GameObject)Instantiate(perb, pos + new Vector3(0, 0, offset), temp);
        enemy newenemy = perb.GetComponent<enemy>();
        id++;
        newenemy.ID = id;
        newenemy.type = type;
        newenemy.canshoot = false;
        newenemy.canrandmove = false;
        switch (type)
        {
            case EnemyType.normal:
                {
                    EnemyNormalA.Add(obj);
                    break;
                }
            case EnemyType.fast:
                {
                    EnemyFastA.Add(obj);
                    break;
                }
            case EnemyType.hard:
                {
                    EnemyHardA.Add(obj);
                    perb = this.EnemyHard;
                    break;
                }
            case EnemyType.boss:
                {
                    EnemyBossA.Add(obj);
                    perb = this.EnemyBoss;
                    break;
                }
            case EnemyType.fool:
                {
                    EnemyFoolA.Add(obj);
                    perb = this.EnemyFool;
                    break;
                }
        }
        return obj;
    }

    private bool ClassicFlow_1_flag = false;
    private void ClassicFlow_1()
    {
        GameObject normal_1 = InitEnemy(EnemyType.normal);
        normal_1.GetComponent<enemy>().Transport(Radius);
        GameObject normal_2 = InitEnemy(EnemyType.normal);
        normal_2.GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        ClassicFlow_1_flag = true;
    }

    private bool ClassicFlow_2_flag = false;
    private void ClassicFlow_2()
    {
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        ClassicFlow_2_flag = true;
    }

    private bool ClassicFlow_3_flag = false;
    private void ClassicFlow_3()
    {
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);

        InitEnemy(EnemyType.fast, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);
        ClassicFlow_3_flag = true;
    }

    private bool ClassicFlow_4_flag = false;
    private void ClassicFlow_4()
    {
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);

        InitEnemy(EnemyType.fast, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 50).GetComponent<enemy>().Transport(Radius);

        InitEnemy(EnemyType.fast, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool,100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 100).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 100).GetComponent<enemy>().Transport(Radius);
        ClassicFlow_4_flag = true;
    }

    private bool ClassicFlow_5_flag = false;
    private void ClassicFlow_5()
    {
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.normal).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.boss).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool).GetComponent<enemy>().Transport(Radius);

        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast,80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fast, 80).GetComponent<enemy>().Transport(Radius);

        InitEnemy(EnemyType.hard, 150).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.hard, 150).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.boss, 200).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.boss, 200).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.boss, 150).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        InitEnemy(EnemyType.fool, 120).GetComponent<enemy>().Transport(Radius);
        ClassicFlow_5_flag = true;
    }
}