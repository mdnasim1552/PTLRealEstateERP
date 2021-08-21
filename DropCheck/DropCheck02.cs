using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

[assembly: WebResource("xMilk02.multiselect02.js", "application/x-javascript")]
[assembly: WebResource("xMilk02.DropDown2.PNG", "Image/bmp")]
namespace xMilk02
{
    [DefaultProperty("Items"),
    ToolboxData("<{0}:DropCheck runat=server></{0}:DropCheck>")]
    public class DropCheck02:System.Web.UI.WebControls.ListControl   
    {

        // need to move these inits to proper init function
        private TextBox t; //will contains the values
        //private TextBox t2; // will contains the Text
        private string title;
        private Unit width=Unit.Pixel(100);
        private string id;
        private bool transitional=true;
        private int maxDropDownHeight=200;

        #region Public Properties

        public int MaxDropDownHeight
        {
            get { return maxDropDownHeight; }
            set { maxDropDownHeight = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public override Unit Width
        {
            get { return width; }
            set { EnsureChildControls(); width = value; t.Width = width; }
        }

        public override string ID
        {
	        get { return id; }
            set {  id = value;}
        }
      
        public override void DataBind()
        {
            base.DataBind();
        }

        public override string Text
        {
            get
            {
                EnsureChildControls();
                return t.Text;
            }
            set
            {
                EnsureChildControls();
                t.Text = value;
            }
        }

        public bool TransitionalMode
        {
            get
            {
                return transitional;
            }
            set
            {
                transitional = value;
            }
        }

        #endregion

        /// <summary>
        /// Generate the DIV that is the 'drop down' pane, 
        /// which contains the checkboxes.
        /// </summary>
        /// <returns>HTML for the Div as a string</returns>
        private string GenerateDiv()
        {
            StringBuilder b = new StringBuilder();


            b.Append("<div id=\"");
            b.Append(t.UniqueID + "Parent\" style=\"width:");
            b.Append(Width.Value + 3);
            b.Append("px; position:absolute; background-color:white; z-index:10000;border:1px;visibility:hidden\"");
            b.Append(">");
            b.Append("<div id=\"");
            b.Append(t.UniqueID + "Top\" style=\"border-left-style:solid; border-left-width:1px;border-top-style:solid; border-top-width:1px;border-right-style:solid; border-right-width:1px;\"");
            b.Append(">");
            //b.Append("<table style=\"width:100%;\" cellspacing=0 cellpadding=0><tr><td><input type=\"button\" onclick=\"selectAll(curVisible)\" value=\"All\" /> </td><td> <input type=\"button\" onclick=\"selectNone(curVisible)\" value=\"None\" /> </td><td style=\"width:80%;\"> <input type=\"text\" value=\"\" style=\"width:90%;\" /> </td></tr></table>");
            b.Append("<table style=\"width:100%;\" cellspacing=0 cellpadding=0><tr><td><input id=\"" + t.UniqueID + "MainChk\" type=\"checkbox\" title=\"Check/Uncheck All\" onclick=\"handleTopCheck(this)\" /> </td><td style=\"width:95%;text-align:left\"> <input type=\"text\" onkeyup=\"searchItem(curVisible,this)\" title=\"Search...\" style=\"width:99%;font-size:11px; height:18px;\" /> </td></tr></table>");
            b.Append("</div>");
            //
            b.Append("<div id=\"");
            b.Append(t.UniqueID);
            b.Append("div\" style=\"width:");
            b.Append(Width.Value+3);
            b.Append("px; background-color:white; color:black;border-style:solid; border-width:1px; ");
            b.Append("height: expression(this.scrollHeight > ");
            b.Append(MaxDropDownHeight);
            b.Append("? '");
            b.Append(MaxDropDownHeight);
            b.Append("px' : 'auto'); max-height: ");
            b.Append(MaxDropDownHeight);
            b.Append("px; overflow:auto;\">");
            //mine code
            //b.Append("<div><input type='checkbox' value='asdf' /></div>");
            //end mine code
            b.Append(GenerateCheckboxes());  //checkboxes
            b.Append("</div>");
            b.Append("</div>");

            return b.ToString();
        }

        private string GenerateCheckboxes()
        {
            StringBuilder b = new StringBuilder();
            int k = 0;
            if(!this.DesignMode)
            {
                b.AppendFormat("<table cellpadding=0 cellspacing=0 style=\"width:100%\">");
                foreach (ListItem i in base.Items)
                {
                    b.AppendFormat("<tr id=\"" + this.UniqueID + k + "row"+ "\"><td style=\"width:20px\"><input id='" + this.UniqueID + k + "' type=\"checkbox\" value=\"{0}\" Title=\"{1}\"/></td><td><label for='" + this.UniqueID + k + "' style=\"font-family:Tahoma;width:100%;font-size:12px\">{1}<br/></label></td></tr>\n", i.Value, i.Text);
                    k++;
                }
                b.AppendFormat("</table>");
            }

            return b.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptResource(typeof(DropCheck02), "xMilk02.multiselect02.js");

            StringBuilder b = new StringBuilder();
            b.Append("function PerformPostActions(controlID) { ");
            if(this.AutoPostBack) b.Append("__doPostBack(controlID,'@@@AutoPostBack'); ");
            //b.Append(Page.GetPostBackEventReference(this, "@@@AutoPostBack"));
            b.Append(" }");
            Page.ClientScript.GetPostBackEventReference(this, "@@@AutoPostBack");
            Page.ClientScript.RegisterClientScriptBlock(typeof(DropCheck02), "autopostbackscript", b.ToString(), true);
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {

                // determine who caused the post back

                string eventArg = Page.Request["__EVENTARGUMENT"];
                string eventTarget = Page.Request["__EVENTTARGET"];

                if (eventTarget == this.ClientID && eventArg != null)
                {
                    int offset = eventArg.IndexOf("@@@AutoPostBack");

                    if (offset > -1)
                    {

                        this.OnSelectedIndexChanged(new EventArgs());
                    }

                }

            }
            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            //border-style:inset; border-width:2px; width:219px; height:20px;

            t = new TextBox();
            t.ID = ID;
            t.Width = Unit.Pixel(100);
            this.Controls.Add(t);

            //t2 = new TextBox();
            //t2.ID = Guid.NewGuid().ToString();
            //t2.Width = Unit.Pixel(100);
            //this.Controls.Add(t2);

            //base.CreateChildControls();
        }


        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            EnsureChildControls();

            //attributes of TextBox that contains selectedValues
            t.Attributes["onclick"] = "placeDiv('" + t.UniqueID + "')";
            t.Attributes["autocomplete"] = "off";
            t.Attributes["alt"] = this.ClientID;
            t.Style["border-width"] = "0px";
            t.Style["vertical-align"] = !transitional?"3px":"5px";
            t.Style["padding"] = "0px";

            //attributes of TextBox that contains selectedText
            //t2.Attributes["onclick"] = "placeDiv('" + t2.UniqueID + "')";
            //t2.Attributes["autocomplete"] = "off";
            //t2.Attributes["alt"] = this.ClientID;
            //t2.Style["border-width"] = "0px";
            //t2.Style["vertical-align"] = !transitional ? "3px" : "5px";
            //t2.Style["padding"] = "0px";
            //t2.Style["display"] = "none";

            t.Width = Unit.Pixel((int)t.Width.Value - 21) ;

            int divWidth = (int)t.Width.Value;

            //if (!transitional)
            //    divWidth += 21;
            //else
            //    divWidth += 17;

            if (!transitional)
                divWidth += 0;
            else
                divWidth += 15;

            output.Write("<div style=\"border-style:inset; overflow:hidden; background:white;border-width:2px; width:" + (int)divWidth + "px; height:" + (!transitional?"22px":"20px") + ";\">");
            output.Write("<table cellspacing=0 cellpadding=0><tr><td>");
            this.RenderChildren(output);

            output.Write("</td><td style='background:blue; background-image:url(../Image/DropDown.PNG);cursor:default;width:20px; height:20px;' onclick=\"placeDiv('" + t.UniqueID + "')\">");
            //output.Write("< style='background:black' onclick=\"placeDiv('" + t.UniqueID + "')\">s</div>");
            //output.Write("<img style=\"height:20px;width:20px;background:black;margin:0px;padding:0px;border-width:0px;\" onclick=\"placeDiv('" + t.UniqueID + "')\" src=\"");
            //output.Write(Page.ClientScript.GetWebResourceUrl(typeof(DropCheck), "DropDown2.PNG"));
            //output.Write("DropDown.PNG");
            //output.Write("></img>");
            output.Write("");
            output.Write("</td></tr></table>");
            output.Write("</div>");

           output.Write(GenerateDiv());
        }
    }
}
