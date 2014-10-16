﻿namespace Gu.Units
{
    using System;

    public interface IQuantity
    {
        Type GetType();
        double SiValue { get; }
    }

    public interface IQuantity<TUnit, TPower> : IQuantity
        where TUnit : IUnit
        where TPower : IPower
    {
    }

    public struct Quantity<TUnit, TPower> : IQuantity<TUnit, TPower>
        where TUnit : IUnit
        where TPower : IPower
    {
        private readonly double _siValue;
        public Quantity(double siValue)
        {
            _siValue = siValue;
        }
        public double SiValue
        {
            get { return _siValue; }
        }
    }

    public interface IQuantity<TUnit1, TPower1, TUnit2, TPower2> : IQuantity
        where TUnit1 : IUnit
        where TPower1 : IPower
        where TUnit2 : IUnit
        where TPower2 : IPower
    {
    }

    public interface IPower
    {
    }

    public interface I1 : IPower
    {
    }

    public interface I2 : IPower
    {
    }

    public interface I3 : IPower
    {
    }

    public interface I4 : IPower
    {
    }

    public interface INeg1 : IPower
    {
    }

    public interface INeg2 : IPower
    {
    }

    public interface INeg3 : IPower
    {
    }

    public interface INeg4 : IPower
    {
    }
}