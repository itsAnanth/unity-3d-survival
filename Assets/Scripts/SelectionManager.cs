using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
        Debug.Log(interaction_text.text);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selectionTransform = hit.transform;


            InteractableObject component = selectionTransform.GetComponent<InteractableObject>();
            if (component != null)
            {
                float distance = Vector3.Distance(Camera.main.transform.position, hit.point);

                if (distance > 3.0)
                    return;

                interaction_text.text = component.GetItemName();
                interaction_Info_UI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    component.HandleClick();
                }
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }

        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }
}