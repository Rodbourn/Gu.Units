﻿namespace GeneratorBox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    [Serializable]
    public class UnitMetaData : TypeMetaData
    {
        private UnitMetaData()
            : base()
        {
            ValueType = new TypeMetaData("");
        }

        public UnitMetaData(string valueType, string ns, string unitTypeName, double conversionFactor, string unitName)
            : this(valueType, ns,unitTypeName,conversionFactor == 0 ? "" : conversionFactor.ToString(CultureInfo.InvariantCulture), unitName)
        {
        }

        public UnitMetaData(string valueType, string ns, string unitTypeName, string conversionFactor, string unitName)
            : base(unitTypeName)
        {
            ValueType = new TypeMetaData(valueType);
            Namespace = ns;
            UnitName = unitName;
            ConversionFactor = conversionFactor;
        }

        public static UnitMetaData Empty
        {
            get
            {
                return new UnitMetaData("", "", "", 0, "");
            }
        }

        public TypeMetaData ValueType { get; set; }

        public string Namespace { get; set; }

        public string UnitName { get; set; }

        public string ConversionFactor { get; set; }

        public bool IsEmpty
        {
            get
            {
                if (!string.IsNullOrEmpty(UnitName))
                {
                    return false;
                }
                return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, ValueType: {1}, Namespace: {2}, UnitName: {3}, ConversionFactor: {4}, IsEmpty: {5}", base.ToString(), this.ValueType, this.Namespace, this.UnitName, this.ConversionFactor, this.IsEmpty);
        }
    }
}