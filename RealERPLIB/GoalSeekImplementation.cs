using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealERPLIB
{
    public class GoalSeekImplementation
    {
        private double SftQty;
        private double Puo;
        private double PercentOfBooking;
        private double NoofInstallment;
        private double BaseofInstallment;
        private double Ratio;
        private double RatioValue;
        private double Rate;
        private double PercntOfDownPayment;


        public GoalSeekImplementation(double sftqty, double puo, double percntofbooking, double noofinstallment, double baseofinstallment, double ratiovalue, double rate,
            double percntOfDownPayment)
        {
            SftQty = sftqty;
            Puo = puo;
            PercentOfBooking = percntofbooking;
            NoofInstallment = noofinstallment;
            BaseofInstallment = baseofinstallment;
            Ratio = 1 + ratiovalue / 12;
            Rate = rate;
            RatioValue = ratiovalue;
            PercntOfDownPayment = percntOfDownPayment;
        }

        public double GetRateFromGoalSeek(double difference, double targetDifference)
        {
            double ratioterm = 0;
            if (NoofInstallment >= BaseofInstallment)
            {
                ratioterm = 1 - Math.Pow(Ratio, BaseofInstallment);
            }
            else
            {
                ratioterm = Math.Pow(Ratio, BaseofInstallment - NoofInstallment) - Math.Pow(Ratio, BaseofInstallment);
            }

            double a = (ratioterm * (SftQty - ((PercentOfBooking + PercntOfDownPayment) * SftQty)) / ((1 - Ratio) * NoofInstallment * SftQty)) +
                (((PercentOfBooking + PercntOfDownPayment) * SftQty * Math.Pow(Ratio, BaseofInstallment)) / SftQty);

            double b = ((ratioterm * Puo - (PercentOfBooking + PercntOfDownPayment) * Puo) / ((1 - Ratio) * NoofInstallment * SftQty)) +
                (((PercentOfBooking + PercntOfDownPayment) * Puo * Math.Pow(Ratio, BaseofInstallment)) / SftQty)
                - Puo / SftQty;

            double FVsft = a * Rate + b;
            double PVsft = FVsft / Math.Pow(1 + RatioValue / 4, BaseofInstallment / 3);

            double GSPVsft = PVsft - (difference + targetDifference);
            double GSFVsft = GSPVsft * Math.Pow(1 + RatioValue / 4, BaseofInstallment / 3);

            double GSRate = (GSFVsft - b) / a;
            return GSRate;
        }


    }
}
