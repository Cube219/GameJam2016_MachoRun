using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapCreator : MonoBehaviour {

	public static MapCreator m;

	public GameObject tile;

	private List<List<int[,]>> maps = new List<List<int[,]>>();

	void Awake()
	{
		m = this;
	}

	public void LoadMapData(string mapName)
	{
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
		int screenIndex = 0;
		int rowNum = maps.Count;
		for(int row=0; row<rowNum; row++) {
			int screenNum = maps[row].Count;
			for(int scr=0; scr<screenNum; scr++) {
				
				for(int i=0; i<9; i++) {
					for(int j=0; j<16; j++) {
						int t = maps[row][scr][8-i, j];

						GameObject o = null;
						switch(t) {
							case 0: // 타일
								o = (GameObject)Instantiate(tile);
								break;
						}
						if(o != null) {
							o.transform.position = new Vector2(0.4f * j + 0.4f * 16 * screenIndex, 0.4f * i + 0.4f * 9 * row);
						}
					}
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
