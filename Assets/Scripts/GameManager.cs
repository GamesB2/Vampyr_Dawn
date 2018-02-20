using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_GameManager = null;
    private static GameObject m_PlayerReference = null;
	private static List<GameObject> m_EnemyAiList = null;

    public void Awake()
    {
        m_PlayerReference = GameObject.Find("Player");
		m_EnemyAiList = new List<GameObject> ();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("EnemyAi");
		foreach (GameObject enemy in enemies)
		{
			m_EnemyAiList.Add (enemy);
		}
    }

    static public GameManager GetInstance()
    {
        if (m_GameManager == null) m_GameManager = new GameManager();
        return m_GameManager;
    }

    static public GameObject GetPlayer()
    {
        return m_PlayerReference;
    }

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightControl))
		{
			foreach (GameObject enemy in m_EnemyAiList)
			{
				if (enemy == null)
				{
					m_EnemyAiList.Remove (enemy);
					m_EnemyAiList.TrimExcess ();
				}
				Health hp = enemy.GetComponent<Health>();
				if (hp != null)
				{
					hp.TakeDamage (10);
				}
			}
		}
	}
}
