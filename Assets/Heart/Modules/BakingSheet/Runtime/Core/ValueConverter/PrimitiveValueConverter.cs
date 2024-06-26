﻿using System;

namespace Pancake.BakingSheet.Internal
{
    public class PrimitiveValueConverter : ISheetValueConverter
    {
        public bool CanConvert(Type type) { return type.IsPrimitive || type == typeof(string) || type == typeof(decimal); }

        public object StringToValue(Type type, string value, SheetValueConvertingContext context) { return Convert.ChangeType(value, type, context.FormatProvider); }

        public string ValueToString(Type type, object value, SheetValueConvertingContext context) { return Convert.ToString(value, context.FormatProvider); }
    }
}