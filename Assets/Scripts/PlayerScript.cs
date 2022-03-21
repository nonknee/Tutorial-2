using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text lives;

    public GameObject winTextObject;

    public GameObject loseTextObject;

    public Vector2 jumpforce;

    private int scoreValue = 0;

    private int livesValue = 3;

    private int level = 1;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    private bool winState = false;
    
    private bool facingRight = true;

    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (hozMovement == 0 && vertMovement == 0)
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        

        if (livesValue == 0)
            {
                Destroy(gameObject);

                if (winState == false)
                {
                    loseTextObject.SetActive(true);
                }
            }

        if (scoreValue == 8 && winState == false)
            {
                winTextObject.SetActive(true);
                winMusic();

            }

        if (scoreValue == 4 && level == 1)
            {
            level += 1;
            livesValue = 3;
            transform.position = new Vector3(51.0f, 0.5f, 0.0f);
            }

        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))

            {
                anim.SetInteger("State", 1);
            }

            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 2);
                rd2d.AddForce(new Vector2(jumpforce.x, jumpforce.y), ForceMode2D.Impulse); 
            }

        }
    }

    void winMusic()
    {
        musicSource.clip = musicClipTwo;
        musicSource.loop = false;
        musicSource.Play();
        winState = true;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}

