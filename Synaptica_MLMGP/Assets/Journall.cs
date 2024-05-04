using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Image _noteImage;
    private bool _isShowingNote = false;  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            _isShowingNote = !_isShowingNote;  
            _noteImage.enabled = _isShowingNote;
        }
    }
}
