using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathCustom : Pathfinding.AIPath
{
	public int GetPathLength()
	{
		return path.vectorPath.Count;
	}

	public Vector3 GetPathPosition(int theIndex)
	{
		return path.vectorPath[theIndex];
	}
}


public class LinePath : MonoBehaviour
{

	public AIPathCustom playerAIPath;
	public LineRenderer myLineRend;


	void Start()
	{
		InvokeRepeating("ResetLine", 0, .5f);
	}


	public void ResetLine()
	{
		//print ("linePath resetLine");

		if (playerAIPath.hasPath)
		{
			//print (playerAIPath.path.vectorPath.Count);
			myLineRend.positionCount = playerAIPath.GetPathLength();

			for (int i = 0; i < playerAIPath.GetPathLength(); i++)
			{
				myLineRend.SetPosition(i, playerAIPath.GetPathPosition(i));
			}

		}
		else
		{
			Debug.Log("no path", gameObject);
		}
	}
}
