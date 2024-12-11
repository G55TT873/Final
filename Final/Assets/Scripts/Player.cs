using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public Sprite skin;
    public string playerName;
    public int speed;
    public int price;
}