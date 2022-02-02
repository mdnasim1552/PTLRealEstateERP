using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealERPLIB
{
    public class PTLCommon
    {


        public class Purchase
        {

            public string _mrf { get; set; }
            public string _refno { get; set; }
            public Purchase()
            {

            }
            public Purchase(string mrf, string refno)
            {
                this._mrf = mrf;
                this._refno = refno;
            }

            public Purchase(string comcod)
            {
                string mrf, refno;
                switch (comcod)
                {
                    case "3101":
                    case "3364":
                        mrf = "MPR No";
                        refno = "REF No";
                        break;
                    default:
                        mrf = "MRF No";
                        refno = "REF No";
                        break;
                }

                this._mrf = mrf;
                this._refno = refno;
            }
        }
        //public List<Purchase> GetPurhase(string comcod)
        //{

        //    List<Purchase> lst = new List<Purchase>();
        //    string mrfno, refno;

        //    switch (comcod)
        //    {

        //        case "3101":
        //            mrfno = "MRF NO";
        //            break;
        //        default:
        //            refno = "MRF NO";
        //            break;
        //    }

        //    lst.Add(new Purchase { mrfno,refno });

        //    return lst;


        //}




        // public string _mrnno = "MRF No";



        //public string mrfno
        //{
        //    get { return _mrnno; }


        //    set { _mrnno = value; }


        //}


    }
}
