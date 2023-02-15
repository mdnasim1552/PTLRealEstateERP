<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryFxtasstIssue.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.EntryFxtasstIssue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
           

            });

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    <style>
        .mt20 {
            margin-top:20px;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <div class="card mt-3 mb-1">
        <div class="card-body p-1">
            <div class="row">
                <div class="col-md-1">
                    <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                        Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                </div>
                <div class="col-md-1">                    
                    <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="Issue No "></asp:Label>
                    <asp:TextBox ID="txtIssueNo" runat="server" CssClass="form-control form-control-sm" TabIndex="3" ReadOnly="true"></asp:TextBox>  
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                    <asp:DropDownList ID="ddldeptName" runat="server"  CssClass="chzn-select form-control form-control-sm" TabIndex="6" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblDeptDesc" runat="server" CssClass="dataLblview" Visible="False" Width="250px"></asp:Label>
                </div>    
                <div class="col-md-1">
                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" TabIndex="4" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                </div>
            </div>
            <div class="row" runat="server" id="divreslist" visible="false">
                <div class="col-md-2">
                    <asp:Label ID="lblResList" runat="server" CssClass=" lblName lblTxt" Text="Resource List"></asp:Label>
                    <asp:DropDownList ID="ddlreslist" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>                                
                </div>
                <div class="col-md-1">
                     <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkselect_Click">Add</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div class="card card-fluid container-data">
        <div class="card-body mt-0 mb-0 pb-0" style="min-height:500px">
            <div class="row mb-0 pb-0 table-responsive" >
                <asp:GridView ID="gvEmpIssue" runat="server" AutoGenerateColumns="False" CssClass="table-striped  table-bordered grvContentarea"
                        ShowFooter="True" OnRowEditing="gvEmpIssue_RowEditing" OnRowUpdating="gvEmpIssue_RowUpdating"
                         OnRowCancelingEdit="gvEmpIssue_RowCancelingEdit">
                        <RowStyle />
                        <Columns>                   
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                        Style="text-align: center"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Serial#">
                                <ItemTemplate>
                                    <asp:Label ID="lgcSlCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText=" Floor">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtfloor" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floorno")) %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:LinkButton ID="TblTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="TblTotal_Click" visible="false" >Total</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>                            
                             <asp:CommandField ShowEditButton="True" EditText="&lt;span class='fa fa-edit'&gt;&lt;/span&gt;" HeaderStyle-Width="30px"/>
                            <asp:TemplateField HeaderText=" User Name">
                                 <EditItemTemplate>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table style="width: 100%;">
                                            <tr>                                              
                                                <td>
                                                    <asp:DropDownList ID="ddlemp" runat="server" CssClass="ddlistPull chzn-select" Width="200px" AutoPostBack="true"  TabIndex="6">
                                                    </asp:DropDownList>
                                                </td> 
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>                                
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblusrname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:LinkButton ID="lUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdate_Click1" Visible="false">Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                           
                                    <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lbldig" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                     
                               <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescription" runat="server" BorderStyle="none"                                        
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>                                  
                                <asp:TemplateField HeaderText="Bal Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblbalqty" runat="server" BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>                                       
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtqty" runat="server" BorderStyle="None" style="text-align:right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>                                       
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                    
                                      <asp:TemplateField HeaderText="Model#">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblmodel" runat="server" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "modelno")) %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Asset id </br> code">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblidcode" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetidcode")) %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 
                             <asp:TemplateField HeaderText="Date of</br>Purchase">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvdate" runat="server" BorderStyle="none"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "purchasedate"))%>'
                                        Width="70px"></asp:TextBox>                                
                                    <cc1:CalendarExtender ID="txtgvdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvdate"></cc1:CalendarExtender>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                
                                  <asp:TemplateField HeaderText="Estimated Life">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblestimated" runat="server" BorderStyle="none"                                        
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estimatedlife")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Purchase Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtpurprice" runat="server" BorderStyle="none" style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purchaseprice")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>                                
                               <asp:TemplateField HeaderText="Rate of Depreciation">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate" runat="server" BorderStyle="none" 
                                        Text='<%#(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedepreciation"))==0.00)? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedepreciation")).ToString("#,##0.00;(#,##0.00); "): Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedepreciation")).ToString("#,##0.00;(#,##0.00); ") + "%"  %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accumulated  Depreciation">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAccmulated" runat="server" BorderStyle="none" style="text-align:right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accudepreciation")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date of Depreciation">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvdepre" runat="server" BorderStyle="none"  style="text-align:center"  
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depreciationdate")) %>'
                                        Width="80px"></asp:TextBox>
                                     <cc1:CalendarExtender ID="txtgvdepre_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvdepre"></cc1:CalendarExtender>
                                </ItemTemplate>
                                </asp:TemplateField>
                         <%--   <asp:TemplateField HeaderText="Current year Depreciation">
                                <ItemTemplate>
                                    <asp:Label ID="txtcurrent" runat="server" BorderStyle="none"  style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "currentyear")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Adjustment /Disposal">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtadjustment" runat="server" BorderStyle="none"  style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjustment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Wdv">
                                <ItemTemplate>
                                    <asp:Label ID="txtwdv" runat="server" BorderStyle="none" style="text-align:right"   
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wdv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Warranty">
                             <ItemTemplate>
                                    <asp:TextBox ID="lblwarranty" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "warranty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>     
                                <HeaderStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="lblremarks" runat="server" BorderStyle="none" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>                                
                        </Columns>
                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>
            </div>
        </div>
    </div>


   

</asp:Content>



