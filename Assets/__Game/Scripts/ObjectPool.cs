using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ObjectPool : MonoBehaviour{
	public static ObjectPool Instance = default;
	
	[System.Serializable]
	public struct objstruct
	{
		public GameObject obj;
		public int amount;
		public int ID;
	}
	public objstruct[] objects;

	private Dictionary<int, Queue<GameObject>> _objectDictionary;
	
	private void Awake()
	{
		if (Instance) Destroy(gameObject);
		else Instance = this;

		EventManager.RegisterListener<ProjectileEventInfo>(GetObjectEvent);
		
		_objectDictionary = new Dictionary<int, Queue<GameObject>>(objects.Length);
		
		for (int i = 0; i < objects.Length; i++)
		{
			Queue<GameObject> objs = new Queue<GameObject>(objects[i].amount);
			
			for (int j = 0; j < objects[i].amount; j++)
			{
				var obj = Instantiate(objects[i].obj, transform);
				objs.Enqueue(obj);
				obj.SetActive(false);
			}
			
			_objectDictionary.Add(objects[i].ID, objs);
		}
	}

	private void OnDisable()
	{
		EventManager.UnregisterListener<ProjectileEventInfo>(GetObjectEvent);
	}

	private void GetObjectEvent(EventInfo ei)
	{
		var eventInfo = (ProjectileEventInfo) ei;
		GetObjectFromPool(eventInfo.ID, eventInfo.Parent.position, eventInfo.Parent.rotation);
	}
	
	public GameObject GetObjectFromPool(int ID, Vector3 position, Quaternion rotation)
	{
		if(Time.timeScale == 0) return null;

		if(!_objectDictionary.ContainsKey(ID))
		{
			return null;
		}

		var obj = _objectDictionary[ID].Dequeue();
		_objectDictionary[ID].Enqueue(obj);
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive(true);

		return obj;
	}
}