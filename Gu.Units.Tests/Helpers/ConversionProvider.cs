﻿namespace Gu.Units.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ConversionProvider : IEnumerable
    {
        private readonly List<IConversion<IQuantity>> _datas;

        public ConversionProvider()
        {
            _datas = new List<IConversion<IQuantity>>
                                        {
                                            new Conversion<Length>("1.2cm","0.012m", Length.Parse),
                                            new Conversion<Length>("1.2cm","12mm", Length.Parse),
                                        };

        }
        public IEnumerator GetEnumerator()
        {
            return _datas.GetEnumerator();
        }

        public interface IConversion<out T> 
        {
            string From { get; }
            T FromQuantity { get; }
            string To { get; }
            T ToQuantity { get; }
        }

        public class Conversion<T> : IConversion<IQuantity>
            where T : IQuantity
        {
            public Conversion(string from, string to, Func<string, T> parseMethod)
            {
                this.From = @from;
                FromQuantity = parseMethod(@from);
                this.To = to;
                ToQuantity = parseMethod(to);
            }

            public string From { get; private set; }

            public IQuantity FromQuantity { get; private set; }

            public string To { get; private set; }

            public IQuantity ToQuantity { get; private set; }

            public override string ToString()
            {
                return string.Format("{0} -> {1}", From, To);
            }
        }
    }
}
