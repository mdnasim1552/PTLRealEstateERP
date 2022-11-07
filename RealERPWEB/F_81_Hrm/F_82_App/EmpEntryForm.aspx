<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpEntryForm.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpEntryForm" %>

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


        function Search_Gridview2(strKey) {
            try {


              //  document.getElementById("<%=this.ddlpagesize.ClientID %>").value = 1000;

               // $('#<%= ddlpagesize.ClientID %>').trigger('change');


                var strData = strKey.value.toLowerCase().split(" ");
                /*alert()*/
                var tblData = document.getElementById("<%=this.gvEmpList.ClientID %>");

                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            }

            catch (e) {
                alert(e.message);
            }
        }
        function GetEmployeeform() {
            $('#EmployeeEntry').modal('toggle');
        }
        function CloseModal() {
            $('#EmployeeEntry').modal('hide');
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
        <div class="col-md-12 col-sm-12 col-lg-12">
            <div class="card card-fluid container-data mt-5">
                <div class="card-header">
                    <div class="row">
                        <h4>Employee List</h4>
                    </div>
                    <div class="row">


                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label2" runat="server" CssClass="btn btn-secondary btn-sm">Department</asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="lblDept" runat="server" CssClass="btn btn-secondary btn-sm">Section</asp:Label>
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
                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label1" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                </div>
                                <asp:TextBox ID="txtSearch" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="Search_Gridview2(this)"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label3" runat="server" CssClass="btn btn-secondary btn-sm">Filter</asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlfilterby" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlfilterby_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="00">All</asp:ListItem>
                                    <asp:ListItem Value="01">New Employee</asp:ListItem>
                                    <asp:ListItem Value="02">Exiting</asp:ListItem>
                                    <asp:ListItem Value="03">Created Users</asp:ListItem>
                                    <asp:ListItem Value="04">Not Created Users</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lnkCreate" runat="server" CssClass="btn btn-xs btn-success" OnClick="lnkCreate_Click"><i class="fa fa-plus"></i>  Create Employee</asp:LinkButton>
                        </div>

                    </div>



                    <div class="card-body">


                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-lg-12">
                                <asp:GridView ID="gvEmpList" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" AllowPaging="True"
                                    ShowFooter="True" PageSize="3000">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server"  Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px"/>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdepname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server"
                                                    Text='<%#Convert.ToString(Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim())  %>'
                                                    Width="250px"> 
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID Card">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblgvcardnoemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px"/>
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <div class="dropdown">
                                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                                        Action
                                                        
                                                    </button>
                                                    <ul class="dropdown-menu">
                                                        <li>
                                                            <asp:LinkButton ID="lnkbtnEdit" ToolTip="Employee Name Edit" OnClick="lnkbtnEdit_Click" runat="server"
                                                                 CssClass="dropdown-item "><i class="fa fa-edit "></i> Edit</asp:LinkButton>
                                                        </li>
                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="lnkView" Target="_blank" ToolTip="Employee Information view"
                                                                NavigateUrl='<%# "~/F_81_Hrm/F_82_App/EmpEntry01?Type=Entry&empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" runat="server"><i class="fa fa-eye "></i> View</asp:HyperLink>
                                                        </li>
                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="lnkOfferLetter" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10003&Entry=Offer Letter For General&empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item " runat="server">Offer Letter</asp:HyperLink>
                                                        </li>
                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="lnkAppoint" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10002&Entry=appoinment Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Appoinment Letter</asp:HyperLink>
                                                            <%--  Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? true:false %>' --%>
                                                        </li>

                                                          <li class="mt-2">
                                                            <asp:HyperLink ID="lnkConfirmation" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10025&Entry=confirmation Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Confirmation Letter</asp:HyperLink>
                                                            <%--  Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? true:false %>' --%>
                                                        </li>

                                                           <li class="mt-2">
                                                            <asp:HyperLink ID="lnkProbExtension" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10026&Entry=probation extension Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Probation Extension Letter</asp:HyperLink>
                                                        </li>

                                                                     <li class="mt-2">
                                                            <asp:HyperLink ID="lnkConfWithProm" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10024&Entry=confirmation with promotion Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Confirmation With Promotion Letter</asp:HyperLink>
                                                        </li>
                                                                <li class="mt-2">
                                                            <asp:HyperLink ID="hyplnkincrmnt" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10008&Entry= increment Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server"> Increment Letter</asp:HyperLink>
                                                        </li>

                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="HyperLink4" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10027&Entry=confirmation without increment Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Confirmation Without Increment Letter</asp:HyperLink>
                                                        </li>

                                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="HyperLink5" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10028&Entry=Salary certificate Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Salary Cerificate Letter</asp:HyperLink>
                                                        </li>

                                                                                                     <li class="mt-2">
                                                            <asp:HyperLink ID="HyperLink6" Target="_blank"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10029&Entry=Salary certificate Letter &empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>' runat="server">Experience Certificate</asp:HyperLink>
                                                        </li>



                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="lnkbtnAggrement" Target="_blank"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>'
                                                                NavigateUrl='<%# "~/F_81_Hrm/F_82_App/HREmpEntry?Type=Aggrement&empid="+Eval("empid") %>'
                                                                CssClass="dropdown-item" runat="server">Agreement</asp:HyperLink>



                                                        </li>
                                                        <li class="mt-2">
                                                            <asp:LinkButton ID="hypDelbtn" OnClick="hypDelbtn_Click" OnClientClick="return confirm('Are You Sure To Delete This Data?');"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? true:false %>'
                                                                CssClass="dropdown-item " runat="server"><i class="fa fa-trash-alt "></i> Delete</asp:LinkButton>
                                                        </li>

                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="HyperLink1" Target="_blank"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>'
                                                                NavigateUrl='<%# "~/F_81_Hrm/F_84_Lea/HRLeaveOpening"%>'
                                                                CssClass="dropdown-item " runat="server">Earn Leave Opening</asp:HyperLink>
                                                        </li>

                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="HyperLink2" Target="_blank"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>'
                                                                NavigateUrl='<%# "~/F_81_Hrm/F_84_Lea/HREmpLeave?Type=LeaveRule"%>'
                                                                CssClass="dropdown-item" runat="server">Company Leave Rule</asp:HyperLink>
                                                        </li>
                                                        <li class="mt-2">
                                                            <asp:HyperLink ID="HyperLink3" Target="_blank"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? false:true %>'
                                                                NavigateUrl='<%# "~/F_81_Hrm/F_83_Att/HREmpOffDays"%>'
                                                                CssClass="dropdown-item " runat="server">Employee Off Days</asp:HyperLink>
                                                        </li>


                                                    </ul>
                                                </div>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="User Create">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUserGenarate" ToolTip="User Create" OnClick="lnkUserGenarate_Click"
                                                    Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")).ToString()=="3365"? true:false %>'
                                                    runat="server" CssClass="btn btn-sm btn-primary "> User Generate</asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Font-Size="16px"/>
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

    </div>
    <div id="EmployeeEntry" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content col-md-12 col-sm-12 ">
                <div class="modal-header hedcon">

                    <h4>Employee Name Entry</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>

                </div>
                <div class="modal-body">

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
                            <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" OnClientClick="CloseModal();" CssClass="btn btn-danger btn-sm">Add</asp:LinkButton>
                            <asp:Label ID="lblEmplastId" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

