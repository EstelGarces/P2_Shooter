using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float misilTime = 0.3f;
    private float shootedTime = 0f;

    public PjController pj;
    private int i;
    private int d;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        shootedTime += Time.deltaTime;
        //Debug.Log(shootedTime);
        if (misilTime <= shootedTime)
        {
            DestroySword();
        }
    }

    void DestroySword()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Neutral") || collision.CompareTag("Proyectil") || collision.CompareTag("Pumpkin"))
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Neutral") || collision.CompareTag("Pumpkin"))
            {
                i = pj.GetNum();
                i++;
                Debug.Log("num " + i);
                pj.SetNum(i);
            }
            Destroy(collision.gameObject);
        // DestroySword();
        }
        if (collision.CompareTag("Demon"))
        {
            d = pj.GetDemonLife();
            d++;
            Debug.Log("numD " + d);
            pj.SetDemonLife(d);

            if (d > 3) 
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
