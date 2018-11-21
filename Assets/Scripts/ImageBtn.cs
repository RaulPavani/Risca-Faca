using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageBtn : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerExitHandler, IDeselectHandler
{

    public Image hatImg;

    private void Start()
    {
        hatImg.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hatImg.gameObject.SetActive(true);
    }
    public void OnSelect(BaseEventData eventData)
    {
        hatImg.gameObject.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        hatImg.gameObject.SetActive(false);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        hatImg.gameObject.SetActive(false);
    }

}
