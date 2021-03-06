﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFirstTry
{
    public static class Settings
    {
        public static int WorldSize = 50;

        public static int StartingEnemyNumber = 10;

        public static float TotalRoundTime = 10f;

        // valores aproximados das bordas do frustum relativo ao SuggestedCameraStartingPosition quando Y = 0

        //Enemy limits
        public static float MaxZPos = 180f;
        public static float MinZPos = -170f;
        public static float MaxXPos = 290f;
        public static float MinXPos = -290f;

        //Player limits
        public static float MinXPlayer = -260f;
        public static float MaxXPlayer = 260;
        public static float MinZPlayer = -155f;
        public static float MaxZPlayer = 150f;


        public static float PlayerRotationFactor = 0f;
        public static float EnemyRotationFactor = (float)Math.PI;

        public static Vector3 SuggestedPlayerStartingPosition = new Vector3(0, 0, 130);
        public static Vector3 SuggestedCameraStartingPosition = new Vector3(0, 400, 1);
        public static Vector3 SuggestedEnemyStartingPosition = new Vector3(0, 0, -170);

        public static float SuggestedPlayerMovementSpeedFactor = 0.1f;
        public static float SuggestedEnemyMovementSpeedFactor = 0.05f;
        

    }
}
