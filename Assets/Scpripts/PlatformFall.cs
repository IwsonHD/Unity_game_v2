using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformFall : MonoBehaviour {
    private Rigidbody2D _rigidBody;
    private Vector3 _startPosition;

    [Range(0.01f, 20.0f)] public float ShakeTime;

    [Range(0.01f, 20.0f)] public float ShakeMagnitude;

    [Range(0.01f, 5.0f)] public float UpwardTolerance = 2.0f;

    // Start is called before the first frame update
    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startPosition = _rigidBody.position;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            StartCoroutine(ShakePlatform());
            Task.Run(async () => {
                await Task.Delay(TimeSpan.FromSeconds(2));
                Destroy(gameObject, 2.0f);
            });
        }
    }

    IEnumerator ShakePlatform() {
        var elapsedTime = 0.0f;
        while (elapsedTime < ShakeTime) {
            transform.position = _startPosition + new Vector3(
                Random.Range(-ShakeMagnitude, ShakeMagnitude),
                Random.Range(-ShakeMagnitude, ShakeMagnitude),
                0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update() { }
}