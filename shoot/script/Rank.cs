using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public GameObject Help;
    public GameObject Player;
    public GameObject mRank;
    public GameObject Exit;
    public GameObject perb;
    public GameObject content;
    public Sprite numb1_1;
    public Sprite numb2_1;
    public Sprite numb3_1;
    public Sprite numb;
    public Sprite normal;

    private float height;
    private float width;

    public void DictionarySort(Dictionary<string, string> dic)
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        if (dic.Count > 0)
        {
            this.transform.FindChild("nodata").gameObject.SetActive(false);
            this.content.GetComponent<RectTransform>().sizeDelta = new Vector2(width, dic.Count * 152);
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>(dic);
            lst.Sort(delegate (KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
            {
                //return float.Parse((s2.Value.Split('|'))[0]).CompareTo(float.Parse((s1.Value.Split('|'))[0]));//比较//小于 0，s2小于参数 s1
                if (float.Parse((s2.Value.Split('|'))[0]).CompareTo(float.Parse((s1.Value.Split('|'))[0])) < 0)
                {
                    return -1;
                }
                else if (float.Parse((s2.Value.Split('|'))[0]).CompareTo(float.Parse((s1.Value.Split('|'))[0])) == 0)
                {
                    if (float.Parse((s2.Value.Split('|'))[1]).CompareTo(float.Parse((s1.Value.Split('|'))[1])) < 0)
                        return 1;
                    else if (float.Parse((s2.Value.Split('|'))[1]).CompareTo(float.Parse((s1.Value.Split('|'))[1])) == 0)
                        return 0;
                    else
                        return -1;
                }
                else if (float.Parse((s2.Value.Split('|'))[0]).CompareTo(float.Parse((s1.Value.Split('|'))[0])) > 0)
                {
                    return 1;
                }
                else
                    return 0;
            });
            dic.Clear();
            int i = 1;
            foreach (KeyValuePair<string, string> kvp in lst)
            {
                if (i <= 3)
                {
                    string[] temp = kvp.Value.Split('|');//250|14.8|classic|normal
                    GameObject obj = Instantiate(perb, content.transform);
                    obj.transform.FindChild("Badge Icon").gameObject.SetActive(true);
                    switch (i)
                    {
                        case 1:
                            obj.transform.FindChild("Badge Icon").gameObject.GetComponent<Image>().sprite = numb1_1;
                            obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().sprite = numb;
                            obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
                            break;
                        case 2:
                            obj.transform.FindChild("Badge Icon").gameObject.GetComponent<Image>().sprite = numb2_1;
                            obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().sprite = numb;
                            obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().color = new Color(0, 1, 0);
                            break;
                        case 3:
                            obj.transform.FindChild("Badge Icon").gameObject.GetComponent<Image>().sprite = numb3_1;
                            obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().sprite = numb;
                            obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().color = new Color(0, 0, 1);
                            break;
                    }
                    obj.transform.FindChild("Badge Text").gameObject.SetActive(false);
                    obj.transform.FindChild("Text Score").gameObject.GetComponent<Text>().text = temp[0] + ".0";
                    obj.transform.FindChild("Text Time").gameObject.GetComponent<Text>().text = temp[1] + "s";
                    obj.transform.FindChild("Text Type").gameObject.GetComponent<Text>().text = temp[2].Substring(0, 1).ToUpper() + temp[2].Substring(1);
                    obj.transform.FindChild("Text Mode").gameObject.GetComponent<Text>().text = temp[3].Substring(0, 1).ToUpper() + temp[3].Substring(1);
                }
                else
                {
                    string[] temp = kvp.Value.Split('|');//250|14.8|classic|normal
                    GameObject obj = Instantiate(perb, content.transform);
                    obj.transform.FindChild("Badge Icon").gameObject.SetActive(false);
                    obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().sprite = normal;
                    obj.transform.FindChild("Main Icon").gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
                    obj.transform.FindChild("Badge Text").gameObject.SetActive(true);
                    obj.transform.FindChild("Badge Text").gameObject.GetComponent<Text>().text = "#" + i;
                    obj.transform.FindChild("Text Score").gameObject.GetComponent<Text>().text = temp[0] + ".0";
                    obj.transform.FindChild("Text Time").gameObject.GetComponent<Text>().text = temp[1] + "s";
                    obj.transform.FindChild("Text Type").gameObject.GetComponent<Text>().text = temp[2].Substring(0, 1).ToUpper() + temp[2].Substring(1);
                    obj.transform.FindChild("Text Mode").gameObject.GetComponent<Text>().text = temp[3].Substring(0, 1).ToUpper() + temp[3].Substring(1);
                }
                i++;
            }
        }
        else
        {
            //没数据
            this.transform.FindChild("nodata").gameObject.SetActive(true);
        }
    }

    void Start()
    {
        this.transform.FindChild("nodata").gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        width= content.transform.GetComponent<RectTransform>().sizeDelta.x;
        height = content.transform.GetComponent<RectTransform>().sizeDelta.y;//+152
    }

    public void open()
    {
        Help.SetActive(false);
        Player.SetActive(false);
        mRank.SetActive(false);
        Exit.SetActive(false);
        Dictionary<string, string> temp = null;
        temp = SimpleData.getInstance().GetJsonDate();
        if (temp != null)
            DictionarySort(temp);
        else
            this.transform.FindChild("nodata").gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void close()
    {
        Help.SetActive(true);
        Player.SetActive(true);
        mRank.SetActive(true);
        Exit.SetActive(true);

        this.gameObject.SetActive(false);
    }
}