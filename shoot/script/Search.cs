using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : MonoBehaviour
{
    public float life = 10.0f;

    private Vector3 pos;
    private float timetemp;
    void Start()
    {
        timetemp = 0;
        this.GetComponent<BoxCollider>().isTrigger = false;
        pos = this.transform.position;
    }

    void Update()
    {
        timetemp += Time.deltaTime;
        if (timetemp >= life)
            Destroy(this.gameObject);
        this.transform.position = pos;
        this.transform.Rotate(new Vector3(0, 5f, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "maincell")
        {
            collision.gameObject.GetComponent<Cell>().dzfull();
            Destroy(this.gameObject);
        }
    }
}