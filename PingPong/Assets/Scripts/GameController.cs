using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler
{
	private bool isDragging = false;
	private Vector3 startDragPoint;

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log(eventData.position);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isDragging = true;
		startDragPoint = eventData.pressPosition;
		Debug.Log(startDragPoint + " !!!");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isDragging = false;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
