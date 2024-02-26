using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor.ShaderGraph.Internal;


public class AgentController : Agent
{
    private Rigidbody rb;
    //private float previousDistance;
    [SerializeField] private Transform target;
    [SerializeField] float moveSpeed = 4f;

        public override void Initialize()  
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-4f,4f),0.3f, Random.Range(-4f, 4f));
        
            target.localPosition = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
       
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        //sensor.AddObservation(target.localRotation);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];

        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, moveRotate * moveSpeed, 0f, Space.Self);



        /*
        Vector3 velocity = new Vector3(moveX,0f,moveZ) ;
        velocity = velocity.normalized * Time.deltaTime * moveSpeed;
        transform.localPosition += velocity;
        */


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            AddReward(3f);
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
      

    }
    //void FixedUpdate()
    //{
    //    // Measure distance moved
    //    float currentDistance = Vector3.Distance(transform.position, rb.position);
    //    float distanceMoved = Mathf.Abs(currentDistance - previousDistance);

    //    // If the agent has not moved significantly, apply penalty
    //    if (distanceMoved < 1f)
    //    {
    //        AddReward(-1);
    //    }

    //    previousDistance = currentDistance;
    //}
}
