using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnTreasure : BaseObject
{
    [SerializeField] private List<GameObject> treasures = new List<GameObject>();

    public override void Initialzie()
    {
        foreach (var gettreasure in treasures)
        {
            gettreasure.gameObject.SetActive(true);
            gettreasure.GetComponent<Rigidbody>().AddForce(Physics.gravity * 10f, ForceMode.Acceleration);
        }

        base.Initialzie();
    }

    public void DoCollected(Vector3 endposition)
    {
        foreach (var gettreasure in treasures)
        {
            gettreasure.transform.DOMove(endposition, 1)
                .OnComplete((() => { gettreasure.gameObject.SetActive(false); }));
        }
    }
}