using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryClass : MonoBehaviour
{
    public List<GameObject> fields = new List<GameObject>();
    public Sprite emptySprite;
    public int tryNumber;
    public static TryClass instance;

    public void OnAwake()
    {
        instance = this;
        instance.emptySprite = this.emptySprite;
    }
}
