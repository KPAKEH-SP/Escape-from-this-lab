using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviour
{
    public int pressedButtonsCount;
    public int usedLeverCount;

    [SerializeField] private int _buttonsCount;
    [SerializeField] private int _leverCount;

    public void CheckButtons()
    {
        if (_buttonsCount == pressedButtonsCount)
        {
            this.gameObject.SetActive(false);
        }

        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void CheckLevers()
    {
        if (_leverCount == usedLeverCount)
        {
            this.gameObject.SetActive(false);
        }

        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
