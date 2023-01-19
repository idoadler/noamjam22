using DG.Tweening;
using Test;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


//[RequireComponent(typeof(Image))]
public class Character : MonoBehaviour, IDragHandler//, IBeginDragHandler, IEndDragHandler
{
    [FormerlySerializedAs("color")] public GameColors[] colors;
    public Sprite idle;
    public Sprite hanged;
    public GameObject shadow;
    public Image image;
    public bool dragOnSurfaces = true;
    public Ease fallEase = Ease.OutBounce;
    public float fallTime = 1;

    // private GameObject m_DraggingIcon;
    private bool _isDragging = false;
    private RectTransform _mDraggingPlane;
    private float _myHeight;
    private float _shadowHeight;
    private Collider _collider;
    private Animator _animator;
    private static readonly int Drag = Animator.StringToHash("OnDrag");
    private static readonly int OnRelease = Animator.StringToHash("OnRelease");
    private static readonly int OnComplete = Animator.StringToHash("OnComplete");
    private Tween _fallAnim;

    private void Start()
    {
        _myHeight = transform.position.y;
        _shadowHeight = shadow.transform.position.y;
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        shadow.transform.position = new Vector2(shadow.transform.position.x, _shadowHeight);
    }

    public void TTOnBeginDrag()
    {
        Debug.Log("1" + name);
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        if(_fallAnim != null && _fallAnim.IsPlaying())
            _fallAnim.Complete();
        _collider.enabled = false;
        _isDragging = true;
        image.sprite = hanged;
        _animator.SetTrigger(Drag);
        
        //SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        Debug.Log("2" + name);
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
            if (transform.position.y > Screen.height * 0.9) 
                transform.position = new Vector2(transform.position.x, Screen.height * 0.9f);
            if (transform.position.x < Screen.width * 0.05) 
                transform.position = new Vector2(Screen.width * 0.05f, transform.position.y);
            if (transform.position.x > Screen.width * 0.95) 
                transform.position = new Vector2(Screen.width * 0.95f, transform.position.y);
        }
    }

    public void TTOnEndDrag()
    {
        Debug.Log("3" + name);
        _collider.enabled = true; 
        _isDragging = false;
        image.sprite = idle;

        if (Mathf.Abs(transform.position.y - _myHeight) < 0.1)
        {
            transform.position = new Vector2(transform.position.x, _myHeight);
            _animator.SetTrigger(OnRelease);
        }
        else
        {
            _fallAnim = transform.DOMoveY(_myHeight, fallTime).SetEase(fallEase);
            _fallAnim.onComplete += () =>
            {
                _animator.SetTrigger(OnRelease);
            };
        }
    }

    private static T FindInParents<T>(GameObject go) where T : Component
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

    public void Dance()
    {
        if(_fallAnim != null && _fallAnim.IsPlaying())
            _fallAnim.Complete();
        _animator.SetTrigger(OnComplete);
    }
}