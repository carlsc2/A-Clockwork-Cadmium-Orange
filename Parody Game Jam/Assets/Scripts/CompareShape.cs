using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CompareShape {

	static float getLen(List<Vector2> input){
		float len = 0.0f;
		for (int i = 0; i < input.Count - 1; i++) {
			len += Vector2.Distance (input [i], input [i + 1]);
		}
		return len;
	}

	static List<Vector2> genShape(List<Vector2> oldshape, float totaldist, int N){
		float ptdist = totaldist / (float)N;
		List<Vector2> newshape = new List<Vector2>();
		float dist2pt = 0.0f;
		//Debug.Log ("ptdist:" + ptdist);
		for(int i = 0; i < oldshape.Count - 1; i++){
			//Debug.Log ("pt " + i);
			Vector2 startpt = oldshape[i];
			Vector2 endpt = oldshape[i + 1];
			float len = Vector2.Distance (endpt, startpt);
			while(len > dist2pt){
				//Debug.Log ("len:" + len + "  dist2pt:" + dist2pt);
				Vector2 newpt = new Vector2(0.0f, 0.0f);
				newpt = startpt + ((endpt - startpt).normalized * dist2pt);
				newshape.Add (newpt);

				dist2pt = ptdist;
				startpt = newpt;
				len = Vector2.Distance (endpt, startpt);
			}

			dist2pt -= len;
		}

		if (newshape.Count != N) {
			Debug.Log ("incorrect size! newshape=" + newshape.Count + ", N=" + N);
			newshape.Add (oldshape [oldshape.Count - 1]);
		}

		return newshape;
	}

	static List<Vector2> Normalize(List<Vector2> input){
		//scale points to fit inside (-1,-1),(1,1) square, centered at 0,0
		float minx = input.Min (pt => pt.x);
		float miny = input.Min (pt => pt.y);
		float maxx = input.Max (pt => pt.x);
		float maxy = input.Max (pt => pt.y);
		Vector2 center = new Vector2 ((minx + maxx) / 2.0f, (miny + maxy) / 2.0f);
		float scale = Mathf.Max(maxx - minx, maxy - miny)/ 2.0f;
		for (int i = 0; i < input.Count; i++) {
			input [i] = (input [i] - center) / scale;
		}
		return input;
	}

	public static string Match(List<Vector2> input){
		if (input.Count > 1) {
			float len = getLen (input);

			//Debug.Log ("got len = " + len);

			List<Vector2> shape = genShape (input, len, 20);

			//Debug.Log ("got shape!");

			shape = Normalize (shape);

			//Debug.Log ("normalize");

			List<Vector2> mountain = new List<Vector2> ();
			float x = -1.0f;
			float y = -1.0f;
			for (int i = 0; i < 41; i++) {
				mountain.Add (new Vector2 (x, y));
				if (i < 20) {
					y += 0.1f;
				} else {
					y -= 0.1f;
				}
				x += 0.05f;
			}

			// x = cos(ang), y = sin(ang)  - ang equidistant from 0 to 2pi
			List<Vector2> cloud = new List<Vector2> ();
			for (int i = 0; i < 41; i++) {
				float tmp = ((float)i / 41.0f) * (2.0f * Mathf.PI);
				cloud.Add (new Vector2 (Mathf.Cos(tmp), Mathf.Sin(tmp)*0.5f));
			}


			List<Vector2> tree = new List<Vector2> ();
			for (int i = 0; i < 15; i++) {
				tree.Add (new Vector2 (0.0f, (float)i / -15.0f));
			}
			for (int i = 0; i < 26; i++) {
				float tmp = ((float)i / 26.0f) * (2.0f * Mathf.PI);
				tree.Add (new Vector2 (Mathf.Cos(tmp)*0.5f, Mathf.Sin(tmp)*0.5f + 0.5f));
			}

			Dictionary<string, float> templates = new Dictionary<string, float>();
			templates.Add ("mountain", leastSqErr (shape, mountain) + leastSqErr (mountain, shape));
			templates.Add ("cloud", leastSqErr (shape, cloud) + leastSqErr (cloud, shape));
			templates.Add ("tree", leastSqErr (shape, tree) + leastSqErr (tree, shape));

			string ret = templates.OrderBy (tmp => tmp.Value).First ().Key;

			return ret;
		}

		return "";
	}

	static void vec2print(List<Vector2> vec){
		string tmp = "";
		for(int i = 0; i < vec.Count; i++) {
			tmp = tmp + vec [i].ToString () + "/";
		}
		Debug.Log (tmp);
	}

	static float leastSqErr(List<Vector2> vec1, List<Vector2> vec2){
		float err = 0.0f;
		foreach (Vector2 i in vec1) {
			float mindist = 10.0f;
			foreach(Vector2 j in vec2){
				mindist = Mathf.Min(mindist, Mathf.Pow(Vector2.Distance (i, j), 2));
			}
			err += mindist;
		}
		return err;
	}
}
