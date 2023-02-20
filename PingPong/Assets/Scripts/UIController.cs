using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Button> colorButtons;

    public void SelectColor(int index)
    { 
    
    }

    public void SetEnabledColors(List<int> enabledColors) 
    {
        for(int i = 0;i< colorButtons.Count;i++)
        {
            colorButtons[i].interactable = enabledColors.Contains(i);
        }
    }
}
