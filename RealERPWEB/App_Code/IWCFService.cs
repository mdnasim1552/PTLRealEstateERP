using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using RealEntity;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFService" in both code and config file together.
[ServiceContract]
public interface IWCFService
{
	[OperationContract]
	string  DoWork();

    [OperationContract]
    int Addtion(int a, int b);

 
    //List<SalesOpening> ShowSalesOpening();

   
    
    //[OperationContract]
    //Pair Subtract(Pair p1, Pair p2);
    
}
