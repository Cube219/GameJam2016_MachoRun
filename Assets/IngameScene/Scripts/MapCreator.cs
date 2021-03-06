﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;

public class MapCreator:NetworkBehaviour {

	public static MapCreator m;

	public GameObject tile;
	public GameObject Gashi;
	public GameObject chulGoo;
	public GameObject Banana;

	public GameObject slow;

	public GameObject endZone;

	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;

	private List<List<int[,]>> maps = new List<List<int[,]>>();
	private GameObject map;

	void Awake()
	{
		m = this;
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		ClientScene.RegisterPrefab(tile);
		ClientScene.RegisterPrefab(Gashi);
		ClientScene.RegisterPrefab(chulGoo);
		ClientScene.RegisterPrefab(Banana);
		ClientScene.RegisterPrefab(endZone);
	}

	public void LoadMapData(string mapName)
	{
		if(!NetworkServer.active)
			return;

		StreamReader sr = new StreamReader("Assets/Resources/Texts/" + mapName + ".macho");
		int mapHeight = int.Parse(sr.ReadLine());
		sr.ReadLine();

		for(int i = 0; i < mapHeight; i++) {
			int mapWidth = int.Parse(sr.ReadLine());

			sr.ReadLine();
			maps.Add(new List<int[,]>());
			for(int j = 0; j < mapWidth; j++) {
				maps[i].Add(new int[9, 16]);
				for(int m = 0; m < 9; m++) {
					string line = sr.ReadLine();
					string[] spliters = line.Split(' ');
					for(int n = 0; n < 16; n++) {
						maps[i][j][m, n] = int.Parse(spliters[n]);
					}
				}
				sr.ReadLine();
			}
		}

		sr.Close();
	}

	public void CreateMap()
	{
		if(!NetworkServer.active)
			return;

		if(map != null) {
			Destroy(map);
			map = null;
		}

		map = new GameObject();
		map.name = "Map";
		map.transform.position = Vector2.zero;
		map.AddComponent<NetworkIdentity>();
		NetworkServer.Spawn(map);


		int screenIndex = 0;
		int rowNum = maps.Count;
		for(int row = 0; row < rowNum; row++) {
			int screenNum = maps[row].Count;
			for(int scr = 0; scr < screenNum; scr++) {

				for(int i = 0; i < 9; i++) {
					for(int j = 0; j < 16; j++) {
						int t = maps[row][scr][8 - i, j];

						GameObject o = null;
						switch(t) {
							case 0: // 타일
								o = (GameObject)Instantiate(tile);
								break;
							case 1: // 바나나
								o = (GameObject)Instantiate(Banana);
								break;
							case 2: // 철구
								o = (GameObject)Instantiate(chulGoo);
								break;
							case 3: // 가시
								o = (GameObject)Instantiate(Gashi);
								break;
							case 5: // 감속
									//	o = (GameObject)Instantiate(slow);
								break;
							case 7: // 엔드존
								o = (GameObject)Instantiate(endZone);
								break;

						}
						if(o != null) {
							o.transform.localScale = new Vector2(2f, 2f);

							o.transform.position = new Vector2(0.8f * j + 0.8f * 16 * screenIndex, 0.8f * i + 0.8f * 9 * row);
							o.transform.SetParent(map.transform);

							NetworkServer.Spawn(o);
						}
					}
					// 배경 생성
			//		int se
				}
				if(row % 2 == 0)
					screenIndex++;
				else
					screenIndex--;
			}
			if(row % 2 == 0)
				screenIndex--;
			else
				screenIndex++;
		}
	}
}