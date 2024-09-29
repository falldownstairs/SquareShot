using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    private float activetime = 0.15f;
    private float timeActivated;
    private float alpha;
    private float alphaSet = 0.95f;
    private float alphamultiplier = 0.9f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;
    private Color color;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SR = GetComponent<SpriteRenderer>();
        playerSR = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        timeActivated = Time.time;
        SR.color = playerSR.color;
    }

    private void Update()
    {
        alpha *= alphamultiplier;
        SR.color = new Color(SR.color.r,SR.color.g,SR.color.b,alpha);

        if (Time.time >= timeActivated + activetime)
        {
            PlayerAfterImagePool.instance.AddToPool(gameObject);
        }

    }

}
