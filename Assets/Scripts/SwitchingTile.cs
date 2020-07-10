using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Mode
{
    public int index;
    public Sprite sprite;
    public bool colliderEnabled;
}

public class SwitchingTile : MonoBehaviour
{
    public List<Mode> modes;
    public int currentMode;

    private SpriteRenderer sr;
    private Collider2D assetCollider;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        assetCollider = GetComponent<Collider2D>();
    }

    public void ChangeToIndex(int index)
    {
        if(index >= modes.Count)
        {
            index = 0;
        }
        currentMode = index;
        sr.sprite = modes[index].sprite;
        assetCollider.enabled = modes[index].colliderEnabled;
    }
}
