<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalesVsAchievement.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSalesVsAchievement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <%--   <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>--%>
    


     <script type="text/javascript" language="javascript">


         $(document).ready(function () {

             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

         });

         function pageLoaded() {

             $("input, select").bind("keydown", function (event) {
                 var k1 = new KeyPress();
                 k1.textBoxHandler(event);
             });

             $('.chzn-select').chosen({ search_contains: true });
         }



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

            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            
                        </div>
                        <div class="col-md-2">
                           
                                <label class="control-label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                           
                        </div>
                        <div class="col-md-3">
                            
                                <label class="control-label">Project Name</label>
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control form-control-sm chzn-single" AutoPostBack="True" ></asp:DropDownList>
                           
                        </div>

                        <div class="col-md-2">
                           
                                <label class="control-label" >Group Name</label>
                                <asp:DropDownList ID="ddlgrp" runat="server"  CssClass="chzn-select form-control form-control-sm form-control-sm chzn-single" AutoPostBack="True"></asp:DropDownList>
                           
                        </div>

                        
                        


                        <div class="col-md-1">
                           
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-sm btn-primary" style="margin-top:28px;" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            
                        </div>



                    </div>
                </div>
            </div>

            <div class="card" style="min-height:480px;">
                <div class="card-body">
                <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server"> 
                <div class="table-responsive">
                    <div class="row">
                         <asp:GridView ID="gvsalesvscoll" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Apt Type">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Type">
                                         
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Size (sft)">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsize" runat="server"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Name of Clients">

                                       <FooterTemplate>
                                                    <asp:Label ID="lblFclients" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" ></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvclient" runat="server" style="margin-left:5px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Project Name">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFprjname" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprjname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followed By">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFfollow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfollow" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "steamdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Rate Per </br> Sft (TK)">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFRate" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status of D/P">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFstatus" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstatus" runat="server" style="margin-left:10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "downstatus")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Value">

                                        <FooterTemplate>
                                                    <asp:Label ID="lblFtvalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtvalue" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Net Value">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFNetvalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNetvalue" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netvalue")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    


                                        <asp:TemplateField HeaderText="Down Payment </br> Amount (20%)">
                                            <FooterTemplate>
                                                    <asp:Label ID="lblFdownpay" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownpay" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "downpamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>                                 

                                    <asp:TemplateField HeaderText="Received Amount">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreceive" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>  


                                     <asp:TemplateField HeaderText="Received For </br>L/O Share" Visible="false">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvreceivedlo" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreceivelo" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField> 


                                      <asp:TemplateField HeaderText="Total Received" Visible="false">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvtreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtreceive" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sales Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsaldate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "saldate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
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
                             </asp:View>

                  <asp:View ID="View2" runat="server"> 
                <div class="table-responsive">
                    <div class="row">
                         <asp:GridView ID="gvDownpayment" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Apt Type">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdowntype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Type">
                                         
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdowndesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Size (sft)">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownsize" runat="server"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Name of Clients">

                                       <FooterTemplate>
                                                    <asp:Label ID="lblFdownclients" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" ></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownclient" runat="server" style="margin-left:5px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Project Name">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFdownprjname" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownprjname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followed By">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFdownfollow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownfollow" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "steamdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Rate Per </br> Sft (TK)">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFdownRate" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownRate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status of D/P">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFdownstatus" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownstatus" runat="server" style="margin-left:10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "downstatus")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Value">

                                        <FooterTemplate>
                                                    <asp:Label ID="lblFdowntvalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdowntvalue" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Net Value">
                                         <FooterTemplate>
                                                    <asp:Label ID="lblFdownNetvalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownNetvalue" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netvalue")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    


                                        <asp:TemplateField HeaderText="Down Payment </br> Amount (20%)">
                                            <FooterTemplate>
                                                    <asp:Label ID="lblFcleardownpay" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcleardownpay" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "downpamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>                                 

                                    <asp:TemplateField HeaderText="Received Amount">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFdowngvreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdownreceive" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>  


                                  <%--   <asp:TemplateField HeaderText="Received For </br>L/O Share" Visible="false">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvreceivedlo" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreceivelo" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField> 


                                      <asp:TemplateField HeaderText="Total Received" Visible="false">
                                          <FooterTemplate>
                                                    <asp:Label ID="lblFgvtreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtreceive" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Sales Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsaldate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "saldate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                    </asp:TemplateField> 

                                      <asp:TemplateField HeaderText="Clear Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcleardate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cleardate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" ForeColor="#ff0000" />
                                        <HeaderStyle HorizontalAlign="center" />
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
                             </asp:View>
                   <asp:View ID="View3" runat="server"> 
                
                    <div class="row">
                         <asp:GridView ID="gvcolcnst" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"  AllowPaging="True"  PageSize="15" OnPageIndexChanging="gvcolcnst_PageIndexChanging">
                               <PagerSettings Position="Bottom" />
                             <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Project Name">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnstname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="190px"></asp:Label>
                                        </ItemTemplate>
                                           
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Customer Name">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnstname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                           
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit">
                                         
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnstdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                   <asp:Label ID="fgvcldtotal" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right">Total</asp:Label>
                                                  </FooterTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Opening Amount">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnstopam" runat="server"
                                               
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                   <asp:Label ID="fgvopamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                  </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText="D/Payment">

                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnstcuinsum" runat="server" style="margin-left:5px"
                                                
                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curbkam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                      <FooterTemplate>
                                                    <asp:Label ID="fgvdpamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" ></asp:Label>
                                         </FooterTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="right" />

                                    </asp:TemplateField>
                                    
                                  <asp:TemplateField HeaderText="Inst/Payment">

                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnstcuinsum" runat="server" style="margin-left:5px"
                                                
                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curinsam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                      <FooterTemplate>
                                                   <asp:Label ID="fgvinamt" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                  </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="right" />

                                    </asp:TemplateField>

                                    


                                    <asp:TemplateField HeaderText="Total Amount">

                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblcolcnsttamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                   <asp:Label ID="fgvtotalam" runat="server" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                  </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" />
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
                    
                   
                             </asp:View>
                </asp:MultiView>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>