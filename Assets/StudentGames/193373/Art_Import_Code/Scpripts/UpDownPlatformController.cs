using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatformController : MonoBehaviour {
    [Header("Movement Parameters")] [Range(0.01f, 20.0f)] [SerializeField]
    private float moveSpeed = 0.1f;

    [Range(1f, 6f)] [SerializeField] private float moveRange = 1f;

    private float startPositionY;

    private bool isMovingUp = false;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() {
        if (isMovingUp) {
            if (transform.position.y < startPositionY + moveRange) {
                moveUp();
            }
            else {
                isMovingUp = false;
            }
        }
        else {
            if (transform.position.y > startPositionY - moveRange) {
                moveDown();
            }
            else {
                isMovingUp = true;
            }
        }
    }

    private void Awake() {
        //rigidBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        startPositionY = transform.position.y;
    }

    private void moveUp() => transform.Translate(0f, moveSpeed * Time.deltaTime, 0.0f, Space.World);

    private void moveDown() => transform.Translate(0.0f, -moveSpeed * Time.deltaTime, 0.0f, Space.World);
}