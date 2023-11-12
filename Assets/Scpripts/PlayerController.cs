using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float moveSpeed = 0.1f;
    //xd
    [SerializeField]
    [Space(10)]
    private float jumpForce = 6.0f;


    private const int keysOnMap = 3;


    private Vector2 startPosition;

    //private BigInteger scoreBig;

    private bool isWalking = false;

    private bool isFacingRight = true;

    private Rigidbody2D rigidBody;

    private Animator animator;

    public LayerMask groundLayer;

    public const float rayLenght = 1.3f;

    Vector3 theScale;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {

        if (GameManager.instance.currentGameState == GameManager.GameState.GS_GAME)
        {

            isWalking = false;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (isFacingRight == false)
                    Flip();
                transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isWalking = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (isFacingRight == true)
                    Flip();
                transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isWalking = true;
            }

            if (isGrounded() && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                isWalking = false;
                Jump();
            }

            animator.SetBool("isGrounded", isGrounded());
            animator.SetBool("isWalking", isWalking);
        }
    }

    //Boy, the sound that whip makes sure is sweet. It's like Jesus gently snapping his fingers

    private void Jump() => rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    private bool isGrounded() => 
        Physics2D.Raycast(transform.position, Vector2.down, rayLenght, groundLayer.value);

    private void Flip() {
        isFacingRight ^= true;
        theScale = transform.localScale; 
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bonus"))
        {
            other.CompareTag("Bonus");
            
            //Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
			GameManager.instance.AddPoints(10);
		}
        
        if(other.CompareTag("Finish"))
		{
            if (GameManager.instance.keysFound == keysOnMap)
                Debug.Log("You have collected all keys and finished the game");

            else 
                Debug.Log("Collect all keys in order to finish this lvl");
        }

        if (other.CompareTag("Enemy"))
        {
            if(transform.position.y > other.transform.position.y)
            {
                //score += 10;
                GameManager.instance.IncreaseEnemiesKilledCounter();
                GameManager.instance.AddPoints(10);
                Debug.Log("Killed an enemy");
                Jump();
            }
            else
            {
                GameManager.instance.TakeHearth();

				transform.position = startPosition;
			}
        }

        if (other.CompareTag("Key"))
        {
            GameManager.instance.AddKeys(other.gameObject);
            //Debug.Log($"You have found {keysFound}");
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("BonusLife"))
        {
            GameManager.instance.AddHearth();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("FallLevel"))
        {

            GameManager.instance.TakeHearth();

		    transform.position = startPosition;



		}

        if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);
        }
         
    }

	private void OnTriggerExit2D(Collider2D other)
	{
		transform.SetParent(null);
	}



}