using DG.Tweening;
using Test;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


//[RequireComponent(typeof(Image))]
public class Character : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [FormerlySerializedAs("color")] public GameColors[] colors;
    public Sprite idle;
    public Sprite hanged;
    public GameObject shadow;
    public bool dragOnSurfaces = true;
    public Ease fallEase = Ease.OutBounce;
    public float fallTime = 1;

    // private GameObject m_DraggingIcon;
    private bool _isDragging = false;
    private RectTransform _mDraggingPlane;
    private float _myHeight;
    private float _shadowHeight;
    private Collider2D _collider2D;

    private void Start()
    {
        _myHeight = transform.position.y;
        _shadowHeight = shadow.transform.position.y;
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        shadow.transform.position = new Vector2(shadow.transform.position.x, _shadowHeight);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        _collider2D.enabled = false;
        _isDragging = true;
        
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        //if (m_DraggingIcon != null)
        if (_isDragging)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            _mDraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_mDraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = _mDraggingPlane.rotation;
            if (transform.position.y < _myHeight)
                transform.position = new Vector2(transform.position.x, _myHeight);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _collider2D.enabled = true; Debug.Log("ttt");
        Tween myTween = transform.DOMoveY(_myHeight, fallTime).SetEase(fallEase);
        //myTween.onComplete += () => { };
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