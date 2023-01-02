<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryPrjCollSumAdj.aspx.cs" Inherits="RealERPWEB.F_23_CR.EntryPrjCollSumAdj" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .btn-space {
    margin-right: 120px;
}
         .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
         .table th, .table td{
             padding:4px;
         }
    </style>
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
    });

    function pageLoaded() {
        $('.chzn-select').chosen({ search_contains: true });
           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>
    }

   
</script>


    }

    <div class="card">
        <div class="card-header">
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
            <div class="row mt-3">
                
                       
                            <div class="col-md-3 d-none">

                                <asp:Label ID="Label5" runat="server"
                                    CssClass="lblTxt lblName" Text=" Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"
                                    Font-Bold="True"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-2">
                                 <asp:Label ID="Label1" runat="server"
                                    CssClass="form-label" Text=" Name:"></asp:Label>
                                <asp:DropDownList ID="ddlProjectName" CssClass="form-control form-control-sm chzn-select" runat="server" Font-Bold="True"
                                   >
                                </asp:DropDownList>
                               <%-- <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>--%>

                                
                            </div>
                            <div class="col-md-1  pull-left" style="margin-top:22px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>



                       
                      
                            <div class="col-md-1">

                                <asp:Label ID="lblPage0" runat="server" CssClass="form-label" Text="Size:" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                               
                            <div class="col-md-1">
                                <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Month:"></asp:Label>

                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass="form-control form-control-sm">
                                </asp:DropDownList>

                                


                            </div>
                           
                        


                                 <%--<asp:Label ID="Label7" runat="server" CssClass=" lblTxt lblName" Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>--%>

                                
                                       <%-- <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date:"> </asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>--%>

                            </div>
                     

                    
                </div>
               
        <div class="card-body">
            <div class="row">
                <div class="table table-responsive">
                    <asp:GridView ID="gvprjstatus" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvprjstatus_PageIndexChanging"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="prjadjcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="prjadjcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjadjcode")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                      <asp:TemplateField HeaderText=" Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPrjcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjcode")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                             

                             <asp:TemplateField HeaderText="Pjrcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>

                                  <FooterTemplate>
                                            <asp:LinkButton ID="btnAdjTotal" runat="server" OnClick="btnAdjTotal_Click" CssClass="btn btn-xs btn-primary primaryBtn btn-space">Total</asp:LinkButton>
                                    
                                           <asp:LinkButton ID="lnkFiUpdate" runat="server"  CssClass="btn btn-xs btn-danger primaryBtn" onClick="lnkFiUpdate_Click" >Update</asp:LinkButton>
                                         
                                  </FooterTemplate>


                                <HeaderStyle HorizontalAlign="Left" />
                               

                            </asp:TemplateField>

                            



                       

                             <asp:TemplateField HeaderText="Collection Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvmramt" runat="server" Style="text-align: right" BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mramt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>


                                <FooterTemplate>
                                    <asp:Label ID="lgvFmramt" runat="server" Font-Bold="True" Font-Size="12px"
                                           ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>

                               </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                 <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            
                        

                           
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>
      
    </div>
        </div>

</asp:Content>


