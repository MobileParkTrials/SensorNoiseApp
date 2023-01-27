using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoEmulator.Models
{
    internal class Sensor
    {
        public string SensorName { get; set; }
        public string SensorShortName { get; set; }
        public double SensorValue { get; set; }
        public double SensorMaxValue { get; set; }
        public double SensorMinValue { get; set; }

        public Sensor()
        {
        }

        public Sensor(Sensor sensor)
        {
            SensorName = sensor.SensorName;
            SensorShortName = sensor.SensorShortName;
            SensorValue = sensor.SensorValue;
            SensorMaxValue = sensor.SensorMaxValue;
            SensorMinValue = sensor.SensorMinValue;
        }

        public SensorData GetData()
        {
            return new SensorData()
            {
                SensorValue = Math.Round(this.SensorValue, 2),
                SensorName = this.SensorShortName
            };
        }
    }
}
