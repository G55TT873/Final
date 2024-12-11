using System.Collections;
using UnityEngine;
public class CollectibleManager : MonoBehaviour
{

    public Collectible collectible;
    private int value;

    void Start()
    {
        value = collectible.value;
    }

}