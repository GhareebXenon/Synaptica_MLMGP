using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SciFiDoor : MonoBehaviour
    {
   
    public bool isOpen;
    [SerializeField]
    private Vector3 slideDirection = Vector3.back;
    [SerializeField]
    private float slideAmount = 1.9f;
    private Vector3 startPos;

    [SerializeField]
    private float speed = 1.0f;
    private Coroutine AnimationCouratine;
    
    private void Awake()
    {
     startPos = transform.position;
    }
    public void Open()
    {
        if (!isOpen)
        {
            if(AnimationCouratine != null)
            {
                StopCoroutine(AnimationCouratine);
            }
            AnimationCouratine = StartCoroutine(DoSlidingOpen());
        }
        
    }
    private IEnumerator DoSlidingOpen()
    {
        Vector3 endpos = startPos+slideAmount*slideDirection;
        Vector3 startPosition = transform.position;
        float time = 0;
        isOpen = true;
        while(time<1)
        {
            transform.position = Vector3.Lerp(startPosition, endpos,time);
            yield return null;
            time += Time.deltaTime*speed;
        }
    }
    public void Close()
    {
        if (isOpen)
        {
            if(AnimationCouratine != null)
            {
                StopCoroutine(AnimationCouratine);
            }
            
            AnimationCouratine = StartCoroutine(DoSlidingClose());

        }
    }
    private IEnumerator DoSlidingClose()
    {
        Vector3 endPos = startPos;
        Vector3 startPosition = transform.position;
        float time = 0;
        isOpen = false;
        
        while(time<1)
        {
            transform.position = Vector3.Lerp(startPosition, endPos,time);
            yield return null;
            time += Time.deltaTime*speed;
        }
    }
}
