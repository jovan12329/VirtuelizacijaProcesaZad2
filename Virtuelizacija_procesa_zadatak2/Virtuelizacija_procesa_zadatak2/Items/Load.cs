using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuelizacija_procesa_zadatak2.Items
{
    
    public class Load
    {
        private static int idCntL = 0;
        private int id;
        private DateTime timeStamp;
        private double forecastValue;
        private double measuredValue;

        public Load() { }

        public Load(DateTime timeStamp, double forecastValue, double measuredValue)
        {
            this.Id = ++idCntL;
            this.TimeStamp = timeStamp;
            this.ForecastValue = forecastValue;
            this.MeasuredValue = measuredValue;
        }

        
        public int Id { get => id; private set => id = value; }
        
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        
        public double ForecastValue { get => forecastValue; set => forecastValue = value; }
        
        public double MeasuredValue { get => measuredValue; set => measuredValue = value; }

        public override string ToString()
        {
            return TimeStamp.ToString().Split(' ')[0]+","+TimeStamp.ToString().Split(' ')[1].Split(':')[0]+":"+ TimeStamp.ToString().Split(' ')[1].Split(':')[1] + "," + forecastValue + "," + MeasuredValue;
        }


    }
}
