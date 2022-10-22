using System;
using Test;
using UnityEngine;
using UnityEngine.EventSystems;


//[RequireComponent(typeof(Image))]
public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameColors[] color;
    public GameObject shadow;
    public bool dragOnSurfaces = true;

    // private GameObject m_DraggingIcon;
    private bool isDragging = false;
    private RectTransform m_DraggingPlane;
    private float myHeight;
    private float shadowHeight;

    private void Start()
    {
        myHeight = transform.position.y;
        shadowHeight = shadow.transform.position.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        isDragging = true;
        
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        //if (m_DraggingIcon != null)
        if (isDragging)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
            if (transform.position.y < myHeight)
                transform.position = new Vector2(transform.position.x, myHeight);            
            shadow.transform.position = new Vector2(shadow.transform.position.x, shadowHeight);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("PLAYER TRIGGER");
    }
}