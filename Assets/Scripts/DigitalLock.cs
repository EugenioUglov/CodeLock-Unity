using System;
using UnityEngine;
using UnityEngine.Events;

public class DigitalLock : MonoBehaviour
{
    [SerializeField] private int _correctCode = 1111;
    [Header("Relations")]
    [SerializeField] private UnityEvent _onCorrectCodeInputed;
    [SerializeField] private UnityEvent _onWrongCodeInputed;
    [SerializeField] private DigitalLockView _digitalLockView;
    
    private string _stringUserCode;
    private bool _isWorking = true;


    private void OnEnable()
    {
        _digitalLockView.OnNumberButtonClickEvent += OnClickNumberButton;
    }
    
    private void OnDisable()
    {
        _digitalLockView.OnNumberButtonClickEvent -= OnClickNumberButton;
    }

    
    public void SetCorrectCode(int newCode)
    {
        _correctCode = newCode;
    }

    public void OnWrongCodeInputed()
    {
        _stringUserCode = "";
        _digitalLockView.SetOutputCodeString(_stringUserCode);
    }

    public void OnCorrectCodeInputed()
    {
        _isWorking = false;
    }
    
    
    private void OnClickNumberButton(int clickedNumber)
    {
        if (_isWorking == false) return;
        
        _stringUserCode += clickedNumber;
        int userCode = Int32.Parse(_stringUserCode);
        _digitalLockView.SetOutputCodeString(_stringUserCode);
        
        if (_stringUserCode.Length == _correctCode.ToString().Length)
        {
            if (userCode == _correctCode)
            {
                // On code is correct.
                _onCorrectCodeInputed?.Invoke();
            }
            else
            {
                _onWrongCodeInputed?.Invoke();
            }
        }
    }
}
