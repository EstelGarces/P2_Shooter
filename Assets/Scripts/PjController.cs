using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PjController : MonoBehaviour
{
    public float velocidad = 3f;
    public GameObject espadaPrefab;

    public Transform puntoDisparoD;
    public Transform puntoDisparoA;
    public Transform puntoDisparoS;
    public Transform puntoDisparoW;

    private Animator animator;

    public bool onClickW = false;
    public bool onClickA = false;
    public bool onClickS = false;
    public bool onClickD = false;

    float horizontal;
    float vertical;
    private Vector2 ultimaDireccion = Vector2.down;

    public int vidas = 3;
    public int i = 0;
    public int d = 0;

    //public Animator mobsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void MovimientoPj()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            ultimaDireccion = new Vector2(horizontal, vertical).normalized;
        }

        Vector2 movimiento = new Vector2(horizontal, vertical) * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 10) 
        {
            StartCoroutine(WinTime());
        }

        MovimientoPj();
        restartWASD();

        if (Input.GetKeyDown(KeyCode.Space))
        {
           // Debug.Log("horizontal = " + horizontal);
           // Debug.Log("vertical = " + vertical);

            Transform puntoDisparo;
            Quaternion rotacionEspada;
            if (ultimaDireccion == Vector2.right)
            {
                puntoDisparo = puntoDisparoD;
                rotacionEspada = Quaternion.Euler(0, 0, 0); // Sin rotación (derecha)
            }
            else if (ultimaDireccion == Vector2.left)
            {
                puntoDisparo = puntoDisparoA;
                //rotacionEspada = Quaternion.Euler(0, 0, 180); // Rotación hacia la izquierda
                rotacionEspada = Quaternion.Euler(0, 0, 0); // Sin rotación, pero hacemos espejo en escala
            }
            else if (ultimaDireccion == Vector2.up)
            {
                puntoDisparo = puntoDisparoW;
                rotacionEspada = Quaternion.Euler(0, 0, 90); // Rotación hacia arriba
            }
            else // Por defecto, hacia abajo
            {
                puntoDisparo = puntoDisparoS;
                rotacionEspada = Quaternion.Euler(0, 0, -90); // Rotación hacia abajo
            }

            GameObject espada = Instantiate(espadaPrefab, puntoDisparo.position, rotacionEspada);
            SwordController swordController = espada.GetComponent<SwordController>();
            swordController.pj = this;  // Asignamos el PjController al SwordController

            //si es hacia la izquierda que la espada dispare de arriba a abajo pero en la direccion contraria
            if (ultimaDireccion == Vector2.left)
            {
                // Invertir la escala en el eje X para crear el efecto de espejo
                Vector3 escalaEspada = espada.transform.localScale;
                escalaEspada.x *= -1; // Invierte el signo del eje X
                espada.transform.localScale = escalaEspada;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnClickWASD(onClickW);
            animator.SetBool("OnClickW", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnClickWASD(onClickA);
            animator.SetBool("OnClickA", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnClickWASD(onClickS);
            animator.SetBool("OnClickS", true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnClickWASD(onClickD);
            animator.SetBool("OnClickD", true);
        }
    }

    private void OnClickWASD(bool onClick)
    {
        onClick = true;
        //Debug.Log("onClick: " + onClick);
    }
    private void restartWASD()
    {
        onClickW = false;
        onClickA = false;
        onClickS = false;
        onClickD = false;
        animator.SetBool("OnClickW", false);
        animator.SetBool("OnClickA", false);
        animator.SetBool("OnClickS", false);
        animator.SetBool("OnClickD", false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Escaleras"))
        {
            i = 0;
            SceneManager.LoadScene("DangeonScene");
        }

        if (vidas >= 0)
        {
            if (other.collider.CompareTag("Proyectil") || other.collider.CompareTag("Pumpkin"))
            {
                vidas--;
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Demon"))
            {
                // mobsAnimator.SetBool("Dead", true);
                Destroy(this.gameObject);
                SceneManager.LoadScene("EndScene");


            }
            if (other.collider.CompareTag("Proyectil") || other.collider.CompareTag("Pumpkin"))
            {
                Destroy(this.gameObject, 1f);
                //SceneManager.LoadScene("EndScene");
                StartCoroutine(LostTime());

            }
        }
    }

    public int GetNum()
    {
        return i;   
    }

    public void SetNum(int a)
    {
        i = a;
    }

    public int GetDemonLife()
    {
        return d;
    }

    public void SetDemonLife(int a)
    {
        d = a;
    }


    private IEnumerator WinTime()
    {

        Debug.Log("WinTime iniciado");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Cargando WinScene...");
        SceneManager.LoadScene("WinScene");
    }

    private IEnumerator LostTime()
    {

        Debug.Log("EndScene iniciado");
        yield return new WaitForSeconds(0.9f);
        Debug.Log("Cargando EndScene...");
        SceneManager.LoadScene("EndScene");
    }


}
