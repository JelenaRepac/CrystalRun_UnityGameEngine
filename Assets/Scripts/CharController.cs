using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public Road road;
    public AudioSource speedUpAudio;
    public Text speedUpText;

    private Rigidbody rb;
    private bool walkingRight = true;
    public Transform rayStart;
    private Animator anim;

    private GameManager gameManager;
    public GameObject crystalEffect;

    private float speed = 2;
    private const float SPEED_UP = 0.5f;
    private const float MAX_SPEED = 6;

    public float Speed => speed;

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


        rb.transform.position = transform.position + transform.forward * speed * Time.deltaTime;
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
            anim.SetTrigger("NotFallingAnymore");
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
            int score = gameManager.IncreaseScore();
            CheckScoreForSpeedUp(score);

            GameObject gameObject = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(gameObject, 2);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "RoadPart")
        {
            StartCoroutine(RemoveRoadPart(collisionGameObject));
        }
        road.CreateNewRoadPart();
    }

    private void CheckScoreForSpeedUp(int score)
    {
        if (score % 5 == 0 && speed + SPEED_UP <= MAX_SPEED)
        {
            speed += SPEED_UP;
            StartCoroutine(ShowSpeedUpText());
            speedUpAudio.Play();
        }
    }

    private IEnumerator RemoveRoadPart(GameObject gameObject)
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private IEnumerator ShowSpeedUpText()
    {
        yield return new WaitForSeconds(0.1f);
        speedUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        speedUpText.gameObject.SetActive(false);
    }
}
