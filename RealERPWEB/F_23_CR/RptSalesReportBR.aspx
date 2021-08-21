<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSalesReportBR.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptSalesReportBR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

        

            $('#tblrpcashflow').gridviewScroll({
                width: 1160,
                height: 420,             
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
                          

            });
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectname" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                   
                                </div>


                                <div class="form-group">


                                    <div class="col-md-4  pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to"
                                            Text="From:"></asp:Label>

                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to"
                                            Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                   
                            <div class="row table-responsive">

                                <asp:Repeater ID="rpcashflow" runat="server" OnItemDataBound="rpcashflow_ItemDataBound" >
                                    <HeaderTemplate>
                                        <table id="tblrpcashflow" class="table-striped table-hover table-bordered grvContentarea">
                                            <tr>
                                                <th>SL</th>
                                                <th style="width: 130px;">Project Name</th>
                                                <th style="width: 120px;">Client Name</th>
                                                <th style="width: 80px;">Apartment</th>
                                                <th style="width: 40px;">Status</th>
                                                <th style="width: 70px;">Sales Value</th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrpscollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Sales Collection as on January"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">Account Receivable </th>
                                                <th style="width: 70px;">Association Fees </th>
                                                <th style="width: 70px;">Modification Charge </th>
                                                <th style="width: 70px;">Delay Charge </th>
                                                <th style="width: 70px;">Modification / Association Fee </th>
                                                
                                                 <th style="width: 70px;">
                                                    <asp:Label ID="lblrpocollam" runat="server" Font-Bold="True"  Style="text-align: right" Text="Others Collection as on January"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">Dues </th>
                                                <th style="width: 70px;">Total Values </th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrptcollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Total Collection as on January"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">Accounts Receivable & Dues </th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrpcurcollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Total Collection as on February"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrpcurocollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Collection as on February"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrpcurtcollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Other Collection as on February"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">Up to Date Collectin </th>
                                                <th style="width: 70px;"> Total Accounts Receivable & Dues</th>
                                                <th style="width: 70px;"> Schedule Date</th>
                                                 <th style="width: 70px;"> Schedule Amount</th>

                                                
                                                <th style="width: 70px;">Last Amount Of Collection</th>
                                                <th style="width: 70px;">Last Collection Date</th>
                                                <th style="width: 70px;">Installment Duest up to Date</th>
                                                <th style="width: 70px;">Date of Sales </th>
                                                <th style="width: 70px;">Dealing Sales Person</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblrpProjectName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))  %>' Width="130px"></asp:Label>


                                            </td>
                                            <td>
                                                <asp:Label ID="lblrpcustname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))  %>' Width="120px"></asp:Label>


                                            </td>

                                              
                                            
                                            
                                            
                                            
                                            <td>
                                                <asp:Label ID="lblrpunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' Width="80px"></asp:Label>


                                            </td>
                                                    
                                             
                                            
                                            <td>
                                                <asp:Label ID="lblrpaptst" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aptst")) %>' Width="40px"></asp:Label>


                                            </td>
                                            
                                            
                                            
                                            
                                            
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpsalesam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salesam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpscollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "scollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpsreceivable" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sreceivable")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpassociaam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "associaam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpmodcharge" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "modcharge")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label1" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delcharge")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptschmadelam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tomadcharge")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>


                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpocollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ocollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpodues" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "odues")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptsaleeam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleeam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptcollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptreceivable" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "treceivable")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpcurcollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curcollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpcurocollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curocollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptcurcollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcurcollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpuptocollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptocollam")).ToString("#,##0;(#,##0); ") %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrptpcreceivable" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpcreceivable")).ToString("#,##0;(#,##0); ") %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>

                                             <td style="text-align: right">
                                                <asp:Label ID="Label3" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "curschdate")) %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>
                                             <td style="text-align: right">
                                                <asp:Label ID="Label2" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curschamt")).ToString("#,##0;(#,##0); ") %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>

                                            
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrplcollam" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcollam")).ToString("#,##0;(#,##0); ") %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblrplcolldate" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lcolldate"))%>' Width="70px" ></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpinsdues" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insdues")).ToString("#,##0;(#,##0); ") %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblrpsalesdate" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "salesdate")) %>' Width="70px" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblrpsalpdesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "salpdesc"))%>' Width="120px" Font-Bold="true"></asp:Label>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFsalesam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFscollam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFsreceivable" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFassociaam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFmodcharge" runat="server" Width="80px"></asp:Label>
                                            </th>

                                             <th style="text-align: right">
                                                <asp:Label ID="lblrpFtoschmadelam" runat="server" Width="80px"></asp:Label>
                                            </th>


                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFocollam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFodues" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFtsaleeam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFtcollam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFtreceivable" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFcurcollam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFcurocollam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFtcurcollam" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFuptocollam" runat="server" Width="80px"></asp:Label>
                                            </th>

                                             <th style="text-align: right">
                                                <asp:Label ID="lblrpFlcollam" runat="server" Width="80px"></asp:Label>
                                            </th>
                                             <th >
                                                
                                            </th>
                                             <th style="text-align: right">
                                                <asp:Label ID="lblrpFinsdues" runat="server" Width="80px"></asp:Label>
                                            </th>
                                             <th style="text-align: right">
                                                
                                            </th>
                                             <th style="text-align: right">
                                               
                                            </th>
                                              <th style="text-align: right">
                                               
                                            </th>
                                              <th style="text-align: right">
                                               
                                            </th>
                                             
                                        </tr>
                                        </table>
                                    </FooterTemplate>


                                </asp:Repeater>


                            </div>


                       
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
