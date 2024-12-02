using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float explosionTime = 1f;
    private float shootedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shootedTime += Time.deltaTime;
        if (explosionTime <= shootedTime)
        {
            DestroyExplosion();
        }
    }

    void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
