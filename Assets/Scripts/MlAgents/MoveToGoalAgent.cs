using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class MoveToGoalAgent : Agent
{
    public float moveSpeed = 5f;
    private Vector3 startLocation;

    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material win;
    [SerializeField] private Material lose;
    [SerializeField] private MeshRenderer plane;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject drone;


    //[SerializeField] private Vector3 goalPosition;
    void Start()
    {
        startLocation = transform.localPosition;
        explosion.SetActive(false);
        gameObject.SetActive(true);
    }

    public override void OnEpisodeBegin()
    {
        //sexplosion.SetActive(false);
        //transform.localPosition = startLocation;
        //transform.localPosition = new Vector3(Random.Range(-20f, 8f), 0.15f, Random.Range(8f, 28f));
        //targetTransform.localPosition = new Vector3(Random.Range(-20f, 8f), 0.15f, Random.Range(8f, 28f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        // Debug.Log("x, z " + moveX + " " + moveZ);

        transform.localPosition += new Vector3(moveX,0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actionSegment = actionsOut.ContinuousActions;
        actionSegment[0] = Input.GetAxisRaw("Horizontal");
        actionSegment[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(1f);

            //plane.material = win;
           Explode(true);
            Debug.Log("win");

            //EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            //plane.material = lose;
            Debug.Log("lose");
            Explode(false);
            EndEpisode();
        }
    }

    private void Explode(bool takeDamage)
    {
        explosion.SetActive(true);
        drone.SetActive(false);
        if (takeDamage)
        {
            targetTransform.GetComponent<PlayerHealth>().takeDamage(20);
        }
        Invoke("DoSomethingAfterDelay", 1.5f);
    }

    private void DoSomethingAfterDelay()
    {
        Debug.Log("after 3sec");
        explosion.SetActive(false);
        EndEpisode();
        Destroy(explosion);
        Destroy(gameObject);

    }
}
