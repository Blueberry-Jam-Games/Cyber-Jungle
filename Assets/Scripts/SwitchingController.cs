using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingController : MonoBehaviour
{
    public void SwitchAllTiles(int newIndex)
    {
        GameObject[] switchers = GameObject.FindGameObjectsWithTag("switcher");
        foreach(GameObject go in switchers)
        {
            if(go.GetComponent<SwitchingTile>() != null)
            {
                go.GetComponent<SwitchingTile>().ChangeToIndex(newIndex);
            }
        }
    }
}
