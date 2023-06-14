using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Event")]
public class Event : ScriptableObject
{
    public enum NegativeGain
    {
        noting,
        money,
        stone,
        wood,
        ice,
        leaf
    }

    public enum PositiveGain
    {
        noting,
        money,
        stone,
        wood,
        ice,
        leaf
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
    public Event.NegativeGain negativeGain;
    public Event.PositiveGain positiveGain;

    public int amount;
}