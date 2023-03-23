<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryPFBenefit.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.EntryPFBenefit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
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
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
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

            $('.chzn-select').chosen({ search_contains: true });
        }

        function filter2(phrase, _id) {
            var words = phrase.value.toLowerCase().split(" ");
            var table = document.getElementById(_id);
            var ele;
            for (var r = 0; r < table.rows.length; r++) {
                ele = table.rows[r].innerHTML.replace(/<[^>]+>/g, "");
                var displayStyle = 'none';
                for (var i = 0; i < words.length; i++) {
                    if (ele.toLowerCase().indexOf(words[i]) >= 0)
                        displayStyle = '';
                    else {
                        displayStyle = 'none';
                        break;
                    }
                }
                table.rows[r].style.display = displayStyle;
            }
        }

    </script>



    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="RealProgressbar">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                        <asp:Label ID="Label2" runat="server">Company</asp:Label>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-3">
                    <div class="form-group">
                        <asp:Label ID="lblDept" runat="server">Department</asp:Label>

                        <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control chzn-select" TabIndex="6">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-3">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server">Section</asp:Label>

                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" TabIndex="6">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1">
                    <div class="form-group">
                        <asp:Label ID="lbllstVouno0" runat="server" Text="Interest "></asp:Label>
                        <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                </div>

                <div class="col-lg-1">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server">Year</asp:Label>
                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-1">
                    <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                </div>
            </div>
            <div class="row">




                <div class="col-lg-2">
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server">ID Card</asp:Label>
                        <input name="txtTerm" onkeyup="filter2(this, '<%=gvProFund.ClientID %>')" type="text" class="form-control form-control-sm" placeholder="Search here">
                    </div>
                </div>


                <div class="col-lg-1">
                    <div cssclass="form-group">
                        <asp:Label ID="lblFDate" runat="server" CssClass="smLbl_to "
                            Text="From:"></asp:Label>
                        <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm " TabIndex="5"></asp:TextBox>
                        <cc1:CalendarExtender runat="server"
                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-lg-1">
                    <asp:Label ID="lblToDate" runat="server" CssClass="smLbl_to "
                        Text="To:"></asp:Label>
                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" TabIndex="5"></asp:TextBox>
                    <cc1:CalendarExtender runat="server"
                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                </div>

                <div class="col-lg-1">
                    <div class="form-group">
                        <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvProFund" runat="server" AutoGenerateColumns="False"
                    ShowFooter="True" AllowPaging="true" CssClass="table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvProFund_PageIndexChanging" EmptyDataText="No records Found">
                    <RowStyle />
                    <Columns>


                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: center"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="lgsection" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Card #">
                            <ItemTemplate>
                                <asp:Label ID="lgvcardno" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                    Width="45px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnFinalUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnFinalUpdate_Click"
                                    Style="text-align: center">Update</asp:LinkButton>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <table style="width: 170px">
                                    <tr>
                                        <td class="style225">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px"
                                                Text=" Employee Name " Width="90px"></asp:Label>
                                        </td>
                                        <td class="style237">
                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                ForeColor="White" Style="text-align: center" Width="80px">Export Exel</asp:HyperLink>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvempname" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                    Width="170px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnCal" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#000" OnClick="lbtnCal_Click" Style="text-align: center">Calculation</asp:LinkButton>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Principle">
                            <ItemTemplate>
                                <asp:Label ID="lblgvPrinciple" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFPrinciple" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Benefit">
                            <ItemTemplate>
                                <asp:Label ID="lblgvBenefit" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "benamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFBen" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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

        </div>
    </div>

    <%--    <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>
                                <div class="pull-left">
                                    <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-4 pading5px">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                    <ProgressTemplate>
                                        <asp:Label ID="s" runat="server" CssClass="btn btn-info primaryBtn " Text="Please wait . . . . . . ."></asp:Label>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3 pading5px asitCol4">
                                <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                </asp:DropDownList>

                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>
                                <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                            </div>

                        </div>

                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSecSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3 pading5px asitCol4">
                                <asp:DropDownList ID="ddlSection" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                </asp:DropDownList>

                                <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlSection">
                                </cc1:ListSearchExtender>

                            </div>

                        </div>

                        <div class="form-group">
                            <div class="col-md-2  pading5px">
                                <asp:Label ID="lbllstVouno0" runat="server" CssClass="lblTxt lblName" Text="Interest "></asp:Label>
                                <asp:TextBox ID="txtInterest" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                            </div>
                            <div class="col-md-2  pading5px">
                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to"  Style="margin-left:-30px;">Year</asp:Label>
                                <asp:DropDownList ID="ddlyear" runat="server" Width="80px" CssClass="form-control inputTxt"></asp:DropDownList>
                        </div>
                          <div class="col-md-8  pading5px">
                                <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to " Style="margin-left:-87px;">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="70px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                               
                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                            </div>



                        </div>
                    </div>

                    <div class="clearfix"></div>

                </fieldset>--%>







    <%--      </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

