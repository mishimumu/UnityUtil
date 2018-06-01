using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UScrollList : MonoBehaviour {

    public List<UScrollRectChild> scrollList;
    public int index;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollList[0].OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        scrollList[0].OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollList[0].OnEndDrag(eventData);
    }


}
