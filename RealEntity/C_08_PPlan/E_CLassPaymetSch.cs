using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEntity.C_08_PPlan
{
    public class E_CLassPaymetSch
    { 
        [Serializable]
        public class OwnerInstallment
        {
            public DateTime insdate { get; set; }
            public double insamt { get; set; }

            public OwnerInstallment()
            {
                
            }
            public OwnerInstallment (DateTime _insdate,double _insamt )
            {
                this.insdate = _insdate;
                this.insamt = _insamt;
            }
        }
       
        
        [Serializable]
        public class ProjectName
        {
            public string actcode { get; set; }
            public string  actdesc { get; set; }

            public ProjectName ( ) { }

            public ProjectName(string _actcode, string _actdesc)
            {
                this.actcode = _actcode;
                this.actdesc = _actdesc;
            }
        }

        [Serializable]
        public class ResourceDes
        {
            public string sircode { get; set; }
            public string sirdesc { get; set; }

            public ResourceDes ( ) { }

            public ResourceDes ( string _sircode, string _sirdesc )
            {
                this.sircode = _sircode;
                this.sirdesc = _sirdesc;
            }
        }
    }
}
