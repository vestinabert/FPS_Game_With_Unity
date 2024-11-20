using TMPro;
using UnityEngine;

public class ItemNameDisplay : MonoBehaviour
{
    [SerializeField] private float maxDistanceToItem;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private TMP_Text itemNameText;

    private void LateUpdate()
    {
        Ray _ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit _hit, maxDistanceToItem))
        {
            IItem _item = _hit.collider.GetComponent<IItem>();
            if (_item != null)
            {
                itemNameText.text = _item.GetName();
            }
            else
            {
                itemNameText.text = string.Empty;
            }
        }
        else
        {
            itemNameText.text = string.Empty;
        }
    }
}