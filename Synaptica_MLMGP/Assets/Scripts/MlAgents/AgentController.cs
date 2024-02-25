using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor.ShaderGraph.Internal;


public class AgentController : Agent
{
   //private Rigidbody rb;
   //private float previousDistance;
    [SerializeField] private Transform target;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0f,0.3f,0f);
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            target.localPosition = new Vector3(-4f, 0.3f, 0f);
        }
        if (rand == 1)
        {
            target.localPosition = new Vector3(4f, 0.3f, 0f);
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localRotation);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float move = actions.ContinuousActions[0];
        float moveSpeed = 2f;

        transform.localPosition += new Vector3(move, 0f) * Time.deltaTime * moveSpeed;

        
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
