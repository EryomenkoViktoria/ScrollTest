using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPlayer : MonoBehaviour
{
    [SerializeField] private Button _thisButton;
    [SerializeField] private TextMeshProUGUI _placeNumber;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _iconPlaer;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _leadPlace;

    private StatusButton _statusButton;
    [SerializeField] private int _index;
    private UISpawner _spawner;

    private void OnEnable()
    {
        _thisButton.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveListener(Click);
    }

    private void Click()
    {
        _spawner.ClickThisButton(_index);
    }

    public void SetInfo(PlayerInfo info, int index, UISpawner spawner)
    {
        _placeNumber.text = info.PlaceNumber.ToString();
        _icon.sprite = info.Icon;
        _name.text = info.Name;
        _iconPlaer.sprite = info.IconPlayer;
        _score.text = info.Score.ToString();

        _index = index;
        _spawner = spawner;

        SetLeaders();
    }

    private void SetLeaders()
    {
        if (_index == 0 || _index == 1 || _index == 2)
        {
            _leadPlace.enabled = true;
        }
    }

    public void SelectedButton(Color32 newColor)
    {
        _backImage.color = newColor;
    }

    public void ChangeStatus(StatusButton status)
    {
        _statusButton = status;
    }

    public void SetStatus(StatusButton status, Color32 newColor)
    {
        SelectedButton(newColor);
        ChangeStatus(status);
    }

    public StatusButton GetChangeStatus() => _statusButton;
}