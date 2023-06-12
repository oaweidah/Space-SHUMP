using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	static public Hero     S;
	
	[Header("Set in Inspector")]
	public float speed = 30;
	public float rollMult = -45;
	public float pitchMult = 30;
	public float gameRestartDelay = 2f;
	public float shieldLevel = 1;
	public GameObject projectilePrefab;
	public float projectileSpeed = 40;
	private float _shieldLevel = 1;
	
	[Header("Set Dynamically")]
	private GameObject lastTriggerGo = null;
	
	void Awake() {
		if (S == null) {
			S = this;
		} else {
			Debug.LogError("Hero.Awake() - Attemted to assign second Hero.S!");
		}
	}

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");
		
		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;
		
		transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			TempFire();
		}
    }
	
	void TempFire() {
		GameObject projGo = Instantiate<GameObject> (projectilePrefab);
		projGo.transform.position = transform.position;
		Rigidbody rigidB = projGo.GetComponent<Rigidbody>();
		rigidB.velocity = Vector3.up * projectileSpeed;
	}
	
	
	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
