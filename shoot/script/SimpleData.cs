using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System;

public class JsonData
{
    public string Time;
    public string Others;

    public JsonData(string Time,string Others)
    {
        this.Time = Time;
        this.Others = Others;
    }
    public JsonData() { }
    public string this[string str]
    {
        get
        {
            if (str == "Time")
                return this.Time;
            else
                return this.Others;
        }
        set
        {
            if (str == "Score")
                this.Time=value;
            else
                this.Others=value;
        }
    }
}

public class SimpleData
{
    private static SimpleData data;
    public state GameType;
    public EasyOrHard Noob;
    public float volume;

    private SimpleData()
    {
        GameType = state.classic;
        Noob = EasyOrHard.easy;
        volume = 1.0f;
    }

    public Dictionary<string, string> GetJsonDate()
    {
        try
        {
#if UNITY_EDITOR
            FileStream fi = new FileStream("C:\\Resources\\Json.txt", FileMode.Open);
#else
        FileStream fi = new FileStream(Application.dataPath + "\\Resources\\Json.txt", FileMode.Open);
#endif
            Dictionary<string, string> jsonDate = new Dictionary<string, string>();
            if (fi.CanRead)
            {
                StreamReader sw = new StreamReader(fi);
                string jsonStr;
                while ((jsonStr = sw.ReadLine()) != null)
                {
                    JsonData data = JsonConvert.DeserializeObject(jsonStr, typeof(JsonData)) as JsonData;
                    jsonDate.Add(data["Time"], data["Others"]);
                }
            }
            return jsonDate;
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }

    public void SaveString(string str)
    {
#if UNITY_EDITOR
        FileInfo fi = new FileInfo("C:\\Resources\\Json.txt");
        Debug.Log("C:\\Resources\\Json.txt");
#else
        FileInfo fi = new FileInfo(Application.dataPath + "\\Resources\\Json.txt");
        Debug.Log(Application.dataPath + "\\Resources\\Json.txt");
#endif
        StreamWriter sw = null;
        if (fi.Exists)
        {
            sw = fi.AppendText();
        }
        else
        {
            sw = fi.CreateText();
        }
        sw.WriteLine(str);
        sw.Close();
    }

    public string ResultToJson(float score,string time,state type,EasyOrHard level)
    {
        StringWriter sw = new StringWriter();
        JsonWriter WriteDate = new JsonTextWriter(sw);
        WriteDate.WriteStartObject();
        WriteDate.WritePropertyName("Time");
        WriteDate.WriteValue(DateTime.Now.ToLocalTime().ToString());
        WriteDate.WritePropertyName("Others");
        WriteDate.WriteValue(score+"|"+time+"|" + (type == state.classic ? "classic" : "infinite")+"|"+ (level == EasyOrHard.easy ? "easy" : "normal"));
        WriteDate.WriteEndObject();
        return sw.ToString();
    }

    public bool isbest(float score, string time)//time是用时
    {
        string temp = ResultToJson(score, time, GameType, Noob);
        SaveString(temp);
        var dic = GetJsonDate();
        if (dic.Count > 0)
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>(dic);
            lst.Sort(delegate (KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
            {
                //return float.Parse((s2.Value.Split('|'))[0]).CompareTo(float.Parse((s1.Value.Split('|'))[0]));//比较
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
            Debug.Log(DateTime.Now.ToLocalTime().ToString());
            Debug.Log(score + "|" + time + "|" + (GameType == state.classic ? "classic" : "infinite") + "|" + (Noob == EasyOrHard.easy ? "easy" : "normal") );
            if (lst[0].Key == DateTime.Now.ToLocalTime().ToString() && lst[0].Value == (score + "|" + time + "|" + (GameType == state.classic ? "classic" : "infinite") + "|" + (Noob == EasyOrHard.easy ? "easy" : "normal")))
                return true;
            else
                return false;
        }
        else
            return true;
    }

    public static SimpleData getInstance()
    {
        if (data == null)
        {
            data = new SimpleData();
        }
        return data;
    }
}