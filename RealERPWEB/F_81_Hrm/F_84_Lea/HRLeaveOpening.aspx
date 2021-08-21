
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HRLeaveOpening.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.HRLeaveOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Yearly Leave</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" Font-Size="12px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"  AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                </div>
                    
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                   


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblCode" runat="server" CssClass="lblTxt lblName">Emp. Code</asp:Label>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmpSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmpSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White"></asp:Label>

                                        <asp:Label ID="lblgmtime" runat="server"></asp:Label>
                                    </div>
                                </div>



                            </div>
                        </fieldset>
                    </div>

                    <div class="row">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvLeaveRule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                >
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
                                            <asp:LinkButton ID="lnkbtnFUpLeave" runat="server"  CssClass="btn  btn-danger btn-xs" OnClick="lnkbtnFUpLeave_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
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

                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            &nbsp;<asp:TextBox ID="txtgvelOpen" runat="server" BackColor="Transparent" BorderStyle="None"
                                                ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opening")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Entitlement">
                                        <ItemTemplate>
                                            &nbsp;<asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
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
                    </div>


                </div>
            </div>

            <%--<table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style36">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text="Yearly Leave :" Width="90px" Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style29">
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                            Width="80px" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtoffdate_CalendarExtender" runat="server" 
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate" TodaysDateFormat="">
                                    </cc1:CalendarExtender>

                                    </td>
                                    <td class="style27">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Text="Company:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style28">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat" 
                                            Width="80px" TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td class="style30">
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgbtnCompany_Click" Width="16px" TabIndex="2" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Size="12px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" 
                                            Width="300px" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px" OnClick="lbtnOk_Click"
                                            BackColor="#003366" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                            ForeColor="White" Style="text-align: center; height: 17px;" Width="50px" 
                                            TabIndex="10">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style36">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Style="margin-left: 0px" Width="85px" TabIndex="4">
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
                                    </td>
                                    <td class="style27">
                                        <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right; margin-left: 0px;" Text="Section Name:" 
                                            Width="81px"></asp:Label>
                                    </td>
                                    <td class="style28">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px" 
                                            TabIndex="5"></asp:TextBox>
                                    </td>
                                    <td class="style30">
                                        <asp:ImageButton ID="imgbtnProSrch" runat="server" Height="16px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgbtnProSrch_Click" Width="16px" TabIndex="6" />
                                    </td>
                                    <td>
                                        &nbsp;<asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="300px" TabIndex="7">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains"
                                            TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style29">
                                        &nbsp;</td>
                                    <td class="style27">
                                        <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Style="text-align: right; margin-left: 0px;" 
                                            Text="Emp. Code:" Width="81px"></asp:Label>
                                    </td>
                                    <td class="style28">
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="txtboxformat" 
                                            Width="80px" TabIndex="8"></asp:TextBox>
                                    </td>
                                    <td class="style30">
                                        <asp:ImageButton ID="imgbtnEmpSrch" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnEmpSrch_Click" 
                                            Width="16px" TabIndex="9" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                               
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                 <tr>
                                    <td colspan="18">
                                        <asp:GridView ID="gvLeaveRule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" Width="834px"
                                            PageSize="15">
                                            <PagerSettings Position="Top" />
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
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
                                                        <asp:LinkButton ID="lnkbtnFUpLeave" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="White" OnClick="lnkbtnFUpLeave_Click">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
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

                                                <asp:TemplateField HeaderText="Opening">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:TextBox ID="txtgvelOpen" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opening")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Entitlement">
                                                    <ItemTemplate>
                                                        &nbsp;<asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                            ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right"/>
                                                </asp:TemplateField>
                                               
                                            </Columns>
                                            <FooterStyle BackColor="#333333" />
                                            <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                Height="20px" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
