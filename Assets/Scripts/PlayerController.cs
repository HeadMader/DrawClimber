using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Image Win;
	[SerializeField]
	private Image GameOver;
	[SerializeField]
	private Text Pause;
	[SerializeField]
	private float deadLine;
	[SerializeField]
	private Transform camTarget;
	[SerializeField]
	private StopWatch stopWatch;
	private float m_Timer;
	private Rigidbody rigidbody;
	public float fadeDuration = 1f;
	public float displayImageDuration = 1f;
	private bool isDead = false;
	private bool win = false;

	bool m_IsPlayerAtExit;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		Win.enabled = false;
		GameOver.enabled = false;
		Pause.enabled = false;
	}
	//Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!Pause.enabled)
			{
				Pause.enabled = true;
				Time.timeScale = 0;
			}
			else
			{
				Pause.enabled = false;
				Time.timeScale = 1;
			}
		}
		Win.CrossFadeAlpha(0, 0, true);
		GameOver.CrossFadeAlpha(0, 0, true);
		if (transform.position.y < deadLine)
		{
			isDead = true;
			EndLevel();
		}

		if (m_IsPlayerAtExit)
		{
			EndLevel();
		}
		camTarget.eulerAngles = new Vector3(0, 25, 0);

		Debug.Log(transform.localRotation.eulerAngles);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Finish")
		{
			m_IsPlayerAtExit = true;
		}
	}

	void EndLevel()
	{
		if (!isDead)
		{
			m_Timer += Time.deltaTime;
			float alpha = m_Timer / fadeDuration;
			Win.enabled = true;
			Win.CrossFadeAlpha(alpha, 0, true);
			stopWatch.timerActive = false;
		}
		else
		{
			m_Timer += Time.deltaTime;
			float alpha = m_Timer / fadeDuration;
			GameOver.enabled = true;
			GameOver.CrossFadeAlpha(alpha, 0, true);
			rigidbody.isKinematic = true;
			stopWatch.timerActive = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}

