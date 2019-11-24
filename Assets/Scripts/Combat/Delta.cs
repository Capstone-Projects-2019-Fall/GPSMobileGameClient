using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public static class Delta
{
    private static JSONObject deltaJSON = new JSONObject();
    private static string damageKey = "damage";
    private static string healingKey = "healing";
    private static string drawCardsKey = "drawCards";
    private static string targetObjectClientKey = "client";
    private static string targetObjectDataKey = "data";
    
    public static string toString()
    {
        return deltaJSON.ToString();
    }

    public static void Reset()
    {
        deltaJSON = new JSONObject();
    }
    public static void AddDamage(float additionalDamage)
    {
        deltaJSON[damageKey] = deltaJSON.GetValueOrDefault(damageKey,0) + additionalDamage;        
    }

    public static void HealTarget(string client, float health)
    {        
        deltaJSON[healingKey].AsArray.Add(createTargetJSONObject(client, health));
    }

    public static void DrawCardsTarget(string client, int numCards)
    {
        deltaJSON[drawCardsKey].AsArray.Add(createTargetJSONObject(client, numCards));
    }

    private static JSONObject createTargetJSONObject(string client, JSONNode data)
    {
        JSONObject target = new JSONObject();
        target[targetObjectClientKey] = client;
        target[targetObjectDataKey] = data;
        return target;
    }
}
