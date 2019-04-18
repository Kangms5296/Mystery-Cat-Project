using UnityEngine;
using System.Collections;


public class DestinationReaction : DelayedReaction
{
	public Transform destination;
	private Transform player;
    public Transform character;

	protected override void SpecificInit()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	protected override void ImmediateReaction()
	{
        // 호환성위해..
        if(character != null)
        {
            character.position = new Vector3(destination.position.x, destination.position.y, destination.position.y);
            return;
        }

        player.position = new Vector3(destination.position.x, destination.position.y, destination.position.y);
    }
}