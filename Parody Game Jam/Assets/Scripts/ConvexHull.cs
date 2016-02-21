using UnityEngine;
using System.Collections.Generic;


public class ConvexHull {


	/// <summary>Computes the convex hull of a polygon, in clockwise order in a Y-up 
	/// coordinate system (counterclockwise in a Y-down coordinate system).</summary>
	/// <remarks>Uses the Monotone Chain algorithm, a.k.a. Andrew's Algorithm.</remarks>

	public static List<Vector2> ComputeConvexHull(List<Vector2> Vector2s, bool sortInPlace) {
		if (!sortInPlace)
			Vector2s = new List<Vector2>(Vector2s);
		Vector2s.Sort((a, b) => a.x == b.x ? a.y.CompareTo(b.y) : (a.x > b.x ? 1 : -1));

		// Importantly, DList provides O(1) insertion at beginning and end
		List<Vector2> hull = new List<Vector2>();
		int L = 0, U = 0; // size of lower and upper hulls

		// Builds a hull such that the output polygon starts at the leftmost Vector2.
		for (int i = Vector2s.Count - 1; i >= 0; i--) {
			Vector2 p = Vector2s[i], p1;

			// build lower hull (at end of output list)
			while (L >= 2 && Vector3.Cross(((p1 = hull[hull.Count-1]) - hull[hull.Count - 2]), p-p1).z >= 0) {
				hull.RemoveAt(hull.Count - 1);
				L--;
			}
			hull.Add(p);
			L++;

			// build upper hull (at beginning of output list)
			while (U >= 2 && Vector3.Cross(((p1 = hull[0]) - hull[1]),p-p1).z <= 0) {
				hull.RemoveAt(0);
				U--;
			}
			if (U != 0) // when U=0, share the Vector2 added above
				hull.Insert(0,p);
			U++;
			//Debug.Assert(U + L == hull.Count + 1);
		}
		hull.RemoveAt(hull.Count - 1);
		return hull;
	}

	public static bool ContainsPoint( List<Vector2> polyPoints, Vector2 p) { 
		var j = polyPoints.Count - 1;
		var inside = false; 
		for (int i = 0; i<polyPoints.Count; j = i++) {
			if ( ((polyPoints[i].y <= p.y && p.y<polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y<polyPoints[i].y)) && 
			 (p.x< (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x)) 
			 inside = !inside; 
	   } 
	   return inside; 
	}
}