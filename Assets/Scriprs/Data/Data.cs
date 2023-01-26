using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data")]
public class Data : ScriptableObject
{
    [Header("Main Settings")]
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;

    [SerializeField] private Color32 _selectedColor;
    public Color32 SelectedColor => _selectedColor;

    [SerializeField] private Color32 _defaultColor;
    public Color32 DefaultColor => _defaultColor;

    [Space(10)]
    [Header("Info")]
    [SerializeField] private bool _isUseDefaultInfo;
    public bool IsUseDefaultInfo => _isUseDefaultInfo;

    [SerializeField] private int _countPlayers;
    public int CountPlayers => _countPlayers;

    [SerializeField] private PlayerInfo _defaultPlayerInfo;
    public PlayerInfo DefaultPlayerInfo => _defaultPlayerInfo;

    [Header("Individual Players")]
    [SerializeField] private List<PlayerInfo> _playerInfo = new List<PlayerInfo>();
    public List<PlayerInfo> PlayerInfo => _playerInfo;
}

[Serializable]
public struct PlayerInfo
{
    public int PlaceNumber;
    public Sprite Icon;
    public string Name;
    public Sprite IconPlayer;
    public int Score;
}

public enum StatusButton
{
    None = 0,
    Selected = 1,
    Unselected = 2,
}