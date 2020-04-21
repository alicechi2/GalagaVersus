using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float pathSpeed;
    float distanceTraveled;
    Vector3 pos;
    public Rigidbody2D m_Rigidbody;
    public bool reachedEnd = false;
    float pathLength;
    

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        pos = transform.position;
        pathLength = pathCreator.path.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedEnd)
        {
            distanceTraveled += pathSpeed*Time.deltaTime;
            if (distanceTraveled >= pathLength)
            {
                reachedEnd = true;
            }
            // transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
            Vector3 newPos = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
            transform.position = newPos;
            // m_Rigidbody.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled);
            // transform.eulerAngles = pathCreator.path.GetDirectionAtDistance(distanceTravelled);
            // transform.forward = pathCreator.path.GetDirectionAtDistance(distanceTravelled);
            // Quaternion angle = pathCreator.path.GetRotationAtDistance(distanceTraveled);
            // Debug.Log("Angle x is " + angle.x);
            // Debug.Log(angle.y);
            // Quaternion angle = pathCreator.path.GetRotation(distanceTraveled);
            // angle.x = 0f;
            // angle.y = 0f;
            // transform.eulerAngles = angle.eulerAngles +;
            // transform.forward = newPos - pos;
            pos = newPos;
            Debug.Log(pathCreator.path.length);
            Debug.Log(distanceTraveled);
        
            // transform.rotation = angle;
        }
        
    }
}
