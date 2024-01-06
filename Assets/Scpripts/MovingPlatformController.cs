using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

	[Header("Movement Parameters")]
	[Range(0.01f, 20.0f)]
	[SerializeField]
	private float moveSpeed = 0.1f;

	[Range(1f, 6f)]
	[SerializeField]
	private float moveRange = 1f;

	private float startPositionX;

	private bool isMovingRight = false;





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
				isMovingRight = false;
			}
		}
		else
		{
			if (transform.position.x > startPositionX - moveRange)
			{

				moveLeft();
			}
			else
			{

				isMovingRight = true;
			}
		}
	}

	private void Awake()
	{
		//rigidBody = GetComponent<Rigidbody2D>();
		//animator = GetComponent<Animator>();
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









}
