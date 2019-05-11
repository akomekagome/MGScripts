using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MG.Utils{
    
    public static class Vector3Extensions{
        
        public static Vector3 SetX(this Vector3 origin, float X)
        {
            return new Vector3(X, origin.y, origin.z);
        }

        public static Vector3 SetY(this Vector3 origin, float Y)
        {
            return new Vector3(origin.x, Y, origin.z);
        }

        public static Vector3 SetZ(this Vector3 origin, float Z)
        {
            return new Vector3(origin.x, origin.y, Z);
        }

        public static Vector3 AddSetX(this Vector3 origin, float X)
        {
            return new Vector3(origin.x + X, origin.y, origin.z);
        }

        public static Vector3 AddSetY(this Vector3 origin, float Y)
        {
            return new Vector3(origin.x, origin.y + Y, origin.z);
        }

        public static Vector3 AddSetZ(this Vector3 origin, float Z)
        {
            return new Vector3(origin.x, origin.y, origin.z + Z);
        }

        public static Quaternion SetX(this Quaternion origin, float X)
        {
            return new Quaternion(X, origin.y, origin.z, origin.w);
        }

        public static Quaternion SetY(this Quaternion origin, float Y)
        {
            return new Quaternion(origin.x, Y, origin.z, origin.w);
        }

        public static Quaternion SetZ(this Quaternion origin, float Z)
        {
            return new Quaternion(origin.x, origin.y, Z, origin.w);
        }
    }
}