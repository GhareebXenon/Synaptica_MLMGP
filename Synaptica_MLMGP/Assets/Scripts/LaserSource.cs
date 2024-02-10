using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserSource : MonoBehaviour
{
    [SerializeField] Transform laserStartPoint;
    Vector3 direction;
    LineRenderer lr;// draw the laser beam
    GameObject tempReflector;// reflector
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        direction = laserStartPoint.forward;// starting direction
        lr.positionCount = 2;// set the lr with two points 
        lr.SetPosition(0, laserStartPoint.position);//position the start point
    }
    void Update()
    {
        RaycastHit hit;//shoots ray from statring direction to check for collision
        if(Physics.Raycast(laserStartPoint.position,direction,out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Reflector"))
            {
                tempReflector = hit.collider.gameObject;
                Vector3 temp = Vector3.Reflect(direction, hit.normal);// calc the new direction
                hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);// activate the reflector
            }
            lr.SetPosition(1, hit.point);// set the end point of the laser to colloision point
        }
        else
        {
            //if no collosion close hit reflector
            if (tempReflector)
            {
                tempReflector.GetComponent<LaserReflector>().CloseRay();
                tempReflector=null;
            }
            lr.SetPosition(1, direction * 200);// laser extend to staright line
        }
    }
}
