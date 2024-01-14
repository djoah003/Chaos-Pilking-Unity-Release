using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsAudio : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    
    [SerializeField] private AudioClip PressClip, UnPressClip;
    [SerializeField] private AudioSource _source;




    public void OnPointerDown(PointerEventData eventData)
    {
        
        _source.PlayOneShot(PressClip);
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        _source.PlayOneShot(UnPressClip);
    }

    public void IWasClicked()
    {
        Debug.Log("Clicked");
    }

}
