using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyNode : NodeStructure, IRadialArea
{
    private static string _type = "Enemy";
    private static Sprite _sprite = Resources.Load<Sprite>("Sprites/enemy-node-01");

    private float _radius;
    private RadialArea _radialArea;

    public override string Type 
    {
        get => _type;
    }
    public override Sprite Sprite 
    {
        get => _sprite;
    }

    public float Radius {
        get => _radius;
        set => _radius = value;
    }
    public RadialArea RadialArea {
        get => _radialArea;
        set => _radialArea = value;
    }

    // Constructor (called BEFORE attached to a Node)
    public EnemyNode()
    {
        GameObject _raObject = new GameObject("RadialArea");
        _raObject.AddComponent<RadialArea>();
        RadialArea = _raObject.GetComponent<RadialArea>();
    }

    // Called by the NodeFactory when binding NodeStruct to Node
    public override void AttachToNode(GameObject node)
    {
        
    }

    public void UpdateAction()
    {
        // TODO: Behavior that is called while the player is within the RadialArea

    }

    // Event handling for player entering the RadialArea.
    public void EnterAction()
    {
        // TODO: Behavior when player enters this RadialArea
    }
    public void SubscribeEnter()
    {
        RadialArea.OnEnterArea += EnterAction;
    }
    public void UnsubscribeEnter()
    {
        RadialArea.OnEnterArea -= EnterAction;
    }

    // Event handling for player exiting the RadialArea.
    public void ExitAction()
    {
        // TODO: Behavior when player exits this RadialArea
    }
    public void SubscribeExit()
    {
        RadialArea.OnEnterArea += ExitAction;
    }
    public void UnsubscribeExit()
    {
        RadialArea.OnExitArea -= ExitAction;
    }
}
