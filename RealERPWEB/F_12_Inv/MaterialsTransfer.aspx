<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MaterialsTransfer.aspx.cs" Inherits="RealERPWEB.F_12_Inv.MaterialsTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .fontbold {
            font-weight: bold !important;
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

                var gvisu = $('#<%=this.grvacc.ClientID %>');
                $.keynavigation(gvisu);
                //gvisu.Scrollable();

            });

            $('.chzn-select').chosen({ search_contains: true });

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
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-7 pading5px">
                                        <asp:Label ID="lblpage" runat="server" CssClass="lblTxt lblName">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to"> Date</asp:Label>
                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">ID</asp:Label>
                                        <asp:Label ID="lblCurTransNo1" runat="server" CssClass="smltxtBox"></asp:Label>
                                        <asp:Label ID="txtCurTransNo2" runat="server" CssClass=" smltxtBox60px" Style="margin-left: 5px;"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to">Ref</asp:Label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass=" inputtextbox" Style="width: 115px;"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                    <div class="col-md-1 ">

                                        <asp:CheckBox ID="chkGatePass" runat="server" TabIndex="10" Text="From Gate Pass" CssClass="btn btn-primary checkBox" AutoPostBack="true" Style="margin: 0 0 0 50px;" OnCheckedChanged="chkGatePass_CheckedChanged" />

                                    </div>



                                    <div class="col-md-3 pull-right">


                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectFromList" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txtSrcNotify" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindNotify" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlprjlistfrom" runat="server" Style="width: 336px;" CssClass="chzn-select form-control  inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">To</asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control  inputTxt" Style="width: 336px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblddlProjectTo" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 pading5px asitCol4 pull-right">

                                        <asp:Label ID="lblPre" runat="server" CssClass=" smLbl_to" Text="Previous List"></asp:Label>
                                        <asp:TextBox ID="txtRefNo1" runat="server" TabIndex="7" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnPreList" runat="server" CssClass="btn btn-primary srearchBtn" Style="float: left;" TabIndex="10" OnClick="ImgbtnPreList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" ddlPage  inputTxt" TabIndex="6" Width="140px">
                                        </asp:DropDownList>

                                    </div>

                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblGatePassNo" runat="server" CssClass="lblTxt lblName" Visible="false">Gate Pass No</asp:Label>
                                        <asp:TextBox ID="txtsrchGatePass" runat="server" CssClass=" inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnGatePassNo" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="false" OnClick="lbtnGatePassNo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlGatePass" runat="server" CssClass="form-control inputTxt" Visible="false">
                                        </asp:DropDownList>

                                    </div>


                                </div>


                            </div>
                        </fieldset>

                        <asp:Panel ID="pnlgrd" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName">Resource List:</asp:Label>
                                            <asp:TextBox ID="txtSearchRes" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlreslist" runat="server" CssClass="chzn-select form-control inputTxt" Style="width: 336px;" OnSelectedIndexChanged="ddlreslist_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass=" chzn-select" Width="90px">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton ID="lnkselect0" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselect_Click">Select</asp:LinkButton>

                                        </div>
                                        <asp:Label ID="lblVoucherNo" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>

                                    </div>

                                </div>


                            </fieldset>






                        </asp:Panel>


                        <asp:Panel ID="pnlGatePass" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblResListgp" runat="server" CssClass="lblTxt lblName">Resource List:</asp:Label>
                                            <asp:TextBox ID="txtSearchResgp" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="lbtnFindResgp" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lbtnFindResgp_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlreslistgp" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlreslistgp_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblSpecificationgp" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlResSpcfgp" runat="server" CssClass="ddlPage62">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lnkselectgp" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselectgp_Click">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lnkselectgpAll" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselectgpAll_Click" Style="margin-left: 2px;">Select All</asp:LinkButton>

                                        </div>
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                    </div>

                                </div>


                            </fieldset>






                        </asp:Panel>

                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                            OnPageIndexChanging="grvacc_PageIndexChanging" OnRowDataBound="grvacc_RowDataBound"
                            OnRowDeleting="grvacc_RowDeleting" PageSize="15">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatCode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MTRF No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmtrref" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Gate Pass No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgatepref" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Resource Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server"
                                            Style="font-size: 11px; text-align: center;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Total</asp:LinkButton>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Balance Quantity">

                                    <ItemTemplate>

                                        <asp:Label ID="lblBalqty" runat="server"
                                            Style="font-size: 11px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                        VerticalAlign="Middle" Font-Size="12px" />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="MTRF  Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmtrfqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Receiving Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>

                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                            Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblamt" runat="server"
                                            Style="font-size: 11px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                        VerticalAlign="Middle" Font-Size="12px" />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="spcfcode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgspcfcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>




                        <div class="form-group">
                            <div class="col-md-6 pading5px">
                                <div class="input-group">
                                    <span class="input-group-addon glypingraddon">
                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                    </span>
                                    <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



