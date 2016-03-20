﻿using System;
using UnityEngine;

namespace Smooth.Foundations.Algebraics
{

    public struct Union<T1, T2, T3> : IEquatable<Union<T1, T2, T3>>
    {

        public T1 Case1
        {
            get
            {
                if (Case != Variant.First)
                {
                    Debug.LogError("Wrong case for union");
                    Debug.Break();
                }
                return _value1;
            }
        }

        public T2 Case2
        {
            get
            {
                if (Case != Variant.Second)
                {
                    Debug.LogError("Wrong case for union");
                    Debug.Break();
                }
                return _value2;
            }
        }

        public T3 Case3
        {
            get
            {
                if (Case != Variant.Third)
                {
                    Debug.LogError("Wrong case for union");
                    Debug.Break();
                }
                return _value3;
            }
        }

        public readonly Variant Case;

        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;

        public static Union<T1, T2, T3> CreateFirst(T1 value) =>
            new Union<T1, T2, T3>(Variant.First, value, default(T2), default(T3));
        public static Union<T1, T2, T3> CreateSecond(T2 value) =>
            new Union<T1, T2, T3>(Variant.Second, default(T1), value, default(T3));
        public static Union<T1, T2, T3> CreateThird(T3 value) =>
            new Union<T1, T2, T3>(Variant.Third, default(T1), default(T2), value);

        private Union(Variant selectedCase, T1 valueFirst, T2 valueSecond, T3 valueThird)
        {
            Case = selectedCase;
            _value1 = valueFirst;
            _value2 = valueSecond;
            _value3 = valueThird;
        }

        public bool Equals(Union<T1, T2, T3> other)
        {
            if (Case != other.Case)
                return false;
            switch (Case)
            {
                case Variant.First:
                    return Collections.EqualityComparer<T1>.Default.Equals(_value1, other.Case1);
                case Variant.Second:
                    return Collections.EqualityComparer<T2>.Default.Equals(_value2, other.Case2);
                case Variant.Third:
                    return Collections.EqualityComparer<T3>.Default.Equals(_value3, other.Case3);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Union<T1, T2, T3> && Equals((Union<T1, T2, T3>)obj);
        }

        public override int GetHashCode()
        {
            switch (Case)
            {
                case Variant.First:
                    return Collections.EqualityComparer<T1>.Default.GetHashCode(_value1);
                case Variant.Second:
                    return Collections.EqualityComparer<T2>.Default.GetHashCode(_value2);
                case Variant.Third:
                    return Collections.EqualityComparer<T3>.Default.GetHashCode(_value3);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool operator ==(Union<T1, T2, T3> lhs, Union<T1, T2, T3> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Union<T1, T2, T3> lhs, Union<T1, T2, T3> rhs)
        {
            return !(lhs == rhs);
        }
    }
}