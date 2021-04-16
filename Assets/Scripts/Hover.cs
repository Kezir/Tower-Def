using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singletone<Hover>
{
    private SpriteRenderer spriteRend;

    private SpriteRenderer rangedSpriteRend;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRend = GetComponent<SpriteRenderer>();

        this.rangedSpriteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void Activate(Sprite sprite)
    {
        this.spriteRend.enabled = true;
        this.spriteRend.sprite = sprite;

        this.rangedSpriteRend.enabled = true;
    }

    public void Deactivate()
    {
        
        this.spriteRend.enabled = false;
        
        this.rangedSpriteRend.enabled = false;
        GameManager.Instance.ReleaseButton();
    }
}
