using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    List<RawImage> healthBarImages;

    public void LoseLife()
    {
        if (healthBarImages.Count > 0)
        {
            // Make a Life Disapear from UI
            healthBarImages[healthBarImages.Count - 1].gameObject.SetActive(false);

            // Delete the Image from the list
            healthBarImages.RemoveAt(healthBarImages.Count - 1);
        }
    }
}
