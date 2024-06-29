using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerControls_Madeline : MonoBehaviour
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

    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sRenderer;

    private Vector3 tempTransform;
    private Vector2 jumpDirection;

    private float holdTime = 0;
    bool grounded;
    bool charging = false;

    bool holdingLeft = false;

    void Start()
    {
        tempTransform = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckGrounded();

        if(!Input.GetMouseButton(0)){
            Move();
        }
        else{
            animator.SetBool("Walking",false);
        }

        if(Mathf.Abs(rb.velocity.x) < 0.01f || !grounded){
            animator.SetBool("Walking",false);
        }

        

        UpdateJumpDirection();

        if(grounded){
            if (Input.GetMouseButtonDown(0)){
                StartJump();
            }
            else if(Input.GetMouseButtonUp(0)){
                ReleaseJump();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.velocity += new Vector2(0, smallJumpPower);
                animator.SetTrigger("StartJump");
            }

            if(charging){
                UpdateJump();
            }
        }
        
        
        GoThroughPlatform();

        setFlip();
    }

    void CheckGrounded(){
        grounded = Mathf.Abs(rb.velocity.y) < 0.01f;
        animator.SetBool("Grounded",grounded);
    }
    void UpdateJumpDirection(){
        Vector3 playerPos = gameObject.transform.position;
        playerPos.z = -1;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;

        jumpDirection =  mousePos - playerPos;

        float thirtyDegrees = Mathf.Abs(jumpDirection.x) / 1.732f;
        if(jumpDirection.y < thirtyDegrees){
            jumpDirection.y = thirtyDegrees;
        }

        jumpDirection = jumpDirection.normalized;
    }

    void StartJump(){
        jumpLine.gameObject.SetActive(true);
        charging = true;
        animator.SetTrigger("StartCharging");
    }

    void ReleaseJump(){


        float percentage = 0;
        if(holdTime >= maxJumpHoldTime) {
            percentage = 1;
            animator.SetTrigger("StartJump");
        }
        else if(holdTime <= minJumpHoldTime) {
            percentage = 0;
            animator.SetTrigger("FailedCharge");
        }
        else {
            percentage = (holdTime-minJumpHoldTime)/(maxJumpHoldTime - minJumpHoldTime);
            animator.SetTrigger("StartJump");
        }

        float holdMultiplier = jumpCurve.Evaluate(percentage);

        rb.velocity = holdMultiplier*bigJumpPower*jumpDirection;

        holdTime = 0;

        jumpLine.gameObject.SetActive(false);
        charging = false;

        // temp visialization
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
    }

    void UpdateJump(){
        holdTime += Time.deltaTime;

        Vector3 playerPos = gameObject.transform.position;
        playerPos.z = -1;




        // temp visualization of charge amount
        float percentage = 0;
        if(holdTime >= maxJumpHoldTime) percentage = 1;
        else if(holdTime <= minJumpHoldTime) percentage = 0;
        else percentage = (holdTime-minJumpHoldTime)/(maxJumpHoldTime - minJumpHoldTime);

        jumpLine.SetPosition(0, playerPos);
        jumpLine.SetPosition(1, playerPos + jumpCurve.Evaluate(percentage)*new Vector3(jumpDirection.x,jumpDirection.y,0));



        gameObject.GetComponent<SpriteRenderer>().color = new Color(1-jumpCurve.Evaluate(percentage),0,0,1);
    }

    void Move(){
        
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && rb.velocity.x > -moveSpeed)
        {
            rb.velocity += new Vector2(-moveSpeed*(1.0f/timeToMaxSpeed)*Time.deltaTime, 0);
            animator.SetBool("Walking",true);
            holdingLeft = true;
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && rb.velocity.x < moveSpeed)
        {
            rb.velocity += new Vector2(moveSpeed*(1.0f/timeToMaxSpeed)*Time.deltaTime, 0);
            animator.SetBool("Walking",true);
            holdingLeft = false;
        }
    }

    void GoThroughPlatform(){
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

    void setFlip(){
        string currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if((currentAnimation == "walk" || currentAnimation == "jump") && holdingLeft){
            sRenderer.flipX = true;
        }
        else{
            sRenderer.flipX = false;
        }

    }
}
