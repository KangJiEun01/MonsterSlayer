
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MinimapSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public MinimapCamera minimapCamera;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(90, transform.parent.eulerAngles.y, 0);
        transform.position = new Vector3(transform.parent.position.x, 0, transform.parent.position.z);

        if(spriteRenderer.isVisible == false)
        {
            minimapCamera.ShowBorderIndicator(transform.position);
        }
        else
        {
            minimapCamera.HideBorderIncitator();
        }
    }
}