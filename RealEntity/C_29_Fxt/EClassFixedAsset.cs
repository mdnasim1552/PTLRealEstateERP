using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_29_Fxt
{
    public class EClassFixedAsset
    {
        [Serializable]
        public class EClassFixedAssetTransfer
        {
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string sirunit { get; set; }
            public double qty { get; set; }
            public double balqty { get; set; }


            public EClassFixedAssetTransfer() { }

        }

        [Serializable]
        public class EClassDepricationCost
        {
            public string comcod { get; set; }
            public string actcode { get; set; }
            public double opnam { get; set; }
            public double curam { get; set; }
            public double disam { get; set; }
            public double saleam { get; set; }
            public double toam { get; set; }
            public double dcharge { get; set; }
            public double opndep { get; set; }
            public double adjam { get; set; }
            public double curdep { get; set; }
            public double todep { get; set; }
            public double clsam { get; set; }
            public string actdesc { get; set; }


            public EClassDepricationCost() { }



        }

        [Serializable]
        public class EClassFixedAssetRegister
        {

            public string comcod { get; set; }
            public string pactode { get; set; }
            public string dept { get; set; }
            public double qty { get; set; }
            public string assetid { get; set; }
            public string rsircode { get; set; }

            public string rsircatcode { get; set; }
            public string slno { get; set; }
            public string empid { get; set; }
            public string dgcod { get; set; }
            public string assetidcode { get; set; }
            public string purdat { get; set; }

            public double prate { get; set; }
            public double pvalu { get; set; }
            public double salval { get; set; }
            public double bookval { get; set; }
            public double depreamt { get; set; }

            public string sirdesc1 { get; set; }
            public string assetnam { get; set; }
            public string punit { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }


            public EClassFixedAssetRegister() { }


        }

        [Serializable]
        public class EClassMaterialStock
        {
            public string comcod { get; set; }
            public string pactcode { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public double opnqty { get; set; }
            public double opnrate { get; set; }
            public double opnam { get; set; }
            public double recqty { get; set; }
            public double recam { get; set; }
            public double tinqty { get; set; }
            public double tinam { get; set; }
            public double toutqty { get; set; }
            public double toutam { get; set; }
            public double lqty { get; set; }
            public double lam { get; set; }
            public double sqty { get; set; }
            public double sam { get; set; }
            public double dqty { get; set; }
            public double dam { get; set; } 
            public double issueqty { get; set; }
            public double issueam { get; set; }
            public double stkqty { get; set; }
            public double stkam { get; set; }
            public double stkrate { get; set; }
            public string pactdesc { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }

            public EClassMaterialStock() { }

        }

        [Serializable]
        public class FixedAssetsStatus
        {
            public string pactdesc { get; set; }
            public string rsirdesc { get; set; }
            public string resunit { get; set; }
            public string rcvdate { get; set; }
            public string trnsdate { get; set; }
            public double rqty { get; set; }
            public double tqty { get; set; }
            public double balqty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public FixedAssetsStatus() { }
        }

        [Serializable]
        public class EquipmentUseStatus
        {
            public string recpdesc { get; set; }
            public string codedesc { get; set; }
            public string resdesc { get; set; }
            public string rcvdate { get; set; }
            public string issuedesc { get; set; }
            public string issuedate { get; set; }
            public string refunddesc { get; set; }
            public string refunddat { get; set; }
            public double recqty { get; set; }
            public double issueqty { get; set; }
            public double refundqty { get; set; }
            public double balqty { get; set; }
            public double amt { get; set; }
            public EquipmentUseStatus() { }
        }

        [Serializable]
        public class FixedAssetRegister
        {
            public string rsircode { get; set; }
            public string slno { get; set; }
            public string empid { get; set; }
            public string floorno { get; set; }
            public string modelno { get; set; }
            public string assetidcode { get; set; }
            public DateTime purchasedate { get; set; }
            public string estimatedlife { get; set; }
            public string categoryid { get; set; }
            public double warranty { get; set; }
            public double purchaseprice { get; set; }
            public double rate { get; set; }
            public double accudepreciation { get; set; }
            public string sirdesc { get; set; }
            public DateTime depreciationdate { get; set; }
            public double currentyear { get; set; }
            public double adjustment { get; set; }
            public double wdv { get; set; }
            public string remarks { get; set; }
            public string empname { get; set; }
            public string desigid { get; set; }
            public string secid { get; set; }
            public string catedesc { get; set; }
            public string designation { get; set; }
            public string section { get; set; }
            public FixedAssetRegister() { }
        }

        [Serializable]
        public class RptDeptITStock
        {
            public string companycode { get; set; }
            public string deptno { get; set; }
            public string empid { get; set; }
            public string desigid { get; set; }
            public string companyname { get; set; }
            public string deptname { get; set; }
            public string empname { get; set; }
            public string desig { get; set; }
            public string idcardno { get; set; }
            public double p1 { get; set; }
            public double p2 { get; set; }
            public double p3 { get; set; }
            public double p4 { get; set; }
            public double p5 { get; set; }
            public double p6 { get; set; }
            public double p7 { get; set; }
            public double p8 { get; set; }
            public double p9 { get; set; }
            public double p10 { get; set; }
            public double p11 { get; set; }
            public double p12 { get; set; }
            public double p13 { get; set; }
            public double p14 { get; set; }
            public double p15 { get; set; }
            public double p16 { get; set; }
            public double p17 { get; set; }
            public double p18 { get; set; }
            public double p19 { get; set; }
            public double p20 { get; set; }
            public double total { get; set; }
            public RptDeptITStock() { }
        }
    }
}
