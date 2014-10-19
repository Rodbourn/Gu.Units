﻿namespace Gu.Units
{
    using System;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// A type for the quantity <see cref="T:Gu.Units.ElectricCharge"/>.
    /// </summary>
    [Serializable]
    public partial struct ElectricCharge : IComparable<ElectricCharge>, IEquatable<ElectricCharge>, IFormattable, IXmlSerializable, IQuantity<TimeUnit, I1, CurrentUnit, I1>
    {
        /// <summary>
        /// The quantity in <see cref="T:Gu.Units.Coulombs"/>.
        /// </summary>
        public readonly double Coulombs;

        private ElectricCharge(double coulombs)
        {
            Coulombs = coulombs;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"><see cref="T:Gu.Units.Coulombs"/>.</param>
        public ElectricCharge(double value, ElectricChargeUnit unit)
        {
            Coulombs = unit.ToSiUnit(value);
        }

        /// <summary>
        /// The quantity in Coulombs
        /// </summary>
        public double SiValue
        {
            get
            {
                return Coulombs;
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="T:Gu.Units.ElectricCharge"/> from its string representation
        /// </summary>
        /// <param name="s">The string representation of the <see cref="T:Gu.Units.ElectricCharge"/></param>
        /// <returns></returns>
        public static ElectricCharge Parse(string s)
        {
            return Parser.Parse<ElectricChargeUnit, ElectricCharge>(s, From);
        }

        /// <summary>
        /// Reads an instance of <see cref="T:Gu.Units.ElectricCharge"/> from the <paramref name="reader"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>An instance of  <see cref="T:Gu.Units.ElectricCharge"/></returns>
        public static ElectricCharge ReadFrom(XmlReader reader)
        {
            var v = new ElectricCharge();
            v.ReadXml(reader);
            return v;
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        public static ElectricCharge From(double value, ElectricChargeUnit unit)
        {
            return new ElectricCharge(unit.ToSiUnit(value));
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <param name="coulombs">The value in <see cref="T:Gu.Units.Coulombs"/></param>
        public static ElectricCharge FromCoulombs(double coulombs)
        {
            return new ElectricCharge(coulombs);
        }

        public static Time operator /(ElectricCharge left, Current right)
        {
            return Time.FromSeconds(left.Coulombs / right.Amperes);
        }

        public static Current operator /(ElectricCharge left, Time right)
        {
            return Current.FromAmperes(left.Coulombs / right.Seconds);
        }

        /// <summary>
        /// Indicates whether two <see cref="T:Gu.Units.ElectricCharge"/> instances are equal.
        /// </summary>
        /// <returns>
        /// true if the quantitys of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">A <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        public static bool operator ==(ElectricCharge left, ElectricCharge right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether two <see cref="T:Gu.Units.ElectricCharge"/> instances are not equal.
        /// </summary>
        /// <returns>
        /// true if the quantitys of <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, false.
        /// </returns>
        /// <param name="left">A <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">A <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        public static bool operator !=(ElectricCharge left, ElectricCharge right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:Gu.Units.ElectricCharge"/> is less than another specified <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is less than the quantity of <paramref name="right"/>; otherwise, false. 
        /// </returns>
        /// <param name="left">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        public static bool operator <(ElectricCharge left, ElectricCharge right)
        {
            return left.Coulombs < right.Coulombs;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:Gu.Units.ElectricCharge"/> is greater than another specified <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is greater than the quantity of <paramref name="right"/>; otherwise, false. 
        /// </returns>
        /// <param name="left">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        public static bool operator >(ElectricCharge left, ElectricCharge right)
        {
            return left.Coulombs > right.Coulombs;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:Gu.Units.ElectricCharge"/> is less than or equal to another specified <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is less than or equal to the quantity of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        public static bool operator <=(ElectricCharge left, ElectricCharge right)
        {
            return left.Coulombs <= right.Coulombs;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="T:Gu.Units.ElectricCharge"/> is greater than or equal to another specified <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is greater than or equal to the quantity of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">An <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        public static bool operator >=(ElectricCharge left, ElectricCharge right)
        {
            return left.Coulombs >= right.Coulombs;
        }

        /// <summary>
        /// Multiplies an instance of <see cref="T:Gu.Units.ElectricCharge"/> with <paramref name="left"/> and returns the result.
        /// </summary>
        /// <param name="right">An instance of <see cref="T:Gu.Units.ElectricCharge"/></param>
        /// <param name="left">An instance of <seealso cref="T:System.Double"/></param>
        /// <returns>Multiplies an instance of <see cref="T:Gu.Units.ElectricCharge"/> with <paramref name="left"/> and returns the result.</returns>
        public static ElectricCharge operator *(double left, ElectricCharge right)
        {
            return new ElectricCharge(left * right.Coulombs);
        }

        /// <summary>
        /// Multiplies an instance of <see cref="T:Gu.Units.ElectricCharge"/> with <paramref name="right"/> and returns the result.
        /// </summary>
        /// <param name="left">An instance of <see cref="T:Gu.Units.ElectricCharge"/></param>
        /// <param name="right">An instance of <seealso cref="T:System.Double"/></param>
        /// <returns>Multiplies an instance of <see cref="T:Gu.Units.ElectricCharge"/> with <paramref name="right"/> and returns the result.</returns>
        public static ElectricCharge operator *(ElectricCharge left, double right)
        {
            return new ElectricCharge(left.Coulombs * right);
        }

        /// <summary>
        /// Divides an instance of <see cref="T:Gu.Units.ElectricCharge"/> with <paramref name="right"/> and returns the result.
        /// </summary>
        /// <param name="left">An instance of <see cref="T:Gu.Units.ElectricCharge"/></param>
        /// <param name="right">An instance of <seealso cref="T:System.Double"/></param>
        /// <returns>Divides an instance of <see cref="T:Gu.Units.ElectricCharge"/> with <paramref name="right"/> and returns the result.</returns>
        public static ElectricCharge operator /(ElectricCharge left, double right)
        {
            return new ElectricCharge(left.Coulombs / right);
        }

        /// <summary>
        /// Adds two specified <see cref="T:Gu.Units.ElectricCharge"/> instances.
        /// </summary>
        /// <returns>
        /// An <see cref="T:Gu.Units.ElectricCharge"/> whose quantity is the sum of the quantitys of <paramref name="left"/> and <paramref name="right"/>.
        /// </returns>
        /// <param name="left">A <see cref="T:Gu.Units.ElectricCharge"/>.</param>
        /// <param name="right">A TimeSpan.</param>
        public static ElectricCharge operator +(ElectricCharge left, ElectricCharge right)
        {
            return new ElectricCharge(left.Coulombs + right.Coulombs);
        }

        /// <summary>
        /// Subtracts an ElectricCharge from another ElectricCharge and returns the difference.
        /// </summary>
        /// <returns>
        /// An <see cref="T:Gu.Units.ElectricCharge"/> that is the difference
        /// </returns>
        /// <param name="left">A <see cref="T:Gu.Units.ElectricCharge"/> (the minuend).</param>
        /// <param name="right">A <see cref="T:Gu.Units.ElectricCharge"/> (the subtrahend).</param>
        public static ElectricCharge operator -(ElectricCharge left, ElectricCharge right)
        {
            return new ElectricCharge(left.Coulombs - right.Coulombs);
        }

        /// <summary>
        /// Returns an <see cref="T:Gu.Units.ElectricCharge"/> whose quantity is the negated quantity of the specified instance.
        /// </summary>
        /// <returns>
        /// An <see cref="T:Gu.Units.ElectricCharge"/> with the same numeric quantity as this instance, but the opposite sign.
        /// </returns>
        /// <param name="ElectricCharge">A <see cref="T:Gu.Units.ElectricCharge"/></param>
        public static ElectricCharge operator -(ElectricCharge ElectricCharge)
        {
            return new ElectricCharge(-1 * ElectricCharge.Coulombs);
        }

        /// <summary>
        /// Returns the specified instance of <see cref="T:Gu.Units.ElectricCharge"/>.
        /// </summary>
        /// <returns>
        /// Returns <paramref name="ElectricCharge"/>.
        /// </returns>
        /// <param name="ElectricCharge">A <see cref="T:Gu.Units.ElectricCharge"/></param>
        public static ElectricCharge operator +(ElectricCharge ElectricCharge)
        {
            return ElectricCharge;
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
            return this.ToString(format, formatProvider, ElectricChargeUnit.Coulombs);
        }

        public string ToString(string format, IFormatProvider formatProvider, ElectricChargeUnit unit)
        {
            var quantity = unit.FromSiUnit(this.Coulombs);
            return string.Format("{0}{1}", quantity.ToString(format, formatProvider), unit.Symbol);
        }

        /// <summary>
        /// Compares this instance to a specified <see cref="T:MathNet.Spatial.Units.ElectricCharge"/> object and returns an integer that indicates whether this <see cref="instance"/> is shorter than, equal to, or longer than the <see cref="T:MathNet.Spatial.Units.ElectricCharge"/> object.
        /// </summary>
        /// <returns>
        /// A signed number indicating the relative quantitys of this instance and <paramref name="quantity"/>.
        /// 
        ///                     Value
        /// 
        ///                     Description
        /// 
        ///                     A negative integer
        /// 
        ///                     This instance is smaller than <paramref name="quantity"/>.
        /// 
        ///                     Zero
        /// 
        ///                     This instance is equal to <paramref name="quantity"/>.
        /// 
        ///                     A positive integer
        /// 
        ///                     This instance is larger than <paramref name="quantity"/>.
        /// 
        /// </returns>
        /// <param name="quantity">A <see cref="T:MathNet.Spatial.Units.ElectricCharge"/> object to compare to this instance.</param>
        public int CompareTo(ElectricCharge quantity)
        {
            return this.Coulombs.CompareTo(quantity.Coulombs);
        }

        /// <summary>
        /// Returns a quantity indicating whether this instance is equal to a specified <see cref="T:Gu.Units.ElectricCharge"/> object.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same ElectricCharge as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:Gu.Units.ElectricCharge"/> object to compare with this instance.</param>
        public bool Equals(ElectricCharge other)
        {
            return this.Coulombs.Equals(other.Coulombs);
        }

        /// <summary>
        /// Returns a quantity indicating whether this instance is equal to a specified <see cref="T:Gu.Units.ElectricCharge"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same ElectricCharge as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:Gu.Units.ElectricCharge"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(ElectricCharge other, double tolerance)
        {
            return Math.Abs(this.Coulombs - other.Coulombs) < tolerance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is ElectricCharge && this.Equals((ElectricCharge)obj);
        }

        public override int GetHashCode()
        {
            return this.Coulombs.GetHashCode();
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
            // Hacking set readonly fields here, can't think of a cleaner workaround
            XmlExt.SetReadonlyField(ref this, x => x.Coulombs, reader, "Value");
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(XmlWriter writer)
        {
            XmlExt.WriteAttribute(writer, "Value", this.Coulombs);
        }
    }
}