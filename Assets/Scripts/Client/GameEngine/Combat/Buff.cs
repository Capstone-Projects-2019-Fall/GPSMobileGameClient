using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private string buffName = "";
    private int roundDuration = 1;
    private float attack_modifier = 1f;
    private float defense_modifier = 1f;

    public string BuffName { get { return buffName; } set { buffName = value; } }
    public int RoundDuration { get { return roundDuration; } set { roundDuration = value; } }
    public float Attack_Modifier { get { return attack_modifier; } set { attack_modifier = value; } }
    public float Defense_Modifier { get { return defense_modifier; } set { defense_modifier = value; } }

    public Buff()
    {
    }

    public Buff(string buff)
    {
        BuffName = buff;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
