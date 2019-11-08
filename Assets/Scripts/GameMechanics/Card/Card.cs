using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Card : MonoBehaviour
{
    private int id;
    private string name;
    private string description;
    private int level;
    private int memoryCost;
    private int pp;
    private int levelupCost;
    private int upgradeId;
    private float attack;
    private float heal;
    private int drawCards;
    private Buff buff;
    

    public static readonly Dictionary<int,Card> masterCardList = new Dictionary<int, Card>{
        {0, new Card(id:0,name:"Strike 1",description:"Basic Strike.",level:1,memoryCost:2,pp:1,levelupCost:100,upgradeId:1,attack:3,heal:0,drawCards:0, buff:new Buff()) },
        {1, new Card(id:1,name:"Strike 2",description:"Intermediate Strike",level:2,memoryCost:4,pp:1,levelupCost:200,upgradeId:2,attack:6,heal:0,drawCards:0, buff:new Buff())},
        {2, new Card(id:2,name:"Strike 3",description:"Advanced Strike",level:3,memoryCost:8,pp:1,levelupCost:-1,upgradeId:-1,attack:12,heal:0,drawCards:0, buff:new Buff()) },

        {3, new Card(id:3,name:"Heal 1",description:"Basic Heal.",level:1,memoryCost:3,pp:1,levelupCost:100,upgradeId:4,attack:0,heal:4,drawCards:0, buff:new Buff()) },
        {4, new Card(id:4,name:"Heal 2",description:"Intermediate Heal.",level:2,memoryCost:6,pp:1,levelupCost:200,upgradeId:5,attack:0,heal:8,drawCards:0, buff:new Buff()) },
        {5, new Card(id:5,name:"Heal 3",description:"Advanced Heal.",level:3,memoryCost:12,pp:1,levelupCost:400,upgradeId:-1,attack:0,heal:16,drawCards:0, buff:new Buff()) },

        {6, new Card(id:6,name:"Draw Cards 1",description:"Basic Card Draw.",level:1,memoryCost:1,pp:1,levelupCost:100,upgradeId:6,attack:0,heal:0,drawCards:1, buff:new Buff()) },
        {7, new Card(id:7,name:"Draw Cards 2",description:"Intermediate Card Draw.",level:2,memoryCost:2,pp:1,levelupCost:200,upgradeId:7,attack:0,heal:0,drawCards:2, buff:new Buff()) },
        {8, new Card(id:8,name:"Draw Cards 3",description:"Advanced Card Draw.",level:3,memoryCost:4,pp:1,levelupCost:400,upgradeId:-1,attack:0,heal:0,drawCards:3, buff:new Buff()) },

        {9, new Card(id:9,name:"Attack Modifier 1",description:"Basic Attack Modifier.",level:1,memoryCost:2,pp:1,levelupCost:100,upgradeId:10,attack:0,heal:0,drawCards:0, buff:new Buff(name:"1.5x Damage",attackModifier:1.5f)) },
        {10, new Card(id:10,name:"Attack Modifier 2",description:"Intermediate Attack Modifier.",level:2,memoryCost:4,pp:1,levelupCost:200,upgradeId:11,attack:0,heal:0,drawCards:0, new Buff(name:"1.5x Damage",roundDuration:2,attackModifier:1.5f)) },
        {11, new Card(id:11,name:"Attack Modifier 3",description:"Advanced Attack Modifier.",level:3,memoryCost:6,pp:1,levelupCost:400,upgradeId:-1,attack:0,heal:0,drawCards:0, buff:new Buff(name:"3x Damage",attackModifier:3f)) },

        {12, new Card(id:12,name:"Defense Modifier 1",description:"Basic Defense Modifier.",level:1,memoryCost:4,pp:1,levelupCost:100,upgradeId:13,attack:0,heal:0,drawCards:0, buff:new Buff(name:"1.5x Damage",defenseModifier:1.5f)) },
        {13, new Card(id:13,name:"Defense Modifier 2",description:"Intermediate Defense Modifier.",level:2,memoryCost:8,pp:1,levelupCost:200,upgradeId:14,attack:0,heal:0,drawCards:0, new Buff(name:"1.5x Damage",roundDuration:2,defenseModifier:1.5f)) },
        {14, new Card(id:14,name:"Defense Modifier 3",description:"Advanced Defense Modifier.",level:3,memoryCost:16,pp:1,levelupCost:400,upgradeId:-1,attack:0,heal:0,drawCards:0, buff:new Buff(name:"2x Damage",defenseModifier:2f)) }
    };

    private Card(int id, string name, string description, int level, int memoryCost, int pp, int levelupCost, int upgradeId, float attack, float heal, int drawCards, Buff buff)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.level = level;
        this.memoryCost = memoryCost;
        this.pp = pp;
        this.levelupCost = levelupCost;
        this.upgradeId = upgradeId;
        this.attack = attack;
        this.heal = heal;
        this.drawCards = drawCards;
        this.buff = buff;
    }

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int Level { get => level; set => level = value; }
    public int MemoryCost { get => memoryCost; set => memoryCost = value; }
    public int PP { get => pp; set => pp = value; }
    public int LevelupCost { get => levelupCost; set => levelupCost = value; }
    public int UpgradeId { get => upgradeId; set => upgradeId = value; }
    private float Attack { get => attack; set => attack = value; }
    private float Heal { get => heal; set => heal = value; }
    public int DrawCards { get => drawCards; set => drawCards = value; }
    public Buff Buff { get => buff; set => buff = value; }

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
        }
    }*/
}
