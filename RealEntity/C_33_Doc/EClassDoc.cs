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

       public string Content{get;set;}
       public string Tag{get;set;}
       public string Uid{get;set;}

       public EClassDoc() 
       {
       
       }

       //public EClassDoc(string Content, string Tag, string Uid) 
       //{
       //    this.Content = Content;
       //    this.Tag = Tag;
       //    this.Uid = Uid;
       
       //}

       
    }
}
