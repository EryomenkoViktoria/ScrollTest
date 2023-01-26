using UnityEngine;
using UnityEngine.UI;

public class ObserverHolder : MonoBehaviour, IHolder
{
    [SerializeField] private RectTransform _contentRect;
    [SerializeField] private ScrollRect _scrollRect;

    private RectTransform _holder;
    private RectTransform _buttonRect;
    private float _upBorder, _downBorder;
    private bool _isSwappedProcess;
    private int _buttonIndex;
    private string _snapHolderName = "HolderFolder";

    private void Start()
    {
        InitSnapSettings();
    }

    private void InitSnapSettings()
    {
        _contentRect.sizeDelta = new Vector2(_contentRect.sizeDelta.x, 0);
        _scrollRect.onValueChanged.AddListener((Vector2 vec) => UpdateRect());
        _holder = new GameObject(_snapHolderName, typeof(RectTransform)).GetComponent<RectTransform>();
        _holder.SetParent(_scrollRect.transform);
    }

    public void Click(int index, RectTransform transform)
    {
        if (_isSwappedProcess)
            SwapToDown();

        _buttonIndex = index;
        _buttonRect = transform;
        _holder.sizeDelta = new Vector2(_buttonRect.sizeDelta.x, _buttonRect.sizeDelta.y);
    }

    private void Update()
    {
        UpdateLimiters();
        UpdateRect();
    }

    private void UpdateLimiters()
    {
        var scrollRectTransform = _scrollRect.GetComponent<RectTransform>();

        _upBorder = scrollRectTransform.position.y + scrollRectTransform.sizeDelta.y / 2 - _holder.sizeDelta.y / 2;
        _downBorder = scrollRectTransform.position.y - scrollRectTransform.sizeDelta.y / 2 + _holder.sizeDelta.y / 2;
    }

    private void UpdateRect()
    {
        if (_buttonRect == null)
            return;

        if (_holder.position.y > _downBorder && _holder.position.y < _upBorder && _isSwappedProcess)
            SwapToDown();

        if (_buttonRect.position.y < _downBorder)
        {
            ToUp(_downBorder);
        }
        else if (_buttonRect.position.y > _upBorder)
        {
            ToUp(_upBorder);
        }
    }

    private void ToUp(float y)
    {
        _holder.position = new Vector3(_buttonRect.position.x, y);

        if (!_isSwappedProcess)
            SwapToUp();
    }

    private void SwapToUp()
    {
        SwapTo(true, _buttonRect, _holder);
        Vector3 oldHolderPos = _holder.position;
        _holder.position = _buttonRect.position;
        _buttonRect.position = oldHolderPos;
    }

    private void SwapToDown()
    {
        SwapTo(false, _holder, _buttonRect);
        _buttonRect.position = _holder.position;
    }

    private void SwapTo(bool isProcess, RectTransform observerHolder, RectTransform parent)
    {
        _isSwappedProcess = isProcess;
        observerHolder.SetParent(parent.parent);
        parent.SetParent(_contentRect);
        parent.SetSiblingIndex(_buttonIndex);
    }
}