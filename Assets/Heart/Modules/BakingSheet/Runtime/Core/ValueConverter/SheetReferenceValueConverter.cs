﻿using System;

namespace Pancake.BakingSheet.Internal
{
    public class SheetReferenceValueConverter : SheetValueConverter<ISheetReference>
    {
        protected override ISheetReference StringToValue(Type type, string value, SheetValueConvertingContext context)
        {
            var reference = (ISheetReference) Activator.CreateInstance(type);
            reference.Id = context.StringToValue(reference.IdType, value);
            return reference;
        }

        protected override string ValueToString(Type type, ISheetReference value, SheetValueConvertingContext context)
        {
            return context.ValueToString(value.IdType, value.Id);
        }
    }
}