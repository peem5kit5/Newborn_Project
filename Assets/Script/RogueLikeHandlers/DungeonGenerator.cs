using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Rule
{
    public GameObject Room;
    public Vector2Int MinPosition;
    public Vector2Int MaxPosition;

    public bool Obligatory;

    public int ProbabilityOfSpawning(int x, int y)
    {
        if (x >= MinPosition.x && x <= MaxPosition.x && y >= MinPosition.y && y <= MaxPosition.y)
            return Obligatory ? 2 : 1;

        return 0;
    }
}
[System.Serializable]
public class RuleTheme
{
    public string ThemeKey;
    public Rule[] Rules;
}

public class DungeonGenerator : Singleton<DungeonGenerator>
{
    public class Cell
    {
        public bool GeneratorVisited = false;
        public bool[] Status = new bool[4];
    }

    public Vector2Int Size;
    public int StartPos = 0;
    public Vector2 Offset;

    [SerializeField] private NavMeshSurface surface;

    private Rule[] ruleTheme;
    private List<Cell> board;
    private List<Transform> positions = new List<Transform>();
    public void SetUpDungeon(RuleTheme _rule)
    {
        ruleTheme = _rule.Rules;
        MazeGenerator();
        surface.BuildNavMesh();
    }

    private void GenerateDungeon()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Cell _currentCell = board[(i + j * Size.x)];
                if (_currentCell.GeneratorVisited)
                {
                    int _randomRoom = -1;
                    List<int> _availableRooms = new List<int>();

                    for (int k = 0; k < ruleTheme.Length; k++)
                    {
                        int p = ruleTheme[k].ProbabilityOfSpawning(i, j);

                        if (p == 2)
                        {
                            _randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                            _availableRooms.Add(k);
                    }

                    if (_randomRoom == -1)
                    {
                        if (_availableRooms.Count > 0)
                            _randomRoom = _availableRooms[Random.Range(0, _availableRooms.Count)];
                        else
                            _randomRoom = 0;

                    }

                    GameObject _newRoomOBJ = Instantiate(ruleTheme[_randomRoom].Room, new Vector3(i * Offset.x, 0, -j * Offset.y), Quaternion.identity, transform);
                    Rooms _newRoom = _newRoomOBJ.GetComponent<Rooms>();
                    _newRoom.UpdateRoom(_currentCell.Status);
                    _newRoom.name += " " + i + "-" + j;
                    positions.Add(_newRoomOBJ.transform);
                }
            }
        }
        //NavMeshBuilder.BuildNavMeshData()
    }

    private void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
                board.Add(new Cell());
        }

        int _currentCell = StartPos;

        Stack<int> _path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[_currentCell].GeneratorVisited = true;

            if (_currentCell == board.Count - 1)
                break;

            //Check the cell's neighbors
            List<int> _neighbors = CheckNeighbors(_currentCell);

            if (_neighbors.Count == 0)
            {
                if (_path.Count == 0)
                    break;
                else
                    _currentCell = _path.Pop();
            }
            else
            {
                _path.Push(_currentCell);

                int _newCell = _neighbors[Random.Range(0, _neighbors.Count)];

                if (_newCell > _currentCell)
                {
                    //down or right
                    if (_newCell - 1 == _currentCell)
                    {
                        board[_currentCell].Status[2] = true;
                        _currentCell = _newCell;
                        board[_currentCell].Status[3] = true;
                    }
                    else
                    {
                        board[_currentCell].Status[1] = true;
                        _currentCell = _newCell;
                        board[_currentCell].Status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (_newCell + 1 == _currentCell)
                    {
                        board[_currentCell].Status[3] = true;
                        _currentCell = _newCell;
                        board[_currentCell].Status[2] = true;
                    }
                    else
                    {
                        board[_currentCell].Status[0] = true;
                        _currentCell = _newCell;
                        board[_currentCell].Status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    private List<int> CheckNeighbors(int _cell)
    {
        List<int> _neighbors = new List<int>();

        //check up neighbor
        if (_cell - Size.x >= 0 && !board[(_cell - Size.x)].GeneratorVisited)
        {
            _neighbors.Add((_cell - Size.x));
        }
        //check down neighbor
        if (_cell + Size.x < board.Count && !board[(_cell + Size.x)].GeneratorVisited)
        {
            _neighbors.Add((_cell + Size.x));
        }
        //check right neighbor
        if ((_cell + 1) % Size.x != 0 && !board[(_cell + 1)].GeneratorVisited)
        {
            _neighbors.Add((_cell + 1));
        }
        //check left neighbor
        if (_cell % Size.x != 0 && !board[(_cell - 1)].GeneratorVisited)
        {
            _neighbors.Add((_cell - 1));
        }
        return _neighbors;
    }

    public Transform CalcuateHowFarFromBoss(Transform _targetA, Transform _targetB, int _howFar)
    {
        _targetA.SetParent(transform);
        int _randomPosA = Random.Range(0, positions.Count);
        _targetA.position = positions[_randomPosA].position;

        _targetB.SetParent(transform);
        if(_howFar > _randomPosA)
        {
            int _distance = Mathf.Abs(_howFar - _randomPosA);
            int _randomPosB = Random.Range(-_distance, _distance);
            _targetB.position = positions[_randomPosB].position;
        }
        else
        {
            int _distance = Mathf.Abs(_howFar + _randomPosA);
            int _randomPosB = Random.Range(-_distance, _distance);
            _targetB.position = positions[_randomPosB].position;
        }

        return _targetA;
    }
}