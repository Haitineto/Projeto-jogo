using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character: MonoBehaviour
{
    public List<IStatus> AppliedStatus = new List<IStatus>();

    public bool Paralyzed;    

    public Health Health { get; set; }

    public AttributePoints AttributePoints { get; set; }

    public Character Target { get; set; }

    public Vector3 OriginPosition { get; set; }

    public void Initialize(int healthAmount, AttributePoints attributePoints, Character target)
    {
        OriginPosition = transform.position;
        Health.Initialize(healthAmount);
        AttributePoints = attributePoints;
        Target = target;    
    }    

    public void Awake()
    {
        Health = GetComponent<Health>();
    }

    public void ClearStatus()
    {
        foreach (var status in AppliedStatus)
        {
            status.DoDestroy();
        }

        AppliedStatus.Clear();
    }

}
