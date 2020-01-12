using UnityEngine;
using Animate;

public class WrapperPlugin : MonoBehaviour
{
    MainAnimation object1;
    void Start()
    {
        //Camera Settings
        Camera mainCam = Camera.main;
        mainCam.transform.position = new Vector3(-200, 200, -1);
        mainCam.orthographicSize = 550;
      
        //Recreating the object from Texture Atlas 
        object1 = new MainAnimation();
        object1.Init();

        //Playing your animation
        /* If your animation doesnot have any frame level, put a blank string, it will play the whole animation.*/
        object1.PlayAnimation("");
        /* If there are frame levels in animation, mention it as object1.PlayAnimation("walk"); */
        //object1.PlayAnimation("walk");
    }
    void FixedUpdate()
    {
        object1.UpdateTimeline(Time.time);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            object1.PlayAnimation("jump");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            object1.PlayAnimation("fall");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            object1.PlayAnimation("walk");
            Vector3 position = object1.mainGameObject.gobj.transform.localPosition;
            position.x -=50;
            object1.mainGameObject.gobj.transform.localPosition = position;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            object1.PlayAnimation("idle");
            Vector3 position = object1.mainGameObject.gobj.transform.localPosition;
            position.x += 50;
            object1.mainGameObject.gobj.transform.localPosition = position;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {             
                Vector3 scale = object1.mainGameObject.gobj.transform.localScale;
                scale.x *= 2;
                scale.y *= 2;
                object1.mainGameObject.gobj.transform.localScale = scale;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 scale = object1.mainGameObject.gobj.transform.localScale;
            scale.x *= (float)0.5;
            scale.y *= (float)0.5;
            object1.mainGameObject.gobj.transform.localScale = scale;
        }
    }
}