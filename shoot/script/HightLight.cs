using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HightLight
{
    public static GameObject dz1;
    public static GameObject dz2;
    public static GameObject dz3;
    public static GameObject dz4;

    public static void Sethightlight()
    {
        int[] array = new int[4] {
         dz1.GetComponent<DZController>().point,
        dz2.GetComponent<DZController>().point,
        dz3.GetComponent<DZController>().point,
         dz4.GetComponent<DZController>().point };
        int max = array[0];
        int temp = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (max < array[i])
            {
                max = array[i];
                temp = i;
            }
        }
        switch (temp)
        {
            case 0:
                dz1.GetComponent<Image>().enabled = true;
                dz2.GetComponent<Image>().enabled = false;
                dz3.GetComponent<Image>().enabled = false;
                dz4.GetComponent<Image>().enabled = false;
                break;
            case 1:
                dz1.GetComponent<Image>().enabled = false;
                dz2.GetComponent<Image>().enabled = true;
                dz3.GetComponent<Image>().enabled = false;
                dz4.GetComponent<Image>().enabled = false;
                break;
            case 2:
                dz1.GetComponent<Image>().enabled = false;
                dz2.GetComponent<Image>().enabled = false;
                dz3.GetComponent<Image>().enabled = true;
                dz4.GetComponent<Image>().enabled = false;
                break;
            case 3:
                dz1.GetComponent<Image>().enabled = false;
                dz2.GetComponent<Image>().enabled = false;
                dz3.GetComponent<Image>().enabled = false;
                dz4.GetComponent<Image>().enabled = true;
                break;
        }
    }
}