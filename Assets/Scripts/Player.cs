
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class JumpType
    {
        public float jumpHeight;
        public string animationTrigger;
    }
    [System.Serializable]
    public class SlideType
    {
        public float SliderSpeed;
        public string SlideTrigger;
    }

    [Header("RigidBody")]
    public Rigidbody Rb;

    [Header("Animation")]
    public Animator PlayerAnimator;

    [Header("Layermasks")]
    public LayerMask GroundLayer;

    [Header("colliders")]
    public CapsuleCollider capsuleCollide;

    [Header("Movement Values")]
    public float MoveSpeed;
    public float HorizontalMoveSpeed;
    public float JumpHeight;

    [Header("Raycast vealues")]
    public float raycastDistance;

    [Header("List of Jumps")]
    public List<JumpType> Jumptypes = new ();

    [Header("List of Slides")]
    public List<SlideType> SlideTypes = new ();

    [Header("Speed Increase Values")]
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 20f;

    [Header("Score Values")]
    public float scoreIncrementDistance = 1f;
    private Vector3 prevpos;

    [Header("Bools")]
    //public bool isGrounded;
    public bool Moving = true;
    public static bool canInput = true;
    public static bool isplayerDied = false;

    private float currentSpeed;
    private Vector3 StartPos;

    public static Player instance;
    public void Start()
    {
        if (instance == null) 
        {
            instance = this;
        }
        PlayerAnimator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        currentSpeed = MoveSpeed;
        prevpos = transform.position;
        StartPos = transform.position ;
        canInput = true;
        //isGrounded = false;
    }

    public void FixedUpdate()
    {
        if (Moving)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += speedIncreaseRate * Time.deltaTime;
            }
            PlayerMoveMent();
            UpdateScore();
        }
    }
    public void Update()
    {
        if (canInput)
        {
         PlayJump();
         PlaySlide();
        }
        if (isplayerDied == true)
        {
            StartCoroutine(AnimationDelay(3f));
        }
        if (PlayerAnimator.GetBool("Dead") == true)
        {
            Moving = false;
        }
        //Debug.Log("Box got hit");
    }

    private void UpdateScore()
    {
        float distance = Vector3.Distance(prevpos, transform.position);
        if (distance >= scoreIncrementDistance)
        {
            int scoreIncrement = Mathf.FloorToInt(distance / scoreIncrementDistance);
            Score_Manager.instance.AddScore(scoreIncrement);
            prevpos = transform.position;
        }
    }

    public void ResetPlayerPos()
    {
        transform.position = StartPos;
    }

    // Player Movemant functions
    void PlayerMoveMent()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new (horizontal * HorizontalMoveSpeed * Time.deltaTime, 0, currentSpeed * Time.deltaTime);
        Rb.MovePosition(transform.position + movement);
    }

    // Slide functions

    void PlaySlide()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded())
        {
            SlideType RandomSlide = SlideTypes[Random.Range(0, SlideTypes.Count)];
            SlideHandle(RandomSlide);
        }
    }

    void SlideHandle(SlideType Slide)
    {
        PlayerAnimator.SetTrigger(Slide.SlideTrigger);
    }

    // Jump function
    void PlayJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            JumpType RandomJump = Jumptypes[Random.Range(0, Jumptypes.Count)];
            JumpHandle(RandomJump);
        }
    }
    void JumpHandle( JumpType jump)
    {
        Rb.AddForce(transform.up * jump.jumpHeight, ForceMode.Impulse);
        PlayerAnimator.SetTrigger(jump.animationTrigger);
    }

    // Player GroundChecking
    bool IsGrounded()
    {
        RaycastHit hit;
        // Cast a ray downwards from the center of the ground check collider
        if (Physics.Raycast(capsuleCollide.transform.position, Vector3.down, out hit, raycastDistance, GroundLayer))
        {
            return true;
        }
        return false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacles") || collision.gameObject.CompareTag("Wall"))
        {
            PlayerAnimator.SetBool("Dead", true);
            isplayerDied = true;
        }
    }

    // bool values
    IEnumerator AnimationDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Pause_Menu.instance.ShowPauseMenu();
    }
}