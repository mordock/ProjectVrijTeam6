using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Event")]
public class Event : ScriptableObject
{
    public enum NegativeGain
    {
        Yes,
        No
    }

    public enum PositiveGain
    {
        Yes,
        No
    }

    public enum PostiveDays
    {
        Yes,
        No
    }

    public enum NegativeEnclosure
    {
        Yes,
        No
    }

    public string eventName;
    [TextArea]
    public string eventText;

    public Option option1, option2;
}

[System.Serializable]
public struct Option
{
    public string optionText;
    [Header("Money/Materials")]
    public Event.NegativeGain negativeGain;
    public Event.PositiveGain positiveGain;

    public int amount;
    public BuildMaterial material;
    [Header("Days")]
    public Event.PostiveDays postiveDays;
    public int dayAmount;
}