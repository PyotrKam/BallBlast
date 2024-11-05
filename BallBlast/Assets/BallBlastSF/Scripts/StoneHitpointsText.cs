using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructible))]
public class StoneHitpointsText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI hitpointText;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();

        destructible.ChangeHitPoints.AddListener(OnChangeHitpoint);
    }

    private void OnChangeHitpoint()
    {
        int hitPoints = destructible.GetHitPoints();
        
        if (hitPoints >= 1000)
        {
            hitpointText.text = hitPoints / 1000 + "K";
        }
        else
        {
            hitpointText.text = hitPoints.ToString();
        }
    }
}
