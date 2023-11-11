using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }
    [Header("Info")]
    public List<EntityParty> PartyMembers;
    public int PartyCurrentCount;
    public int PartyMaxCount;
    public GameObject[] PartyPrefabs;
    public List<GameObject> CurrentOBJs = new List<GameObject>();
    [Space]
    [Header("GUI Setting")]
    public float spacing;
    public RectTransform UIHeadParty;
    public GameObject PartyUIbuttonTemplate;


    public Action OnPartyMemberChanged = delegate { };
    public void Awake()
    {
        SetUp();
    }
    void SetUp()
    {
        if (Instance != null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        OnPartyMemberChanged += RefreshPartyMember;
    }
    public void AddParty(EntityParty _member)
    {
        if(PartyCurrentCount < PartyMaxCount)
        {
            SpawnEntity(_member);
        }
        else
        {
            return;
        }
        
    }

    public void SpawnEntity(EntityParty _member)
    {
        foreach (GameObject _obj in PartyPrefabs)
        {
            EntityParty _party = _obj.GetComponent<EntityParty>();
            if (_party.Equals(_member))
            {
                Instantiate(_obj, GameManager.Instance.Player.transform.position, Quaternion.identity);
                _party.Init();
                _party.VirtualInit();
                CurrentOBJs.Add(_obj);
                PartyMembers.Add(_member);
                OnPartyMemberChanged?.Invoke();
            }
        }
      
    }
    public void DeleteEntity(EntityParty _member)
    {
        foreach(GameObject _obj in CurrentOBJs)
        {
            EntityParty _party = _obj.GetComponent<EntityParty>();
            if(_party.Equals(_member))
            {
                Destroy(_obj);
                PartyMembers.Remove(_member);
                OnPartyMemberChanged?.Invoke();
            }
        }
        
    }
    public void RemoveParty(EntityParty _member)
    {
        if (PartyMembers.Contains(_member))
        {
            
            DeleteEntity(_member);
        }
        else
        {
            return;
        }
    }
    public void RefreshPartyMember()
    {
        for(int i = 0; i < PartyMembers.Count; i++)
        {
            GameObject _uiBar = Instantiate(PartyUIbuttonTemplate, UIHeadParty);
            RectTransform _rect = _uiBar.GetComponent<RectTransform>();
            Image _uiImage = _uiBar.transform.Find("Image").GetComponent<Image>();
            _uiImage.sprite = PartyMembers[i].Icon;

            Vector2 _newPos = new Vector2(0.0f, -i * (spacing + _rect.sizeDelta.y));
            _rect.anchoredPosition = _newPos;
        }
    }
}
