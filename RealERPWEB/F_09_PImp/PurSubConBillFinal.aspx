<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurSubConBillFinal.aspx.cs" Inherits="RealERPWEB.F_09_PImp.PurSubConBillFinal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
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

            var gvSubBill = $('#<%=this.gvSubBill.ClientID %>');
            gvSubBill.gridviewScroll({
                width: 1165,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                headerrowcount: 2,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6


            });


        };


    </script>




    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-6">
                                <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"
                                    Text="Date:"></asp:Label>

                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>


                                <asp:Label ID="lbldateTo" runat="server" Font-Bold="True"
                                    Text="Ref No" CssClass="smLbl_to" Visible="true"></asp:Label>

                                <asp:TextBox ID="txtCBillRefNo" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                <asp:Label ID="lblRefNo2" runat="server" Font-Bold="True"
                                    Text="Bill No" CssClass="smLbl_to" Visible="true"></asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="inputtextbox"></asp:Label>
                                <asp:Label ID="lblCurNo2" runat="server" CssClass="inputtextbox"></asp:Label>
                                <asp:LinkButton ID="lbtnOk" runat="server"
                                    OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">
                                <asp:Label ID="Label1" runat="server"
                                    Text="Project Name" CssClass="lblTxt lblName"></asp:Label>

                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>


                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" runat="server"
                                    AutoPostBack="True" CssClass="chzn-select form-control  inputTxt chzn-select" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged1">
                                </asp:DropDownList>
                                <asp:Label ID="lblProjectdesc" runat="server" Visible="false" CssClass=" form-control inputTxt"></asp:Label>


                            </div>

                        </div>
                        <div class="form-group">

                            <div class="col-md-3 asitCol3 pading5px">
                                <asp:Label ID="Label2" runat="server"
                                    Text="Contractor Name" CssClass="lblTxt lblName"></asp:Label>

                                <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindSubConName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>


                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlSubName" runat="server" OnSelectedIndexChanged="ddlSubName_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="chzn-select form-control  inputTxt chzn-select">
                                </asp:DropDownList>
                                <asp:Label ID="lblSubDesc" runat="server" Visible="false" CssClass=" form-control inputTxt"></asp:Label>


                            </div>

                        </div>

                        <div class="form-group">

                            <div class="col-md-3 asitCol3 pading5px">
                                <asp:Label ID="lblPreList" runat="server"
                                    Text="Previous List" CssClass="lblTxt lblName"></asp:Label>

                                <asp:TextBox ID="txtSrcPreBill" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <asp:LinkButton ID="ibtnPreBillList" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnPreBillList_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>


                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlPrevBillList" runat="server"
                                    AutoPostBack="True" CssClass="chzn-select form-control  inputTxt">
                                </asp:DropDownList>

                            </div>

                        </div>

                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Font-Bold="True"
                                    Text="Size:" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="ddlistPull"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                    Width="85px">
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


                            <%--<div class="col-md-3 pading5px">

                                         <asp:Label ID="lblbillref" runat="server" Font-Bold="True"
                                            Text="Bill Type:" CssClass="smLbl_to" Visible="false"></asp:Label>

                                        <asp:TextBox ID="txtbillref" runat="server" CssClass="inputtextbox" Width="200px" Visible="false"></asp:TextBox>
                                          </div>--%>

                            <div class="col-md-1 pading5px">
                                <asp:CheckBox ID="ChkTopSheet" runat="server" CssClass="margin5px btn btn-primary  checkBox" AutoPostBack="True" Visible="false"
                                    Text="Top Sheet" />

                            </div>

                            <div class="col-md-3 asitCol3 pading5px">
                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>


                            </div>


                        </div>
                    </div>
                </fieldset>


                <asp:Panel ID="panel11" runat="server" Visible="false">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">

                                <div class="col-md-3 asitCol3 pading5px">
                                    <asp:Label ID="lblRAList" runat="server"
                                        Text="R/A List" CssClass="lblTxt lblName"></asp:Label>

                                    <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>


                                <div class="col-md-4 asitCol4 pading5px">
                                    <asp:DropDownList ID="ddlRAList" runat="server"
                                        AutoPostBack="True" CssClass=" form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectRes_Click">Select</asp:LinkButton>
                                    <asp:Label ID="lblVounum" runat="server" Visible="False"></asp:Label>
                                </div>

                            </div>

                        </div>


                    </fieldset>
                </asp:Panel>
                <asp:GridView ID="gvSubBill" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvSubBill_PageIndexChanging" OnRowDataBound="gvSubBill_RowDataBound"
                    ShowFooter="True" Width="650px">
                    <PagerSettings Position="Top" />
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>

                                <asp:LinkButton ID="btnDelbill" OnClick="btnDelbill_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs" ToolTip="Delete Bill Checked">
                                    
                                                        <i style="color:red" class="fa  fa-trash" aria-hidden="true"></i> </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="25px" />
                            <HeaderStyle HorizontalAlign="Center" Width="25px" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Issue No1" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgcIsuno1" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno")) %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue No">
                            <ItemTemplate>
                                <asp:Label ID="lgcIsuno" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno2")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ref No">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnTotal" runat="server"
                                    OnClick="lbtnTotal_Click" CssClass="btn  btn-primary  primarygrdBtn">Total</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgcIsurefno" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisurefno")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Floor Description">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnDeleteBill" runat="server" Font-Bold="True"
                                    OnClick="lbtnDeleteBill_Click"
                                    CssClass="btn btn-primary primarygrdBtn">Delete All</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgcFlrDesc" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Labour">

                            <ItemTemplate>
                                <asp:Label ID="lgclbdesc" runat="server"
                                    Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")   %>'
                                    Width="280px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnUpdate" runat="server"
                                    OnClick="lbtnUpdate_Click" CssClass="btn  btn-danger  primarygrdBtn">Update</asp:LinkButton>
                            </FooterTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit ">
                            <ItemTemplate>
                                <asp:Label ID="lgvlbUnit" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblgvwrkqty" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="70px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Percent %">
                            <ItemTemplate>
                                <asp:Label ID="lblgvprcent" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="60px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Budgeted Qty" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvbgdqty" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="60px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Budgeted Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblschrate" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="70px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Bal.Qty" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblbalqty" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="65px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Issued % on Budget">
                            <ItemTemplate>
                                <asp:Label ID="lblperobgd" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronbgd")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="70px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Con. Qty">
                            <ItemTemplate>
                                <asp:Label ID="lgvconqty" runat="server" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnConfirmed" runat="server" Font-Bold="True"
                                    OnClick="lbtnConfirmed_Click" CssClass="btn  btn-primary primarygrdBtn">Approved</asp:LinkButton>

                            </FooterTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:TextBox ID="lgvSubRate" runat="server" Style="text-align: right; border-style: none;" BackColor="Transparent"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.000;-#,##0.000; ") %>'
                                    Width="70px"></asp:TextBox>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="right" />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="70px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                    Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                            </FooterTemplate>




                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Ded. Qty" Visible="false">

                            <ItemTemplate>
                                <asp:Label ID="lblgvdedqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="50px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ded. Unit" Visible="false">

                            <ItemTemplate>
                                <asp:Label ID="lblgvdedunit" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dedunit")) %>'
                                    Width="50px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Ded. Rate" Visible="false">

                            <ItemTemplate>
                                <asp:Label ID="lblgvdedrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="50px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ded. Amount" Visible="false">
                            <FooterTemplate>
                                <asp:Label ID="lblFidedamt" runat="server" Style="text-align: right"
                                    Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                            </FooterTemplate>

                            <ItemTemplate>
                                <asp:Label ID="lblgvdedamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "idedamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="50px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Bill Amount After Deduction" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvadedamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adedamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="70px" Style="text-align: right"></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblgvFadedamount" runat="server" Style="text-align: right"
                                    Width="70px" Font-Size="12px" ForeColor="#000"></asp:Label>
                            </FooterTemplate>

                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Above" Visible="false">

                            <ItemTemplate>
                                <asp:TextBox ID="txtgvabove" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "above")).ToString("#,##0.00;-#,##0.00; ") %>'
                                    Width="50px" Style="text-align: right"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bill  Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvamt" runat="server" Style="text-align: right; border-style: none;" BackColor="Transparent"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                    Width="70px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFBillAmt" runat="server" Style="text-align: right" Font-Bold="True"
                                    Font-Size="12px" ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="right" />

                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>


                <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                    <fieldset class=" scheduler-border fieldset_Nar">
                        <div class="form-horizontal">

                            <div class="form-group">

                                <div class="col-md-12  pading5px">
                                    <asp:Label ID="lblsecurity" runat="server" CssClass="lblTxt lblName" Text="Security Deposit:"></asp:Label>
                                    <asp:TextBox ID="txtpercentage" runat="server" CssClass="inputtextbox" Style="width: 40px; text-align: right;" Text=""></asp:TextBox>
                                    <asp:LinkButton ID="lbtnDepost" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnDepost_Click">Amt</asp:LinkButton>
                                    <asp:TextBox ID="txtSDAmount" runat="server" CssClass="inputtextbox" Style="text-align: right;"></asp:TextBox>
                                    <asp:Label ID="lbldeduction" runat="server"
                                        Text="Deduction" CssClass=" smLbl_to"></asp:Label>
                                    <asp:TextBox ID="txtDedAmount" runat="server" CssClass="inputtextbox" Style="text-align: right;"></asp:TextBox>
                                    <asp:Label ID="lblpenalty" runat="server"
                                        Text="Penalty" CssClass=" smLbl_to"></asp:Label>
                                    <asp:TextBox ID="txtPenaltyAmount" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>

                                    <asp:Label ID="lblAdvanced" runat="server"
                                        Text="Advanced" CssClass=" smLbl_to"></asp:Label>
                                    <asp:TextBox ID="txtAdvanced" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>


                                    <asp:Label ID="lblReward" runat="server"
                                        Text="Reward" CssClass=" smLbl_to"></asp:Label>
                                    <asp:TextBox ID="txtreward" runat="server" CssClass="inputtextbox" Style="width: 60px; text-align: right;"></asp:TextBox>



                                    <asp:Label ID="lblnettotal" runat="server"
                                        Text="Net Total:" CssClass=" smLbl_to"></asp:Label>

                                    <asp:Label ID="lblvalnettotal" runat="server" CssClass="smLbl_to" Style="text-align: right; color: blue;"></asp:Label>

                                    <a href="<%=this.ResolveUrl ("~/F_09_PImp/RptConTractorBillAll")%>" target="_blank" class="btn btn-info btn-sm" style="margin-left: 10px; font-weight: bold; font-size: 14px;">Details<span class="fa fa-history"></span></a>



                                </div>
                            </div>

                            <div class="form-group">

                                <div class="col-md-12  pading5px">
                                    <asp:Label ID="lblremarks" runat="server"
                                        Text="Remarks" CssClass="lblTxt lblName"></asp:Label>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="inputtextbox" TextMode="MultiLine" Style="height: 50px; width: 572px;"></asp:TextBox>
                                    <asp:Label ID="lblBillType" runat="server"
                                        Text="Bill Type" CssClass="  smLbl_to"></asp:Label>
                                    <asp:DropDownList ID="ddlbilltype" runat="server"
                                        CssClass=" ddlPage" Style="width: 115px; background-color: orange">
                                    </asp:DropDownList>


                                </div>

                            </div>
                        </div>


                    </fieldset>

                </asp:Panel>

                <asp:Panel runat="server" ID="pnlAttached" Visible="False">

                    <fieldset class="scheduler-border fieldset_Nar">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <asp:Panel runat="server" ID="pnlQutatt">


                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <span class="glyphicon glyphicon-upload"></span>BILL Documents Upload
   
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <fieldset class="alert alert-success">

                                                                <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                    OnClientUploadComplete="uploadComplete" runat="server"
                                                                    ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                    CompleteBackColor="White"
                                                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                    OnUploadedComplete="FileUploadComplete" />
                                                                <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                                <br />
                                                                <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                            </fieldset>


                                                        </div>



                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                                <div class="col-md-7">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <span class="glyphicon glyphicon-picture"></span>Uploaded Files
       
                                            <div class="pull-right">
                                                <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs" Text="Show Image" OnClick="btnShowimg_Click" />
                                                <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="panel-body ">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                <div id="EmpAll" runat="server">

                                                                    <asp:Label ID="ImgLink" Visible="False" runat="server" Text='<%# Eval("itemsurl") %>'></asp:Label>
                                                                    <asp:Label ID="billno" Visible="False" runat="server" Text='<%# Eval("billno") %>'></asp:Label>
                                                                    <asp:Label ID="id" Visible="False" runat="server" Text='<%# Eval("id") %>'></asp:Label>

                                                                    <a href="../../Upload/Purchase/<%# Eval("itemsurl") %>" class="uploadedimg" target="_blank">
                                                                        <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                                    </a>
                                                                    <div class="checkboxcls">
                                                                        <asp:CheckBox ID="ChDel" runat="server" />
                                                                    </div>


                                                                </div>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </fieldset>
                </asp:Panel>
            </div>


        </div>
    </div>









    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--  </asp:UpdatePanel>--%>


    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
</asp:Content>

