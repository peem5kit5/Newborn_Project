using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : Singleton<QuestManager>
{
    public Quest_SO CurrentQuest;
    public Quest_SO[] QuestDatas;
    public Dictionary<string, Quest_SO> MainQuestDict = new Dictionary<string, Quest_SO>();
    [SerializeField] private UI_Quest uiQuest;

    public override void Awake()
    {
        base.Awake();
        MainQuestDict = ConvertToDict();
    }

    public void RandomQuests()
    {
        int _random = Random.Range(0, QuestDatas.Length);

        if(MainQuestDict.ContainsValue(QuestDatas[_random]))
            CurrentQuest = QuestDatas[_random];

        if(CurrentQuest != null)
        {
            switch (CurrentQuest.QuestID)
            {
                case 0:
                    CurrentQuest.SubQuests = null;
                    break;

                case 1:
                    SubQuest _subQuest_1 = new SubQuest(1, 1);
                    CurrentQuest.SubQuests.Add(_subQuest_1);
                    break;

                case 2:
                    HashSet<SubQuest> _subQuests = new HashSet<SubQuest>();
                    for (int i = 0; i < 3; i++)
                    {
                        SubQuest _subQuest_0 = new SubQuest(i, 1);
                        _subQuests.Add(_subQuest_0);
                    }
                    CurrentQuest.SubQuests = _subQuests;
                    break;
            }
        }
    }
    private Dictionary<string, Quest_SO> ConvertToDict()
    {
        Dictionary<string, Quest_SO> _questDict = new Dictionary<string, Quest_SO>();

        if (QuestDatas.Length > 0)
            foreach (Quest_SO _quest in QuestDatas)
                _questDict.Add(_quest.QuestName, _quest);

        return _questDict;
    }
}
