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

//using RealEntity;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFService" in code, svc and config file together.
public class WCFService : IWCFService
{
    UserManager userManager = new UserManager();
    List<Student> Std = new List<Student>();
	public string DoWork()
	{
        return "Programmer";
	}

 

    //public List<SalesOpening> ShowSalesOpening()
    //{
    //    List<SalesOpening> lst = userManager.ShowSalesOpening();
    //    return lst;
    //}
   
    public int Addtion(int a , int b)
    {
        int c = a + b;
        return c;
        
        
        }
    //public class Pair
    //{
    //    int m_first;
    //    int m_second;

    //    public Pair()
    //    {
    //        m_first = 0;
    //        m_second = 0;
    //    }

    //    public Pair(int first, int second)
    //    {
    //        m_first = first;
    //        m_second = second;
    //    }

    //    [DataMember]
    //    public int First
    //    {
    //        get { return m_first; }
    //        set { m_first = value; }
    //    }

    //    [DataMember]
    //    public int Second
    //    {
    //        get { return m_second; }
    //        set { m_second = value; }
    //    }

    //}



    //    Pair IWCFService.Add(Pair p1, Pair p2)
    //        {
    //            Pair result = new Pair();

    //            result.First = p1.First + p2.First;
    //            result.Second = p1.Second + p2.Second;

    //            return result;
    //        }

    //    Pair IWCFService.Subtract(Pair p1, Pair p2)
    //        {
    //            Pair result = new Pair();

    //            result.First = p1.First - p2.First;
    //            result.Second = p1.Second - p2.Second;

    //            return result;
    //        }
      
    //}

   
}
