using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Healthbar))]
public class MpButtonHandler : MonoBehaviour
{
    private Healthbar _myHealthBar;
    private Text _playerName;

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

    private void Awake()
    {
        _myHealthBar = gameObject.GetComponent<Healthbar>();
        _playerName = gameObject.transform.Find("pname").GetComponent<Text>();

        Assert.IsNotNull(_playerName);
    }

    public void ChangeSomething()
    {
        //_myHealthBar.updateHealthbar(_myHealthBar.CurrentFill - 0.1f);
        Debug.Log("ChangeSomething!");
        _playerName.text = "Clicked!";   
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
