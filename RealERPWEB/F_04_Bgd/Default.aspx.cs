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
    public partial class Default : System.Web.UI.Page
    {

        #region Property

        public DataTable DataSource
        {
            get;
            set;
        }


        #endregion


        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.CommonLoad();

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void CommonLoad()
        {

            string comcod = this.GetCompCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMPANY", "", "", "", "", "", "", "", "", "");
            DataSource = ds1.Tables[0];
            PopulateTree(MyTreeView);
        }

        private void PopulateTree(TreeView objTreeView)
        {
            if (DataSource != null)
            {
                foreach (DataRow dataRow in DataSource.Rows)
                {
                    if (dataRow["departmentidparent"] == DBNull.Value)
                    {
                        TreeNode treeRoot = new TreeNode();
                        treeRoot.Text = dataRow["name"].ToString();
                        treeRoot.Value = dataRow["departmentid"].ToString();
                        treeRoot.Expanded = false;
                        objTreeView.Nodes.Add(treeRoot);


                        foreach (TreeNode childnode in GetChildNode
                       (Convert.ToString(dataRow["departmentid"])))
                        {
                            treeRoot.ChildNodes.Add(childnode);
                        }
                    }
                }
            }
        }

        private TreeNodeCollection GetChildNode(string parentid)
        {
            TreeNodeCollection childtreenodes = new TreeNodeCollection();
            DataView dataView1 = new DataView(DataSource);
            String strFilter = ("departmentidparent='" + parentid.ToString() + "'");// ("departmentidparent ='" + parentid.ToString() + '");
            dataView1.RowFilter = strFilter;

            if (dataView1.Count > 0)
            {
                foreach (DataRow dataRow in dataView1.ToTable().Rows)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = dataRow["name"].ToString();
                    childNode.Value = dataRow["departmentid"].ToString();
                    //childNode.ImageUrl = "~/Images/oInboxF.gif";
                    //  childNode.ExpandAll()=false;

                    foreach (TreeNode cnode in GetChildNode
            (Convert.ToString(dataRow["departmentid"])))
                    {
                        childNode.ChildNodes.Add(cnode);
                    }
                    childtreenodes.Add(childNode);
                }
            }
            return childtreenodes;
        }


        //private void Form19_Load(object sender, EventArgs e)
        //{

        //    DataTable dt = new DataTable("data");

        //    dt.Columns.Add("id", typeof(int));

        //    dt.Columns.Add("ParentId", typeof(int));

        //    dt.Columns.Add("description");

        //    dt.Columns.Add("link");

        //    dt.Rows.Add(1, null, "Root", "www.microsoft.com");

        //    dt.Rows.Add(2, 1, "Child1", "www.microsoft.com");

        //    dt.Rows.Add(3, 1, "Child2", "www.microsoft.com");

        //    dt.Rows.Add(4, 2, "GrandChild1", "www.microsoft.com");

        //    dt.Rows.Add(5, 2, "GrandChild2", "www.microsoft.com");

        //    dt.Rows.Add(6, 2, "GrandChild3", "www.microsoft.com");

        //    dt.Rows.Add(7, 3, "GrandChild4", "www.microsoft.com");

        //    dt.Rows.Add(8, 3, "GrandChild5", "www.microsoft.com");



        //    //Use a DataSet to manage the data

        //    DataSet ds = new DataSet();

        //    ds.Tables.Add(dt);

        //    //add a relationship

        //    ds.Relations.Add("rsParentChild", ds.Tables["data"].Columns["id"],

        //        ds.Tables["data"].Columns["ParentId"]);



        //    foreach (DataRow dr in ds.Tables["data"].Rows)
        //    {

        //        if (dr["ParentId"] == DBNull.Value)
        //        {

        //            TreeNode root = new TreeNode(dr["description"].ToString());
        //            root.Tag = dr["link"].ToString();

        //            this.MyTreeView.Nodes.Add(root);

        //            PopulateTree(dr, root);

        //        }

        //    }

        //    MyTreeView.ExpandAll();

        //}



        //public void PopulateTree(DataRow dr, TreeNode pNode)
        //{

        //    foreach (DataRow row in dr.GetChildRows("rsParentChild"))
        //    {

        //        TreeNode cChild = new TreeNode(row["description"].ToString());

        //       // cChild.Tag = row["link"].ToString();

        //       // pNode.Nodes.Add(cChild);

        //        //Recursively build the tree

        //        PopulateTree(row, cChild);

        //    }

        //}
        protected void lbtnPrintAnaLysis_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string treevel = this.MyTreeView.SelectedValue.ToString();
        }
        protected void MyTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}