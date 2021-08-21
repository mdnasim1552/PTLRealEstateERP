<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ShowAllDoc.aspx.cs" Inherits="RealERPWEB.F_33_Doc.ShowAllDoc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .txtboxformat
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    font-size: 12px;
	    font-weight: normal;
	margin-right: 0px;
   text-align: left;
    margin-left: 0px;
}
        .style59
        {
            width: 75px;
        }
        .style60
        {
            width: 86px;
        }
        .style61
        {
            width: 333px;
        }
        .style62
        {
            width: 5px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script type="text/javascript">


        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            try {
              
                $('.chzn-select').chosen({ search_contains: true });
                
            }
            catch(e) {
                alert(e);
            }
         
       };

    </script> 
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
             <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                    <fieldset class="scheduler-border fieldset_A">
                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:Label ID="Label6" runat="server" CssClass="lblName smLbl_to" Font-Size="12px" Text="Document Type" Width="100px"></asp:Label>
                                  
                                       <%-- <asp:TextBox ID="txtSrcType" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Width="80px"></asp:TextBox>
                                   
                                        <asp:ImageButton ID="imgbtnFindType" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindType_Click" 
                                            Width="16px" />--%>
                                   
                                        <asp:DropDownList ID="ddlDocumentType" runat="server" AutoPostBack="True"  CssClass="form-control chzn-select"
                                            Font-Bold="True" Font-Size="12px" 
                                            onselectedindexchanged="ddlDocumentType_SelectedIndexChanged" Width="220px">
                                        </asp:DropDownList>
                                </div>
                            </div>
                             <div class="form-group">
                                 <div class="col-md-4 pading5px asitCol4">
                                      <asp:Label ID="Label5" runat="server" Font-Bold="True" CssClass="lblName" Text="Document Name:"  Font-Size="12px"></asp:Label>
                                  
                                        <%--<asp:TextBox ID="txtSrcDoc" runat="server" 
                                            Font-Bold="True" Width="80px" BorderStyle="None"></asp:TextBox>
                                   
                                        <asp:ImageButton ID="imgbtnFindDoc" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                                            onclick="imgbtnFindDoc_Click" />--%>
                               
                                        <asp:DropDownList ID="ddlDocumentName" runat="server" Font-Bold="True" CssClass="form-control chzn-select"
                                            Font-Size="12px" Width="220px" AutoPostBack="True">
                                        </asp:DropDownList>
                                        
                                 </div>
                                 <div class="col-md-1">
                                      <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" Font-Size="12px" onclick="lbtnOk_Click">Ok</asp:LinkButton>
                                 </div>
                                 <div class="col-md-3 pading5px asitCol3 pull-right">
                                     <asp:Label ID="lblPage" runat="server" CssClass="lblName smLbl_to"   Text="Page Size:"></asp:Label>
                               
                             
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                             Font-Bold="True" Font-Size="12px"
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="80px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                 </div>

                             </div>        

                        <fieldset/>
                   </div>
                    <div class="row">
                            
                                <asp:GridView ID="gvDoc" runat="server" AutoGenerateColumns="False"  CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="785px" onrowcommand="gvDoc_RowCommand" 
                                    DataKeyNames="id" AllowPaging="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvID" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>' 
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                          
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGdesc" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                    Text='<%#  "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                  (DataBinder.Eval(Container.DataItem, "gdesc1").ToString().Trim().Length > 0 ? 
                                                  (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                  Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")).Trim() : "")%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSname" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" Font-size="11px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srtname")) %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgDetDesc" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "details")) %>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgDate" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" 
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "imgdate")).ToString("dd-MMM-yyyy") %>' 
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgFname" runat="server" AutoCompleteType="Disabled" 
                                                    BackColor="Transparent" BorderStyle="None" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "filename")) %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                         <%--<asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/GetImage.aspx?ImgID="+ Eval("id") %>' Height="120px" Width="120px"/>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Download">
                                            <ItemTemplate>
                                                <asp:HyperLink id="lnkDownload"  Text="Download" runat="server" Target="_blank" 
                                                    NavigateUrl='<%# "~/DownloadDoc.aspx?ImgID="+Eval("id")+"&ActionType=Download" %>' 
                                                    ondatabinding="lnkDownload_DataBinding"></asp:HyperLink>   
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <asp:ButtonField ButtonType="Image"  
                                            ImageUrl="~/Image/download.png" 
                                            CommandName="Download" 
                                            HeaderText="Download" />

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                  <div />
             <div />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

