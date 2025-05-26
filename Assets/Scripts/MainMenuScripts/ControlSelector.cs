using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSelector : MonoBehaviour
{
   public ControlMode controlMode;
   
   public void SelectControl()
    {
        GameManager.Instance.selectedControlMode = controlMode;
        Debug.Log("Selected control: " +  controlMode);
    }

}
