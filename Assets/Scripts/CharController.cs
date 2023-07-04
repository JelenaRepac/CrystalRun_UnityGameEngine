using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody rb;
    private bool walkingRight = true;
    public Transform rayStart;
    private Animator anim;

    private GameManager gameManager;
    public GameObject crystalEffect;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        //Prelazak u stanje trcanja
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            anim.SetTrigger("GameStarted");
        }


        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }


    //Poziva se u svakom frejmu i proverava da li je karakter na stazi
    //Definisanje stanje u kome se karakter nalazi
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        RaycastHit hit;
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            anim.SetTrigger("IsFalling");
        }
        else
        {
            //anim.SetTrigger("notFallingAnymore");
        }

        if (transform.position.y < -2)
        {
            gameManager.EndGame();
        }
    }

    //Promena pravca kretanja
    private void Switch()
    {
        if (gameManager.gameStarted == false)
        {
            return;
        }

        walkingRight = !walkingRight;

        if (walkingRight)
        {
            //promena pravca za 45 stepeni 
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }

    }

    //Kristal efekat
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            Destroy(other.gameObject);
            gameManager.IncreaseScore();
        }
    }
}
