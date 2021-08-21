﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="UserLoginfrm.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.UserLoginfrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



            var gv = $('#<%=this.gvPermission.ClientID %>');
            gv.Scrollable();
        }
    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:TextBox ID="txtSrcName" runat="server" CssClass="inputTxt lblTxt inpPixedWidth"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindName_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px">

                                        <asp:Label ID="lblId" CssClass=" lblName" runat="server" Visible="False" Text="User Name"></asp:Label>
                                        <asp:Label ID="txtuserid" CssClass=" lblName" runat="server" Visible="False" Text="User Name"></asp:Label>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3 pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblMsg" CssClass="btn-danger primaryBtn btn disabled" runat="server"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="918px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvUseForm_PageIndexChanging"
                            OnRowCancelingEdit="gvUseForm_RowCancelingEdit"
                            OnRowEditing="gvUseForm_RowEditing" OnRowUpdating="gvUseForm_RowUpdating"
                            PageSize="100">
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
                                <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />
                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUserId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                            Width="50px" OnClick="lbtnUserId_Click"></asp:LinkButton>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvuserid" runat="server" BackColor="Transparent"
                                            BorderStyle="None" MaxLength="7" Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusrShorName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusrFullName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrFullName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                    </EditItemTemplate>


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pass Word">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="140px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                    </ItemTemplate>
                                    <%--<EditItemTemplate>
                                                    
                                                </EditItemTemplate>--%>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="User Name">
                                     <EditItemTemplate>
                                         <asp:Panel ID="Panel2" runat="server">
                                            

                                             <table style="width: 100%;">
                                                 <tr>
                                                     <td>
                                                         <asp:TextBox ID="txtSearchUserName" runat="server"  CssClass=" inputtextbox" TabIndex="4" Width="86px"></asp:TextBox>
                                                     </td>
                                                     <td>
                                                          <asp:LinkButton ID="ibtnSrchUse" runat="server" OnClick="ibtnSrchProject_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                                         
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="ddlUserName" runat="server"  CssClass="ddlPage" Width="200px" TabIndex="6">
                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </asp:Panel>
                                     </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProName" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size" Visible="false"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                                    BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                    Width="70px">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                    <asp:ListItem Value="400">400</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True"
                                                    Font-Bold="True"
                                                    OnCheckedChanged="chkShowall_CheckedChanged" Text="Show All" CssClass="primaryBtn checkbox" />
                                            </div>

                                            <div class="col-md-3 pading5px">
                                                <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                                    Font-Bold="True" Font-Size="12px"
                                                    OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass=" form-control inputTxt">
                                                    <asp:ListItem Value="05">My Interface(Sales)</asp:ListItem>
                                                    <asp:ListItem Value="07">My Interface(General)</asp:ListItem>
                                                    <asp:ListItem Value="10">My Interface(CR)</asp:ListItem>

                                                      <asp:ListItem Value="21">KPI Setup(Sales)</asp:ListItem>
                                                      <asp:ListItem Value="23">KPI Setup(General)</asp:ListItem>
                                                      <asp:ListItem Value="26">KPI Setup(CR)</asp:ListItem>


                                                    
                                                    <asp:ListItem Value="32">MIS(Sales)</asp:ListItem>
                                                    <asp:ListItem Value="33">MIS(General)</asp:ListItem>
                                                    <asp:ListItem Value="35">MIS(CR)</asp:ListItem>


                                                  
                                                    <asp:ListItem Value="00" Selected="True">All</asp:ListItem>

                                                    <%--<asp:ListItem Value="01">Budget</asp:ListItem>
                                                        <asp:ListItem Value="02">Project Implementaion</asp:ListItem>
                                                        <asp:ListItem Value="03">Procurement</asp:ListItem>
                                                        <asp:ListItem Value="04">Accounts</asp:ListItem>
                                                        <asp:ListItem Value="05">Marketing</asp:ListItem>
                                                        <asp:ListItem Value="06">Sales &amp; Recovery</asp:ListItem>
                                                        <asp:ListItem Value="07">Credit Realization(CR)</asp:ListItem>
                                                        <asp:ListItem Value="08">Management</asp:ListItem>
                                                        <asp:ListItem Value="09">Feasibility</asp:ListItem>
                                                        <asp:ListItem Value="10">MIS</asp:ListItem>
                                                        <asp:ListItem Value="11">Fixed Assets</asp:ListItem>
                                                        <asp:ListItem Value="12">Billing</asp:ListItem>
                                                        <asp:ListItem Value="13">Tender</asp:ListItem>
                                                        <asp:ListItem  Value="14">Tender Management</asp:ListItem>
                                                        <asp:ListItem  Value="17">Land Proposal</asp:ListItem>
                                                        <asp:ListItem  Value="18">Finance Module</asp:ListItem>
                                                         <asp:ListItem Value="31"> MIS Module</asp:ListItem> 
                                                        <asp:ListItem Value="32"> Management Module</asp:ListItem>  
                                                        <asp:ListItem Value="99">Admin</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2 pading5px">
                                                   
                                                <asp:LinkButton ID="lnkbtnBack" runat="server"  CssClass="btn btn-danger primaryBtn"
                                                    OnClick="lnkbtnBack_Click">Back</asp:LinkButton>
                                            </div>
                                            <div class="col-md-2 pull-right">
                                                <asp:Label ID="lblMsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
