﻿
namespace GeneratorBox
{
    using System;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// An Time
    /// </summary>
    [Serializable]
    public partial struct Time : IComparable<Time>, IEquatable<Time>, IFormattable, IXmlSerializable, IUnitValue
    {
        /// <summary>
        /// The value in <see cref="T:GeneratorBox.Seconds"/>.
        /// </summary>
        public readonly double Seconds;

        private Time(double seconds)
        {
            Seconds = seconds;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="unit"></param>
        public Time(double seconds, Seconds unit)
        {
            Seconds = seconds;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public Time(double value, MilliSeconds unit)
        {
            Seconds = UnitConverter.ConvertFrom(value, unit);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public Time(double value, Hours unit)
        {
            Seconds = UnitConverter.ConvertFrom(value, unit);
        }

        /// <summary>
        /// The value in milliSeconds
        /// </summary>
        public double MilliSeconds
        {
            get
            {
                return UnitConverter.ConvertTo(Seconds, TimeUnit.MilliSeconds);
            }
        }

        /// <summary>
        /// The value in hours
        /// </summary>
        public double Hours
        {
            get
            {
                return UnitConverter.ConvertTo(Seconds, TimeUnit.Hours);
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="T:GeneratorBox.Time"/> from its string representation
        /// </summary>
        /// <param name="s">The string representation of the <see cref="T:GeneratorBox.Time"/></param>
        /// <returns></returns>
        public static Time Parse(string s)
        {
            return UnitParser.Parse(s, From);
        }

        /// <summary>
        /// Reads an instance of <see cref="T:GeneratorBox.Time"/> from the <paramref name="reader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>An instance of  <see cref="T:GeneratorBox.Time"/></returns>
        public static Time ReadFrom(XmlReader reader)
        {
            var v = new Time();
            v.ReadXml(reader);
            return v;
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public static Time From<T>(double value, T unit) where T : ITimeUnit
        {
            return new Time(UnitConverter.ConvertFrom(value, unit));
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="value"></param>
        public static Time FromSeconds(double value)
        {
            return new Time(value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="value"></param>
        public static Time FromMilliSeconds(double value)
        {
            return new Time(UnitConverter.ConvertFrom(value, TimeUnit.MilliSeconds));
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <param name="value"></param>
        public static Time FromHours(double value)
        {
            return new Time(UnitConverter.ConvertFrom(value, TimeUnit.Hours));
        }

        /// <summary>
        /// Indicates whether two <see cref="T:GeneratorBox.Time"/> instances are equal.
        /// </summary>
        /// <returns>
        /// true if the values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">A <see cref="T:GeneratorBox.Time"/>.</param>
        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether two <see cref="T:GeneratorBox.Time"/> instances are not equal.
        /// </summary>
        /// <returns>
        /// true if the values of <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">A <see cref="T:GeneratorBox.Time"/>.</param>
        public static bool operator !=(Time left, Time right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:GeneratorBox.Time"/> is less than another specified <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <returns>
        /// true if the value of <paramref name="left"/> is less than the value of <paramref name="right"/>; otherwise, false. 
        /// </returns>
        /// <param name="left">An <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">An <see cref="T:GeneratorBox.Time"/>.</param>
        public static bool operator <(Time left, Time right)
        {
            return left.Seconds < right.Seconds;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:GeneratorBox.Time"/> is greater than another specified <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <returns>
        /// true if the value of <paramref name="left"/> is greater than the value of <paramref name="right"/>; otherwise, false. 
        /// </returns>
        /// <param name="left">An <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">An <see cref="T:GeneratorBox.Time"/>.</param>
        public static bool operator >(Time left, Time right)
        {
            return left.Seconds > right.Seconds;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:GeneratorBox.Time"/> is less than or equal to another specified <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <returns>
        /// true if the value of <paramref name="left"/> is less than or equal to the value of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">An <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">An <see cref="T:GeneratorBox.Time"/>.</param>
        public static bool operator <=(Time left, Time right)
        {
            return left.Seconds <= right.Seconds;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:GeneratorBox.Time"/> is greater than or equal to another specified <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <returns>
        /// true if the value of <paramref name="left"/> is greater than or equal to the value of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">An <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">An <see cref="T:GeneratorBox.Time"/>.</param>
        public static bool operator >=(Time left, Time right)
        {
            return left.Seconds >= right.Seconds;
        }

        /// <summary>
        /// Multiplies an instance of <see cref="T:GeneratorBox.Time"/> with <paramref name="left"/> and returns the result.
        /// </summary>
        /// <param name="right">An instance of <see cref="T:GeneratorBox.Time"/></param>
        /// <param name="left">An instance of <seealso cref="T:System.Double"/></param>
        /// <returns>Multiplies an instance of <see cref="T:GeneratorBox.Time"/> with <paramref name="left"/> and returns the result.</returns>
        public static Time operator *(double left, Time right)
        {
            return new Time(left * right.Seconds);
        }

        /// <summary>
        /// Multiplies an instance of <see cref="T:GeneratorBox.Time"/> with <paramref name="right"/> and returns the result.
        /// </summary>
        /// <param name="left">An instance of <see cref="T:GeneratorBox.Time"/></param>
        /// <param name="right">An instance of <seealso cref="T:System.Double"/></param>
        /// <returns>Multiplies an instance of <see cref="T:GeneratorBox.Time"/> with <paramref name="right"/> and returns the result.</returns>
        public static Time operator *(Time left, double right)
        {
            return new Time(left.Seconds * right);
        }

        /// <summary>
        /// Divides an instance of <see cref="T:GeneratorBox.Time"/> with <paramref name="right"/> and returns the result.
        /// </summary>
        /// <param name="left">An instance of <see cref="T:GeneratorBox.Time"/></param>
        /// <param name="right">An instance of <seealso cref="T:System.Double"/></param>
        /// <returns>Divides an instance of <see cref="T:GeneratorBox.Time"/> with <paramref name="right"/> and returns the result.</returns>
        public static Time operator /(Time left, double right)
        {
            return new Time(left.Seconds / right);
        }

        /// <summary>
        /// Adds two specified <see cref="T:GeneratorBox.Time"/> instances.
        /// </summary>
        /// <returns>
        /// An <see cref="T:GeneratorBox.Time"/> whose value is the sum of the values of <paramref name="left"/> and <paramref name="right"/>.
        /// </returns>
        /// <param name="left">A <see cref="T:GeneratorBox.Time"/>.</param>
        /// <param name="right">A TimeSpan.</param>
        public static Time operator +(Time left, Time right)
        {
            return new Time(left.Seconds + right.Seconds);
        }

        /// <summary>
        /// Subtracts an Time from another Time and returns the difference.
        /// </summary>
        /// <returns>
        /// An <see cref="T:GeneratorBox.Time"/> that is the difference
        /// </returns>
        /// <param name="left">A <see cref="T:GeneratorBox.Time"/> (the minuend).</param>
        /// <param name="right">A <see cref="T:GeneratorBox.Time"/> (the subtrahend).</param>
        public static Time operator -(Time left, Time right)
        {
            return new Time(left.Seconds - right.Seconds);
        }

        /// <summary>
        /// Returns an <see cref="T:GeneratorBox.Time"/> whose value is the negated value of the specified instance.
        /// </summary>
        /// <returns>
        /// An <see cref="T:GeneratorBox.Time"/> with the same numeric value as this instance, but the opposite sign.
        /// </returns>
        /// <param name="Time">A <see cref="T:GeneratorBox.Time"/></param>
        public static Time operator -(Time Time)
        {
            return new Time(-1 * Time.Seconds);
        }

        /// <summary>
        /// Returns the specified instance of <see cref="T:GeneratorBox.Time"/>.
        /// </summary>
        /// <returns>
        /// Returns <paramref name="Time"/>.
        /// </returns>
        /// <param name="Time">A <see cref="T:GeneratorBox.Time"/></param>
        public static Time operator +(Time Time)
        {
            return Time;
        }

        public override string ToString()
        {
            return this.ToString((string)null, (IFormatProvider)NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format)
        {
            return this.ToString(format, (IFormatProvider)NumberFormatInfo.CurrentInfo);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString((string)null, (IFormatProvider)NumberFormatInfo.GetInstance(provider));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return this.ToString(format, formatProvider, TimeUnit.Seconds);
        }

        public string ToString<T>(string format, IFormatProvider formatProvider, T unit) where T : ITimeUnit
        {
            var value = UnitConverter.ConvertTo(this.Seconds, unit);
            return string.Format("{0}{1}", value.ToString(format, formatProvider), unit.ShortName);
        }

        /// <summary>
        /// Compares this instance to a specified <see cref="T:MathNet.Spatial.Units.Time"/> object and returns an integer that indicates whether this <see cref="instance"/> is shorter than, equal to, or longer than the <see cref="T:MathNet.Spatial.Units.Time"/> object.
        /// </summary>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="value"/>.
        /// 
        ///                     Value
        /// 
        ///                     Description
        /// 
        ///                     A negative integer
        /// 
        ///                     This instance is smaller than <paramref name="value"/>.
        /// 
        ///                     Zero
        /// 
        ///                     This instance is equal to <paramref name="value"/>.
        /// 
        ///                     A positive integer
        /// 
        ///                     This instance is larger than <paramref name="value"/>.
        /// 
        /// </returns>
        /// <param name="value">A <see cref="T:MathNet.Spatial.Units.Time"/> object to compare to this instance.</param>
        public int CompareTo(Time value)
        {
            return this.Seconds.CompareTo(value.Seconds);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:GeneratorBox.Time"/> object.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same Time as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:GeneratorBox.Time"/> object to compare with this instance.</param>
        public bool Equals(Time other)
        {
            return this.Seconds.Equals(other.Seconds);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:GeneratorBox.Time"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same Time as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:GeneratorBox.Time"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(Time other, double tolerance)
        {
            return Math.Abs(this.Seconds - other.Seconds) < tolerance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Time && this.Equals((Time)obj);
        }

        public override int GetHashCode()
        {
            return this.Seconds.GetHashCode();
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, 
        /// you should return null (Nothing in Visual Basic) from this method, and instead, 
        /// if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the
        ///  <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> 
        /// method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized. </param>
        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            var e = (XElement)XNode.ReadFrom(reader);

            // Hacking set readonly fields here, can't think of a cleaner workaround
            XmlExt.SetReadonlyField(ref this, x => x.Seconds, XmlConvert.ToDouble(XmlExt.ReadAttributeOrElementOrDefault(e, "Value")));
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(XmlWriter writer)
        {
            XmlExt.WriteAttribute(writer, "Value", this.Seconds);
        }
    }
}