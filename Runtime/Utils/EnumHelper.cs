using System;

namespace Medrick.Nardeboon {
    public static class EnumHelper {
        static System.Random _R = new System.Random ();
        public static T RandomEnumValue<T> ()
        {
            var v = Enum.GetValues (typeof (T));
            return (T) v.GetValue (_R.Next(v.Length - 1));
        }

        public static int EnumLength<T> () {
            var v = Enum.GetNames(typeof (T));
            return v.Length;
        }
    }
}