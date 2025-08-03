using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private SceneController _sceneController;
    [SerializeField]
    private int speed = 40; // default value of 40
    [SerializeField]
    private float jumpInitialVelocity = 10;

    [SerializeField] private float springInitialVelocity = 20;

    private float _jumpVelocity;
    private Vector2 _dirJump;
    [FormerlySerializedAs("_isGrounded")] [SerializeField]
    private bool isGrounded;
    private bool _isWalking;
    [SerializeField]
    private bool isFloating;
    private Animator _loopAnimator;
    private SpriteRenderer _loopSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        _sceneController = GameObject.FindObjectOfType<SceneController>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
        isGrounded = true;
        _isWalking = false;
        _loopAnimator = transform.Find("Loop").GetComponent<Animator>();
        _loopSprite = transform.Find("Loop").GetComponent<SpriteRenderer>();
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

        if (!isGrounded || _isWalking)
        {
            transform.parent = null;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
            Move();

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            Jump();
        
        if (Input.GetKey(KeyCode.Space) && isFloating)
            Ground();
    }

    public void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        transform.position += movement * (Time.deltaTime * speed);
        if (isFloating)
        {
            Ground();
        }
    }

    private void Spring()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(Vector2.up*springInitialVelocity, ForceMode2D.Impulse);
    }

    private void Jump()
    {
        DetachToPlatform();
        _rigidbody.AddForce(Vector2.up * jumpInitialVelocity, ForceMode2D.Impulse);
        if (isFloating)
        {
            Ground();
        }
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
        if (other.gameObject.GetComponentInParent<MovingPlatformController>() != null && isGrounded)
        {
            AttachToPlatform(other);
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            int count = other.GetContacts(contacts);
            
            ContactPoint2D contact = contacts[0];
            for (int i = 0; i < count; i++)
            {
                if (contacts[i].point.y > contact.point.y)
                {
                    contact = contacts[i];
                }
            }
            
            Vector2 contactPoint = contact.point;
            if (gameObject.transform.position.y  - (_boxCollider.bounds.size.y / 2f)  > contactPoint.y)
            {
                isGrounded = true;
            }
        }
    }
  
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

        if (other.gameObject.GetComponentInParent<MovingPlatformController>() != null)
        {
            DetachToPlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            _sceneController.NextLevel();
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

    public void Float()
    {
        _loopSprite.enabled = true;
        // _loopAnimator.Pla();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        isFloating = true;
    }

    public void Ground()
    {
        _loopSprite.enabled = false;
        // _loopAnimator.StopPlayback();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFloating = false;
    }
}
