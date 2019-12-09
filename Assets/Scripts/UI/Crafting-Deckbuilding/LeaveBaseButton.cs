using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveBaseButton : MonoBehaviour
{
    private GameObject _uiCanvas;
    private CardScrollList _csList;
    private CardInventoryZone _ciZone;

    private Deck _csDeck;
    private Deck _ciDeck;


    private void Awake()
    {
        _uiCanvas = GameObject.Find("Canvas");
        _csList = _uiCanvas.transform.Find("CardScrollList").GetComponent<CardScrollList>();
        _ciZone = _uiCanvas.transform.Find("CardInventoryZone").GetComponent<CardInventoryZone>();

        _csDeck = _csList.MyCards;
        _ciDeck = _ciZone.MyCards;
    }



    public void LeaveBase()
    {
        SceneManager.LoadScene("GPSMobileGame");
        /*
        JSONArray finalJSON = new JSONArray();

        JSONArray myDeckJSON = _csDeck.JSONDeck(true);
        JSONArray myCollectionJSON = _ciDeck.JSONDeck(false);

        foreach(JSONObject jso in myDeckJSON)
        {

        }
        foreach(JSONObject jso in myCollectionJSON)
        {

        }*/
    }
}
