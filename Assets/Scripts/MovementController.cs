using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler

{
    public RectTransform gamePad;
    public float moveSpeed = 0.5f;


    GameObject arObject;
    GameObject childObj;
    Vector3 move;

    bool walking;

    void Start()
    {
        arObject = GameObject.Find("ContentParent");
        // arObject = GameObject.FindGameObjectWithTag("Spider");
         
    }

    public void OnDrag(PointerEventData eventData)
    {
        childObj = arObject.transform.GetChild(0).gameObject;
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)gamePad.position, gamePad.rect.width * 0.5f);

        move = new Vector3(transform.localPosition.x, 0f, transform.localPosition.y).normalized; // no movement in y

        if (!walking)
        {
            if (childObj.tag=="Player")
            {
                walking = true;
                childObj.GetComponent<Animator>().SetBool("Walk", true); // on drag start the walk animation
            }
         
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // do the movement when touched down
        StartCoroutine(PlayerMovement());


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        childObj = arObject.transform.GetChild(0).gameObject;
        transform.localPosition = Vector3.zero; // joystick returns to mean pos when not touched
        move = Vector3.zero;
        StopCoroutine(PlayerMovement());
        if (childObj.tag=="Player")
        {
            walking = false;
            childObj.GetComponent<Animator>().SetBool("Walk", false);
        }
       


    }

    IEnumerator PlayerMovement()
    {
        while (true)
        {
            arObject.transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            if (move != Vector3.zero)
                arObject.transform.rotation = Quaternion.Slerp
                    (arObject.transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 5.0f);

            yield return null;


        }
    }


}
