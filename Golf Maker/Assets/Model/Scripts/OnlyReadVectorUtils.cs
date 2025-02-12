
using UnityEngine;

public class OnlyReadVector3{
    public static void SetX(ref Vector3 onlyReadVector, float x){
        onlyReadVector = new Vector3(x, onlyReadVector.y, onlyReadVector.z);
    }

    public static void SetY(ref Vector3 onlyReadVector, float y){
        onlyReadVector = new Vector3(onlyReadVector.x, y, onlyReadVector.z);
    }

    public static void SetZ(ref Vector3 onlyReadVector, float z){
        onlyReadVector = new Vector3(onlyReadVector.x, onlyReadVector.y, z);
    }

    public static void SetXY(ref Vector3 onlyReadVector, Vector2 xy){
        onlyReadVector = new Vector3(xy.x, xy.y, onlyReadVector.z);
    }

    public static Vector3 GetNewUsingX(Vector3 onlyReadVector, float x){
        return new Vector3(x, onlyReadVector.y, onlyReadVector.z);
    }

    public static Vector3 GetNewUsingY(Vector3 onlyReadVector, float y){
        return new Vector3(onlyReadVector.x, y, onlyReadVector.z);
    }

    public static Vector3 GetNewUsingZ(Vector3 onlyReadVector, float z){
        return  new Vector3(onlyReadVector.x, onlyReadVector.y, z);
    }

    public static Vector3 GetNewUsingXY(Vector3 onlyReadVector, Vector2 xy){
        return new Vector3(xy.x, xy.y, onlyReadVector.z);
    }
}