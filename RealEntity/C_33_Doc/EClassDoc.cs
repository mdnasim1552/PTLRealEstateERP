using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_33_Doc
{

    [Serializable]
    public class EClassDoc
    {

        public string Content { get; set; }
        public string Tag { get; set; }
        public string Uid { get; set; }

        public EClassDoc()
        {

        }

    }
    [Serializable]
    public class ActiveSimUser
    {
        public string comcod { get; set; }
        public string empid { get; set; }
        public string empname { get; set; }
        public string actiondate { get; set; }
        public string simop { get; set; }
        public string mobileno { get; set; }
        public string remarks { get; set; }
        public ActiveSimUser() { }
    }
}
