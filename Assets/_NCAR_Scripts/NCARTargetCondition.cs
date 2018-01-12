using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	[System.Serializable]
public static class ARTargetCondition
{
	public static bool IsPerpTo (NCARTrackableEventHandler wall, NCARTrackableEventHandler floor, float angleThreshold = 0, float distThreshold = 0)
	{
		Vector3 vFloorBotEdge = floor.wBottomRight - floor.wBottomLeft;
		Vector3 vWallBotEdge = wall.wBottomLeft - wall.wBottomRight;
		float angBottom = Vector3.Angle (vFloorBotEdge, vWallBotEdge);

		Vector3 vFloorTopEdge = floor.wTopRight - floor.wTopLeft;
		Vector3 vWallTopEdge = wall.wTopLeft - wall.wTopRight;
		float angTop = Vector3.Angle (vFloorTopEdge, vWallTopEdge);


		Vector3 diffBotLeft = floor.wBottomLeft - wall.wBottomRight;
		Vector3 diffTopLeft = floor.wTopLeft - wall.wTopRight;

		return (
			floor.IsBeingTracked && wall.IsBeingTracked
			&& angBottom < (90 + (angleThreshold))
			&& angBottom > (90 - (angleThreshold))
			&& angTop < (90 + (angleThreshold))
			&& angTop > (90 - (angleThreshold))
			&& diffBotLeft.magnitude < distThreshold
			&& diffTopLeft.magnitude < distThreshold 
		);     

	}

	public static bool IsLeftTo (NCARTrackableEventHandler leftTarget, NCARTrackableEventHandler rightTarget, float angleThreshold = 0, float distThreshold = 0)
	{
		Vector3 vBottom1 = leftTarget.wBottomLeft - leftTarget.wBottomRight;
		Vector3 vBottom2 = rightTarget.wBottomLeft - rightTarget.wBottomRight;
		float angBottom = Vector3.Angle(vBottom1, vBottom2);

		Vector3 vTop1 = leftTarget.wTopLeft - leftTarget.wTopRight;
		Vector3 vTop2 = rightTarget.wTopLeft - rightTarget.wTopRight;
		float angTop = Vector3.Angle(vTop1, vTop2);

		Vector3 diffBot = leftTarget.wBottomRight - rightTarget.wBottomLeft;
		Vector3 diffTop = leftTarget.wTopRight - rightTarget.wTopLeft;

		return ( rightTarget.IsBeingTracked && rightTarget.IsBeingTracked
			&& angBottom < angleThreshold
			&& angTop < angleThreshold
			&& diffBot.magnitude < distThreshold
			&& diffTop.magnitude < distThreshold)
			;
	}

	public static bool isOnTopOf(NCARTrackableEventHandler topTarget, NCARTrackableEventHandler botTarget, float angleThreshold = 0, float distThreshold = 0)
	{
		Vector3 vLeft1 = topTarget.wBottomLeft - topTarget.wTopLeft;
		Vector3 vLeft2 = botTarget.wBottomLeft - botTarget.wTopLeft;
		float angLeft = Vector3.Angle(vLeft1, vLeft2);

		Vector3 vRight1 = topTarget.wBottomRight - topTarget.wTopRight;
		Vector3 vRight2 = botTarget.wBottomRight - botTarget.wTopRight;
		float angRight = Vector3.Angle(vRight1, vRight2);

		Vector3 diffLeft = topTarget.wBottomLeft - botTarget.wTopLeft;
		Vector3 diffRight = topTarget.wBottomRight - botTarget.wTopRight;
		//Debug.Log("--" + angBottom + "--" + angTop + "--" + diffTop.magnitude + "--" + diffBot.magnitude);
		return (topTarget.IsBeingTracked && botTarget.IsBeingTracked
			&& angLeft < angleThreshold 
			&& angRight < angleThreshold 
			&& diffLeft.magnitude < distThreshold 
			&& diffRight.magnitude < distThreshold );
	}


}