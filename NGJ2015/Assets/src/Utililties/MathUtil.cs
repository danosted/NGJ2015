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
    }
}
