<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktGrandNoteSheet.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktGrandNoteSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

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
    <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }
        /*.cald {
             z-index: 1;
        }*/
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

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" Visible="false" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select inputTxt" Width="350px" TabIndex="12">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>
                                        <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" Width="350px" CssClass="lblTxt lblName txtAlgLeft"></asp:Label>
                                    </div>

                                    <div class=" col-md-7  pading5px">
                                        <asp:Label ID="lblSearch" CssClass=" smLbl_to" runat="server" Text="Unit Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchunit" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnsrchunit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnsrchunit_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        <asp:Label ID="lmsg" runat="server" CssClass="lblTxt lblName  btn-danger pull-right" Visible="false"></asp:Label>
                                    </div>
                                </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:CommandField ShowEditButton="True" HeaderStyle-Width="80px" CancelText="&lt;span class='glyphicon glyphicon-remove pull-left'&gt;&lt;/span&gt;" DeleteText="&lt;span class='glyphicon glyphicon-remove'&gt;&lt;/span&gt;" EditText="&lt;span class='glyphicon glyphicon-pencil'&gt;&lt;/span&gt;" UpdateText="&lt;span class='glyphicon glyphicon-ok'&gt;&lt;/span&gt;" />

                                <asp:TemplateField HeaderText="Sl.No.">



                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>


                                        <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize"
                                            OnClick="lbtnusize_Click" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Parking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoneyaa" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Utility">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmvoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Co-oparative">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmincbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cooperative")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminsbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Min Booking Money" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Mgt Booking" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <%--<asp:TemplateField HeaderText="Client Name" Visible="false">
                                    <EditItemTemplate>
                                        
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcclientname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" Visible="false" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>

                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewSchedule" runat="server">



                            <div class="row">

                                <fieldset class="scheduler-border fieldset_B" runat="server">

                                    <div class="form-horizontal">

                                        <asp:Panel ID="pnlgenMrr" runat="server" Visible="False">
                                            <div class=" form-group">
                                                <div class="col-md-12 pading5px">

                                                    <asp:Label ID="lbltotalamt" runat="server" CssClass=" lblTxt lblName">Total Amount</asp:Label>
                                                    <asp:TextBox ID="txttoamt" runat="server" CssClass="  inputtextbox txtAlgRight"></asp:TextBox>

                                                    <asp:Label ID="lblfinsdate" runat="server" CssClass="smLbl_to">First Ins. Date</asp:Label>
                                                    <asp:TextBox ID="txtfinsdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfinsdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfinsdate"></cc1:CalendarExtender>

                                                    <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="Installment No"></asp:Label>
                                                    <asp:TextBox ID="txtnofins" runat="server" CssClass=" smltxtBox60px txtAlgRight"></asp:TextBox>




                                                    <asp:Label ID="lblacbooking" runat="server" CssClass="smLbl_to" Text="Actual Booking"></asp:Label>
                                                    <asp:TextBox ID="txtacbooking" runat="server" CssClass=" smltxtBox60px txtAlgRight"></asp:TextBox>

                                                    <asp:Label ID="lblacinstallment" runat="server" CssClass="smLbl_to" Text="Actual Ins."></asp:Label>
                                                    <asp:TextBox ID="txtacinstallment" runat="server" CssClass=" smltxtBox60px txtAlgRight"></asp:TextBox>

                                                    <asp:Label ID="lblduration" runat="server" CssClass="  lblTxt lblName">Duration</asp:Label>
                                                    <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="True"
                                                        CssClass="ddlPage" Width="90px">
                                                        <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                        <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                        <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                                        <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                        <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                                        <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                        <asp:ListItem Value="7">7 Month</asp:ListItem>
                                                        <asp:ListItem Value="8">8 Month</asp:ListItem>
                                                        <asp:ListItem Value="9">9 Month</asp:ListItem>
                                                        <asp:ListItem Value="10">10 Month</asp:ListItem>
                                                        <asp:ListItem Value="11">11 Month</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary primaryBtn margin5px" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>


                                                </div>

                                                <div class="clearfix"></div>
                                            </div>
                                        </asp:Panel>


                                        <asp:Panel ID="PanelAddIns" runat="server" Visible="False">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Installement</asp:Label>
                                                    <asp:TextBox ID="txtsrchInstallment" runat="server" CssClass="inputTxt inpPixedWidth"></asp:TextBox>

                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="ibtnFindInstallment" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindInstallment_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 pading5px asitCol3">
                                                    <asp:DropDownList ID="ddlInstallment" runat="server" CssClass="form-control inputTxt" TabIndex="12">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lbtnAddInstallment" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAddInstallment_Click">Add</asp:LinkButton>
                                                </div>
                                                <div class="clearfix"></div>

                                            </div>
                                        </asp:Panel>

                                        <div class="form-group">


                                            <div class="col-md-3">
                                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass="chkBoxControl margin5px"
                                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment" />
                                                <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                                    CssClass="chkBoxControl"
                                                    OnCheckedChanged="chkAddIns_CheckedChanged" Text="Add.Installment" />
                                                <asp:LinkButton ID="lbtnUpdate" CssClass="btn  btn-danger  btn-xs " runat="server" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </div>

                                        </div>

                                        <div class="form-group">

                                            <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="row">
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lblschdule" runat="server" CssClass="btn btn-success primaryBtn" Style="width: 300px;" Text="Sehedule"></asp:Label>
                                    <div class="clearfix"></div>
                                    <asp:GridView ID="gvdumpay" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="398px"
                                        Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslnodumpay" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="gcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgcod" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescription" runat="server" ForeColor="Black"
                                                        Height="16px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                        Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvScheduledate_CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvScheduledate"></cc1:CalendarExtender>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotaldumsch" CssClass="btn btn-primary  btn-xs" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lbtnTotaldumsch_Click"></asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Schedule Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvdumschamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFdumschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="lbtnAdddsch" OnClick="lbtnAdddsch_Click" ToolTip="Add Installment" runat="server" Visible="false"><span  class="glyphicon glyphicon-plus"></span> </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDeldsch" OnClick="lbtnDeldsch_Click" ToolTip="Delete Installment" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Top" />
                                            </asp:TemplateField>




                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>


                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lblactual" runat="server" CssClass="btn btn-success primaryBtn" Style="width: 300px;" Text="Actaul"></asp:Label>
                                    <div class="clearfix"></div>
                                    <asp:GridView ID="gvacpay" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="300px"
                                        Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslnoacpay" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvacScheduledate" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                        Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvacScheduledate_CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvacScheduledate"></cc1:CalendarExtender>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalacsch" CssClass="btn btn-primary btn-xs" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Text="Total" OnClick="lbtnTotalacsch_Click"></asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Schedule Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvacschamt" runat="server" Style="text-align: right; border-style: none;" BackColor="Transparent"
                                                        BorderStyle="None"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFacschamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>

                                                    <asp:LinkButton ID="lbtnDelacshall" ToolTip="Delete Installment" OnClick="lbtnDelacshall_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>

                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="lbtnAddacsch" ToolTip="Add Installment" OnClick="lbtnAddacsch_Click" runat="server"><span  class="glyphicon glyphicon-plus"></span> </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelacsch" ToolTip="Delete Installment" OnClick="lbtnDelacsch_Click"  runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>


                                </div>
                                <div class="col-md-3 pading5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="btn btn-success primaryBtn" Style="width: 300px;" Text="Sehedule Vs Actual"></asp:Label>
                                    <div class="clearfix"></div>
                                    <fieldset class="scheduler-border fieldset_B" runat="server">

                                        <div class="form-horizontal">

                                            <div class=" form-group">
                                                <div class="col-md-12 pading5px">

                                                    <asp:Label ID="lbldiscount" runat="server" CssClass=" lblTxt lblName">Discount</asp:Label>
                                                    <asp:TextBox ID="txtdiscount" runat="server" CssClass="  smltxtBox60px txtAlgRight"></asp:TextBox>

                                                    <asp:Label ID="lblparking" runat="server" CssClass=" smLbl_to">Parking Amt</asp:Label>
                                                    <asp:TextBox ID="txtParking" runat="server" CssClass="  smltxtBox60px txtAlgRight"></asp:TextBox>




                                                </div>


                                            </div>

                                            <div class="form-group">
                                                <div class=" col-md-12 pading5px ">
                                                    <asp:Label ID="lblentryben" CssClass=" lblTxt lblName" runat="server" Text="Early Benefit:"></asp:Label>
                                                    <asp:TextBox ID="txtentryben" runat="server" CssClass=" smltxtBox60px txtAlgRight"></asp:TextBox>
                                                    <asp:Label ID="lbldelaychrg" CssClass="smLbl_to" runat="server" Text="Delay Charge:"></asp:Label>
                                                    <asp:TextBox ID="txtdelaychrg" runat="server" CssClass=" smltxtBox60px txtAlgRight" Style="width: 40px;"></asp:TextBox>
                                                </div>


                                            </div>





                                        </div>
                                    </fieldset>
                                    <asp:Label ID="lblsdumschdule" runat="server" CssClass="btn btn-success primaryBtn" Style="width: 300px;" Text="Sehedule"></asp:Label>
                                    <asp:GridView ID="gvsumdumsch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="300px"
                                        Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>


                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslnoacpay" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdumdesc" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                        Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="150px"></asp:Label>



                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdumamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <asp:Label ID="lblsacschdule" runat="server" CssClass="btn btn-success primaryBtn" Style="width: 300px;" Text="Actual"></asp:Label>
                                    <asp:GridView ID="gvsumacsch" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="300px"
                                        Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>


                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslsumacsch" runat="server" Font-Bold="True" ForeColor="Black"
                                                        Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvacdesc" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="none"
                                                        Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="150px"></asp:Label>



                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvacamt" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="none"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

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
                                <asp:Label ID="lblInterestCaclution" runat="server" CssClass="btn btn-success primaryBtn" Style="width: 300px;" Text="Interest Calculation"></asp:Label>
                                <div class="clearfix"></div>

                                <asp:GridView ID="gvInterest" runat="server"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                    CssClass="table table-striped table-hover table-bordered grvContentarea" Width="639px" OnRowCreated="gvInterest_RowCreated" OnRowDataBound="gvInterest_RowDataBound">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <FooterTemplate>


                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                    CssClass="btn   btn-xs" ToolTip="Export Excel" Text="name"><i class="fa fa-file-excel-o" aria-hidden="true"></i>
                                                </asp:HyperLink>

                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Installment">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px" Text="Total"></asp:Label>
                                            </FooterTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvinsdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFinsamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvinsamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpaiddate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paiddate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpayamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Delay/Discount in Days" >
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdodisday" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dodisday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Interest Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvintrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intrate")).ToString("#,##0;(#,##0); ")+"%" %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Interest Amount (Per Day)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvintamtpday" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intamtpday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delay/Discount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdelordis" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdelodis" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delodis")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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




