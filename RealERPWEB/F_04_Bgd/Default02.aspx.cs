using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;




using RealERPLIB;
using RealERPRPT;


namespace RealERPWEB.F_04_Bgd
{
    public partial class Default02 : System.Web.UI.Page
    {
        ClassDerived objder = new ClassDerived();
        ProcessAccess Data = new ProcessAccess();

        public class Product
        {
            private int? _Id;
            private string _Name;
            private string _Descrition;

            public Product() { }

            public Product(int Id, string Name, string Description)
            {
                this._Id = Id;
                this._Name = Name;
                this._Descrition = Description;
            }


            public int? Id
            {
                get { return _Id; }
                set { _Id = value; }
            }

            /// <summary>
            /// Product Name
            /// </summary>
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }


            public string Description
            {
                get { return _Descrition; }
                set { _Descrition = value; }
            }
        }


        public class ProductList
        {
            private IList<Product> _ProductDB = new List<Product>();

            public ProductList()
            {
                this._ProductDB.Add(new Product(1, "Computer", "Complete hardware with software included."));
                this._ProductDB.Add(new Product(2, "Kitchen Calendar", "Beautiful caledar for your kitchen."));
                this._ProductDB.Add(new Product(3, "Shoes", "Most advanced anti-impact system in a small shoe."));
                this._ProductDB.Add(new Product(4, "Pen", "What you think, must be written. This pen is your helper."));
                this._ProductDB.Add(new Product(5, "Cell Phone", "Powerfull comunication thing. Today is part of your body. Get one more."));
                this._ProductDB.Add(new Product(6, "Computer", "Complete hardware with software included."));
                this._ProductDB.Add(new Product(7, "Kitchen Calendar", "Beautiful caledar for your kitchen."));
                this._ProductDB.Add(new Product(8, "Shoes", "Most advanced anti-impact system in a small shoe."));
                this._ProductDB.Add(new Product(9, "Pen", "What you think, must be written. This pen is your helper."));
                this._ProductDB.Add(new Product(10, "Cell Phone", "Powerfull comunication thing. Today is part of your body. Get one more."));
                this._ProductDB.Add(new Product(11, "Computer", "Complete hardware with software included."));
                this._ProductDB.Add(new Product(12, "Kitchen Calendar", "Beautiful caledar for your kitchen."));
                this._ProductDB.Add(new Product(13, "Shoes", "Most advanced anti-impact system in a small shoe."));
                this._ProductDB.Add(new Product(14, "Pen", "What you think, must be written. This pen is your helper."));
                this._ProductDB.Add(new Product(15, "Cell Phone", "Powerfull comunication thing. Today is part of your body. Get one more."));
            }

            public IList<Product> GellAll()
            {
                return this._ProductDB;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            this.CallOveride();


            //if (!IsPostBack)
            //{
            //    DataSet ds1 = Data.GetTransInfo("3305", "SP_ENTRY_SALSMGT", "TEST", "", "", "", "", "", "", "", "", "");
            //    Session["Value"] = ds1.Tables[0];
            //    Data_Bind();
            //}

        }

        private void CallOveride()
        {

            double res = objder.VirtualMethod(20);


        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {


        }




        protected void DataPagerProducts_PreRender(object sender, EventArgs e)
        {
            ProductList db = new ProductList();
            this.ListViewProducts.DataSource = db.GellAll();
            this.ListViewProducts.DataBind();
        }


        private void Data_Bind()
        {
            this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            gvReqStatus.DataSource = Session["Value"];
            gvReqStatus.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnOk0_Click(object sender, EventArgs e)
        {






            //Collection
            //ArrayList alist = new ArrayList();
            //alist.Add("emdad");
            //alist.Add("shobuz");
            //string name="";
            //foreach(string a1 in alist)
            //   name=name+a1;

            //Generic
            //this.lblcollection.Text = name;
            //List<string> glist = new List<string>();
            //glist.Add("emdad");
            //glist.Add("shobuz");
            //string name="";
            //foreach ( string fglist in glist) 
            //name = name + fglist;
            //this.lblcollection.Text = name;
            List<classracer> cracer = new List<classracer>();
            cracer.Add(new classracer("emdaad", "fzr"));
            cracer.Add(new classracer("shobuz", "parADO"));
            string name = "";
            string car = "";
            foreach (classracer r in cracer)
            {
                name = r.Name;
                car = r.Car;

            }
            //classracer objracer = new classracer();
            classracer objracer = new classracer("rahim", "jip");


            //    name = name + r;
            //this.lblcollection.Text = name;

            name = objracer.Name; ;
            car = objracer.Car;






        }


        private void TreeNode()
        {


        }





        protected void MyTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}