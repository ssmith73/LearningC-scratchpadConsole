using FileHelpers;
using System;
using System.Linq;
using Interpolation;

namespace scratchpadConsole
{


    public class Program
    {


        static void Main()
        {
            Program program = new Program();

            /*! .  

                B equation: T = B / ln(R/R#)

                  Where R# = R0e(-B/T0)
                  Where R0 = Resistance at T0
                  Where T0 = Temperature at 25'C (298.15K)

                Steinhart-Hart equation: 1/T = a + b.ln(R) + c(ln(R))3

                  T = absolute-temperature, R is the associated thermistor resistance
                  Where A,B,C are Steinhart-Hart parameters, specific for
                  a given thermistor

             */

            // Initialize the interpolation manager using the sample data
            InterpolationManager im = 
                new InterpolationManager("Panasonic_Thermistor.csv");

            // Interpolate using the gixen X axis, using the given Y axis, 
            // and using the given X value (returns the associated Y value)
            double val = im.Interpolate("Temperature (degC)", "Resistance (ohms)", 22);
            

            // Interpolate ASSSUMING the first column is the X axis,
            // using the given Y axis, and using the given X value (returns the associated Y value)
            double val2 = im.Interpolate("Resistance (ohms)", 50);

            // Interpolate ASSSUMING the first column is the X axis, 
            // ASSUMING the second column is the Y axis, and using the 
            // given X value (returns the associated Y value)
            double val3 = im.Interpolate(111);



            #region LINEAR INTERPOLATION - FROM EXCEL EQUATION
            // Extract linear interpolation
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
            double EnteredTemperature = -70.44;
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

            #endregion
        }
        public decimal ThermistorInterpelator(InterpolatorInputs ips) => Convert.ToDecimal(
                (ips.ResAtMaxTemp - ips.ResAtMinTemp) / (ips.MaxTemp - ips.MinTemp) *
                (ips.EnteredTemperature - ips.MinTemp) + ips.ResAtMinTemp);
    }
    public struct InterpolatorInputs
    {
        public double EnteredTemperature { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double ResAtMaxTemp { get; set; }
        public double ResAtMinTemp { get; set; }
    }
}