<asp:GridView ID="gvPermission" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                OnPageIndexChanging="gvPermission_PageIndexChanging"
                                                OnRowDeleting="gvPermission_RowDeleting" ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Form Name" Visible="false"><ItemTemplate><asp:Label ID="lgvufrmname" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'
                                                                Width="120px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Form id" Visible="false"><ItemTemplate><asp:Label ID="lgvufrmid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>'
                                                                Width="120px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Query Type" Visible="false"><ItemTemplate><asp:Label ID="lgvQrytype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'
                                                                Width="120px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:Label ID="lgvDescription" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "dscrption").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "dscrption")).Trim(): "") 
                                                                         
                                                                    %>'
                                                                Width="280px"></asp:Label></ItemTemplate><FooterTemplate><asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True"  CssClass="btn btn-danger primaryBtn"  OnClick="lbtnUpPer_Click">Update</asp:LinkButton></FooterTemplate><HeaderTemplate><table style="width: 100%;"><tr><td class="style22">Description</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style23">&nbsp;</td><td><asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " /></td></tr></table></HeaderTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Permission"><HeaderTemplate><table style="width: 90px;"><tr><td><asp:Label ID="Label3" runat="server" Text="Permission"></asp:Label></td></tr><tr><td><asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkallView_CheckedChanged" /></td></tr></table></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkPermit" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                                Width="50px" /></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry or Edit </Br> or Cancel"><HeaderTemplate><table style="width: 90px;"><tr><td><asp:Label ID="Label4" runat="server" Text="Entry or Edit </Br> or Cancel"></asp:Label></td></tr><tr><td><asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkallEntry_CheckedChanged" /></td></tr></table></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkEntry" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entry"))=="True" %>'
                                                                Width="50px" /></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View & Print"><HeaderTemplate><table style="width: 90px;"><tr><td><asp:Label ID="Label5" runat="server" Text="View & Print"></asp:Label></td></tr><tr><td><asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkallPrint_CheckedChanged" /></td></tr></table></HeaderTemplate><ItemTemplate><asp:CheckBox ID="chkPrint" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>'
                                                                Width="50px" /></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="left" VerticalAlign="Top" /></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Check All"><ItemTemplate><asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkall_CheckedChanged" /></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="Left" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#F5F5F5" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                            </asp:GridView>
                                <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>
                            </div>
                        </asp:View>
                    </asp:MultiView>

                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

