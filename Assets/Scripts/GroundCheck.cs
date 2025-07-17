using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform player;

    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.position;
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerController.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerController.grounded = false;
        }
    }
}
