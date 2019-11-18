using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(menuName ="Card")]
public abstract class Card : MonoBehaviour, ICardInterface
{
    abstract public int Id { get; }
    abstract public string Name { get; }
    abstract public string Detail { get; }
    abstract public string Flavor { get; }
    abstract public int Level { get; }
    abstract public int MemoryCost { get; }
    //abstract public Sprite CardSprite { get; }
    
    public virtual void playCard(Player p, Enemy e) { }
    
    // Initializes this card

    public Card(){

    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
