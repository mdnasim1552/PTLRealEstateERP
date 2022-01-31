﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpEntryForm.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpEntryForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .badgechk label {
            margin: 0 0 0 5px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
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

    <div class="row">

        <div class="col-md-3 col-sm-3 col-lg-3">
            <div class="card card-fluid container-data mt-5">
                <div class="card-header">
                    <div class="row">
                            <h4>Employee Name Entry</h4>

                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-lg-12">

                            <div class="form-group">
                                <label for="tf1">Company</label>
                                <asp:DropDownList ID="ddlCompName" runat="server" CssClass="form-control" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                          

                            <div class="form-group">
                                <label for="txtEmpName">
                                    Employee Name 
                            
                             <asp:CheckBox ID="chkNewEmp" Checked="true" Text=" New Employee" runat="server" CssClass="d-none badge badge-secondary badgechk" OnCheckedChanged="chkNewEmp_CheckedChanged"
                                 AutoPostBack="True" />
                                </label>
                                <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger btn-sm">Add</asp:LinkButton>
                                <asp:Label ID="lblEmplastId" runat="server" Visible="false"></asp:Label>
                            </div>





                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="col-md-8 col-sm-8 col-lg-8">
            <div class="card card-fluid container-data mt-5">
                <div class="card-header">
                    <div class="row">
                         <h4>Employee List</h4>
                    </div>
                    <div class="row">

                      
                        <div class="col-md-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label2" runat="server" CssClass="btn btn-secondary btn-sm">Department</asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="lblDept" runat="server"  CssClass="btn btn-secondary btn-sm">Section</asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="lblPage" runat="server" CssClass="btn btn-secondary btn-sm">Page Size</asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">                                    
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                </div>



                <div class="card-body">

                    
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvEmpList" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" AllowPaging="True" 
                                ShowFooter="True" PageSize="20">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdepname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server"
                                                Text='<%#Convert.ToString(Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim())  %>'
                                                Width="250px"> 
                                              
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Card #">
                                        <ItemTemplate>
                                             <asp:Label ID="lblEmpid" runat="server" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblgvcardnoemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesignationemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Joining Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                             <asp:HyperLink ID="lnkView" Target="_blank"   
                                                NavigateUrl='<%# "~/F_81_Hrm/F_82_App/EmpEntry01?Type=Entry&empid="+Eval("empid") %>'
                                                CssClass="btn btn-sm btn-info " runat="server"><i class="fa fa-eye "></i></asp:HyperLink>

                                            <asp:HyperLink ID="lnkOfferLetter" Target="_blank"   
                                                NavigateUrl='<%# "~/LetterDefault?Type=10003&Entry=Offer Letter For General&empid="+Eval("empid") %>'
                                                CssClass="btn btn-sm btn-warning " runat="server">Offer Letter</asp:HyperLink>

                                           

                                            <asp:HyperLink ID="lnkbtnAggrement" Target="_blank"   
                                                NavigateUrl='<%# "~/F_81_Hrm/F_82_App/HREmpEntry?Type=Aggrement&empid="+Eval("empid") %>'
                                                CssClass="btn btn-sm btn-success  " runat="server">Agreement</asp:HyperLink>


                                              
                                             <%--<asp:LinkButton ID="LinkButton3" runat="server"><i class="fa fa-trash-alt "></i></asp:LinkButton>--%>
                                             <asp:LinkButton ID="lnkbtnEdit" OnClick="lnkbtnEdit_Click" runat="server"><i class="fa fa-edit "></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
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
        </div>
    </div>

     
</asp:Content>

