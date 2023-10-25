using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{

	private Animator animator;

	private Vector3 theScale;


	[Header("Movement Parameters")]
	[Range(0.01f, 20.0f)]
	[SerializeField]
	private float moveSpeed = 0.1f;

	[Range(1f, 6f)]
	[SerializeField]
	private float moveRange = 1f;

	private float startPositionX;

	private bool isMovingRight = false;

	private bool isFacingRight = false;




	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (isMovingRight)
		{
			if (transform.position.x < startPositionX + moveRange)
			{
				moveRight();
				
			}
			else
			{
				Flip();
				isMovingRight = false;
			}
		}
		else
		{
			if(transform.position.x > startPositionX - moveRange)
			{
				
				moveLeft();
			}
			else
			{
				Flip();
				isMovingRight = true;
			}
		}



        
    }

	private void Flip()
	{
		isFacingRight ^= true;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void Awake()
	{
		//rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		startPositionX = transform.position.x;
	 
	}

	private void moveRight()
	{
		transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
	}

	private void moveLeft()
	{
		transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
	}

	private async void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if(other.transform.position.y > transform.position.y)
			{
				animator.SetBool("isDead", true);
				StartCoroutine(KillOnAnimationEnd());
				
			}
		}

	}

	private IEnumerator KillOnAnimationEnd()
	{
		yield return new WaitForSeconds(1.2f);
		gameObject.SetActive(false);
	}





}
