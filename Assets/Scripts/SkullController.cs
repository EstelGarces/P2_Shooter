using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    private Animator animator;
    private Animator animatorExp;

    public float velocidad = 2f; // Velocidad del movimiento
    public Vector2 direccionMovimiento; // Direcci�n del movimiento
    public float skullTime = 6f;
    private float shootedTime = 0f;

    public Transform enemigo;

    public GameObject explosionEffect; // Referencia al prefab de la explosi�n (opcional)
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //direccionMovimiento = enemigo.right.normalized;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    { 
        Mover();
        //direccionMovimiento = new Vector2(1f, 0f);
        shootedTime += Time.deltaTime;
        if (skullTime <= shootedTime)
        {
            DestroySkull();
        }
    }

    // Mueve al animal
    private void Mover()
    {
        Vector2 movimiento = direccionMovimiento * velocidad * Time.deltaTime;
        transform.Translate(movimiento);

        // Voltear el sprite dependiendo de la direcci�n del movimiento
        if (direccionMovimiento.x < 0) // Si el movimiento es hacia la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Volteamos el sprite horizontalmente
        }
        else if (direccionMovimiento.x > 0) // Si el movimiento es hacia la derecha
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Devolvemos el sprite a su orientaci�n normal
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Elegir una nueva direcci�n aleatoria
        float angulo = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        direccionMovimiento = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));

        if (other.collider.CompareTag("Pj"))
        {
            Explode();
        }

    }

    void DestroySkull()
    {
        Destroy(this.gameObject);
    }

    private void Explode()
    {
        // Instanciar el efecto de explosi�n en la posici�n del enemigo
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Asegurarte de que el prefab de explosi�n se destruye despu�s de la animaci�n
            Animator explosionAnimator = explosion.GetComponent<Animator>();
            if (explosionAnimator != null)
            {
                float explosionDuration = explosionAnimator.GetCurrentAnimatorStateInfo(0).length;
                Destroy(explosion, explosionDuration); // Destruir la explosi�n despu�s de la animaci�n
            }
            else
            {
                Destroy(explosion, 1f); // Valor por defecto si no hay animador
            }
        }

        // Desactivar el enemigo inmediatamente
        spriteRenderer.enabled = false;
        collider2D.enabled = false;

        // Destruir el enemigo despu�s de un peque�o retraso
        Destroy(gameObject, 0.5f);
    }


}
