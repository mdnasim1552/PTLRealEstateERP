﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SalesAllReports.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesAllReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvprjcoll.ClientID %>');         
            gv.Scrollable();
              
            var gvpaystatus = $('#<%=this.gvpaystatus.ClientID %>');
            gvpaystatus.Scrollable();

            var dvsoldunsold = $('#<%=this.dvsoldunsold.ClientID %>');
            dvsoldunsold.Scrollable();

            var gvCollectionStatement = $('#<%=this.gvCollectionStatement.ClientID %>');
            gvCollectionStatement.Scrollable();
            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });

            $(function () {
                $('[id*=listproj]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });
            
        }

    </script>
    <style type="text/css">
           .multiselect  {
            width:300px !important;
           border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
           
        }
        .multiselect-container{
            overflow: scroll;
            max-height: 300px !important;
        }
        /*.multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }*/

        .multiselect-text {
            width: 200px !important;
        }

        /*.multiselect-container {
            height: 250px !important;
            width: 300px !important;
            overflow-y: scroll !important;
        }*/
        .caret {
            display: none !important;
        }
        span.multiselect-selected-text {
            width: 200px !important;
        }

        #ContentPlaceHolder1_divgrp {
            /*width: 395px !important;*/
        }

        .chzn-single{
                border-radius: 3px!important;
                height: 29px!important;
            }
    </style>

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



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        
                        
                        <div class="col-md-2 mt-2">
                            <div class="form-group">
                                <asp:Label ID="ddlReportsName" runat="server" CssClass="form-label">Reports </asp:Label>                              
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" AutoPostBack="true">                                  
                                    <asp:ListItem Value="" Selected="True">-------------Select-----------</asp:ListItem>
                                    <asp:ListItem Value="PrjCollect"> Project Wise Colletion</asp:ListItem>
                                    <asp:ListItem Value="PrjCollTilldate"> Project Wise Colletion Till Date</asp:ListItem>
                                    <asp:ListItem Value="SoldUnSoldUnit"> Sold UnSold Unit </asp:ListItem>
                                    <asp:ListItem Value="CollectionStatement"> Collection Statement </asp:ListItem>

                                   
                      <%--              <asp:ListItem Value="qtybasisp">Inventory Report-Quantity Basis(Periodic)</asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                        

                        <div class="col-md-2" runat="server" visible="false" id="clfdate">
                            <div class="form-group">
                                <label ID="lblDate" runat="server" CssClass="form-label" Visible="false">From Date</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    <div class="col-md-2" ID="clstodat" runat="server" visible="false">
                            <div class="form-group">
                                <label ID="lbltoDate" runat="server" CssClass="form-label" Visible="false">To Date</label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>

                        </div>

                              <div class="col-md-2 mt-2"  visible="false"  ID="clsprjname" runat="server">
                            <div class="from-group">
                                <asp:Label ID="prjName" runat="server" CssClass="form-label">Project Name </asp:Label>
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control form-control-sm chzn-select" style="width:200px" AutoPostBack="True" OnSelectedIndexChanged="ddlPrjName_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 pl-0 " style="margin-top:27px;" visible="false"  ID="clsprjnamemul" runat="server">
                                     <div class="from-group">
                                      
                                        <asp:ListBox ID="listproj" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple" Style="min-height: 200px !important;" AutoPostBack="true"></asp:ListBox>
                                    </div>

                                </div>
                        <%-- <div class="col-md-3 pl-0">

                                    <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>

                                </div>--%>
                       <div class="col-md-2 mt-2" runat="server" visible="false" id="clcust">
                           <div class="form-group">
                              <%--  <asp:Label ID="Label6" runat="server" CssClass="control-label"></asp:Label>--%>
                                <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="form-control form-control-sm" TabIndex="14" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindCustomer" runat="server" CssClass="" OnClick="imgbtnFindCustomer_Click" TabIndex="15"><span style="font-size:14px;" Visible="false">Customer Name&nbsp;<i class="fas fa-search"></i></span></asp:LinkButton>
                                <asp:DropDownList ID="ddlCustName" runat="server" CssClass="form-control form-control-sm chzn-select" style="width:200px" TabIndex="13" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                           </div>


                        <div class="col-md-2" runat="server" visible="false" id="grpid">
                            <div class="from-group">
                                <label id="lblgrp" CssClass="form-label""> Group </label>
                                <asp:DropDownList ID="ddlgrp" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" Width="180px"></asp:DropDownList>
                            </div>
                        </div>


                    
                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="Label1" runat="server" class="control-label" visible="false">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"  visible="false"                                  
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
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
                    <div class="col-md-1" style="margin-top: 23px; margin-left:-65px;">
                            
                     <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                           
                        </div>
                        

                    </div>
                </div>
            </div>

           <%-- <div class="card card-fluid">
                <div class="row">
                    
                </div>


            </div>--%>

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Viewprjcoll" runat="server">

                            <asp:GridView ID="gvprjcoll" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True"  AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprjName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvprjNameF"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcolltype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aptdesc"))%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                             <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvcollFtype">Total :</asp:Label>

                                        </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtamount" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFtamount"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>                                                                                                   
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="Viewprjcolltilldate" runat="server">
                            <asp:GridView ID="gvprjcolltilldate" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlcolltilldat" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprjNametill" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvprjNametillF"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                  
                                     <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcolltypetill" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aptdesc"))%>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                         <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvcollFtypetill">  Total :</asp:Label>

                                        </FooterTemplate>
                                         <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtamounttill" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFtamounttill"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>  
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="ViewPaymentStatus" runat="server">
                            <asp:GridView ID="gvpaystatus" runat="server" AutoGenerateColumns="false" 
                                ShowFooter="True"  AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNopay" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchequedat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFchequedat"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Clearance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcleardat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndate"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFcleardat"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchequeno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno"))%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFchequeno"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBankName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFBankName"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="left" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MR No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmrno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFmrno"> Total :</asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                  
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPaidamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvFPaidamt"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="ViewSoldunsold" runat="server">
                            <asp:GridView ID="dvsoldunsold" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNos" runat="server" Font-Bold="True" Height="16px"
                                                
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                         <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPrjName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCategory" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total </br>Saleable </br> Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtunit" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tunit")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total </br>Saleable </br> Area">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtarea" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sold Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsoldunit" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldunit")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Sold Area">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsoldarea" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldsize")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UnSold Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunsold" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsoldunit")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="UnSold Area">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunsoldarea" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsoldsize")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    

                                    
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="ViewCollectionStatement" runat="server">
                            <asp:GridView ID="gvCollectionStatement" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCollectionStatement_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNos" runat="server" Font-Bold="True" Height="16px"
                                                
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                         <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvPrjNam" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcliName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc"))%>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                                    <asp:Label ID="lgvunamt" runat="server" Font-Bold="True"
                                                         Style="text-align: right" Width="35px"></asp:Label>
                                                </FooterTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Opening Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                    <asp:Label ID="lgvOpamt" runat="server" Font-Bold="True"
                                                         Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Down Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtdwpay" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curbkam")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                    <asp:Label ID="lgvDpamt" runat="server" Font-Bold="True"
                                                         Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inst/Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvinstpay" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curinsam")).ToString("#,##0;(#,##0); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                                    <asp:Label ID="lgvIPamt" runat="server" Font-Bold="True"
                                                         Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtolam" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0;(#,##0); ")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                            <FooterTemplate>

                                            <asp:Label runat="server" ID="lgvtotamt" Width="80px"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>




            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
