using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;


[CreateAssetMenu(menuName ="Card")]
public abstract class Card : MonoBehaviour
{
    // abstract fields; to be overriden by individual card types
    abstract public int Id { get; }
    abstract public string Name { get; }
    abstract public string Detail { get; }
    abstract public string Flavor { get; }
    abstract public int Level { get; }
    abstract public int MemoryCost { get; }
    //abstract public Sprite CardSprite { get; }

    // shared fields; common among all cards
    static public CombatController _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();
    static public DeckManager _dm = GameObject.Find("CombatUtils").GetComponent<DeckManager>();

    public virtual void PlayCard (Player p, Enemy e) { }

    // Initializes this card

    private void Awake()
    {
        Assert.IsNotNull(_cc);
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
