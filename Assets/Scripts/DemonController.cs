using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    public float velocidad = 5f;
    public GameObject skullPrefab;
    public Transform puntoDisparo;
    public float velocidadProyectil = 10f;
    public float tiempoDisparo = 2f;
    public float refrescoDisparo = 0f;
    //public Transform spawnPosition;
    private AnimalsController enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<AnimalsController>();
        refrescoDisparo = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (refrescoDisparo > tiempoDisparo)
        {
            Disparar();
            refrescoDisparo = 0f;
        }
        refrescoDisparo += Time.deltaTime;

    }

    void Disparar()
    {
        GameObject skull = Instantiate(skullPrefab, puntoDisparo.position, puntoDisparo.rotation);

        SkullController skullController = skull.GetComponent<SkullController>();
        skullController.direccionMovimiento = enemy.ObtenerDireccionMov(); // Pasa la dirección actual

        /*Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-1f, 0f) * velocidadProyectil;*/
    }

}
