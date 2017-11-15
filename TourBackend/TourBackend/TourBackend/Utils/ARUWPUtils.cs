/*
*  ARUWPUtils.cs
*  ARToolKitUWP-Unity
*
*  This file is a part of ARToolKitUWP-Unity.
*
*  ARToolKitUWP-Unity is free software: you can redistribute it and/or modify
*  it under the terms of the GNU Lesser General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  ARToolKitUWP-Unity is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU Lesser General Public License for more details.
*
*  You should have received a copy of the GNU Lesser General Public License
*  along with ARToolKitUWP-Unity.  If not, see <http://www.gnu.org/licenses/>.
*
*  Copyright 2017 Long Qian
*
*  Author: Long Qian
*  Contact: lqian8@jhu.edu
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Numerics;

/// <summary>
/// ARUWPUtils class provides utility functions for ARUWP package, including transformation
/// manipulation, FPS recording, logging.
/// </summary>
/// 
namespace TourBackend
{
    public static class ARUWPUtils
    {

        /// <summary>
        /// Class and object identifier for logging. [internal use]
        /// </summary>
        private static string TAG = "ARUWPUtils";

        /// <summary>
        /// The queue to record the timestamps of rendering, used to calculate render FPS. [internal use]
        /// </summary>
        private static Queue<long> qRenderTick = new Queue<long>();

        /// <summary>
        /// The queue to record the timestamps of video frames, used to calculate video FPS. [internal use]
        /// </summary>
        private static Queue<long> qVideoTick = new Queue<long>();

        /// <summary>
        /// The queue to record the timestamps of tracking performed, used to calculate tracking FPS. [internal use]
        /// </summary>
        private static Queue<long> qTrackTick = new Queue<long>();

        /// <summary>
        /// Record the current timestamp for rendering a frame. [internal use]
        /// </summary>
        public static void RenderTick()
        {
            while (qRenderTick.Count > 49)
            {
                qRenderTick.Dequeue();
            }
            qRenderTick.Enqueue(DateTime.Now.Ticks);
        }

        /// <summary>
        /// Get the average rendering period for the previous 50 occurrence. [public use]
        /// </summary>
        /// <returns>Average rendering period in millisecond</returns>
        public static float GetRenderDeltaTime()
        {
            if (qRenderTick.Count == 0)
            {
                return float.PositiveInfinity;
            }
            return (DateTime.Now.Ticks - qRenderTick.Peek()) / 500000.0f;
        }


        /// <summary>
        /// Record the current timestamp for video frame arrival. [internal use]
        /// </summary>
        public static void VideoTick()
        {
            while (qVideoTick.Count > 49)
            {
                qVideoTick.Dequeue();
            }
            qVideoTick.Enqueue(DateTime.Now.Ticks);
        }

        /// <summary>
        /// Get the average video frame period for the previous 50 occurrence. [public use]
        /// </summary>
        /// <returns>Average video frame period in millisecond</returns>
        public static float GetVideoDeltaTime()
        {
            if (qVideoTick.Count == 0)
            {
                return float.PositiveInfinity;
            }
            return (DateTime.Now.Ticks - qVideoTick.Peek()) / 500000.0f;
        }

        /// <summary>
        /// Record the current timestamp for tracking performed. [internal use]
        /// </summary>
        public static void TrackTick()
        {
            while (qTrackTick.Count > 49)
            {
                qTrackTick.Dequeue();
            }
            qTrackTick.Enqueue(DateTime.Now.Ticks);
        }

        /// <summary>
        /// Get the average tracking period for the previous 50 occurrence. [public use]
        /// </summary>
        /// <returns>Average tracking period in millisecond</returns>
        public static float GetTrackDeltaTime()
        {
            if (qTrackTick.Count == 0)
            {
                return float.PositiveInfinity;
            }
            return (DateTime.Now.Ticks - qTrackTick.Peek()) / 500000.0f;
        }


        /// <summary>
        /// Convert row major 3x4 matrix returned by ARToolKitUWP to Matrix4x4 used in Unity.
        /// Right-hand coordinates to left-hand coordinates conversion is performed.
        /// That is, the Y-axis is flipped. Unit is changed from millimeter to meter. [internal use]
        /// </summary>
        /// <param name="t">Flat float array with length 12, obtained from ARToolKitUWP</param>
        /// <returns>The Matrix4x4 object representing the transformation in Unity</returns>
        public static Matrix4x4 ConvertARUWPFloatArrayToMatrix4x4(float[] t)
        {
            Matrix4x4 m = new Matrix4x4();
            m.M11 = t[0];
            m.M12 = -t[1];
            m.M13 = t[2];
            m.M14 = t[3] / 1000.0f;
            m.M21 = -t[4];
            m.M22 = t[5];
            m.M23 = -t[6];
            m.M24 = -t[7] / 1000.0f;
            m.M31 = t[8];
            m.M32 = -t[9];
            m.M33 = t[10];
            m.M34 = t[11] / 1000.0f;
            m.M41 = 0;
            m.M42 = 0;
            m.M43 = 0;
            m.M44 = 1;
            return m;
        }

        /// <summary>
        /// Extract Vector3 representation of translation from Matrix4x4 object. [public use]
        /// </summary>
        /// <param name="m">Matrix4x4 object</param>
        /// <returns>Translation represented in Vector3</returns>
        public static Vector3 PositionFromMatrix(Matrix4x4 m)
        {
            return new Vector3(m.M14,m.M24,m.M34);
        }



        /// <summary>
        /// Extract Vector3 representation of scale from Matrix4x4 object. [public use]
        /// </summary>
        /// <param name="m">Matrix4x4 object</param>
        /// <returns>Scale represented in Vector3</returns>
        public static Vector3 ScaleFromMatrix(Matrix4x4 m)
        {
            var x = (float)Math.Sqrt(m.M11 * m.M11 + m.M12 * m.M12 + m.M13 * m.M13);
            var y = (float)Math.Sqrt(m.M21 * m.M21 + m.M22 * m.M22 + m.M23 * m.M23);
            var z = (float)Math.Sqrt(m.M31 * m.M31 + m.M32 * m.M32 + m.M33 * m.M33);

            return new Vector3(x, y, z);
        }


    }
}
