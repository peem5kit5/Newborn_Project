using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Data/PuzzleData")]
public class Puzzle_SO : ScriptableObject
{
    public int QuestID;
    public string QuestName;

    public int Goal;
    public int CurrentScore;
    public bool IsFinished;

    public HashSet<SubQuest> SubQuests;
    public void Update()
    {
        CurrentScore = SubQuests.Where(_subquest => _subquest.IsFinished).Count();

        if(CurrentScore >= Goal)
            IsFinished = true;
    }
}

public class SubQuest
{
    public int SubQuestID;
    public int Goal;
    public int CurrentScore;
    public bool IsFinished;

    public SubQuest(int _id,int _goal)
    {
        SubQuestID = _id;
        Goal = _goal;
        CurrentScore = 0;
        IsFinished = false;
    }
    public void Score(int _score)
    {
        if (CurrentScore < Goal)
            CurrentScore += _score;
        else
            IsFinished = true;
    }
}
