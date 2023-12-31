using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : BaseRogueLikeHandler
{
    public class Cell
    {
        public bool Visited = false;
        public bool[] Status = new bool[4];
    }

    [Header("Dungeon Setting")]
    public Vector2 Size;
    public int StartPos = 0;
    public int DungeonSize;
    public int PlayerPos;

    [Header("Room For Generator")]
    public GameObject RoomPrefab;
    public Rooms Room;
    public Vector2 offset;

    private List<Cell> Board;
    private List<Rooms> RoomsList;

    public void AssignRoom(ThemeSet_SO _theme)
    {
        Theme = _theme;
        CellGenerator();
    }
    public void CellGenerator()
    {
        Board = new List<Cell>();

        for (int i = 0; i < Size.x; i++)
            for (int j = 0; j < Size.y; j++)
                Board.Add(new Cell());

        int _currentCell = StartPos;
        Stack<int> _path = new Stack<int>();

        int _k = 0;

        while (_k < DungeonSize)
        {
            _k++;
            Board[_currentCell].Visited = true;

            if (_currentCell == Board.Count - 1)
                break;

            List<int> _neighbors = CheckNeighbors(_currentCell);

            if (_neighbors.Count == 0)
            {
                if (_path.Count != 0)
                    _currentCell = _path.Pop();
                else
                    break;
            }
            else
            {
                _path.Push(_currentCell);

                int _newCell = _neighbors[Random.Range(0, _neighbors.Count)];

                if (_newCell > _currentCell)
                {
                    //DOWN
                    if (_newCell - 1 == _currentCell)
                    {
                        Board[_currentCell].Status[2] = true;
                        _currentCell = _newCell;
                        Board[_currentCell].Status[3] = true;
                    }
                    //RIGHT
                    else
                    {
                        Board[_currentCell].Status[1] = true;
                        _currentCell = _newCell;
                        Board[_currentCell].Status[0] = true;
                    }
                }
                else
                {
                    //UP
                    if (_newCell + 1 == _currentCell)
                    {
                        Board[_currentCell].Status[3] = true;
                        _currentCell = _newCell;
                        Board[_currentCell].Status[2] = true;
                    }
                    //LEFT
                    else
                    {
                        Board[_currentCell].Status[0] = true;
                        _currentCell = _newCell;
                        Board[_currentCell].Status[1] = true;
                    }
                }
            }

            GenerateElements();
        }
    }
    public override void GenerateElements()
    {
        if (!IsCorrectTheme() && Room != null)
        {
            Debug.LogError("Generated Wrong Theme.");
            return;
        }

        for (int i = 0; i < Size.x; i++)
            for (int j = 0; j < Size.y; j++)
            {
                Cell _currentCell = Board[Mathf.FloorToInt(i + j * Size.x)];

                if (!_currentCell.Visited) continue;

                var _room = Instantiate(RoomPrefab, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform)
                       .GetComponent<Rooms>();
                _room.UpdateRoom(Board[Mathf.FloorToInt(i + j * Size.x)].Status);
                _room.name += " " + i + "-" + j;

                RoomsList.Add(_room);
            }

        int _randomPlayerPos = Random.Range(0, RoomsList.Count);
        GameManager.Instance.Player.transform.position = RoomsList[_randomPlayerPos]
                                                       .transform.position;
        GameManager.Instance.Boss.transform.position = RoomsList[CalculateBossPosition(_randomPlayerPos)]
                                                       .transform.position;
    }
    private int CalculateBossPosition(int _playerPos)
    {
        int _distance = 3;
        int _bossPos = 0;
        if (_playerPos < RoomsList.Count)
        {
            _bossPos = _playerPos + _distance;
            if (_bossPos >= RoomsList.Count)
                _bossPos %= RoomsList.Count;
        }

        return _bossPos;
    }
    private bool IsCorrectTheme()
    {
        return Room.Theme == ThemeHolder.Instance.CurrentThemeSet;
    }
    private List<int> CheckNeighbors(int _cell)
    {
        List<int> _neighbors = new List<int>();
        //UP
        if(_cell - Size.x >= 0 && !Board[Mathf.FloorToInt(_cell - Size.x)].Visited)
            _neighbors.Add(Mathf.FloorToInt(_cell - Size.x));
        //DOWN
        if (_cell + Size.x >= 0 && !Board[Mathf.FloorToInt(_cell + Size.x)].Visited)
            _neighbors.Add(Mathf.FloorToInt(_cell + Size.x));
        //RIGHT
        if ((_cell + 1) % Size.x != 0 && !Board[Mathf.FloorToInt(_cell + 1)].Visited)
            _neighbors.Add(Mathf.FloorToInt(_cell + 1));
        //LEFT
        if (_cell % Size.x != 0 && !Board[Mathf.FloorToInt(_cell - 1)].Visited)
            _neighbors.Add(Mathf.FloorToInt(_cell - 1));

        return _neighbors;
    }
}

public class PrefabsPath
{

}
