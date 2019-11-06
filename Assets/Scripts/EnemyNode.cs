using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : NodeStructure, IRadialArea
{
    private static string _type = "Enemy";
    private static Sprite _sprite = Resources.Load<Sprite>("Sprites/enemy-node-01");

    public override string Type 
    {
        get => _type;
    }
    public override Sprite Sprite 
    {
        get => _sprite;
    }

    private float _radius;
    private RadialArea _myRadialArea;

    public float Radius {
        get => _radius;
        set => _radius = value;
    }
    public RadialArea MyRadialArea {
        get => _myRadialArea;
        set => _myRadialArea = value;
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
        MyRadialArea.OnEnterArea += EnterAction;
    }
    public void UnsubscribeEnter()
    {
        MyRadialArea.OnEnterArea -= EnterAction;
    }

    // Event handling for player exiting the RadialArea.
    public void ExitAction()
    {
        // TODO: Behavior when player exits this RadialArea
    }
    public void SubscribeExit()
    {
        MyRadialArea.OnEnterArea += ExitAction;
    }
    public void UnsubscribeExit()
    {
        MyRadialArea.OnExitArea -= ExitAction;
    }
}
