using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Assertions;
using System;

[RequireComponent(typeof(Healthbar))]
public class MpButtonHandler : MonoBehaviour
{
    private Healthbar _myHealthBar;
    private Text _playerName;
    private bool _isSelected;

    public event EventHandler<SelectionEventArgs> SelectionEvent;

    [SerializeField] private GameObject _buttonPF;
    [SerializeField] private Transform _gridLayout;

    public Healthbar Healthbar {
        get => _myHealthBar;
    }

    public Text PlayerName {
        get => _playerName;
        set => _playerName = value;
    }

    public string NameString {
        get => _playerName.text;
        set => _playerName.text = value;
    }

    public string CleanedNameString{
        get => _playerName.text.Replace("*","");
    }

    public bool IsSelected{
        get  => _isSelected;
        set => _isSelected = value;
    }

    public void OnSelectionEvent(SelectionEventArgs e)
    {
        SelectionEvent?.Invoke(this, e);
    }

    private void Awake()
    {
        _myHealthBar = gameObject.GetComponent<Healthbar>();
        _playerName = gameObject.transform.Find("pname").GetComponent<Text>();

        Assert.IsNotNull(_playerName);
    }

    public void SetSelection(bool isSelected)
    {
        if(!(_isSelected && isSelected))
        {
            _isSelected = isSelected;
            UpdateButtonState();
            if(isSelected)
            {
                SelectionEventArgs args = new SelectionEventArgs { SelectedPlayerName = CleanedNameString };
                OnSelectionEvent(args);
            }
        }
    }

    public void UpdateButtonState()
    {
        if(_isSelected)
        {
            _playerName.text = string.Format("**{0}**", _playerName.text);
        }
        else
        {
            _playerName.text = CleanedNameString;
        }
    }

    public void MakeSomething()
    {
        GameObject newButton = MonoBehaviour.Instantiate(_buttonPF);
        newButton.transform.SetParent(_gridLayout);
        newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void SaySomething()
    {
        Debug.Log("Clicked MP HP Button!");
    }
}
