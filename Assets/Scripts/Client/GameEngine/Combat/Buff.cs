using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private string buffName = "";
    private bool debuff = false;

    public string BuffName { get { return buffName; } set { buffName = value; } }
    public bool IsDebuff { get { return debuff; } set { debuff = value; } }

    public Buff()
    {
    }

    public Buff(string buff, bool isdebuff)
    {
        IsDebuff = isdebuff;
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
