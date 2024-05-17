using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Google.Protobuf.WellKnownTypes;


public class AgentController : Agent
{
    private Rigidbody rb;
    //private float previousDistance;
    [SerializeField] private Transform target;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] private Transform shootingPoint;
    public int minTimeBetweenShots = 50;
    public int damage = 100;
    private bool shotAvailable = true;
    private int stepsUntilShotAvailable = 0;
    private Vector3 StartingPosition;

    public override void Initialize()  
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));

        target.localPosition = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
        rb.velocity = Vector3.zero;
    
        shotAvailable = true;
        
    }
    private void Shoot()
    {
        if (!shotAvailable) return;

        var layerMask = 1 << LayerMask.NameToLayer("Enemy");
        var direction = transform.forward;

        Debug.Log(message: "Shot");
        Debug.DrawRay(shootingPoint.position, direction * 288f, Color.green,  2f);

        if (Physics.Raycast(origin: shootingPoint.position, direction: direction, out var hit, 200f, layerMask: layerMask))
        {
            
            hit.transform.GetComponent<Enemy>().GetShot(damage,  this);
        }

        shotAvailable = false;
        stepsUntilShotAvailable = minTimeBetweenShots;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect agent's position as an observation
        sensor.AddObservation(transform.localPosition);

        // Additional observations can be added here
        // Example: sensor.AddObservation(target.localRotation);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Retrieve continuous actions for rotation and forward movement
        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];

        // Move the agent forward based on received continuous action for forward movement
        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);

        // Rotate the agent based on received continuous action for rotation
        transform.Rotate(0f, moveRotate * moveSpeed, 0f, Space.Self);

        // Check if the value of the continuous action for shooting is greater than or equal to 1
        if (Mathf.RoundToInt(actions.ContinuousActions[2]) >= 1)
        {
            // Execute shooting action
            Shoot();
        }

        /*
        // Alternative approach for updating agent's position using velocity (commented out)
        Vector3 velocity = new Vector3(moveX, 0f, moveZ);
        velocity = velocity.normalized * Time.deltaTime * moveSpeed;
        transform.localPosition += velocity;
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AddReward(1f);
            EndEpisode();
        }
        if (other.gameObject.tag == "Wall")
        {
            AddReward(-2f);
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
     
        continousActions[1] = Input.GetAxisRaw("Vertical");
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[2] = Input.GetKey(KeyCode.P)? 1f:0f;
      

    }
    void FixedUpdate()
    { 

           if (!shotAvailable)

            stepsUntilShotAvailable--;  // Decrement StepsUntilShotIsAvailable

                 if (stepsUntilShotAvailable <= 8)

                  shotAvailable = true;

    }
    public void RegisterKill() {
        AddReward(1f);
        EndEpisode() ;

    }
}
