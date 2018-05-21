using FileHelpers;
using System;
using System.Linq;

namespace scratchpadConsole
{

    public struct InterpolatorInputs
    {
        public double EnteredTemperature { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double ResAtMaxTemp { get; set; }
        public double ResAtMinTemp { get; set; }
    }

    public class Program
    {

        public decimal ThermistorInterpelator(InterpolatorInputs ips) => Convert.ToDecimal(
                (ips.ResAtMaxTemp - ips.ResAtMinTemp) / (ips.MaxTemp - ips.MinTemp) *
                (ips.EnteredTemperature - ips.MinTemp) + ips.ResAtMinTemp);

        static void Main()
        {
            Program program = new Program();

            var engine = new FileHelperEngine<ResistanceData>();
            var omega = engine.ReadFile("44006_44031.csv");
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
                ResAtMaxTemp = 1694000,
                ResAtMinTemp = 1821000
            };

            InterpolatorInputs ip2 = new InterpolatorInputs();
            double EnteredTemperature = -75.5;
            ip2.EnteredTemperature = EnteredTemperature;
            ip2.MaxTemp = Math.Ceiling(EnteredTemperature);
            ip2.MinTemp = Math.Floor(EnteredTemperature);
            ip2.ResAtMaxTemp = omega.FirstOrDefault(x => x.Temerature == ip2.MaxTemp).Resistance;
            ip2.ResAtMinTemp = omega.FirstOrDefault(x => x.Temerature == ip2.MinTemp).Resistance;


            Console.WriteLine(
                $"Entered Temperature: {ip.EnteredTemperature}" + "\n" +
                $"Max Temperature: {ip.MaxTemp}" + "\n" +
                $"Min Temperature: {ip.MinTemp}" + "\n" +
                $"Resistance at Max Temperature: {ip.ResAtMaxTemp}" + "\n" +
                $"Resistance at Min Temperature: {ip.ResAtMinTemp}" + "\n" +
                $"Interpolated resistance: {program.ThermistorInterpelator(ip)}"
                );
            Console.WriteLine();

            Console.WriteLine(
                $"Entered Temperature: {ip2.EnteredTemperature}" + "\n" +
                $"Max Temperature: {ip2.MaxTemp}" + "\n" +
                $"Min Temperature: {ip2.MinTemp}" + "\n" +
                $"Resistance at Max Temperature: {ip2.ResAtMaxTemp}" + "\n" +
                $"Resistance at Min Temperature: {ip2.ResAtMinTemp}" + "\n" +
                $"Interpolated resistance: {program.ThermistorInterpelator(ip2)}"
                );
        }
    }
}
