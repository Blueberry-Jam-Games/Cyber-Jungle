using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject leftWalk;
    public GameObject leftRun;
    public GameObject rightWalk;
    public GameObject rightRun;

    public GameObject jump;
    public GameObject grapple;

    private Slider leftWalkSlide;
    private Slider leftRunSlide;
    private Slider rightWalkSlide;
    private Slider rightRunSlide;

    private Slider jumpSlide;
    private Slider grappleSlide;

    // Start is called before the first frame update
    void Start()
    {
        leftWalkSlide = leftWalk.GetComponent<Slider>();
        leftRunSlide = leftRun.GetComponent<Slider>();
        rightWalkSlide = rightWalk.GetComponent<Slider>();
        rightRunSlide = rightRun.GetComponent<Slider>();
        jumpSlide = jump.GetComponent<Slider>();
        grappleSlide = grapple.GetComponent<Slider>();

        SetRunMode(false);
    }

    public void Update()
    {
        SetRunMode(Input.GetKey(KeyCode.LeftShift));
    }

    public void NotifyJump()
    {
        jumpSlide.value -= 1;
    }

    public void NotifyMoveLeft(float distance, bool run)
    {
        if (run)
        {
            leftRunSlide.value -= distance;
        }
        else
        {
            leftWalkSlide.value -= distance;
        }
    }

    public void NotifyMoveRight(float distance, bool run)
    {
        if (run)
        {
            rightRunSlide.value -= distance;
        }
        else
        {
            rightWalkSlide.value -= distance;
        }
    }

    public void NotifyGrapple()
    {
        grappleSlide.value -= 1;
    }

    public void SetRunMode(bool enabled)
    {
        leftWalk.SetActive(!enabled);
        rightWalk.SetActive(!enabled);
        leftRun.SetActive(enabled);
        rightRun.SetActive(enabled);
    }

    public float GetWalksLeft()
    {
        return leftWalkSlide.value;
    }

    public float GetWalksRight()
    {
        return rightWalkSlide.value;
    }

    public float GetRunLeft()
    {
        return leftRunSlide.value;
    }

    public float GetRunRight()
    {
        return rightRunSlide.value;
    }

    public float GetJumps()
    {
        return jumpSlide.value;
    }

    public float GetGrapples()
    {
        return grappleSlide.value;
    }
}
