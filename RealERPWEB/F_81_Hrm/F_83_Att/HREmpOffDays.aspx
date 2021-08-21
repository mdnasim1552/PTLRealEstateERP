﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HREmpOffDays.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HREmpOffDays" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    

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
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:LinkButton ID="lnkbtnOffDay" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="240" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Section Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt"  Width="240" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>




                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlEmp" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control inputTxt"  TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:Label ID="lmsg" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>


                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Employee</asp:Label>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnsrchEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnsrchEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:LinkButton ID="lnkbtnoffShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnoffShow_Click">Show</asp:LinkButton>

                                        </div>



                                    </div>
                                </div>
                            </fieldset>



                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnloffDays" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:CheckBoxList ID="chkDate" runat="server" CssClass=" btn  checkBox" Style="border:1px solid yellow;" 
                                                RepeatColumns="7" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>

                                            <asp:Label ID="Label20" runat="server" CssClass="lblTxt lblName" Text="Reason's:"></asp:Label>

                                            <asp:TextBox ID="txtReason" runat="server"
                                                CssClass="inputTxt inputName inpPixedWidth" Font-Bold="True" TextMode="MultiLine"></asp:TextBox>

                                            
                                            <asp:CheckBox ID="Chkgov" runat="server" AutoPostBack="True" CssClass="btn btn-primary checkBox" Text="Govt.Holiday"
                                            Width="120px" />
                                                



                                            <asp:LinkButton ID="lnkbtnAllUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnAllUpdate_Click">Update</asp:LinkButton>



                                        </div>
                                    </div>
                                </div>
                            </fieldset>


                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel3" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-2 pading5px">
                                             <asp:CheckBox ID="chkoffDays" runat="server" AutoPostBack="True" CssClass="btn btn-primary checkBox"
                                           
                                            OnCheckedChanged="chkoffDays_CheckedChanged" Text="Off Days" Visible="False"
                                            Width="97px" />



                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                              <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size:" Visible="False"
                                            Width="100px"></asp:Label>

                                              <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                           
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                                </div>
                            </fieldset>

                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvoffday" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="695px"
                            OnPageIndexChanging="gvoffday_PageIndexChanging"
                            OnRowDeleting="gvoffday_RowDeleting">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDepartmane" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "section").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim(): "")  %>'
                                            Width="250px"> 
                                        
 
                                        
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>




                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnFUpOff" runat="server" Font-Bold="True"
                                            Font-Size="12px"  CssClass="btn  btn-danger   primarygrdBtn" OnClick="lnkbtnFUpOff_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ID CARD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvidcard" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesig")) %>'
                                            Width="170px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvOffdate" runat="server" BorderStyle="None" Font-Size="11px" ForeColor="Black" BackColor="Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wkdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="75px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvOffdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvOffdate">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reasons">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvReason" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reason")).ToString() %>'
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" />
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


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

