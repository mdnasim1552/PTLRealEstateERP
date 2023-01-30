<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSupplierOvAllPSummary.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptSupplierOvAllPSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
      <script type="text/javascript" language="javascript">

          $(document).ready(function () {

              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

          });
          function pageLoaded() {

              try {

                  var gvspaysummary = $('#<%=this.gvspaysummary.ClientID %>');
                  gvspaysummary.Scrollable();

                  var gvspaymentdetails = $('#<%=this.gvspaymentdetails.ClientID %>');
                  var gvconsummary = $('#<%=this.gvconsummary.ClientID %>');
                  var gvcondetails = $('#<%=this.gvcondetails.ClientID %>');
              

                  //gv1.Scrollable();

                  gvspaymentdetails.Scrollable();
                  gvconsummary.Scrollable();
                  gvcondetails.Scrollable();
              

                  var gridViewScroll = new GridViewScroll({
                  //    elementID: "gvspaymentdetails",
                  //    width: 1240,
                  //    height: 648,
                  //    freezeColumn: true,
                  //    freezeFooter: false,
                  //    freezeColumnCssClass: "GridViewScrollItemFreeze",
                  //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                  //    freezeHeaderRowCount: 1,
                  //    freezeColumnCount: 8,

                  });

               //   gridViewScroll.enhance();
                
                 
             
                  $('.chzn-select').chosen({ search_contains: true });
          <%--  $('#<%=this.gvsupstatus.ClientID%>').tblScrollable();--%>
                  $(function () {
                      $('[id*=chkSupCategory').multiselect({
                          includeSelectAllOption: true,

                          enableCaseInsensitiveFiltering: true,
                          //enableFiltering: true,

                      });

                  });
                 
              }

              catch (e) {
                  alert(e);
              }
          }
      </script>
   <%-- <style type="text/css">

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

    <style>



        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .lnkbtmfromtop {
            margin-top: 28px;
            margin-left: 5px;
        }

        .srchbtmfromtop {
            margin-top: 5px !important;
        }

        .grvContentarea {
        }
         .GridViewScrollHeader TH, .GridViewScrollHeader TD,.GridViewScroll1Header TH, .GridViewScroll1Header TD,.GridViewScroll2Header TH, .GridViewScroll2Header TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }


        .GridViewScrollItem TD, .GridViewScroll1Item TD,.GridViewScroll2Item TD{
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: transparent;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD, .GridViewScroll1ItemFreeze TD,  .GridViewScroll2ItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD, .GridViewScroll1FooterFreeze TD, .GridViewScroll2FooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 38px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }
                       .mt20 {
            margin-top: 20px;
        }
                       .table th, .table td{
                           padding:0px;
                       }
                       .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;}

                
         
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label" id="lblsupname" runat="server">Supplier Name</label>
                                <asp:DropDownList ID="ddlSuplist" runat="server" CssClass="form-control form-control-sm  chzn-select" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="from-group">
                                <label class="control-label" id="lblgrp" runat="server"  >Supplier Group</label>
                                <asp:DropDownList ID="dddSupgrp" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 ml-4">

                            <asp:Label ID="Label1" runat="server" Font-Size="12px">Type</asp:Label>
                            <asp:RadioButtonList ID="rbtnAtStatus" runat="server" AutoPostBack="True" Style="border-radius: 5px; padding: 0 5px;"
                                CssClass="custom-control custom-control-inline custom-checkbox rbtnAtStatus d-block p-0 mt-3"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">&nbsp; Summary &nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem>&nbsp; Details</asp:ListItem>
       

                            </asp:RadioButtonList>

                       <asp:CheckBox ID="chkWithPay" runat="server"  Text="With Payment" />

                        </div>


                        

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="veiwsummary" runat="server">
                         
            
                <asp:GridView ID="gvspaysummary" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass="table-striped  table-bordered grvContentarea" 
                     OnRowDataBound="gvspaysummary_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupCodealsasub" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="210px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprjName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grp" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrp" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Discount</br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldiscount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discountamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvftdiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Bill Amount</br>(After</br> Discount)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblafdiscount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "afterdiscount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvafftdiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Vat Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvVatAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFVatAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Tax Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTaxAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTaxAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SD Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSdAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   
                                       


                                     

                                        <asp:TemplateField HeaderText="Net Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNetAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbillamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     
                                       <asp:TemplateField HeaderText="Bill Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillpayAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbillpaAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                        

                                    
                                        <asp:TemplateField HeaderText="Net Payable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPayable" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPayable" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Remarks" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRmk" runat="server" CssClass="GridLebel"
                                                Text=""
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvRmk" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                          


                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle CssClass="grvHeader" />
                                 <HeaderStyle CssClass="grvHeader" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                               
                            </asp:GridView>
                                       
                </asp:View>


                <asp:View ID="ViewDetails" runat="server">
   
                  <asp:GridView ID="gvspaymentdetails" runat="server"  CssClass="table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvspaymentdetails_RowDataBound">
                <PagerSettings Visible="False" />
                         <FooterStyle CssClass="grvHeader" />
                          
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupCodealsasub" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                       

                                      <asp:TemplateField HeaderText="Supplier Name" >

                                    <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Supplier Name" Width="180px"></asp:Label>                              
                                </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpnamalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="150px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Group" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgrp" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvgrp" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Voucher No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvounum" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFvounum" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Vou.Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvoudat" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFvoudat" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbill" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFgvbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Ref">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillref" runat="server" CssClass="GridLebel"
                                               Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFgvbillref" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmountalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmtalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount </br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldiscount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discountamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvftdiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bill Amount<br>(After </br>Discount)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblafdiscount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "afterdiscount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvafftdiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Vat Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmountalsasubsdvat" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmtalsasubsdvat" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Tax Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmountalsasubsdtax" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmtalsasubsdtax" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SD Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmountalsasubsd" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmtalsasubsd" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      
                                        

                                      

                                     <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmountalsasubNet" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbillamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmtalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                  

                                    <asp:TemplateField HeaderText="Payment</br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmtalsasubpay" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmtalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Net payable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmnetpayable1" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayble")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmnetpayable1" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                             
                                </Columns>

                               
                                 <HeaderStyle CssClass="grvHeader" />
                               <HeaderStyle HorizontalAlign="Center" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                               
                            </asp:GridView>
                   
                    
                    
                             
                    
                                
                    

                </asp:View>

                <asp:View ID="viewconsummary" runat="server">
                            
 
                <asp:GridView ID="gvconsummary" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass="table-striped  table-bordered grvContentarea" OnRowDataBound="gvconsummary_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo7" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconsumcode" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Contractor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConsumName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="210px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConsumprjName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="230px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grp" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConsumgrp" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                         

                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConsumbilAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                                                        
                                     <asp:TemplateField HeaderText="Vat Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConsumVatAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFConsumVatAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Tax Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConsumTaxAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFConsumTaxAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SD Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConsumSdAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFConsumSdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   
                                       


                                     

                                        <asp:TemplateField HeaderText="Net Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConsumNetAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbillamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFConsumNetAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     
                                       <asp:TemplateField HeaderText="Bill Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillConsumpayAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFConsumbillpaAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                        

                                    
                                        <asp:TemplateField HeaderText="Net Payable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConsumPayable" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFConsumPayable" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                   


                                 


                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle CssClass="grvHeader" />
                                 <HeaderStyle CssClass="grvHeader" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                               
                            </asp:GridView>
                          
                </asp:View>

                 <asp:View ID="Viewcondetails" runat="server">    
                     <div class="row">

                               <asp:GridView ID="gvcondetails" runat="server"  CssClass="table-striped table-hover table-bordered grvContentarea" ClientIDMode="Static"
                AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvcondetails_RowDataBound">
                <PagerSettings Visible="False" />
                         <FooterStyle CssClass="grvHeader" />
                         
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo5" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblconrescode" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                       

                                      <asp:TemplateField HeaderText="Contractor Name" >

                                    <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Contractor Name" Width="180px"></asp:Label>

                                </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="170px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdprjname" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFdprjname" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="180px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Group" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcongrp" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvcongrp" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="20px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Voucher. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconvounum" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFconvounum" runat="server" Font-Bold="True" Font-Size="10px"
                                                Style="text-align: left" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Vou.Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconvoudat" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFvoudat" runat="server" Font-Bold="True" Font-Size="10px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderText="Bill No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconbill" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFgvconbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Ref">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconbillref" runat="server" CssClass="GridLebel"
                                               Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFgvconbillref" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: Left" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconamt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFconamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Discount Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldisconcount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discountamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvfcontdiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Bill Amount<br>(After Discount)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblcondiscount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "afterdiscount")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvafftcondiscount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Vat Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="gvconvatamt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="gvFconvatamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Tax Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="gvcontaxamt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="gvFcontaxamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S/M </br>Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsmamt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFsmamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                                                                                             
                                     <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcondeNet" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbillamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFcondeNet" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                  

                                    <asp:TemplateField HeaderText="Payment </br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconpayment" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFconpayment" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Net Payable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconpayable" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayble")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFconpayable" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                              
                                </Columns>

                               
                                 <HeaderStyle CssClass="grvHeader" />
                               <HeaderStyle HorizontalAlign="Center" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                               
                            </asp:GridView>
                     </div>
           
                                
                    

                </asp:View>

            </asp:MultiView>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

