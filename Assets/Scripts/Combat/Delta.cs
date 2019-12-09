using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public static class Delta
{
    private static JSONObject deltaJSON = new JSONObject();
    private static JSONObject deltaJSONResponse = new JSONObject();
    private static string damageKey = "damage";
    private static string playerHealthKey = "playerHealth";
    private static string healingKey = "healing";
    private static string drawCardsKey = "drawCards";
    private static string buffKey = "buff";
    private static string targetObjectClientKey = "client";
    private static string targetObjectDataKey = "data";
    private static string enemyAttackKey = "attack";

    public static void SetDeltaResponse(JSONObject response)
    {
        deltaJSONResponse = response;
    }
    
    public static string toString()
    {
        return deltaJSON.ToString();
    }

    public static void Reset()
    {
        deltaJSON = new JSONObject();
        deltaJSONResponse = new JSONObject();
        deltaJSON[damageKey] = 0;
        deltaJSON[playerHealthKey] = 0;
        deltaJSON[healingKey] = new JSONArray();
        deltaJSON[drawCardsKey] = new JSONArray();
        deltaJSON[buffKey] = new JSONArray();

    }
    public static void AddDamage(float additionalDamage)
    {
        deltaJSON[damageKey] = deltaJSON.GetValueOrDefault(damageKey,0) + additionalDamage;        
    }

    public static void SetPlayerHealth(float playerHealth)
    {
        deltaJSON[playerHealthKey] = playerHealth;        
    }

    public static void DrawCardsTarget(string client, int numCards)
    {
        deltaJSON[drawCardsKey].AsArray.Add(createTargetJSONObject(client, numCards));
    }

    public static void HealTarget(string client, float health)
    {        
        deltaJSON[healingKey].AsArray.Add(createTargetJSONObject(client, health));
    }

    public static void BuffTarget(string client, Buff buff)
    {
        // Debug.LogFormat("Buff as JSON: {0}", JsonUtility.ToJson(buff));
        deltaJSON[buffKey].AsArray.Add(createTargetJSONObject(client, JSONNode.Parse(JsonUtility.ToJson(buff))));
    }

    private static JSONObject createTargetJSONObject(string client, JSONNode data)
    {
        JSONObject target = new JSONObject();
        target[targetObjectClientKey] = client;
        target[targetObjectDataKey] = data;
        return target;
    }

    public static string GetEnemyAttack()
    {
        return deltaJSONResponse[enemyAttackKey];
    }

    public static float GetMyHealing(string name)
    {
        float healing = 0;
        for(int i = 0; i < deltaJSONResponse[healingKey].Count; i++)
        {
            if(deltaJSONResponse[healingKey][i][targetObjectClientKey] == name)
            {
                healing += deltaJSONResponse[healingKey][i][targetObjectDataKey];
            }
        }
        // Debug.LogFormat("Healing {0}: {1}", name, healing);
        return healing;
    }

    public static int GetMyDrawCards(string name)
    {
        int numCards = 0;
        for(int i = 0; i < deltaJSONResponse[drawCardsKey].Count; i++)
        {
            if(deltaJSONResponse[drawCardsKey][i][targetObjectClientKey] == name)
            {
                numCards += deltaJSONResponse[drawCardsKey][i][targetObjectDataKey];
            }
        }
        // Debug.LogFormat("DrawCards {0}: {1}", name, numCards);
        return numCards;
    }

    public static List<Buff> GetEntityBuffs(string name)
    {
        List<Buff> buffs = new List<Buff>();
        for(int i = 0; i < deltaJSONResponse[buffKey].Count; i++)
        {
            if(deltaJSONResponse[buffKey][i][targetObjectClientKey] == name)
            {
                JSONObject buffAsJSON = deltaJSONResponse[buffKey][i][targetObjectDataKey].AsObject;
                buffs.Add(new Buff(name: buffAsJSON["name"], roundDuration: buffAsJSON["roundDuration"], attackModifier:buffAsJSON["attackModifier"], defenseModifier: buffAsJSON["defenseModifier"]));
                // Debug.LogFormat("Received buff: {0}", buffs[buffs.Count - 1].Name);
            }
        }
        return buffs;
    }
}
