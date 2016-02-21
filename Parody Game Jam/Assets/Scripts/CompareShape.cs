using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CompareShape : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float getLen(List<Vector2> input){
		float len = 0.0f;
		for (int i = 0; i < input.Count - 1; i++) {
			len += Vector2.Distance (input [i], input [i + 1]);
		}
		len /= (float)input.Count;
		return len;
	}

	List<Vector2> genShape(List<Vector2> oldshape, float totaldist, int N){
		float ptdist = totaldist / (float)N;
		List<Vector2> newshape = new List<Vector2>();
		float dist2pt = 0.0f;
		for(int i = 0; i < oldshape.Count - 1; i++){
			Vector2 startpt = oldshape[i];
			Vector2 endpt = oldshape[i + 1];
			float len = Vector2.Distance (endpt, startpt);
			while(len > dist2pt){
				Vector2 newpt = new Vector2(0.0f, 0.0f);
				newpt = startpt + (Vector2.Normalize (endpt - startpt) * dist2pt);
				newshape.Add (newpt);

				dist2pt = ptdist;
				startpt = newpt;
				len = Vector2.Distance (endpt, startpt);
			}

			dist2pt -= len;
		}

		if (newshape.Count != N) {
			print ("incorrect size! newshape=" + newshape.Count + ", N=" + N);
			newshape.Add (oldshape [oldshape.Count - 1]);
		}

		return newshape;
	}

	List<Vector2> Normalize(List<Vector2> input){
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

	string Match(List<Vector2> input){
		len = getLen (input);

		List<Vector2> shape = genShape (input, len, 20);

		shape = Normalize (shape);

	}
}
