using System;
using FileHelpers;

namespace scratchpadConsole
{
    public class DoubleConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Convert.ToDouble(double.Parse(from));
        }

    }
}
