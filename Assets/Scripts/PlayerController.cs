using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject thankYouText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject TryAgainButton;

    int currentScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        maxCount = pointsParent.transform.childCount;

        int.TryParse(SceneManager.GetActiveScene().name, out currentScene);
    }

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

            if (count == maxCount && SceneManager.sceneCountInBuildSettings == currentScene + 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                endText.gameObject.SetActive(true);
                backButton.SetActive(true);
                thankYouText.SetActive(true);
            }
            else if (count == maxCount)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                endText.gameObject.SetActive(true);
                nextButton.SetActive(true);
                backButton.SetActive(true);
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
            backButton.SetActive(true);
            TryAgainButton.SetActive(true);
        }
    }

    public void Move (InputAction.CallbackContext MovementValue)
    {
        Vector2 movementVector = MovementValue.ReadValue<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void Jump (InputAction.CallbackContext JumpValue)
    {
        if (grounded && JumpValue.performed)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    void SetCountText()
    {
        countText.text = "Points:" + count.ToString();
    }
}
