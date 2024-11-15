using UnityEngine;

public class PoliceCarChase : MonoBehaviour
{
    [SerializeField]
    private Transform playerCar;

    [SerializeField]
    private float chaseSpeed = 20f;

    [SerializeField]
    private float turnSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        // get the rigidbody component
        rb = GetComponent<Rigidbody>();

        // ensure the player car is assigned
        if (playerCar == null)
        {
            // find the player car by tag
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerCar = playerObj.transform;
            }
            else
            {
                Debug.LogError("player car not found");
            }
        }
    }

    void FixedUpdate()
    {
        // calculate direction towards the player car
        Vector3 direction = (playerCar.position - transform.position).normalized;

        // move the police car forward
        Vector3 move = transform.forward * chaseSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // rotate towards the player car
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);
    }
}
