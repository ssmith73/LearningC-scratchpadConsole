using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace scratchpadConsole
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    [IgnoreFirst(1)]
    public class ResistanceData
    {
        [FieldConverter(typeof(DoubleConverter))]
        public double Temerature { get; set; }

        [FieldConverter(typeof(DoubleConverter))]
        public double Resistance { get; set; }
    }
}
