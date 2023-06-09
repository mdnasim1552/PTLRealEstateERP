﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalesVsCollTeamWise.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSalesVsCollTeamWise" %>
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

             var gv1 = $('#<%=this.gvsalesvscoll.ClientID %>');
             gv1.Scrollable();

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
                            
                                <label class="control-label">Executive Name</label>
                                <asp:DropDownList ID="ddlSalesperson" runat="server" CssClass="chzn-select form-control form-control-sm chzn-single" AutoPostBack="True" ></asp:DropDownList>
                           
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
              <%--  <div class="table-responsive">--%>
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

                                        <asp:TemplateField HeaderText="Executive Name">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFfollow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfollow" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salestname")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Apt Type">
                                        
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apttype")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>


                                    

                                    
                                  

                                      

                                     <%--<asp:TemplateField HeaderText="Rate Per </br> Sft (TK)">
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
                                    </asp:TemplateField>--%>

                                   <asp:TemplateField HeaderText="Unit">
                                           <FooterTemplate>
                                                    <asp:Label ID="lblFsUnit" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit" runat="server" style="margin-left:10px"
                                              Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                           <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="center" />
                                         <FooterStyle HorizontalAlign="Center" />
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



                                    <asp:TemplateField HeaderText="Total Value">

                                        <FooterTemplate>
                                                    <asp:Label ID="lblFtvalue" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                         </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtvalue" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
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
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                    </div>
                    
                    <%--</div>--%>
                    </asp:View>

                  
                   
                </asp:MultiView>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>