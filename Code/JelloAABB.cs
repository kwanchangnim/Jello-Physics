/*
Copyright (c) 2014 David Stier

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.


******Jello Physics was born out of Walabers JellyPhysics. As such, the JellyPhysics license has been include.
******The original JellyPhysics library may be downloaded at http://walaber.com/wordpress/?p=85.


Copyright (c) 2007 Walaber

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/



using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
		
/// <summary>
/// A class representing  a 2D axis-aligned bounding box, for collision detection, etc;
/// </summary>
[Serializable]
public class JelloAABB
{
    #region PUBLIC VARIABLES

	/// <summary>
	/// JelloAABB validity enum.
	/// </summary>
    public enum PointValidity { Invalid, Valid };

    /// <summary>
    /// The Minimum point of this JelloAABB.
    /// (lower left)
    /// </summary>
	public Vector2 Min;   

    /// <summary>
    /// The Maximum point of this JelloAABB.
    /// (upper right)
    /// </summary>
	public Vector2 Max;

    /// <summary>
    /// Property that indicated whether or not this JelloAABB is valid.
    /// </summary>
	public PointValidity Validity = PointValidity.Invalid;
	
    #endregion

	

    #region CONSTRUCTORS
	
    /// <summary>
    /// Basic constructor.  Creates a JelloAABB that is invalid (describes no space).
    /// </summary>
    public JelloAABB()
    {
		Min = Max = Vector2.zero;
        Validity = PointValidity.Invalid;
    }

    /// <summary>
    /// create a JelloAABB with the given min and max points.
    /// </summary>
    /// <param name="min">The JelloAABB.Min point (lower left).</param>
	/// <param name="max">The JelloAABB.max point(upper right).</param>
    public JelloAABB(ref Vector2 min, ref Vector2 max)
    {
        Min = min;
        Max = max;
        Validity = PointValidity.Valid;
    }
	
    #endregion

    #region HELPER FUNCTIONS
	
    /// <summary>
    /// Resets this JelloAABB to invalid and describing no space.
    /// </summary>
    public void clear()
    {
        Min.x = Max.x = Min.y = Max.y = 0;
        Validity = PointValidity.Invalid;
    }
	
	/// <summary>
	/// draws the JelloAABB.
	/// </summary>
	public void DebugDrawMe()
	{	
		Debug.DrawLine(Min, new Vector2(Min.x, Max.y));
		Debug.DrawLine(new Vector2(Min.x, Max.y), Max);
		Debug.DrawLine(Max, new Vector2(Max.x, Min.y));
		Debug.DrawLine(new Vector2(Max.x, Min.y), Min);
	}
	
    #endregion

    #region EXPANSION
	
	/// <summary>
	/// Expands the JelloAABB to include a given point.
	/// ref version.
	/// </summary>
	/// <param name="point">The point to base expansion off of.</param>
    public void expandToInclude(ref Vector2 point)
    {
        if (Validity == PointValidity.Valid)
        {
            if (point.x < Min.x) { Min.x = point.x; }
            else if (point.x > Max.x) { Max.x = point.x; }

            if (point.y < Min.y) { Min.y = point.y; }
            else if (point.y > Max.y) { Max.y = point.y; }
        }
        else
        {
            Min = Max = point;
            Validity = PointValidity.Valid;
        }
    }
	
	/// <summary>
	/// Expands the JelloAABB to include a given point.
	/// </summary>
	/// <param name="point">The point to base expansion off of.</param>
    public void expandToInclude(Vector2 point)
    {
       if (Validity == PointValidity.Valid)
        {
            if (point.x < Min.x) { Min.x = point.x; }
            else if (point.x > Max.x) { Max.x = point.x; }

            if (point.y < Min.y) { Min.y = point.y; }
            else if (point.y > Max.y) { Max.y = point.y; }
        }
        else
        {
            Min = Max = point;
            Validity = PointValidity.Valid;
        }
    }
	
    #endregion

    #region COLLISION / OVERLAP
	
	/// <summary>
	/// Determine whether a given point is within this JelloAABB.
	/// </summary>
	/// <param name="point">The point to test.</param>
	/// <returns>Whether the point lies inside the JelloAABB.</returns>
    public bool contains( ref Vector2 point)
    {
        if (Validity == PointValidity.Invalid) { return false; }

        return ((point.x >= Min.x) && (point.x <= Max.x) && (point.y >= Min.y) && (point.y <= Max.y));
    }
	
	/// <summary>
	/// Determine whether a given point is within this JelloAABB.
	/// </summary>
	/// <param name="point">The point to test.</param>
	/// <returns>Whether the point lies inside the JelloAABB.</returns>
    public bool contains(Vector2 point)
    {
        if (Validity == PointValidity.Invalid) { return false; }
		
        return ((point.x >= Min.x) && (point.x <= Max.x) && (point.y >= Min.y) && (point.y <= Max.y));
    }
	
	/// <summary>
	/// Determines whether or not a given JelloAABB overlaps this one.
	/// </summary>
	/// <param name="aabb">The JelloAABB to test against.</param>
	/// <returns>Whether the JelloAABBs overlap.</returns>
    public bool intersects(JelloAABB aabb)
    {
		return ((Min.y <= aabb.Max.y) && (Max.y >= aabb.Min.y) && (Min.x <= aabb.Max.x) && (Max.x >= aabb.Min.x));
    }
	
    #endregion

}
