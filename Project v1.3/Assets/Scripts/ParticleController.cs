using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particles;
    public static bool burst = false;

    public int particlePerSecond = 75;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.emissionRate = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(burst)
        {
            particles.Emit((int)(particlePerSecond * Time.deltaTime));
            burst = false;
        }
        else
        {
            burst = false;
        }
    }
}
