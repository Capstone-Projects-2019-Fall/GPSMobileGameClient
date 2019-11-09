﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNode : NodeStructure, IRadialArea
{
    private static string _type = "Friendly";
    private static Sprite _sprite = Resources.Load<Sprite>("Sprites/friendly-node-01");

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
    public FriendlyNode()
    {
        /*GameObject _raObject = new GameObject("RadialArea");
        _raObject.AddComponent<RadialArea>();
        RadialArea = _raObject.GetComponent<RadialArea>();*/
    }

    public override void AttachToNode(GameObject node)
    {
        try
        {
            GameObject radialArea = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/RadialArea"));
            radialArea.transform.SetParent(node.transform, true);
            radialArea.GetComponent<RadialArea>().DrawAreaOfEffect();
        }
        catch (Exception e) { Debug.Log(e); }
    }

    public override void nAttachToNode(GameObject node)
    {
        // Obtain reference to the Node's sprite child and set it to the proper sprite
        GameObject nodeSprite = node.transform.Find("NodeSprite").gameObject;
        SpriteRenderer nsRenderer = nodeSprite.GetComponent<SpriteRenderer>();
        nsRenderer.sprite = Sprite;
        nodeSprite.transform.localPosition = Vector3.zero;
        nodeSprite.transform.localScale = Vector3.one;

        // Attach a RadialArea to the Node
        GameObject radialArea = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/RadialArea"));
        radialArea.transform.SetParent(node.transform, true);
        radialArea.GetComponent<RadialArea>().DrawAreaOfEffect();

    }

    public void UpdateAction()
    {
        // TODO: Behavior that is called while the player is within the RadialArea
    }

    // Event handling for player entering the RadialArea.
    public void OnEnterAction(object sender, EventArgs e)
    {
        // TODO: Behavior when player enters this RadialArea
        Debug.Log("FriendlyNode.OnEnterAction!");
    }
    public void SubscribeEnter()
    {
        RadialArea.EnteredArea += OnEnterAction;
    }
    public void UnsubscribeEnter()
    {
        RadialArea.EnteredArea -= OnEnterAction;
    }

    // Event handling for player exiting the RadialArea.
    public void OnExitAction(object sender, EventArgs e)
    {
        // TODO: Behavior when player exits this RadialArea
        Debug.Log("FriendlyNode.OnExitAction!");
    }
    public void SubscribeExit()
    {
        RadialArea.EnteredArea += OnExitAction;
    }
    public void UnsubscribeExit()
    {
        RadialArea.ExitedArea -= OnExitAction;
    }
}