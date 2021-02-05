using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterTabExample : MonoBehaviour
{
    [TabGroup("Character Details")] 
    public CharacterInfo characterInfo;

    [TabGroup("Character Stats")] 
    public CharacterStats stats;

    [TabGroup("Other")] 
    public List<InventoryObject> inventory;

    [TabGroup("Other")] 
    public List<Skill> skillList;
}

[System.Serializable]
public class Skill
{
    //[TabGroup("Character Details")]
    //[TabGroup("Character Stats")]
    //[TabGroup("Other")]
    //[TabGroup("Skills")]
    public string skillName;
    public Sprite skillIcon;
}

[System.Serializable]
public class CharacterStats
{
    [Range(0,20)]
    public int strength;
    [Range(0,20)]
    public int vitality;
    [Range(0,20)]
    public int intelligence;
    [Range(0,20)]
    public int dexeterity;
    [Range(0,20)]
    public int charisma;
}

[System.Serializable]
public class CharacterInfo
{
    public string characterName;
    public string characterStory;
    public Sprite charcterImage;
}

[System.Serializable]
public class InventoryObject
{
    public Sprite icon;
    public string itemName;
    public string itemDescription;
    public ItemStats itemStats;
}

[System.Serializable]
public class ItemStats
{
    public int cost;
    public int weight;
}




