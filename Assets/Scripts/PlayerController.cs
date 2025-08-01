using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private int speed = 40; // default value of 40
    [SerializeField]
    private float jumpInitialVelocity = 10;

    [SerializeField] private float springInitialVelocity = 20;

    private float _jumpVelocity;
    private Vector2 _dirJump;
    [SerializeField]
    private bool _isGrounded;
    private bool _isWalking;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _isGrounded = true;
        _isWalking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            _isWalking = false;
        }
        else
        {
            _isWalking = true;
        }

        if (!_isGrounded || _isWalking)
        {
            _rigidbody.isKinematic = false;
            transform.parent = null;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        transform.position += movement * (Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
            Jump();
    }

    private void Spring()
    {
        _rigidbody.AddForce(Vector2.up*springInitialVelocity, ForceMode2D.Impulse);
    }

    private void Jump()
    {
        DetachToPlatform();
        _rigidbody.AddForce(Vector2.up * jumpInitialVelocity, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spring"))
        {
            Spring();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponentInParent<MovingPlatformController>() != null && _isGrounded)
        {
            Debug.Log($"attaching: {other.gameObject.name}");
            AttachToPlatform(other);
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            ContactPoint2D contact = other.GetContact(0);
            Vector2 contactPoint = contact.point;
            Vector3 center = contact.collider.bounds.center;
            if (contactPoint.y > center.y)
            {
                _isGrounded = true;
            }
        }
    }
  
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            _isGrounded = false;
        }

        if (other.gameObject.GetComponentInParent<MovingPlatformController>() != null)
        {
            Debug.Log($"detaching: {other.gameObject.name}");
            DetachToPlatform();
        }
    }

    private void AttachToPlatform(Collision2D other)
    {
        gameObject.transform.parent = other.transform;
    }

    private void DetachToPlatform()
    {
        gameObject.transform.parent = null;
    }
}
