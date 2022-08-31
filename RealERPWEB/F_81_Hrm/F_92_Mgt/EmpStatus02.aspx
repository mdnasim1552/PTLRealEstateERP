<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpStatus02.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpStatus02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>
    

    <script language="javascript" type="text/javascript">

        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvinacEmplist.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
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
        function Search_GridviewEc(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvEmpCon.ClientID %>");
             var rowData;
             for (var i = 1; i < tblData.rows.length; i++) {
                 rowData = tblData.rows[i].cells[cellNr].innerHTML;
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
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var grvJoinStat = $('#<%=this.grvJoinStat.ClientID %>');
            grvJoinStat.Scrollable();
          <%--  var gvJoinEmp = $('#<%=this.gvJoinEmp.ClientID %>');
            gvJoinEmp.Scrollable();--%>
            var gvEmpCon = $('#<%=this.gvEmpCon.ClientID %>');
            gvEmpCon.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });

        };

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


            <div class="card mt-5">
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



                        <div class="col-md-2 ">
                            <div class="form-group mt-4 mb-0">

                                <asp:CheckBox ID="chkbdate" runat="server" AutoPostBack="True" Font-Bold="True" CssClass="" />
                                <asp:Label ID="withBirth" runat="server">With Birth Date</asp:Label>
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-info btn-md mb-2" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>




                    </div>




                    <div runat="server">
                        <div class="row">
                            <div class="col-md-3" id="desFrom" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lblfrmd" CssClass="mb-2 d-block" runat="server">Form</asp:Label>

                                    <asp:DropDownList ID="ddlfrmDesig" runat="server" OnSelectedIndexChanged="ddlfrmDesig_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3" id="desTo" runat="server">
                                <div class="form-group">
                                    <asp:Label ID="lbltdeg" CssClass="mb-2 d-block" runat="server">To</asp:Label>

                                    <asp:DropDownList ID="ddlToDesig" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblfrmdate" CssClass="mb-2 d-block" runat="server">From</asp:Label>

                                    <asp:TextBox ID="txtFdate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lbltodate" CssClass="mb-2 d-block" runat="server">To</asp:Label>

                                    <asp:TextBox ID="txtTdate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTdate"></cc1:CalendarExtender>
                                </div>
                            </div>

                        </div>
                    </div>


                    <div class="row">


                        <div class="col-md-4" id="SepType" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label6" CssClass="mb-2 d-block" runat="server">Separeation Type</asp:Label>

                                <asp:DropDownList ID="ddlSepType" runat="server" Width="233" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlToDesig_SelectedIndexChanged" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" id="comlist" visible="False">
                            <div class="form-group">
                                <asp:Label CssClass="mb-2 d-block" runat="server">Companies</asp:Label>
                                <asp:DropDownList ID="ddlComName" class="ComName form-control ClCompAndMod" runat="server" TabIndex="2" Width="224">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="JoiningRpt" runat="server">

                                <asp:GridView ID="grvJoinStat" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AllowPaging="True" OnPageIndexChanging="grvJoinStat_PageIndexChanging" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComp" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDept" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department")) %>'
                                                    Width="240px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number of Joiners">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvNoJ" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noj")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRemarks" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                </td>
                            </asp:View>
                            <asp:View ID="ViewJoingEmpList" runat="server">
                                <asp:GridView ID="gvJoinEmp" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvJoinEmp_PageIndexChanging"
                                    ShowFooter="True" Width="420px" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <HeaderTemplate>
                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Company Name" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center"><i class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanyname" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))  +"<B>"  %>' Width="240px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardno" runat="server" Style="text-align: center"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <%-- Request by : BTI Iqbal--%>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdepartment" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Section & Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdeptandemployee" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                            Width="250px"> 
                                              
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle"  />
                                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsection" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Salary">

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFsalary" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsalary" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Replace/New">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="ViewEmpList" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvEmpList" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" AllowPaging="true"
                                        ShowFooter="True" PageSize="300">
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


                                            <asp:TemplateField HeaderText="Company Name">

                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Company Name" Width="140px"></asp:Label>
                                                    <asp:HyperLink ID="hlbtntbCdataExcelemplist" runat="server"
                                                        CssClass="btn  btn-success btn-sm" ToolTip="Export Excel"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcustname" runat="server"
                                                        Text='<%# "<B>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))+"<B>" %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>



                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Company Name">



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanynameemp" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))  +"<B>"  %>'
                                                    Width="150px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle"  />
                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Department Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdepname" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
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
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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


                                            <asp:TemplateField HeaderText="Blood Group">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbloodgrp" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blood")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmobile" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Extention">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExtion" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "extention")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: left" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Length">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvserlength" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slength")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Gross Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvemplist2" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salary")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFlblgvemplist2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewTransfer" runat="server">
                                <asp:GridView ID="grvTransList" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grvTransList_PageIndexChanging"
                                    ShowFooter="True" Width="420px" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpname" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                                    Width="150px"> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer From &lt;br&gt; Organisation Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFCompanyna" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "fcompname"))  +"<B>"  %>'
                                                    Width="150px"> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFdesig" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fdesig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFdept" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfdeptname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=""></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer To &lt;br&gt; Organisation Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtCompanyna" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "tcompname"))  +"<B>"  %>'
                                                    Width="150px"> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtdesig" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdesig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtdept" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttdeptname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrmrks" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewConFirmation" runat="server">

                                <asp:GridView ID="gvEmpCon" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvEmpCon_PageIndexChanging"
                                    ShowFooter="True" Width="420px" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcompanyandemp" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                    Width="250px"> 
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                              <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchid" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Card #" onkeyup="Search_GridviewEc(this,2)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardnocon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationcon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeptnamecon" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Confirmation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcondate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "condate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvgsal" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="ViewManpower" runat="server">
                                <asp:GridView ID="grvManPwr" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grvManPwr_PageIndexChanging"
                                    ShowFooter="True" Width="420px" OnRowDataBound="grvManPwr_RowDataBound" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanyname" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))  +"<B>"  %>' Width="150px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptand" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>' Width="150px">  
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Strength">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpening" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvJoining" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noj")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer In">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnotrIn" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "notrin")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Out">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnotrout" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "notrout")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDep" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "departure")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewSeparation" runat="server">
                                <%--     <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                            </div>


                                        </div>
                                    </fieldset>--%>

                                <div class="col-md-9">
                                    <asp:GridView ID="grvEmpSep" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmpCon_PageIndexChanging"
                                        ShowFooter="True" Width="420px" PageSize="300">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <table style="width: 200px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                    Text="Company &amp; Employee Name" Width="120px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center"><i class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcompanyandemp" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                        Width="250px"> 
                                              
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcardnocon" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdesignationcon" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDeptnamecon" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJoindate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Separation Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcondate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "spdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Separation Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSPType" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spdesc")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>


                            </asp:View>

                            <asp:View ID="ViewEmpHold" runat="server">
                                <asp:GridView ID="gvEmpHold" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvEmpHold_PageIndexChanging"
                                    ShowFooter="True" Width="420px" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcompanyhold" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                    Width="250px"> 
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardhold" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationhold" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeptnamehold" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfrmdate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "frmdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtodate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "todate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Number Of Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtoabsday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewGradeADesignation" runat="server">
                                <asp:GridView ID="grvEmpLHSal" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grvEmpLHSal_PageIndexChanging"
                                    ShowFooter="True" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo4" runat="server"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Campany Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGrade" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grade")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesign" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="# of Employees">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nofemp")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lowest Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvLowSal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lowsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Highest Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvHighSal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "highsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTSal" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTSal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="View1" runat="server">

                                <asp:GridView ID="gvgwemp" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvgwemp_PageIndexChanging"
                                    ShowFooter="True" Visible="true" Width="420px" PageSize="300">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcompanyandemp" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gradedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")
                                                                    %>'
                                                    Width="300px">
                                                    
                                                    
                                                    
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardno0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignation0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeptname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Of Joining">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindate0" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Period">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeptname0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "serperiod")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalary" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:View>

                            <asp:View ID="View2" runat="server">
                                <asp:GridView ID="gvinacEmplist" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvinacEmplist_PageIndexChanging" AllowSorting="True"
                                    ShowFooter="True" Width="420px" PageSize="300">

                                    <RowStyle />



                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo15" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Company Name">

                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchcpName" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="CompanyName" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanynameinemp" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))  +"<B>"  %>'
                                                    Width="150px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptandemployeeinemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="250px"> 
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Department Name" Width="200px"></asp:Label>
                                                <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-sm" ToolTip="Export Excel"> 
                                                        <i class="fa fa-file-excel" aria-hidden="true"></i>

                                                </asp:HyperLink>
                                            </HeaderTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptandemployeeinemp2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="230px"> 

                                                    
                                                   <%-- Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'--%>
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID #">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearcid" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Card #" onkeyup="Search_Gridview(this,4)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardnoinemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationinemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindateinemp" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resigning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreinsign" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "resdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resign Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrestype" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "septype") %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>


                            <asp:View ID="View3" runat="server">
                                <asp:GridView ID="gvtemplist" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvtemplist_PageIndexChanging" AllowPaging="true"
                                    ShowFooter="True" Width="420px" PageSize="300">

                                    <RowStyle />


                                    <Columns>



                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo20" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanytemp" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))  +"<B>"  %>'
                                                    Width="150px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department &amp; Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptandemployeeempt" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                    Width="250px"> 
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardnotempt" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationempt" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindateempt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resign Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblresigndat" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "resdat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "resdat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>


                            <asp:View ID="ViewPabx" runat="server">
                                <div class="table table-responsive">
                                    <div class="col-md-6">

                                        <div class="input-group input-group-alt">
                                            <div class="input-group-prepend ">
                                                <asp:Label ID="Label3" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                            </div>
                                            <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>

                                        </div>
                                        <br />
                                        <%--<asp:TextBox ID="inputtextbox" runat="server" onkeyup="Search_Gridview(this);" onkeypress="return searchKeyPress(event);" CssClass="form-control" placeholder="Search here........." Font-Size="12px " Width="100%" />--%>
                                    </div>
                                    <asp:GridView ID="gvPabxInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvPabxInfo_PageIndexChanging" AllowPaging="true"
                                        ShowFooter="True" Width="420px" PageSize="300">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID Card #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcardnoemp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname").ToString())  %>'
                                                        Width="250px"> 
                                              
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdesignationemp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                            </asp:TemplateField>





                                            <asp:TemplateField HeaderText="Ext#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExtion" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "extention")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmobile" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" />
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
                            </asp:View>

                        </asp:MultiView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
