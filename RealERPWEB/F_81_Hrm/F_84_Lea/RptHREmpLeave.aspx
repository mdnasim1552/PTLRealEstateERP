
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptHREmpLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.RptHREmpLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
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
                    <div>
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                     <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompanyAgg" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindCompanyAgg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindCompanyAgg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCompanyNameAgg" runat="server" Style="border: none; line-height: 1.5" CssClass="form-control dataLblview" Height="22" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lbldeptnameagg" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                            <asp:TextBox ID="txtsrchdeptagg" runat="server" CssClass="inputTxt inputName inpPixedWidth" ></asp:TextBox>
                                            <asp:LinkButton ID="lbtndeptagg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lbtndeptagg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblvaldeptagg" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                                        </div>



                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblsection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3 pull-right">
                                        </div>

                                    </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblpreAdv" runat="server" CssClass="lblTxt lblName">Employee</asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:DropDownList ID="ddlEmployee" runat="server"  CssClass="chzn-select form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                        
                                    </div>
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">ok</asp:LinkButton>


                                </div>

                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                    </div>

                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewEmpLeave" runat="server">

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblleavesummary" runat="server" CssClass="btn btn-primary primaryBtn" Visible="False">Leave Summary</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-md-12">
                                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="567px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvLeaveStatus_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFOpening" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnleave" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Entitled Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleaveentitled" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleaveentitled" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Enjoyed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleaveenjoy" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleaveenjoy" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavebal" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleavebal" runat="server"
                                                    Style="color: white; font-size: 11px; font-weight: bold;" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                    Width="200px"></asp:Label>
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
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblleavesDetails" runat="server" CssClass="btn btn-primary primaryBtn" Visible="False">Leave Details</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvLeavedetails" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="567px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvLeavedetails_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescriptionld" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Apply Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvapplydate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Leave Applied(From)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvfrmdate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "strtdat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Leave Applied(End)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenddate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "enddat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Leave Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavedays" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFleavedays" runat="server"
                                                        Style="color: white; font-size: 11px; font-weight: bold;" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Reasons">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavereason" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                                        Width="200px"></asp:Label>
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
                            </div>


                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>


            
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
