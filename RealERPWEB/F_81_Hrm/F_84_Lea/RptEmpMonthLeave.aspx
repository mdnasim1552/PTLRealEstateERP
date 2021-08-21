<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpMonthLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.RptEmpMonthLeave" %>
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

                                <asp:Repeater ID="rpleave" runat="server" >
                                    <HeaderTemplate>
                                        <table id="tblrpleave" class="table-striped table-hover table-bordered grvContentarea">
                                            <tr>
                                                <th>SL</th>
                                                 <th style="width: 120px;">Department</th>
                                                <th style="width: 150px;">Name</th>
                                                <th style="width: 120px;">Designation </th>
                                                <th style="width: 60px;">Leave</br>(Days) </th>
                                                <th style="width: 50px;">Leave From</th>
                                                <th style="width: 50px;">Leave To</th>
                                                <th style="width: 50px;">Causal Leave</th>
                                                <th style="width: 50px;">Earn Leave</th>
                                                <th style="width: 50px;">Sick Leave</th>
                                               
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section"))  %>' Width="120px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>' Width="150px"></asp:Label>
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig"))  %>' Width="120px"></asp:Label>
                                            </td>
                                            <td style="text-align:right">
                                                <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leavedif"))  %>' Width="50px"></asp:Label>
                                            </td>
                                            
                                            <td style="text-align:left">
                                                <asp:Label ID="lbl01" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align:left">
                                                <asp:Label ID="lbl02" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align:right">
                                                <asp:Label ID="lbl03" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cleave")).ToString("#,##0;(#,##0); ") %>' Width="50px"></asp:Label>
                                            </td>
             
                                            <td style="text-align:right">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enleave")).ToString("#,##0;(#,##0); ") %>' Width="50px"></asp:Label>
                                            </td>
                                            <td style="text-align:right">
                                                <asp:Label ID="Label7" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sleave")).ToString("#,##0;(#,##0); ") %>' Width="50px"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                 

                                </asp:Repeater>


                            </div>

                     </div>
                   </div>
                       
                </div>
            




            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>






