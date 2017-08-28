using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    private float lifetime = 1.0f;
    private float timeadd = 0.0f;
    public AudioClip Clip;

    void Start()
    {
        lifetime = this.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
        this.GetComponent<ParticleSystem>().Play();
        AudioSource temp = this.gameObject.AddComponent<AudioSource>();
        temp.clip = Clip;
        temp.Play();
    }
    void Update()
    {
        timeadd += Time.deltaTime;
        if (timeadd > lifetime)
            Destroy(this.gameObject);
    }
}