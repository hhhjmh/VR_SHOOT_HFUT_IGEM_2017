using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bullet
{
    normal,
    slow,
    enormal,
    ebig,
    efast
}

public class Bullet : MonoBehaviour
{
    public float LifeTime = 2f;
    public Vector3 size;
    public float damage = 20f;
    public float speed = 1f;
    public bullet type = bullet.normal;
    public Color color;
    public Vector3 direction;//世界方向
    public float TimeDV = 0.5f;//射击间隔时间
    private float temptime;
    private Vector3[] ArchimedeanSpiralsPoints = null;
    private Vector3[] PolarRosesPoints = null;
    private int count;

    void Start()
    {
        count = 0;
        ArchimedeanSpiralsPoints = GenerateArchimedeanSpirals(300, Vector3.zero, 1, 0.1f, 0.02f);
        PolarRosesPoints = GeneratePolarRoses(200, Vector3.zero, 3, 2, 0.07f);
        this.gameObject.transform.localScale = size;
        this.gameObject.transform.GetComponent<MeshRenderer>().materials[0].color = color;
        temptime = 0;
    }

    void Update()
    {
        temptime += Time.deltaTime;
        if (temptime > LifeTime)
            destory();
        move();
    }
    
    public void move()
    {
        switch (this.type)
        {
            case bullet.normal:
                Vector3 temp = Vector3.Normalize(direction) * speed * Time.deltaTime;
                this.transform.Translate(temp, Space.World);
                break;
            case bullet.slow:
                this.transform.Translate(Vector3.Normalize(direction) * speed * Time.deltaTime, Space.World);
                break;
            case bullet.enormal:
                //this.transform.position = ArchimedeanSpiralsPoints[count];
                this.transform.Translate((Vector3.Normalize(direction)+ ArchimedeanSpiralsPoints[count]) * speed * Time.deltaTime, Space.World);
                break;
            case bullet.efast:
                this.transform.Translate((Vector3.Normalize(direction)+PolarRosesPoints[count]) * speed * Time.deltaTime, Space.World);
                break;
            case bullet.ebig:
                this.transform.Translate((Vector3.Normalize(direction) + ArchimedeanSpiralsPoints[count]) * speed * Time.deltaTime, Space.World);
                break;
        }
        count = (count + 1) >= 200 ? 0 : (count + 1);
    }

    public Vector3[] GenerateArchimedeanSpirals(int points, Vector3 centre, int circles, float a, float b)
    {
        Vector3[] coordinates = new Vector3[points];
        float radius;
        //调整角度的分配模式，circles为螺旋的圈数
        float theta = 2f * Mathf.PI / points * circles;
        for (int t = 0; t < points; t++)
        {
            //公式1
            radius = a + b * theta;
            //公式5   
            coordinates[t] = new Vector3(radius * Mathf.Cos(theta) + centre.x, radius * Mathf.Sin(theta) + centre.y, 0f);
            theta += 2f * Mathf.PI / points * circles;
        }
        return coordinates;
    }

    public Vector3[] GeneratePolarRoses(int points, Vector3 centre, float k, float c, float scale)
    {
        Vector3[] coordinates = new Vector3[points];
        //将2π均分为points份，并依次赋予theta
        float theta = 2f * Mathf.PI / points;
        float radius;
        for (int t = 0; t < points; t++)
        {
            //公式4转换为代码，算出theta从0至2π每个角度的r值
            radius = Mathf.Cos(k * theta) + c;
            //公式5转换为代码，既是将极坐标转换为直角坐标
            coordinates[t] = new Vector3(scale * radius * Mathf.Cos(theta) + centre.x, scale * radius * Mathf.Sin(theta) + centre.y, 0f);
            theta += 2f * Mathf.PI / points;
        }
        return coordinates;
    }

    public void destory()
    {
        Destroy(this.gameObject);
    }
}