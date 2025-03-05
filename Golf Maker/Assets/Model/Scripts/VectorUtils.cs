
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
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


public static class Vector3IntOperations{

    public static double Module(Vector3Int vector) => Math.Sqrt(vector.x*vector.x + vector.y*vector.y + vector.z*vector.z);


    public static Vector3Int[] InterpolateVectors(Vector3Int initial, Vector3Int final){

        
        Vector3Int diff = final - initial;
        int mod = (int) Module(diff);

        Vector3Int[] vectorList = new Vector3Int[mod+1];
        vectorList[0] = initial;
        vectorList[vectorList.Length-1] = final;

        Vector3 unitDiff = Vector3.Normalize(diff);
        
        Vector3Int addVector;
        for (int i = 1; i < mod; i++){
            addVector = initial + Vector3Int.RoundToInt(i*unitDiff);
            vectorList[i] = addVector;
        }

        return vectorList;
    }
}