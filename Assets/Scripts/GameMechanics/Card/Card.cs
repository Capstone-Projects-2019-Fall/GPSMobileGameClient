using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;


[CreateAssetMenu(menuName ="Card")]
public abstract class Card : MonoBehaviour
{
    private static int _Id;
    private string _Name;
    private string _Detail;
    private string _Flavor;
    private int _Level;
    private int _MemoryCost;
    private double _UpgradeCost;

    // abstract fields; to be overriden by individual card types
    abstract public int Id { get;}
    abstract public string Name { get; }
    abstract public string Detail { get; }
    abstract public string Flavor { get; }
    abstract public int Level { get; }
    abstract public int MemoryCost { get; }
    abstract public double UpgradeCost { get; }
    abstract public Sprite CardArt { get; }
    abstract public Sprite CardBannerArt { get;  }

    // Plays a card
    public virtual void PlayCard (Player p, Enemy e) { }
    
    // Returns the id of the card this card is upgrading to
    public virtual void UpgradeCard() { }

    // Initializes this card
    private void Awake()
    {
        Assert.IsNotNull(_cc);
    }

    // String display of card. For debugging purposes.
    public string DisplayText()
    {
        return "ID: " + Id
            + "\nName: " + Name
            + "\nDetail:" + Detail
            + "\nFlavor: " + Flavor
            + "\nLevel: " + Level
            + "\nMemory Cost: " + MemoryCost
            + "\nUpgrade Cost: " + UpgradeCost;
    }

    /*
    public bool Card_pp(string CardName, bool isRefresh)
    {
        string json = "\"cardname\": " + CardName + ",\n\"isrefresh\": " + isRefresh.ToString();
        StartCoroutine(PostRequest("https://gps-mobile-game-server.herokuapp.com/user/deck", json));
    }

    IEnumerator PostRequest(string uri, string json)
    {
        var uwr = new UnityWebRequest(uri, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            pp = Int32.Parse(uwr.downloadHandler.text);
        }
    }*/
}
