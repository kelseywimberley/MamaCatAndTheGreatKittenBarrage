using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerControls_Kelsey : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    public float moveSpeed = 5;

    [Range(0.01f, 0.5f)]
    public float timeToMaxSpeed = 0.1f;

    [Range(5.0f, 20.0f)]
    public float smallJumpPower = 10;

    [Range(5.0f, 50.0f)]
    public float bigJumpPower = 30;

    [Range(0.0f, 1.0f)]
    public float minJumpHoldTime = .1f;

    [Range(0.0f, 2.0f)]
    public float maxJumpHoldTime = 1;



    public AnimationCurve jumpCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));



    public LineRenderer jumpLine;



    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;



    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sRenderer;
    BoxCollider2D boxCollider;

    private Vector3 tempTransform;
    private Vector2 jumpDirection;
    private Vector2 lastVelocity;

    private float holdTime = 0;
    bool grounded;
    bool justLanded = false;
    bool charging = false;

    bool holdingLeft = false;

    private int roomNum;
    private int stairNum;
    private CinemachineConfiner2D confiner;
    private Transform roomColliderParent;
    private Transform stairSpawnParent;

    void Start()
    {
        tempTransform = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        confiner = GameObject.Find("Virtual Camera").GetComponent<CinemachineConfiner2D>();
        roomColliderParent = GameObject.Find("RoomBounds").transform;
        stairSpawnParent = GameObject.Find("StairSpawns").transform;
    }

    void Update()
    {
        StairInteractions();
        CheckGrounded();

        if (!Input.GetMouseButton(0))
        {
            Move();
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (Mathf.Abs(rb.velocity.x) < 0.01f || !grounded)
        {
            animator.SetBool("Walking", false);
        }



        UpdateJumpDirection();

        if (grounded)
        {
            // stop airtime audio on land
            if (justLanded && lastVelocity.y < -0.1f)
            {
                audioSource.Stop();
                S_SoundManager.instance.PlayClip(landSound, transform, Mathf.Clamp(Mathf.Pow(-lastVelocity.y / 20f, 2), 0.15f, 1f));

                // landing operations are done, reset justLanded
                justLanded = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                StartJump();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ReleaseJump();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.velocity += new Vector2(0, smallJumpPower);
                animator.SetTrigger("StartJump");

                S_SoundManager.instance.PlayClipWithFade(jumpSound, transform, 1f, jumpSound.length * 0.2f);
            }

            if (charging)
            {
                UpdateJump();
            }
        }

        GoThroughPlatform();

        setFlip();

        lastVelocity = rb.velocity;
    }

    void CheckGrounded()
    {
        bool lastGrounded = grounded; // grounded value from last frame
        grounded = Mathf.Abs(rb.velocity.y) < 0.01f;
        if (grounded && grounded != lastGrounded) justLanded = true;
        animator.SetBool("Grounded", grounded);
    }

    void UpdateJumpDirection()
    {
        Vector3 playerPos = gameObject.transform.position;
        playerPos.z = -1;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;

        jumpDirection = mousePos - playerPos;

        float thirtyDegrees = Mathf.Abs(jumpDirection.x) / 1.732f;
        if (jumpDirection.y < thirtyDegrees)
        {
            jumpDirection.y = thirtyDegrees;
        }

        jumpDirection = jumpDirection.normalized;
    }

    void StartJump()
    {
        jumpLine.gameObject.SetActive(true);
        charging = true;
        animator.SetTrigger("StartCharging");
    }

    void ReleaseJump()
    {
        float percentage = 0;
        if (holdTime >= maxJumpHoldTime)
        {
            percentage = 1;
            animator.SetTrigger("StartJump");

            S_SoundManager.instance.PlayClip(jumpSound, transform, 1f);
        }
        else if (holdTime <= minJumpHoldTime)
        {
            percentage = 0;
            animator.SetTrigger("FailedCharge");
        }
        else
        {
            percentage = (holdTime - minJumpHoldTime) / (maxJumpHoldTime - minJumpHoldTime);
            animator.SetTrigger("StartJump");

            S_SoundManager.instance.PlayClipWithFade(jumpSound, transform, 1f, jumpSound.length * Mathf.Pow(percentage, 2));
        }

        float holdMultiplier = jumpCurve.Evaluate(percentage);

        rb.velocity = holdMultiplier * bigJumpPower * jumpDirection;

        holdTime = 0;

        jumpLine.gameObject.SetActive(false);
        charging = false;

        // temp visialization
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        if (rb.velocity.magnitude > 25f)
        {
            audioSource.Play();
        }
    }

    void UpdateJump()
    {
        holdTime += Time.deltaTime;

        Vector3 playerPos = gameObject.transform.position;
        playerPos.z = -1;




        // temp visualization of charge amount
        float percentage = 0;
        if (holdTime >= maxJumpHoldTime) percentage = 1;
        else if (holdTime <= minJumpHoldTime) percentage = 0;
        else percentage = (holdTime - minJumpHoldTime) / (maxJumpHoldTime - minJumpHoldTime);

        jumpLine.SetPosition(0, playerPos);
        jumpLine.SetPosition(1, playerPos + jumpCurve.Evaluate(percentage) * new Vector3(jumpDirection.x, jumpDirection.y, 0));



        gameObject.GetComponent<SpriteRenderer>().color = new Color(1 - jumpCurve.Evaluate(percentage), 0, 0, 1);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && rb.velocity.x > -moveSpeed)
        {
            rb.velocity += new Vector2(-moveSpeed * (1.0f / timeToMaxSpeed) * Time.deltaTime, 0);
            animator.SetBool("Walking", true);
            holdingLeft = true;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && rb.velocity.x < moveSpeed)
        {
            rb.velocity += new Vector2(moveSpeed * (1.0f / timeToMaxSpeed) * Time.deltaTime, 0);
            animator.SetBool("Walking", true);
            holdingLeft = false;
        }
    }

    void GoThroughPlatform()
    {
        // Go through GoThroughPlatform layer objects when going up
        if (rb.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
        }
        // Don't go through GoThroughPlatform layer objects when falling down
        else
        {
            Physics2D.IgnoreLayerCollision(3, 7, false);
        }
    }

    void setFlip()
    {
        string currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if ((currentAnimation == "walk" || currentAnimation == "jump") && holdingLeft)
        {
            sRenderer.flipX = true;
        }
        else
        {
            sRenderer.flipX = false;
        }
    }

    void StairInteractions()
    {
        if (stairNum == -1)
        {
            return;
        }

        if (stairNum == 0 && Input.GetKey(KeyCode.S))
        {
            roomNum = 1;
            transform.position = stairSpawnParent.GetChild(roomNum).transform.position;
            confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
        }
        else if (stairNum == 1 && Input.GetKey(KeyCode.W))
        {
            roomNum = 0;
            transform.position = stairSpawnParent.GetChild(roomNum).transform.position;
            confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
        }
        else if (stairNum == 2 && Input.GetKey(KeyCode.W))
        {
            roomNum = 3;
            transform.position = stairSpawnParent.GetChild(roomNum).transform.position;
            confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
        }
        else if (stairNum == 3 && Input.GetKey(KeyCode.S))
        {
            roomNum = 2;
            transform.position = stairSpawnParent.GetChild(roomNum).transform.position;
            confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Stairs0")
        {
            stairNum = 0;
        }
        else if (collision.name == "Stairs1")
        {
            stairNum = 1;
        }
        else if (collision.name == "Stairs2")
        {
            stairNum = 2;
        }
        else if (collision.name == "Stairs3")
        {
            stairNum = 3;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Door0")
        {
            if (roomNum == 0 && transform.position.x < collision.transform.position.x)
            {
                roomNum = 3;
                confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
            }
            else if (roomNum == 3 && transform.position.x > collision.transform.position.x)
            {
                roomNum = 0;
                confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
            }
        }
        else if (collision.name == "Door1")
        {
            if (roomNum == 1 && transform.position.x < collision.transform.position.x)
            {
                roomNum = 2;
                confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
            }
            else if (roomNum == 2 && transform.position.x > collision.transform.position.x)
            {
                roomNum = 1;
                confiner.m_BoundingShape2D = roomColliderParent.GetChild(roomNum).GetComponent<PolygonCollider2D>();
            }

            if (collision.name == "Stairs0" || collision.name == "Stairs1" ||
                collision.name == "Stairs2" || collision.name == "Stairs3")
            {
                stairNum = -1;
            }
        }
    }
}
