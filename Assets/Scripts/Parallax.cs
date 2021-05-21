using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	class Objetos
	{
		public Transform transform;
		public bool enUso;
		public Objetos(Transform t) { transform = t; }
		public void Usar() { enUso = true; }
		public void Mover() { enUso = false; }
	}

	
	public GameObject Prefab;
	public int cantObjetos;
	public float velocidad;
	public float ratioDeSpawn;

	public float minY;
	public float maxY;
	public Vector3 spawnPos;
	public float despawnPos;

	float spawnTimer;
	Objetos[] objetos;

	void Awake()
	{
		Configure();
	}

	void Update()
	{
		Mover();
		spawnTimer += Time.deltaTime;
		if (spawnTimer > ratioDeSpawn)
		{
			Spawn();
			spawnTimer = 0;
		}
	}

	void Configure()
	{
		
	
		objetos = new Objetos[cantObjetos];
		for (int i = 0; i < objetos.Length; i++)
		{
			GameObject go = Instantiate(Prefab) as GameObject;
			Transform t = go.transform;
			t.SetParent(transform);
			t.position = Vector3.one * 1000;
			objetos[i] = new Objetos(t);
		}
		SpawnInicial();
	}

    void Spawn()
    {
        Transform t = ObtenerObjeto();
        if (t == null) return;
        Vector3 pos = Vector3.zero;
        pos.y = Random.Range(minY, maxY);
		pos.x = spawnPos.x; 
        t.position = pos;
    }

	void SpawnInicial()
	{
		Transform t = ObtenerObjeto();
		if (t == null) return;
		Vector3 pos = Vector3.zero;
		pos.y = Random.Range(minY, maxY);
		pos.x = -3.72f;
		t.position = pos;
		Spawn();
	}


	void Mover()
	{
		for (int i = 0; i < objetos.Length; i++)
		{
			objetos[i].transform.position += Vector3.left * velocidad * Time.deltaTime;
			RevisarObjeto(objetos[i]);
		}
	}

	void RevisarObjeto(Objetos arrayObjetos)
	{
		if (arrayObjetos.transform.position.x < despawnPos)
		{
			arrayObjetos.Mover();
			arrayObjetos.transform.position = Vector3.one * 1000;
		}
	}

	Transform ObtenerObjeto()
	{
		for (int i = 0; i < objetos.Length; i++)
		{
			if (!objetos[i].enUso)
			{
				objetos[i].Usar();
				return objetos[i].transform;
			}
		}
		return null;
	}
}
