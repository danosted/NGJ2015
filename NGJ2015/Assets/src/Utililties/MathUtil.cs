using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Backend.Utililties
{
    public class MathUtil
    {
        public static Vector2 RandomOnUnitCircle()
        {
            var pi = Mathf.PI;
            var random = Random.Range(-1f, 1f) * pi;
            return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
        }

        public static Vector2 RandomOnSquareFromAspect(Vector2 aspectRatio)
        {
            var result = Vector2.zero;
            var halfX = aspectRatio.x * 0.5f;
            var halfY = aspectRatio.y * 0.5f;
            var onHorizontal = Random.Range(0, 2);
            Debug.LogWarning("onHorizontal " + onHorizontal);
            if (onHorizontal == 1)
            {
                var onUpperEdge = Random.Range(0, 2);
                result.x = Random.Range(-halfX, halfX);
                if (onUpperEdge == 1)
                {
                    result.y = halfY;
                    return result;
                }
                result.y = -halfY;
                return result;
            }
            var onRightEdge = Random.Range(0, 2);
            result.y = Random.Range(-halfY, halfY);
            if (onRightEdge == 1)
            {
                result.x = halfX;
                return result;
            }

            result.x = halfX;
            return result;

        }
    }
}
