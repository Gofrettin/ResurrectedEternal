using ResurrectedEternal.Objects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal
{
    public static class EngineMath
    {
        public static double GetRandomNumber(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static double RandomDouble(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (mantissa * exponent);
        }
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        public static bool AlmostEquals(this float double1, float double2, float precision)
        {
            return Math.Abs(double1 - double2) <= precision;
        }

        public static float GetDistanceToPoint(Vector3 pointA, Vector3 pointB)
        {
            return (float)Math.Abs((pointA - pointB).Length());
        }
        public static float GetDistanceToPoint(Vector2 pointA, Vector2 pointB)
        {
            return (float)Math.Abs((pointA - pointB).Length());
        }
        public static float DistanceToOtherEntityInMetres(Vector3 from, Vector3 to)
        {
            return GetDistanceToPoint(from, to) * 0.01905f;
        }

        public static float Vector3Dot(Vector3 a, Vector3 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }
        /// <summary>
        /// Gets the Field of View distance to the specified destination.
        /// </summary>
        /// <param name="EyePosition">LocalPlayer.ViewOffset + LocalPlayer.Position</param>
        /// <param name="ViewAngle">LocalPlayer.ViewAngle</param>
        /// <param name="Destination">Enemy Head Position</param>
        /// <returns></returns>
        public static double GetFov(Vector3 EyePosition, Vector3 ViewAngle, Vector3 Destination)
        {
            Vector3 angle = CalcAngle(EyePosition, Destination);
            Vector3 aim = AngleVectors(ViewAngle);
            angle = AngleVectors(angle);
            double mag_s = Math.Sqrt((aim[0] * aim[0]) + (aim[1] * aim[1]) + (aim[2] * aim[2]));
            double mag_d = mag_s;
            double u_dot_v = aim[0] * angle[0] + aim[1] * angle[1] + aim[2] * angle[2];
            double val = Math.Acos(u_dot_v / (mag_s * mag_d)) * (180.0f / Math.PI);
            //System.IO.File.AppendAllText("doubles.txt", val.ToString() + Environment.NewLine);
            return val;
        }
        //returns angle between two vectors
        //input two vectors u and v
        //for 'returndegrees' enter true for an answer in degrees, false for radians
        public static Vector3 AngleVectors(Vector3 angles)
        {
            double sp, sy, cp, cy;
            //MathNet.Numerics.LinearAlgebra.Vector.a
            sy = Math.Sin(ConvertToRadians(angles.Y));
            sp = Math.Sin(ConvertToRadians(angles.X));
            cp = Math.Cos(ConvertToRadians(angles.X));
            cy = Math.Cos(ConvertToRadians(angles.Y));
            return new Vector3((float)(cp * cy), (float)(cp * sy), (float)-sp);
        }
        public static double ConvertToRadians(float angle)
        {
            return (Math.PI / 180) * angle;
        }
        public static Vector3 CalcAngle(SharpDX.Vector3 src, SharpDX.Vector3 dst)
        {
            Vector3 ret = new Vector3();
            Vector3 vDelta = src - dst;
            //vDelta = NormalizeAngles(vDelta);
            /* 	float Length2d( void ) const
	            {
		            return sqrtf( x * x + y * y );
	            } 
            */

            float fHyp = (float)Math.Sqrt((vDelta.X * vDelta.X) + (vDelta.Y * vDelta.Y));

            ret.X = RadToDeg((float)Math.Atan(vDelta.Z / fHyp));
            ret.Y = RadToDeg((float)Math.Atan(vDelta.Y / vDelta.X));

            if (vDelta.X >= 0.0f)
                ret.Y += 180.0f;
            return ret;
        }

        public static Vector3 NormalizeAngles(Vector3 v)
        {
            for (int i = 0; i < 3; i++)
            {
                if (v[i] < -180.0f) v[i] += 360.0f;
                if (v[i] > 180.0f) v[i] -= 360.0f;
            }

            //if (v.X > 89.0) v.X = 89f;
            //if (v.X < -89) v.X = -89f;
            //if (v.Y < -180) v.Y = -179.999f;
            //if (v.Y > 180) v.Y = 179.999f;

            //v.Z = 0;

            return v;
        }

        public static float MakeDot(Vector3 i, Vector3 o)
        {
            return (i.X * o.X + i.Y * o.Y + i.Z * o.Z);
        }

        //public static Vector2[] WorldToScreen(Matrix4x4 vMatrix, Size2 screenSize, params Vector3[] points)
        //{
        //    Vector2[] worlds = new Vector2[points.Length];
        //    for (int i = 0; i < worlds.Length; i++)
        //        worlds[i] = WorldToScreen(vMatrix, screenSize, points[i]);
        //    return worlds;

        //}

        public static Vector2 ScreenOffset
        {
            get
            {
                return new Vector2((float)g_Globals.Config.HudConfig.ScreenOffsetX.Value, (float)g_Globals.Config.HudConfig.ScreenOffsetY.Value);
            }
        }

        public static bool WorldToScreen(view_matrix_t viewMatrix, Size2 screenSize, Vector3 point3D, out Vector2 screenPos)
        {
            screenPos = Vector2.Zero;
            float w = viewMatrix[3, 0] * point3D.X + viewMatrix[3, 1] * point3D.Y + viewMatrix[3, 2] * point3D.Z + viewMatrix[3, 3];
            if (w < 0.01f)
                return false;
            float inverseX = 1f / w;
            screenPos.X =
                (screenSize.Width / 2f) +
                (0.5f * (
                (viewMatrix[0, 0] * point3D.X + viewMatrix[0, 1] * point3D.Y + viewMatrix[0, 2] * point3D.Z + viewMatrix[0, 3])
                * inverseX)
                * screenSize.Width + 0.5f);
            screenPos.Y =
                (screenSize.Height / 2f) -
                (0.5f * (
                (viewMatrix[1, 0] * point3D.X + viewMatrix[1, 1] * point3D.Y + viewMatrix[1, 2] * point3D.Z + viewMatrix[1, 3])
                * inverseX)
                * screenSize.Height + 0.5f);
            screenPos += ScreenOffset;
            return true;
        }


        public static float SmoothStep(float a, float b, float t)
        {
            return t * t * (a - b * t);
        }

        public static Vector3 Slerp(Vector3 start, Vector3 end, float t)
        {
            float dot = Vector3.Dot(start, end);
            float theta = (float)Math.Acos(dot) * t;
            var _relative = end - start * dot;
            //_relative.Normalize();
            return ((start * (float)Math.Cos(theta)) + (_relative * (float)Math.Sin(theta)));
        }

        public static Vector3 SmoothStep(Vector3 a, Vector3 b, float t)
        {
            return t * t * (a - b * t);
        }

        public static Vector3 InverseSmoothStep(Vector3 a, Vector3 b, float t)
        {
            return t * t * (a + b * t);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return a - ((b + a) * t);
        }

        public static Vector3 InverseLerp(Vector3 a, Vector3 b, float t)
        {
            return a + ((b - a) * t);
        }

        public static float InverseLerp(float a, float b, float t)
        {
            return a - (b + a) * t;
        }
        public static float Lerp(float a, float b, float t)
        {
            return a - (b - a) * t;
        }

        public static float Angle(Vector3 from, Vector3 to)
        {
            from.Normalize();
            to.Normalize();
            return (float)(Math.Acos(RRWAPI.MATH.Clamp(Vector3.Dot(from, to), -1f, 1f)) * 57.29578f);
        }

        public static Quaternion CreateQuaternion(float yaw, float pitch, float roll)
        {
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin((double)rollOver2);
            float cosRollOver2 = (float)Math.Cos((double)rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
            float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin((double)yawOver2);
            float cosYawOver2 = (float)Math.Cos((double)yawOver2);
            Quaternion result;
            result.X = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.Y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.Z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.W = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }
        public static Quaternion CreateQuaternion(Vector3 vec)
        {
            float rollOver2 = vec.Z * 0.5f;
            float sinRollOver2 = (float)Math.Sin((double)rollOver2);
            float cosRollOver2 = (float)Math.Cos((double)rollOver2);
            float pitchOver2 = vec.Y * 0.5f;
            float sinPitchOver2 = (float)Math.Sin((double)pitchOver2);
            float cosPitchOver2 = (float)Math.Cos((double)pitchOver2);
            float yawOver2 = vec.X * 0.5f;
            float sinYawOver2 = (float)Math.Sin((double)yawOver2);
            float cosYawOver2 = (float)Math.Cos((double)yawOver2);
            Quaternion result;
            result.X = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.Y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.Z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.W = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;
        }

        public static Vector3 ClampAngle(Vector3 vec)
        {
            if (vec.X > 180)
                vec.X -= 360;

            else if (vec.X < -180)
                vec.X += 360;

            if (vec.Y > 180)
                vec.Y -= 360;

            else if (vec.Y < -180)
                vec.Y += 360;

            if (vec.X < -89)
                vec.X = -89;

            if (vec.X > 89)
                vec.X = 89;

            while (vec.Y < -180.0f)
                vec.Y += 360.0f;

            while (vec.Y > 180.0f)
                vec.Y -= 360.0f;

            vec.Z = 0;

            return vec;
            //if (qaAng.X > 89.0f && qaAng.X <= 180.0f)
            //    qaAng.X = 89.0f;

            //while (qaAng.X > 180.0f)
            //    qaAng.X = qaAng.X - 360.0f;

            //if (qaAng.X < -89.0f)
            //    qaAng.X = -89.0f;

            //while (qaAng.Y > 180.0f)
            //    qaAng.Y = qaAng.Y - 360.0f;

            //while (qaAng.Y < -180.0f)
            //    qaAng.Y = qaAng.Y + 360.0f;

            //return qaAng;
        }
        public struct BoxBounds
        {
            public float X;
            public float Y;
            public float W;
            public float H;
            public Vector2 CenterPoint;
            public BoxBounds(float x, float y, float w, float h, Vector2 center)
            {
                X = x;
                Y = y;
                W = w;
                H = h;
                CenterPoint = center;
            }
        }
        public static float DotProduct(Vector3 a, Vector3 b)
        {
            return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
        }

        public static double Lenght2D(Vector3 toConvert)
        {
            return Math.Sqrt(toConvert.X * toConvert.X + toConvert.Y * toConvert.Y);
        }

        public static Vector3 VectorTransform(Vector3 in1, matrix3x4 in2)
        {
            return new Vector3(
                DotProduct(in1, new Vector3(in2.first[0].second[0], in2.first[0].second[1], in2.first[0].second[2])) + in2.first[0].second[3],
                DotProduct(in1, new Vector3(in2.first[1].second[0], in2.first[1].second[1], in2.first[1].second[2])) + in2.first[1].second[3],
                DotProduct(in1, new Vector3(in2.first[2].second[0], in2.first[2].second[1], in2.first[2].second[2])) + in2.first[2].second[3]);
        }
        //public static float PixelDistance(Vector2 screenMid, CCSPlayer ent, Size2 WindowFrame)
        //{
        //    Vector2 wtsPos = WorldToScreen(mem.Manager.Aether.LocalPlayer.lpInfo.ViewMatrix, WindowFrame, ent.pInfo.Head + (Vector3.UnitZ * 6f));
        //    if (wtsPos == Vector2.Zero)
        //        return float.MaxValue;
        //    return Math.Abs((screenMid - wtsPos).Length());
        //}
        //public static float PixelDistance(Vector2 screenMid, Vector3 worldPos, Size2 WindowFrame)
        //{
        //    Vector2 wtsPos = WorldToScreen(mem.Manager.Aether.LocalPlayer.lpInfo.ViewMatrix, WindowFrame, worldPos);
        //    if (wtsPos == Vector2.Zero)
        //        return float.MaxValue;
        //    return Math.Abs((screenMid - wtsPos).Length());
        //}
        public static double RealFovDistance(Vector3 eyepos, Vector3 viewangle, Vector3 destination)
        {
            double val = GetFov(eyepos, viewangle, destination);
            if (double.IsNaN(val))
                return 0;
            return Math.Round(val, 4);
        }
        public static float RadToDeg(float deg) { return (float)(deg * (180f / Math.PI)); }

        //public float ConvertToRadians(float angle)
        //{
        //    return (Math.PI / 180) * angle;
        //}

        public static bool PointInCircle(Vector2 point, Vector2 circleCenter, float radius)
        {
            return Math.Sqrt(((circleCenter.X - point.X) * (circleCenter.X - point.X)) + ((circleCenter.Y - point.Y) * (circleCenter.Y - point.Y))) < radius;
        }
        public static Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, float angleInDegrees)
        {
            float angleInRadians = (float)(angleInDegrees * (Math.PI / 180f));
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);
            return new Vector2
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public static float FloatLerp(float a, float b, float t)
        {
            //return firstFloat * by + secondFloat * (1 - by);
            return (1f - t) * a + t * b;
        }
    }
}
