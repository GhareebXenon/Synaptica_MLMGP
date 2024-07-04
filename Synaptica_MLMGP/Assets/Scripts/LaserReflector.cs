using cowsins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReflector : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    float rotationSpeed = 0.4f;

    Vector3 position;
    Vector3 direction;
    LineRenderer lr;
    public bool isOpen;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private float dragDistanceThreshold = 6f;
    [SerializeField] private string mission;
    [SerializeField] private UnityEvent OnCompleted;

    GameObject tempReflector;
    void Start()
    {
        isOpen = false; //initailze lazer as closed
        lr = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isOpen)
        { // activate the laser
            lr.positionCount = 2;// set the laser line
            lr.SetPosition(0, position);//start point
            RaycastHit hit;// check for collision 
            if (Physics.Raycast(position, direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Reflector"))
                { // if hit reflector
                    // interact
                    tempReflector = hit.collider.gameObject;
                    Vector3 temp = Vector3.Reflect(direction, hit.normal);
                    hit.collider.gameObject.GetComponent<LaserReflector>().OpenRay(hit.point, temp);
                }
                else if (hit.collider.CompareTag("CenterReflector"))
                {
                    OnCompleted?.Invoke();
                    MissionManager.Instance.UpdateMission(mission, 1);
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    PlayerStats player = hit.collider.gameObject.GetComponent<PlayerStats>();
                    player.Damage(1);
                }
                else
                {
                    if (tempReflector)
                    {
                        tempReflector.GetComponent<LaserReflector>().CloseRay();
                        tempReflector = null;
                    }
                }
                lr.SetPosition(1, hit.point);// set laser to the collosion point 
            }
            
            else
            {
                if (tempReflector)
                { // if no collision
                    // deactive
                    tempReflector.GetComponent<LaserReflector>().CloseRay();
                    tempReflector=null;
                }
                lr.SetPosition(1,direction*100);
            }
        }

        else
        {
            if (tempReflector)
            {
                tempReflector.GetComponent<LaserReflector>().CloseRay();
                tempReflector = null;
            }
        }
    }
    // open and close laser
    public void OpenRay(Vector3 pos,Vector3 dir)
    {
        isOpen = true;
        position = pos;
        direction = dir;
    }
    public void CloseRay()
    {
        isOpen = false;
        lr.positionCount = 0;
    }
    // mouse rotation
    void OnMouseDown()
    {
        if (gameObject.tag == "CenterReflector") return;

        weaponController.canShoot = false;

        // calculate initialization mouse for dragging
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    // update position based on mouse movement
    void OnMouseDrag()
    {
        if (gameObject.tag == "CenterReflector" || Vector3.Distance(playerTransform.position, transform.position) > dragDistanceThreshold) return;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        Vector3 newPosition = new Vector3(curPosition.x, transform.position.y, curPosition.z);
        transform.position = newPosition;

        // rotate cube
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down, rotationSpeed);
        }
    }
    void OnMouseUp()
    {
        if (gameObject.tag == "CenterReflector") return;
        StartCoroutine(EnableShooting());
    }
    private IEnumerator EnableShooting()
    {
        yield return new WaitForEndOfFrame();
        weaponController.canShoot = true;
        yield return null;
    }

}
