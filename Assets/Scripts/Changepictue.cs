using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changepictue : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.yellow;

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.6f, 1.6f, 1.0f);
        SceneController controller = targetObject.GetComponent<SceneController>();

        controller.Field.State++;
        if (controller.Field.State == 3)
        {
            controller.Field.State = 0;
        }
        controller.Restart();
    }
    public void OnMouseUp()
    {
        transform.localScale = new Vector3(2.2f, 2.2f, 1.0f);
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}