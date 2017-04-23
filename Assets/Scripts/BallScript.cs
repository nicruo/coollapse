using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 2;
    public float jumpHeight = 5;

    public Transform world;

    private Vector2 relativeVelocity;
    private Vector3 lastPosition;
    private bool rotating;

    private GameManager GameManagerScript;

    public GameObject gameManagerObject;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        GameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        var hit = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), Vector2.down);
        if (hit)
        {
            lastPosition = hit.transform.position;
        }
        else
        {
            if(transform.position.x > lastPosition.x)
            {
                if (!rotating)
                {
                    StartCoroutine(RotateRoutine(0.2f, 90));
                }
            }
            else
            {
                if (!rotating)
                {
                    StartCoroutine(RotateRoutine(0.2f, -90));
                }
            }
        }
    }

    void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(horizontal * moveSpeed * (1 + GameManagerScript.SpeedMultiplier), rigidbody.velocity.y);
    }

    private IEnumerator RotateRoutine(float duration, float angle)
    {
        rotating = true;

        var from = world.rotation;
        Quaternion to = world.rotation * Quaternion.Euler(0, 0, angle);
        var fromP = world.position;
        var toP = Quaternion.Euler(0, 0, angle) * (world.position - lastPosition) + lastPosition;

        float time = 0;
        while (time < 1)
        {
            time += Time.smoothDeltaTime / duration;
            
            world.rotation = Quaternion.Slerp(from, to, time);

            world.position = Vector2.Lerp(fromP, toP, time);

            yield return null;
        }

        rotating = false;

        yield return null;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity != Vector2.zero)
        {
            relativeVelocity = collision.relativeVelocity;
        }

        if (relativeVelocity.y < 0)
        {
            rigidbody.velocity = -relativeVelocity.normalized * jumpHeight;
        }
    }
}