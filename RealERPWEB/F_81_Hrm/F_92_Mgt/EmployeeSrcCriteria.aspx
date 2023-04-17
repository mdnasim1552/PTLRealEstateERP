<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmployeeSrcCriteria.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmployeeSrcCriteria" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <script type="text/javascript" language="javascript">
                 $(document).ready(function () {
                     Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                 });
                 function pageLoaded() {
                     $("input, select").bind("keydown", function (event) {
                         var k1 = new KeyPress();
                         k1.textBoxHandler(event);
                     });
                     $('.chzn-select').chosen({ search_contains: true });
                 };


             </script>

    <style>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" >

        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }


        .tableFixHead {
            overflow: auto;
            height: 500px;
        }

            .tableFixHead tr th {
                position: sticky;
                top: 0;
                z-index: 1;
            }

        /* Just common table stuff. Really. */
        table {
            border-collapse: collapse;
        }
        /* th, td { padding: 8px 16px; }*/
        th {
            background: #eee;
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


            <div class="card mt-3">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Company
                                </label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Department
                                </label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3" id="divSection" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblDept" CssClass="mb-2 d-block" runat="server">Section</asp:Label>


                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" CssClass="mb-2 d-block" runat="server">Page Size</asp:Label>


                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem Selected="True">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                        <div class="col-md-2" style="margin-top: 28px;">

                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-info btn-md mb-2" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>

                    <div class="row">
    
                        <div class="col-lg-2">
                            <asp:Label runat="server">Gender</asp:Label>
                            <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0000">All</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                            <div class="col-lg-2">
                            <asp:Label runat="server">Religion</asp:Label>
                            <asp:DropDownList ID="ddlreligion" runat="server" CssClass="form-control">
                                 <asp:ListItem Value="0000">All</asp:ListItem>
                                <asp:ListItem Value="Islam">Islam</asp:ListItem>
                                <asp:ListItem Value="Hindu">Hindu</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-lg-1">
                            <asp:Label runat="server">From Age</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfrmage" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                        </div>

                          <div class="col-lg-1">
                            <asp:Label runat="server">To Age</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txttoage" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                        </div>


                                      <div class="col-lg-1">
                            <asp:Label runat="server">from  Salary</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtfrmsal" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                        </div>

                          <div class="col-lg-1">
                            <asp:Label runat="server">To Salary</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txttosal" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="card card-fluid">
                    <div class="card-body" >
                        <div class="row">
                            <asp:GridView ID="gvEmpList" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" AllowPaging="true"
                                ShowFooter="True" PageSize="10">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Department Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdepname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                ></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ?
                                                                             Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                Width="150px"> 
                                              
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcardnoemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesignationemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Joining Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Religion">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreligion" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "religion")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSex" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sex")) %>'
                                                Width="45px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Age">
                                        <ItemTemplate>
                                            <asp:Label ID="lblage" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "age")) %>'
                                                Width="45px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgssal" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gssal")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



