using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private PaddleMovement paddle1;
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] private float randomFactor = 0.2f;

    private bool ballLaunched = false;

    private Vector2 diffPos;

    private AudioSource myAudioSource;

    private Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        diffPos = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballLaunched)
        {
            LockTheBall();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(velocityX, velocityY);
            ballLaunched = true;
        }
    }

    public void LockTheBall()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + diffPos;
        ballLaunched = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (ballLaunched)
        {
            myAudioSource.PlayOneShot(ballSounds[Random.Range(0, ballSounds.Length)]);
            myRigidBody2D.velocity += velocityTweak;
        }
            
    }
}
