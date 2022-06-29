using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private int count = 0;
    public CapsuleCollider col;
    public AudioSource jumpsound;
    public AudioSource slidesound;
    public float speed;
    public float jumpforce = 5f;
    public LayerMask groundlayers;
    public Text pointText;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        pointText.text = "Points: "+count;
    }

    void Update()
    {
        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(Vector3.forward * Time.deltaTime*10);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0);
        
        rb.AddForce(movement * speed);  
        
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Slide", true);
            slidesound.Play();
        }
        else {
            anim.SetBool("Slide", false);
        }


        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
            jumpsound.Play();
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
        else {
            anim.SetBool("Jump", false);
        }
    }

    private bool isGrounded() {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
                             col.bounds.min.y, col.bounds.center.z),col.radius*.9f, groundlayers);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Points"))
        {
            count++;
            SetPointText();
        }
        
       
        if (other.gameObject.CompareTag("Obstacles"))
        {
            SceneManager.LoadScene("GameOver");
        }
       

    }

    private void SetPointText()
    {
        pointText.text = "Points: " + count;
        
        
        if (count>=9) {
            SceneManager.LoadScene("LevelScene");
        }
          
    }
}
