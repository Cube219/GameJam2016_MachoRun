using UnityEngine;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Map{

	int curLine = 0;

	List<List<int[,]>> maps = new List<List<int[,]>> ();


	public void Load(string mapname)
	{
		
		StreamReader sr = new StreamReader ("Assets/Resources/Texts/"+mapname+".macho");
		int mapHeight = int.Parse(sr.ReadLine ());
		sr.ReadLine ();

		int count = 0;

		for(int i=0;i<mapHeight;i++) {
			int mapWidth = int.Parse(sr.ReadLine ());

			sr.ReadLine ();
			maps.Add(new List<int[,]>());
			for (int j = 0; j < mapWidth; j++) {
				maps[i].Add(new int[9,16]);
				for (int m = 0; m < 9; m++) {
					string line = sr.ReadLine ();
					string[] spliters = line.Split (' ');
					for (int n = 0; n < 16; n++) {
						maps [i] [j] [m, n] = int.Parse(spliters [n])	;
					}
				}
				sr.ReadLine ();
			}
		}

		sr.Close ();
	}


}
