<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HREmpLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.HREmpLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   



    
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
        }

    </script>

     <script type="text/javascript" language="javascript">

         $(document).ready(function () {

             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

         });

       
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
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="col-md-3 pading5px asitCol3">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Yearly Leave</asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="68px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
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
                                <div class="col-md-8 pading5px">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblcompname" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4  pading5px asitCol4">
                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="233" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control inputTxt pull-left" TabIndex="2" >
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click" Text="Ok"></asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Section Name</asp:Label>
                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4  pading5px asitCol4">
                                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="233" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblmobno" runat="server" Visible="false" CssClass="lblTxt lblName">Section Name</asp:Label>


                                            

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblEmpCode" runat="server" CssClass="lblTxt lblName">Emp. Code</asp:Label>
                                            <asp:TextBox ID="txtEmpSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnEmpSeach" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmpSeach_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="leaveRule" runat="server">

                            <div class="row">

                                <fieldset class="scheduler-border fieldset_A">
                                    <asp:Panel ID="pnlleave" runat="server" BackColor="#F8F8F8" Visible="False">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Earn Leave</asp:Label>
                                                    <asp:TextBox ID="txternleave" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Causual Leave</asp:Label>
                                                    <asp:TextBox ID="txtcsleave" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Sick Leave</asp:Label>
                                                    <asp:TextBox ID="txtskleave" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Maternity Leave</asp:Label>
                                                    <asp:TextBox ID="txtmtleave" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName">Without Pay Leave </asp:Label>
                                                    <asp:TextBox ID="txtWPayleave" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                                </div>
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <asp:Label ID="Label14" runat="server"  CssClass="lblTxt lblName">Adjustment Leave</asp:Label>
                                                    <asp:TextBox ID="txtTrainleave" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:LinkButton ID="lnkbtnGenLeave" runat="server" CssClass="btn btn-primary primaryBtn"
                                                        OnClick="lnkbtnGenLeave_Click" TabIndex="14">Generate</asp:LinkButton>
                                                </div>


                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-sm-12">
                                        <div class="form-horizontal">
                                            <div class=" form-group">
                                                <asp:CheckBox ID="chkLeave" runat="server" CssClass="btn chkBoxControl primaryBtn" AutoPostBack="True" OnCheckedChanged="chkLeave_CheckedChanged" Text="Leave" Visible="False"
                                                    TabIndex="15" />

                                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>

                            </div>
                            <div class="row">

                                <asp:GridView ID="gvLeaveRule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" Width="834px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    PageSize="15">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp. ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempid" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID CARD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvidcard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnFUpLeave" runat="server" CssClass="btn btn-danger btn-xs"
                                                    OnClick="lnkbtnFUpLeave_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Join Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesig" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Earned Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Casual Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sick Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maternity Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvml" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Without Pay Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvWPl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Training Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvTrL" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trpleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
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


                        </asp:View>

                        <asp:View ID="LeaveApp" runat="server">

                            <div class="row">
                                <asp:Panel ID="PnlEmp" runat="server" Visible="False">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:RadioButtonList ID="rblstapptype" runat="server" CssClass="rbtnList1 chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Width="220px" TabIndex="16" Visible="False">
                                                        <asp:ListItem>Type 1</asp:ListItem>
                                                        <asp:ListItem>Type 2</asp:ListItem>
                                                        <asp:ListItem>Type 3</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblpreAdv" runat="server" CssClass="lblTxt lblName">Emp.  Name</asp:Label>
                                                    <asp:TextBox ID="txtlAppEmpSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnlAppEmpSeaarch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnlAppEmpSeaarch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                                <div class="col-md-4 pading5px asitCol4"> 
                                                    <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" CssClass=" chzn-select form-control inputTxt pull-left" TabIndex="2" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-2 pading5px asitCol2">
                                                    <asp:Label ID="lbltrnleaveid" runat="server" Visible="False"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                                    <asp:Label ID="lblComPany" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                                    <asp:Label ID="lblSection" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                                    <asp:Label ID="lblDesignation" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>
                                                <div class="col-md-3 pull-right">
                                                    <asp:Label ID="lmsg11" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Joining Date</asp:Label>
                                                    <asp:Label ID="lblJoiningDate" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="form-horizontal">
                                            <asp:Panel ID="PnlPreLeave" runat="server" Visible="False">

                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Pre. Leave</asp:Label>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                            TargetControlID="txtaplydate" TodaysDateFormat="">
                                                        </cc1:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:DropDownList ID="ddlPreLeave" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Width="200px" TabIndex="20">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2 pading5px">

                                                        <asp:LinkButton ID="lnkbtnPreLeave" runat="server" CssClass="btn btn-primary primaryBtn"
                                                            OnClick="lnkbtnPreLeave_Click" TabIndex="21">Show</asp:LinkButton>
                                                    </div>

                                                </div>
                                                <table style="width: 100%;">
                                                    <%--<tr>
                                                                <td class="style33">
                                                                    <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                        Style="text-align: left" Text="Pre. Leave:" Width="80px"></asp:Label>
                                                                </td>
                                                                <td class="style46">
                                                                    <cc1:CalendarExtender ID="txtaplydate_CalendarExtender0" runat="server" Format="dd-MMM-yyyy"
                                                                        TargetControlID="txtaplydate" TodaysDateFormat="">
                                                                    </cc1:CalendarExtender>



                                                                    <cc1:ListSearchExtender ID="ddlPreLeave_ListSearchExtender" runat="server" QueryPattern="Contains"
                                                                        TargetControlID="ddlPreLeave">
                                                                    </cc1:ListSearchExtender>
                                                                </td>
                                                                <td class="style31"></td>
                                                                <td class="style36">
                                                                    <cc1:CalendarExtender ID="txtApprdate_CalendarExtender0" runat="server" Format="dd-MMM-yyyy"
                                                                        TargetControlID="txtApprdate" TodaysDateFormat="">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td class="style51"></td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>--%>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </fieldset>


                                </asp:Panel>
                            </div>
                            <div class="row">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Pnlapply" runat="server" Visible="False">
                                        <div class="form-group">
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName">Apply Date</asp:Label>
                                                <asp:TextBox ID="txtaplydate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtaplydate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtaplydate" TodaysDateFormat="">
                                                </cc1:CalendarExtender>
                                                <asp:Label ID="Label19" runat="server" CssClass="lblTxt lblName">Appr.Date</asp:Label>
                                                <asp:TextBox ID="txtApprdate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtApprdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtApprdate" TodaysDateFormat="">
                                                </cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:LinkButton ID="lnkbtnRef" runat="server" OnClick="lnkbtnRef_Click" CssClass="btn btn-primary primaryBtn">Refresh</asp:LinkButton>
                                                <asp:CheckBox ID="chkPreLeave" runat="server" AutoPostBack="True" CssClass="chkBoxControl" OnCheckedChanged="chkPreLeave_CheckedChanged"
                                                    Text="Previous Leave" TabIndex="24" />
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    <asp:Label ID="lblleaveApp" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="#000"
                                        Text="Leave Application" Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvLeaveApp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="600px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnTotalLeave" runat="server"  CssClass="btn   btn-primary btn-xs"  OnClick="lnkbtnTotalLeave_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateLeave" runat="server" Font-Bold="True" Font-Size="12px" CssClass="btn  btn-danger primarygrdBtn"
                                                    OnClick="lnkbtnUpdateLeave_Click">Update </asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Applied For">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvlapplied" runat="server" BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lapplied")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" BackColor="Transparent" Font-Size="12px"
                                                    Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" Font-Bold="True" Font-Size="12px" CssClass="btn btn-primary primaryBtn"
                                                    ForeColor="#000" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Std. Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvenjoydt1" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1">
                                                </cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave End Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvenjoydt2" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2">
                                                </cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <asp:Label ID="lblleaveStatus" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="#000"
                                    Text="Leave Status" Visible="False"></asp:Label>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="925px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Leave Adjusted">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlvadjusted" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjfleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Std. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <asp:Panel ID="PnlRmrks" runat="server" Visible="False">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style31"></td>
                                            <td class="style37">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left"
                                                    Text="Reason(s) :" Width="100px" ForeColor="#000"></asp:Label>
                                            </td>
                                            <td class="style66">
                                                <asp:TextBox ID="txtLeavLreasons" runat="server" BorderWidth="1px"
                                                    Height="37px" TextMode="MultiLine"
                                                    Width="220px" TabIndex="25"></asp:TextBox>
                                            </td>
                                            <td class="style45">
                                                <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: left" Text="Address of enjoing time:"
                                                    Width="140px"></asp:Label>
                                            </td>
                                            <td class="style68">
                                                <asp:TextBox ID="txtaddofenjoytime" runat="server" BorderWidth="1px"
                                                    Height="37px" MaxLength="6" TabIndex="26"
                                                    TextMode="MultiLine" Width="220px"></asp:TextBox>
                                            </td>
                                            <td class="style49"></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="style69"></td>
                                            <td class="style70">
                                                <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: left" Text="Remarks :" Width="100px"></asp:Label>
                                            </td>
                                            <td class="style71">
                                                <asp:TextBox ID="txtLeavRemarks" runat="server" BorderWidth="1px" Height="37px"
                                                     TabIndex="27"
                                                    TextMode="MultiLine" Width="220px"></asp:TextBox>
                                            </td>
                                            <td class="style72">
                                                <asp:Label ID="Label43" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Height="38px" Style="text-align: left"
                                                    Text="While on Leave, Duties will Performed by :" Width="200px"></asp:Label>
                                            </td>
                                            <td class="style73">
                                                <asp:TextBox ID="txtdutiesnameandDesig" runat="server" BorderWidth="1px"
                                                    Height="37px"  TabIndex="27"
                                                    TextMode="MultiLine" Width="220px"></asp:TextBox>
                                            </td>
                                            <td class="style74"></td>
                                            <td class="style75"></td>
                                            <td class="style75"></td>
                                            <td class="style75"></td>
                                            <td class="style75"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Label ID="lblleaveInformation" runat="server" Font-Bold="True"
                                    Font-Size="14px" ForeColor="#000" Text="Leave Information" Visible="False"></asp:Label>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvleaveInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="420px" OnRowDataBound="gvleaveInfo_RowDataBound"
                                    OnRowDeleting="gvleaveInfo_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="trnleaveid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltrnleaveid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" />

                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvledescription" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="120px">
                                                                
                                                                
                                                                
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Apply Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvapplydate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="From Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstartdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstrtdat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlenddate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lenddat"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvleavedays" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreason" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvremarks" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lrmarks")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
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
                        <asp:View ID="LeaveAppform" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <asp:Panel ID="PnlEmplApp" runat="server" Visible="False">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:RadioButtonList ID="rblstFormType" runat="server" CssClass="rbtnList1 chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Width="220px" TabIndex="16" Visible="False">
                                                        <asp:ListItem>Type 1</asp:ListItem>
                                                        <asp:ListItem>Type 2</asp:ListItem>
                                                        <asp:ListItem>Type 3</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName">Emp.  Name</asp:Label>
                                                    <asp:TextBox ID="txtlFEmpSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnlFEmpSeaarch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnlFEmpSeaarch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <asp:DropDownList ID="ddlEmpNamelApp" runat="server" OnSelectedIndexChanged="ddlEmpNamelApp_SelectedIndexChanged" CssClass=" chzn-select form-control inputTxt pull-left" TabIndex="2" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-5 pading5px asitCol5">
                                                    <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to ">Date</asp:Label>
                                                    <asp:TextBox ID="txtformdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtformdate">
                                                    </cc1:CalendarExtender>

                                                    <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to">From</asp:Label>
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtformdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                                    <asp:Label ID="lblComPanylApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                                    <asp:Label ID="lblSectionlApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label23" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                                    <asp:Label ID="lblDesignationlApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>
                                                <div class="col-md-3 pull-right">
                                                    <asp:Label ID="Label27" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label29" runat="server" CssClass="lblTxt lblName">Joining Date</asp:Label>
                                                    <asp:Label ID="lblJoiningDatelApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                </fieldset>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvLeaveStatus01" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="208px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This. Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvltaken1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt21" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy ") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
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




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
