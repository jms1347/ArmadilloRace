using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool DuplicateOnBeginDrag;
	//[ShowIf("DuplicateOnBeginDrag")]
	public Transform OverrideParentOnInstantiatedObject;
	//[ShowIf("DuplicateOnBeginDrag")]
	public bool InstantiateOnce = true;
	public bool FollowPointerOnDrag;
	//[ShowIf("FollowPointerOnDrag")]
	public bool RestrictXAxis;
	//[ShowIf("FollowPointerOnDrag")]
	public bool RestrictYAxis;
	public bool DestroyOnEndDrag;
	public UnityAction<GameObject, PointerEventData> OnBeginDragAction;
	public UnityAction<GameObject, PointerEventData> OnDragAction;
	public UnityAction<GameObject, GameObject, PointerEventData> OnEndDragAction;
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (DuplicateOnBeginDrag)
		{
			Transform ParentTransform = OverrideParentOnInstantiatedObject == null ? transform.parent : OverrideParentOnInstantiatedObject;
			GameObject InstantiatedObject = Instantiate(gameObject, ParentTransform, false);
			InstantiatedObject.name = name;
			eventData.pointerDrag = InstantiatedObject;
			InstantiatedObject.GetComponent<Image>().raycastTarget = false;
			if (InstantiateOnce)
				InstantiatedObject.GetComponent<DraggableUI>().DuplicateOnBeginDrag = false;
		}
		else
		{
			GetComponent<Image>().raycastTarget = false;
		}
		OnBeginDragAction?.Invoke(gameObject, eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (FollowPointerOnDrag)
		{
			Vector3 PointerWorldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
			if (RestrictXAxis)
			{
				transform.position = PointerWorldPosition/*.WithX(transform.position.x)*/;
			}
			else if (RestrictYAxis)
			{
				transform.position = PointerWorldPosition/*.WithY(transform.position.y)*/;
			}
			else
			{
				transform.position = PointerWorldPosition;
			}
		}
		OnDragAction?.Invoke(gameObject, eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (DestroyOnEndDrag)
		{
			Destroy(gameObject);
		}
		GetComponent<Image>().raycastTarget = true;
		OnEndDragAction?.Invoke(gameObject, eventData.pointerEnter, eventData);
	}
}
