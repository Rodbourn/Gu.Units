﻿namespace Gu.Units.Wpf
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Windows.Data;
    using System.Windows.Markup;

    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class AngleConverter : MarkupExtension, IValueConverter
    {
        private AngleUnit? unit;
        private Binding binding;
        private QuantityFormat<AngleUnit> quantityFormat;
        private QuantityFormat<AngleUnit> bindingQuantityFormat;
        private string valueFormat;
        private bool initialized;
        private StringBuilder errorText = new StringBuilder();

        public AngleConverter()
        {
        }

        public AngleConverter([TypeConverter(typeof(AngleUnitTypeConverter))]AngleUnit unit)
        {
            Unit = unit;
        }

        [ConstructorArgument("unit"), TypeConverter(typeof(AngleUnitTypeConverter))]
        public AngleUnit? Unit
        {
            get { return this.unit; }
            set
            {
                if (value == null)
                {
                    this.errorText.AppendLine($"{nameof(Unit)} cannot be null");
                    if (Is.DesignMode)
                    {
                        throw new ArgumentException(this.errorText.ToString(), nameof(value));
                    }
                }

                this.unit = value.Value;
            }
        }

        public string ValueFormat
        {
            get { return this.valueFormat; }
            set
            {
                if (!StringFormatParser<AngleUnit>.CanParseValueFormat(value))
                {
                    if (Is.DesignMode)
                    {
                        StringFormatParser<AngleUnit>.VerifyValueFormat(value);
                    }

                    this.errorText.AppendLine(StringFormatParser<AngleUnit>.CreateFormatErrorString(value, typeof(double)));
                }

                this.valueFormat = value;
            }
        }

        public SymbolFormat? SymbolFormat { get; set; }

        public UnitInput? UnitInput { get; set; }

        public string StringFormat
        {
            get { return this.quantityFormat?.CompositeFormat; }
            set
            {
                if (StringFormatParser<AngleUnit>.TryParse(value, out this.quantityFormat))
                {
                    if (this.unit != null && this.unit != this.quantityFormat.Unit)
                    {
                        var message = $"Unit is set to '{this.unit.Value.symbol}' but StringFormat is '{value}'";
                        if (Is.DesignMode)
                        {
                            throw new InvalidOperationException(message);
                        }
                    }
                }
                else
                {
                    if (Is.DesignMode)
                    {
                        StringFormatParser<AngleUnit>.VerifyQuantityFormat(value);
                    }

                    this.errorText.AppendLine(StringFormatParser<AngleUnit>.CreateFormatErrorString(value, typeof(AngleUnit)));
                }
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // the binding does not have stringformat set at this point
            // caching the binding to resolve later.
            try
            {
                var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                var targetObject = provideValueTarget?.TargetObject;
                this.binding = targetObject as Binding;
                if (this.binding == null && targetObject != null)
                {
                    this.errorText.AppendLine("TargetObject is not a binding");
                    if (Is.DesignMode)
                    {
                        throw new InvalidOperationException(this.errorText.ToString());
                    }
                }
            }
            catch (NullReferenceException)
            {
                this.errorText.AppendLine("Touching provideValueTarget?.TargetObject threw");
                if (Is.DesignMode)
                {
                    throw new InvalidOperationException(this.errorText.ToString());
                }
            }

            return this;
        }

        public object Convert(object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (!this.initialized)
            {
                Initialize();
            }

            var message = this.errorText.ToString();
            if (!IsValidConvertTargetType(targetType))
            {
                message += $"{GetType().Name} does not support converting to {targetType.Name}";
            }

            if (value != null && !(value is Angle))
            {
                message += $"{GetType().Name} only supports converting from {typeof(Angle)}";
            }

            if (message != string.Empty)
            {
                message = message.TrimEnd('\r', '\n');
                if (Is.DesignMode)
                {
                    throw new InvalidOperationException(message);
                }

                return message;
            }

            if (value == null)
            {
                return targetType == typeof(string)
                    ? string.Empty
                    : null;
            }

            var angle = (Angle)value;
            if (this.bindingQuantityFormat != null)
            {
                if (string.IsNullOrEmpty(this.bindingQuantityFormat.SymbolFormat))
                {
                    return angle.GetValue(this.unit.Value);
                }

                return angle;
            }

            if (targetType == typeof(string) || targetType == typeof(object))
            {
                if (UnitInput == Wpf.UnitInput.SymbolRequired)
                {
                    return angle.ToString(this.quantityFormat, culture);
                }

                return angle.GetValue(this.unit.Value);
            }

            return angle.GetValue(this.unit.Value);
        }

        public object ConvertBack(object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (!this.initialized)
            {
                Initialize();
            }

            var message = this.errorText.ToString();
            if (!(targetType == typeof(Angle) || targetType == typeof(Angle?)))
            {
                message += $"{GetType().Name} does not support converting to {targetType.Name}";
            }

            if (message != string.Empty)
            {
                message = message.TrimEnd('\r', '\n');
                if (Is.DesignMode)
                {
                    throw new InvalidOperationException(message);
                }

                return message;
            }

            if (value == null)
            {
                return null;
            }


            if (value is double)
            {
                return new Angle((double)value, this.unit.Value);
            }

            var text = value as string;
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var unitInput = UnitInput ?? Wpf.UnitInput.ScalarOnly;
            switch (unitInput)
            {
                case Wpf.UnitInput.ScalarOnly:
                    {
                        double d;
                        if (double.TryParse(text, NumberStyles.Float, culture, out d))
                        {
                            return new Angle(d, this.unit.Value);
                        }

                        return value; // returning raw to trigger error
                    }
                case Wpf.UnitInput.SymbolAllowed:
                    {
                        double d;
                        int pos = 0;
                        WhiteSpaceReader.TryRead(text, ref pos);
                        if (DoubleReader.TryRead(text, ref pos, NumberStyles.Float, culture, out d))
                        {
                            WhiteSpaceReader.TryRead(text, ref pos);
                            if (pos == text.Length)
                            {
                                return new Angle(d, this.unit.Value);
                            }
                        }

                        goto case Wpf.UnitInput.SymbolRequired;
                    }
                case Wpf.UnitInput.SymbolRequired:
                    {
                        Angle result;
                        if (Angle.TryParse(text, NumberStyles.Float, culture, out result))
                        {
                            return result;
                        }

                        return text;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Initialize()
        {
            this.initialized = true;
            BindingStringFormat.TryGet(this.binding, out this.bindingQuantityFormat);
            if (!string.IsNullOrEmpty(this.bindingQuantityFormat?.SymbolFormat))
            {
                if (this.ValueFormat != null)
                {
                    this.errorText.AppendLine($"ValueFormat cannot be set when Binding.StringFormat is a unit format.");
                }
                if (this.StringFormat != null)
                {
                    this.errorText.AppendLine($"ValueFormat cannot be set when Binding.StringFormat is a unit format.");
                }
            }

            if (this.quantityFormat != null)
            {
                if (this.ValueFormat != null)
                {
                    this.errorText.AppendLine($"Both ValueFormat and StringFormat cannot be set.");
                }
            }
            else
            {
                if (this.unit != null && SymbolFormat != null)
                {
                    this.quantityFormat = FormatCache<AngleUnit>.GetOrCreate(ValueFormat, this.unit.Value, SymbolFormat.Value);
                }
            }

            if (this.unit == null)
            {
                var hasFmtUnit = !string.IsNullOrEmpty(this.quantityFormat?.SymbolFormat);
                var hasBindingUnit = !string.IsNullOrEmpty(this.bindingQuantityFormat?.SymbolFormat);
                if (!hasFmtUnit && !hasBindingUnit)
                {
                    this.errorText.AppendLine($"Unit cannot be null.");
                    this.errorText.AppendLine($"Must be specified Explicitly or in StringFormat or in Binding.StringFormat.");
                }
                else if (hasFmtUnit && !hasBindingUnit)
                {
                    this.unit = this.quantityFormat.Unit;
                }
                else if (!hasFmtUnit && hasBindingUnit)
                {
                    this.unit = this.bindingQuantityFormat.Unit;
                }
                else
                {
                    if (this.quantityFormat.Unit != this.bindingQuantityFormat.Unit)
                    {
                        this.errorText.AppendLine($"Ambiguous units StringFormat: {quantityFormat.CompositeFormat} Binding.StringFormat: {this.bindingQuantityFormat.CompositeFormat}");
                    }

                    this.unit = this.quantityFormat.Unit;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.quantityFormat?.SymbolFormat) &&
                    this.unit != this.quantityFormat.Unit)
                {
                    this.errorText.AppendLine($"Unit is set to '{this.unit}' but StringFormat is '{StringFormat.Replace("{0:", string.Empty).Replace("}", string.Empty)}'");
                }

                var hasBindingUnit = !string.IsNullOrEmpty(this.bindingQuantityFormat?.SymbolFormat);
            }

            if (this.UnitInput == Wpf.UnitInput.SymbolRequired)
            {
                if (string.IsNullOrEmpty(this.quantityFormat?.SymbolFormat))
                {
                    if (string.IsNullOrEmpty(this.bindingQuantityFormat?.SymbolFormat))
                    {
                        if (this.unit == null)
                        {
                            this.errorText.AppendLine("UnitInput == SymbolRequired but no unit format is specified");
                        }
                        else if (this.SymbolFormat != null)
                        {
                            this.quantityFormat = FormatCache<AngleUnit>.GetOrCreate(this.ValueFormat, this.unit.Value, this.SymbolFormat.Value);
                            if (this.UnitInput == null)
                            {
                                this.UnitInput = Wpf.UnitInput.SymbolRequired;
                            }
                            else if (this.UnitInput == Wpf.UnitInput.ScalarOnly)
                            {
                                this.errorText.AppendLine("Cannot have ScalarOnly and SymbolFormat specified");
                            }
                        }
                        else
                        {
                            this.quantityFormat = FormatCache<AngleUnit>.GetOrCreate(this.ValueFormat, this.unit.Value);
                            if (this.UnitInput == null)
                            {
                                this.UnitInput = Wpf.UnitInput.ScalarOnly;
                            }
                            else if (this.UnitInput == Wpf.UnitInput.ScalarOnly)
                            {
                                this.errorText.AppendLine("Cannot have ScalarOnly and SymbolFormat specified");
                            }
                        }
                    }
                    else
                    {
                        this.quantityFormat = this.bindingQuantityFormat;
                    }
                }
            }

            if (UnitInput == null)
            {
                if (!string.IsNullOrEmpty(this.quantityFormat?.SymbolFormat) ||
                    !string.IsNullOrEmpty(this.bindingQuantityFormat?.SymbolFormat))
                {
                    UnitInput = Wpf.UnitInput.SymbolRequired;
                }
            }
        }

        private static bool IsValidConvertTargetType(Type targetType)
        {
            return targetType == typeof(string) ||
                   targetType == typeof(double) ||
                   targetType == typeof(object);
        }
    }
}