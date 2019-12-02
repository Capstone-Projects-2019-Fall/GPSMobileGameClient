using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(Healthbar))]
public class MpButtonHandler : MonoBehaviour
{
    private Healthbar _myHealthBar;
    private Text _playerName;
    private string _nameString;

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
        get => _nameString;
        set => _nameString = value;
    }

    private void Awake()
    {
        _myHealthBar = gameObject.GetComponent<Healthbar>();
        _playerName = gameObject.transform.Find("playerName").GetComponent<Text>();
    }

    public void ChangeSomething()
    {
        _myHealthBar.updateHealthbar(_myHealthBar.CurrentFill - 0.1f);
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
