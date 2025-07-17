using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public bool grounded;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    public int count;
    private int maxCount;
    [SerializeField] private GameObject pointsParent;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI endText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        maxCount = pointsParent.transform.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);

            count++;
            SetCountText();
            
            if (count == maxCount)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                endText.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && count > 0)
        {
            Destroy(gameObject);

            endText.gameObject.SetActive(true);
            endText.text = "YOU DIED";
        }
    }

    void OnMove (InputValue MovementValue)
    {
        Vector2 movementVector = MovementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump (InputValue JumpValue)
    {
        if (grounded)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    void SetCountText()
    {
        countText.text = "Points:" + count.ToString();
    }
}
