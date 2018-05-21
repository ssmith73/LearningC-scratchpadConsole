using FileHelpers;
using System;

namespace scratchpadConsole
{

    public struct InterpolatorInputs
    {
        public double EnteredTemperature { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double ResAtT2 { get; set; }
        public double ResAtT1 { get; set; }
    }

    public class Program
    {

        public decimal ThermistorInterpelator(InterpolatorInputs ips)
        {
            return Convert.ToDecimal(
                (ips.ResAtT2 - ips.ResAtT1) / (ips.MaxTemp - ips.MinTemp) *
                (ips.EnteredTemperature - ips.MinTemp) + ips.ResAtT1);
        }

        static void Main()
        {
            Program program = new Program();

            var engine = new FileHelperEngine<ResistanceData>();
            var result = engine.ReadFile("44006_44031.csv");
            var panasonic = engine.ReadFile("Panasonic_Thermistor.csv");

            foreach (ResistanceData data in panasonic)
            {
                Console.WriteLine($"Temperature: {data.Temerature} --> Associated Resistance: {data.Resistance}");
            }

            InterpolatorInputs ip = new InterpolatorInputs
            {
                EnteredTemperature = -70.2,
                MaxTemp = -70,
                MinTemp = -71,
                ResAtT2 = 1694000,
                ResAtT1 = 1821000
            };

            Console.WriteLine(
                $"Entered Temperature: {ip.EnteredTemperature}" + "\n" +
                $"Max Temperature: {ip.MaxTemp}" + "\n" +
                $"Min Temperature: {ip.MinTemp}" + "\n" +
                $"Resistance at Max Temperature: {ip.ResAtT2}" + "\n" +
                $"Resistance at Min Temperature: {ip.ResAtT1}" + "\n" +
                $"Interpolated resistance: {program.ThermistorInterpelator(ip)}"
                );
        }
    }
}
