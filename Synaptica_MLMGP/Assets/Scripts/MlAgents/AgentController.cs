using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor.ShaderGraph.Internal;


public class AgentController : Agent
{
//    public Transform shootingPoint;
//    public int minStepsBetweenShots = 50;
//    public int damage = 100;
//    private Rigidbody rb;
//    private bool shotAvailable = true;
//    private int stepsUntilShotAvailable;
//    //private float previousDistance;
//    [SerializeField] private Transform target;
//    [SerializeField] float moveSpeed = 4f;
//    private Vector3 startingPosition;

//        public override void Initialize()  
//    {
//        rb = GetComponent<Rigidbody>();
//        startingPosition = transform.position;
//    }
//    public override void OnEpisodeBegin()
//    {
//        //transform.localPosition = new Vector3(Random.Range(-4f,4f),0.3f, Random.Range(-4f, 4f));

//        //    target.localPosition = new Vector3(Random.Range(-4f, 4f), 0.3f, Random.Range(-4f, 4f));
//        Debug.Log("EpisodeBegun");
//       transform.position = startingPosition;
//        rb.velocity = Vector3.zero;
//        shotAvailable = true;
        
//    }
//    public override void CollectObservations(VectorSensor sensor)
//    {
//        sensor.AddObservation(transform.localPosition);

//        //sensor.AddObservation(target.localRotation);
//    }
//    public override void OnActionReceived(ActionBuffers actions)
//    {
//        float moveRotate = actions.ContinuousActions[0];
//        float moveForward = actions.ContinuousActions[1];

//        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
//        transform.Rotate(0f, moveRotate * moveSpeed, 0f, Space.Self);


//        if (Mathf.RoundToInt(actions.ContinuousActions[2]) >= 1)
//        {
//            Shoot();
//        }
//        /*
//        Vector3 velocity = new Vector3(moveX,0f,moveZ) ;
//        velocity = velocity.normalized * Time.deltaTime * moveSpeed;
//        transform.localPosition += velocity;
//        */


//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if(other.gameObject.tag == "Enemy")
//        {
//            AddReward(3f);
//            EndEpisode();
//        }
//        if (other.gameObject.tag == "Wall")
//        {
//            AddReward(-2f);
//            EndEpisode();
//        }
//    }
//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
//        continousActions[1] = Input.GetAxisRaw("Vertical");
//        continousActions[0] = Input.GetAxisRaw("Horizontal");
//        continousActions[2] = Input.GetKey(KeyCode.P)? 1f:0f;



//    }
//    private void Shoot()
//    {
        
//        if (!shotAvailable)
//            return;

//        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
//        var direction = transform.forward;

//        Debug.Log("Shot");
//        Debug.DrawRay(shootingPoint.position, direction * 200f, Color.green, 2f);
//       if( Physics.Raycast(shootingPoint.position, direction,out var hit,200f, layerMask))
//        {
//            hit.transform.GetComponent<EnemyAgent>().GetShot(damage,this);
//        }
//        shotAvailable = false;
//}
//    private void OnMouseDown()
//    {
//        Shoot();
//    }
//    void FixedUpdate()
//    {
//        if(!shotAvailable)
//        {
//            stepsUntilShotAvailable--;
//            if(stepsUntilShotAvailable <= 0)
//            {
//                shotAvailable = true;
//            }
//        }
//    }
//    public void RegisterKill()
//    {
//        AddReward(1f);
//        EndEpisode();
//    }
}
