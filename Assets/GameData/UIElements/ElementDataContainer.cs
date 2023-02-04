using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementDataContainer : MonoBehaviour
{
    [SerializeField] Image _elementIcon;
    [SerializeField] Text _levelLabel;
    [SerializeField] Text _stepLabel;
    [SerializeField] Button _upgradeButton;
    [SerializeField] ElementProgressBar _progressBar;


    public void Init(ElementData data)
    {
        // Set icon from storage of icons

        _levelLabel.text = data.LevelNumber.ToString();
        _stepLabel.text = data.StepNumber.ToString();

        
    }
}