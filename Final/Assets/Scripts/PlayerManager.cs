using System.Collections;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{

    public Player player;
    private string playerName;
    private Sprite skin;
    private int speed;
    private int price;

    void Start()
    {
        playerName = player.playerName;
        skin = player.skin;
        speed = player.speed;
        price = player.price;

    }

}