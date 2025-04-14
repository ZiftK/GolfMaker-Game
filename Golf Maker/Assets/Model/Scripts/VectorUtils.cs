using System;
using Unity.Mathematics;
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

    public static Vector3Int[] InterpolateVectorsAsSquare(Vector3Int initial, Vector3Int final){

        int width = final.x-initial.x, height = final.y-initial.y, signWidth = Math.Sign(width), signHeight = Math.Sign(height);

        int size = 2 * (Math.Abs(width) + Math.Abs(height)) + 1; // Corrección del cálculo del tamaño

        if (size == 0){
            return new Vector3Int[] {initial};
        }

        Vector3Int[] vectorList = new Vector3Int[size];

        int index = 0;

        Vector3Int buttom = new Vector3Int(initial.x, final.y, 0);
        Vector3Int top = initial;

        for (int i = 0; i < Math.Abs(width); i++){
            vectorList[index++] = top + signWidth*i*Vector3Int.right;
            vectorList[index++] = buttom + signWidth*i*Vector3Int.right;
        }
        vectorList[index++] = top + signWidth*Math.Abs(width)*Vector3Int.right;

        Vector3Int left = initial;
        Vector3Int right = new Vector3Int(final.x, initial.y,0);

        for (int j = 1; j <= Math.Abs(height); j++){
            vectorList[index++] = left + signHeight*j*Vector3Int.up;
            vectorList[index++] = right + signHeight*j*Vector3Int.up;
        }


        return vectorList;
    }
}