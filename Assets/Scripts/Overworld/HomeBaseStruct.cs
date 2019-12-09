using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeBaseStruct : NodeStructure
{
    private static string _type = "HomeBase";
    private static Sprite _sprite = Resources.Load<Sprite>("Sprites/house-icon");

    public override string Type => _type;

    public override Sprite Sprite => _sprite;

    public override void AttachToNode(GameObject node)
    {
        throw new System.NotImplementedException();
    }

    public override void nAttachToNode(GameObject node)
    {
        GameObject nodeSprite = node.transform.Find("NodeSprite").gameObject;
        SpriteRenderer nsRenderer = nodeSprite.GetComponent<SpriteRenderer>();
        nsRenderer.sprite = Sprite;
        nodeSprite.transform.localPosition = Vector3.zero;
        nodeSprite.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    public override void OnClicked(string nodeIdentifier)
    {
        Debug.Log("HomeBase OnClicked!");
        SceneManager.LoadScene("HomeBase");
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
