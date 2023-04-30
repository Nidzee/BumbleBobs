using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UniversalButton : BasicButton
{

    public enum ButtonStyle
    {
        Green = 0,
        Blue = 1,
        Yellow = 3,
        Red = 4,
        Gray = 5,
        Custom = 100,
    }

    public enum LabelStyle
    {
        Title = 0,
        TitleBold = 1,
        TitleCoins = 3,
        TitleCrystals = 4,
        Custom = 100,
    }
    
    [SerializeField] ButtonStyle _buttonStyle;
    [SerializeField] LabelStyle _labelStyle;


    [SerializeField] Image _buttonImage;
    [SerializeField] TMP_Text _buttonLabel;
    [SerializeField] string _buttonText = "Button";


    public void SetButtonPrice(int price)
    {
        _buttonLabel.text = price.ToString();
    }

    public void SetLabel(string label)
    {
        _buttonLabel.text = label;
    }


}
