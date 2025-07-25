using UnityEngine;
using UnityEngine.InputSystem;

public class Grapple : MonoBehaviour
{
    [SerializeField] GameObject obj;
    Rigidbody rb;

    Vector3 mousePos;
    Vector3 point;

    float distance;

    Vector3 direction;

    [SerializeField]LayerMask environment;

    LineRenderer line;

    [SerializeField] GameObject jointPrefab;
    GameObject jointObject;
    ConfigurableJoint joint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        line = obj.GetComponent<LineRenderer>();

        distance = transform.position.y + Camera.main.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);

        mousePos = Mouse.current.position.ReadValue();

        Aim();


    }

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, environment))
        {
            direction = hit.point - transform.position;
        }
        else
        {
            direction = ray.GetPoint(distance);
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, environment))
            {
                line.gameObject.SetActive(true);
                line.SetPosition(1, hit.point);

                point = hit.point;

                jointObject = Instantiate(jointPrefab, hit.point, Quaternion.identity);
                joint = jointObject.GetComponent<ConfigurableJoint>();
                
                joint.connectedBody = rb;
            }
        }
        else if (context.canceled)
        {
            Destroy(jointObject);
            line.gameObject.SetActive(false);
        }
    }
}
