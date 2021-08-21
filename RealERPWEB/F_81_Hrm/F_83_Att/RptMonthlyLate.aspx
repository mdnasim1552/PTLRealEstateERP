
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonthlyLate.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptMonthlyLate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            <div class="form-horizontal">
                           

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                                              
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                
                        </fieldset>
              

            <div class="table-responsive">

                                <asp:Repeater ID="rplateAtt" runat="server" >
                                    <HeaderTemplate>
                                        <table id="tblrpcashflow" class="table-striped table-hover table-bordered grvContentarea">
                                            <tr>
                                                <th>SL</th>
                                                 <th style="width: 100px;">Department</th>
                                                <th style="width: 100px;">Name</th>
                                               
                                                <th style="width: 100px;">Designation </th>
                                                 <th style="width: 60px;">Total </br>(Days) </th>
                                                <th style="width: 60px;">01</th>
                                                <th style="width: 60px;">02</th>
                                                <th style="width: 60px;">03</th>
                                                <th style="width: 60px;">04</th>
                                                <th style="width: 60px;">05</th>
                                                <th style="width: 60px;">06</th>
                                                 <%--<asp:Label ID="lblrpscollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Sales Collection as on January"></asp:Label>--%>
                                                
                                                <th style="width: 60px;">07 </th>
                                                <th style="width: 60px;">08 </th>
                                                <th style="width: 60px;">09 </th>
                                                <th style="width: 60px;">10 </th>
                                                 <%-- <asp:Label ID="lblrpocollam" runat="server" Font-Bold="True"  Style="text-align: right" Text="Others Collection as on January"></asp:Label>--%>
                                              
                                                <th style="width: 60px;">11 </th>
                                                <th style="width: 60px;">12 </th>
                                                <th style="width: 60px;">13</th>
                                                   <%-- <asp:Label ID="lblrptcollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Total Collection as on January"></asp:Label>--%>
                                                
                                                <th style="width: 60px;">14</th>
                                                <th style="width: 60px;">15 </th>
                                                <th style="width: 60px;">16 </th>
                                                <th style="width: 60px;">17 </th>

                                                <th style="width: 60px;">18 </th>
                                                <th style="width: 60px;">19 </th>
                                                 <th style="width: 60px;">20 </th>
                                                 <th style="width: 60px;">21 </th>
                                                 <th style="width: 60px;">22 </th>

                                                 <th style="width: 60px;">23 </th>
                                                 <th style="width: 60px;">24 </th>
                                                 <th style="width: 60px;">25 </th> 
                                                <th style="width: 60px;">26 </th> 
                                                <th style="width: 60px;">27 </th>
                                                 <th style="width: 60px;">28 </th>
                                                 <th style="width: 60px;">29 </th> 
                                                <th style="width: 60px;">30 </th>
                                                 <th style="width: 60px;">31 </th>
                                                
                                                
                                                   <%-- <asp:Label ID="lblrpcurcollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Total Collection as on February"></asp:Label>--%>
                                               
                                               <%-- <th style="width: 70px;">
                                                    <asp:Label ID="lblrpcurocollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Collection as on February"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">
                                                    <asp:Label ID="lblrpcurtcollam" runat="server" Font-Bold="True" Style="text-align: right" Text="Other Collection as on February"></asp:Label>
                                                </th>
                                                <th style="width: 70px;">Up to Date Collectin </th>
                                                <th style="width: 70px;">Accounts Receivable & Dues</th>
                                                <th style="width: 70px;">Last Amount Of Collection</th>
                                                <th style="width: 70px;">Last Collection Date</th>
                                                <th style="width: 70px;">Installment Duest up to Date</th>
                                                <th style="width: 70px;">Date of Sales </th>
                                                <th style="width: 70px;">Dealing Sales Person</th>--%>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section"))  %>' Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>' Width="100px"></asp:Label>
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig"))  %>' Width="100px"></asp:Label>
                                            </td>
                                            <td style="text-align:right">
                                                <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addday"))  %>' Width="60px"></asp:Label>
                                            </td>
                                            
                                            <td>
                                                <asp:Label ID="lbl01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col1"))  %>' Width="70px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col2"))  %>' Width="70px"></asp:Label>


                                            </td>
     
                                            
                                            
                                            <td>
                                                <asp:Label ID="lbl03" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col3")) %>' Width="70px"></asp:Label>


                                            </td>
                                                    
                                             <td>
                                                <asp:Label ID="lbl04" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col4")) %>' Width="70px"></asp:Label>


                                            </td> 
                                            
                           
                                           
                                             <td>
                                                <asp:Label ID="lbl05" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col5")) %>' Width="70px"></asp:Label>


                                            </td>
                                           
                                            
                                            <td >
                                                <asp:Label ID="lbl06" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col6")) %>' Width="70px"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl07" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col7")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl08" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col8")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl09" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col9")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl10" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col10")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:Label ID="lbl11" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col11"))%>' Width="70px"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:Label ID="lbl12" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col12")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:Label ID="lbl13" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col13"))%>' Width="70px"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:Label ID="lbl14" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col14"))%>' Width="70px"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:Label ID="lbl15" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col15")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl16" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col16")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl17" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col17")) %>' Width="70px"></asp:Label>
                                            </td>
                                           
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl18" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col18")) %>' Width="70px" ></asp:Label>
                                            </td>
                                            <td style="text-align:left">
                                                <asp:Label ID="lbl19" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col19")) %>' Width="70px" ></asp:Label>
                                            </td>


                                            <td style="text-align: left">
                                                <asp:Label ID="lbl20" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col20")) %>' Width="70px" ></asp:Label>
                                            </td>
                                            
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl21" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col21")) %>' Width="70px" ></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl22" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col22")) %>' Width="70px" ></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lbl23" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col23"))%>' Width="70px" ></asp:Label>
                                            </td>


                                               <td style="text-align: left">
                                                <asp:Label ID="lbl24" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col24"))%>' Width="70px" ></asp:Label>
                                            </td>
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl25" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col25"))%>' Width="70px" ></asp:Label>
                                           
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl26" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col26"))%>' Width="70px" ></asp:Label>
                                            </td>
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl27" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col27"))%>' Width="70px" ></asp:Label>
                                            </td>
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl28" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col28"))%>' Width="70px" ></asp:Label>
                                            </td>
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl29" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col29"))%>' Width="70px" ></asp:Label>
                                            </td>
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl30" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col30"))%>' Width="70px" ></asp:Label>
                                            </td>
                                               <td style="text-align: left">
                                                <asp:Label ID="lbl31" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "col31"))%>' Width="70px" ></asp:Label>
                                            </td>
                                              

                                        </tr>
                                    </ItemTemplate>
                                  <%--  <FooterTemplate>
                                        <tr>
                                            <th></th>
                                            <th>Total</th>
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
                                    </FooterTemplate>--%>


                                </asp:Repeater>


                            </div>

                     </div>
                   </div>
                       
                </div>
            




            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




