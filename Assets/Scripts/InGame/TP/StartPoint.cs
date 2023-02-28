using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Player_Abilities player;

    [SerializeField] private Quaternion facingDirection;

    public string uuid; //Universal Unique Identifier

    void Start()
    {
        player = FindObjectOfType<Player_Abilities>();

        if (!player.nextUuid.Equals(uuid))
        {
            return;
        }
        player.transform.position = transform.position;

        player.transform.rotation = facingDirection;
    }
}
