using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PumpkinController : MonoBehaviour
{
    private Animator animator;

    public GameObject explosionEffect; // Referencia al prefab de la explosión (opcional)
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Pj"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instanciar el efecto de explosión en la posición del enemigo
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Asegurarte de que el prefab de explosión se destruye después de la animación
            Animator explosionAnimator = explosion.GetComponent<Animator>();
            if (explosionAnimator != null)
            {
                float explosionDuration = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
                Destroy(explosion, explosionDuration); // Destruir la explosión después de la animación
            }
            else
            {
                Destroy(explosion, 1f); // Valor por defecto si no hay animador
            }
        }

        // Desactivar el enemigo inmediatamente
        spriteRenderer.enabled = false;
        collider2D.enabled = false;

        // Destruir el enemigo después de un pequeño retraso
        Destroy(gameObject, 0.5f);
    }

}
