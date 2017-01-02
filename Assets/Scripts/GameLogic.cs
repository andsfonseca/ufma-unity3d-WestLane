using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static GameLogic Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = GameObject.FindObjectOfType<GameLogic>();
				DontDestroyOnLoad(m_instance.gameObject);
			}

			return m_instance;
		}
	}

	/// <summary>
	/// The m instance.
	/// </summary>
	private static GameLogic m_instance;
	void Awake()
	{
		if (m_instance == null)
		{
			m_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if (this != m_instance)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
