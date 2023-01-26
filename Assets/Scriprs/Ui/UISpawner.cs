using System.Collections.Generic;
using UnityEngine;

public class UISpawner : MonoBehaviour
{
    [SerializeField] private Data _data;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _observerObject;

    private IHolder _iObserver;
    private GameObject _prefab;
    private Color32 _selectedColor;
    private Color32 _defaultColor;

    private List<InfoPlayer> _infoPlayers = new List<InfoPlayer>();

    private void Awake()
    {
        ContentCreater();
        SetIObserver();
    }

    private void ContentCreater()
    {
        if (_data == null)
        {
            Debug.LogError("Data is not Found!");
            return;
        }

        _prefab = _data.Prefab;
        _selectedColor = _data.SelectedColor;
        _defaultColor = _data.DefaultColor;

        ClearInfoPlayer();

        if (_data.IsUseDefaultInfo)
        {
            CreateDefaultButton();
        }
        else
        {
            CreateIndividualsButton();
        }
    }

    private void SetIObserver()
    {
        if (_observerObject.TryGetComponent(out IHolder holder))
        {
            _iObserver = holder;
        }
    }

    private void CreateDefaultButton()
    {
        for (int i = 0; i < _data.CountPlayers; i++)
        {
            AddList(_data.DefaultPlayerInfo, i);
        }
    }

    private void CreateIndividualsButton()
    {
        for (int i = 0; i < _data.PlayerInfo.Count; i++)
        {
            AddList(_data.PlayerInfo[i], i);
        }
    }

    private void AddList(PlayerInfo playerInfo, int i)
    {
        var palyer = Instantiate(_prefab, _content).GetComponent<InfoPlayer>();
        palyer.SetInfo(playerInfo, i, this);
        _infoPlayers.Add(palyer);
    }

    public void ClickThisButton(int index)
    {
        SetNewSelected();
        _infoPlayers[index].SetStatus(StatusButton.Selected, _selectedColor);
        RectTransform transform = _infoPlayers[index].GetComponent<RectTransform>();
        _iObserver.Click(index, transform);
    }

    private void SetNewSelected()
    {
        foreach (var player in _infoPlayers)
        {
            if (player.GetChangeStatus() == StatusButton.Selected)
            {
                player.SetStatus(StatusButton.Unselected, _defaultColor);
                break;
            }
        }
    }

    private void ClearInfoPlayer() => _infoPlayers.Clear();
}
