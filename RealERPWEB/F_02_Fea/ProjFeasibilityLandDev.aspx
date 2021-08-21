<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjFeasibilityLandDev.aspx.cs" Inherits="RealERPWEB.F_02_Fea.ProjFeasibilityLandDev" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

    
   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <%-- <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>--%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server"
                                            Visible="False" CssClass="form-control inputTxt"></asp:Label>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pull-right pading5px">

                                        <asp:Label ID="lblFeaProLock" runat="server" Visible="False"></asp:Label>
                                    </div>

                                </div>
                                <asp:Panel ID="PanelSelName" runat="server" Visible="False">

                                    <div class="form-group">
                                        <div class="col-sm-1">
                                            <asp:CheckBox ID="chkAllRes" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllSInf_CheckedChanged" CssClass="btn btn-primary primaryBtn margin5px chkBoxControl"
                                                Text="Show All" />
                                        </div>
                                        <div class="col-sm-6 pading5px">
                                            <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#0B88C5" ForeColor="#000" AutoPostBack="True" CssClass="btn rbtnList1 margin5px  primaryBtn " OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                                                RepeatColumns="6" RepeatDirection="Horizontal">
                                                <asp:ListItem>Project Information</asp:ListItem>
                                                <asp:ListItem>Cost</asp:ListItem>
                                                <asp:ListItem>Sales Revenue</asp:ListItem>
                                                <asp:ListItem>Reports</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3 pading5px">

                                            <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                        <div class="clearfix"></div>
                                    </div>



                                </asp:Panel>
                            </div>




                            <table style="width: 100%;">

                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="ViewProjectInfo" runat="server">
                                                 <div class="table table-responsive">
                                                <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" Width="772px">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo0"
                                                                    runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle
                                                                HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label
                                                                    ID="lblgvItmCode" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle
                                                                HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <FooterTemplate>
                                                                <asp:CheckBox ID="chkProjectLock" runat="server" Font-Bold="True"
                                                                    Font-Size="12px" ForeColor="#000" Text="Project Lock" Width="90px" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label
                                                                    ID="lgcResDesc1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True"
                                                                HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center"
                                                                VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgp" runat="server"
                                                                    Font-Bold="True" Font-Size="12px" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                    Width="4px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label
                                                                    ID="lgvgval" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lUpdatProInfo"
                                                                    runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn"
                                                                    OnClick="lUpdatProInfo_Click">Update Information</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox
                                                                    ID="txtgvVal" runat="server" BackColor="Transparent" BorderColor="#660033"
                                                                    BorderStyle="Solid" BorderWidth="1px" Height="20px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>'
                                                                    Width="510px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle
                                                                HorizontalAlign="Center" VerticalAlign="Top" />
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

                                            <asp:View ID="ViewCost" runat="server">
                                                 <div class="table table-responsive">
                                                <asp:GridView ID="gvFeaPrjFC" runat="server" AutoGenerateColumns="False"
                                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" Width="700px">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodfc" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnTotalCostFC" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalCostFC_Click">Total</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description of Item">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnfUpdateCostFC" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateCostFC_Click">Final Update</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>


                                                                <asp:TextBox ID="txtgvItemdescc" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                                    Width="200px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgUnitnum2" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Land(Bigha)">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvlandsize" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Katha">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvconktosft" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Total SFT">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvsftperf" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Saleable %">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFFar" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvFar" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "far")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="% of Road">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFBenifit" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvBenifit" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "benifit")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="GL">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvpercntge" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Per/Khata">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtlsizes" runat="server" Font-Size="11px" BackColor="Transparent" BorderStyle="None"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="No. of Plot">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvstonum" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0.0;(#,##0.0); ") %>'
                                                                    Width="50px"></asp:Label>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Saleable Katha">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFtcsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvtcsft" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BW(RFT)">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFRft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRft" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rft")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <FooterStyle CssClass="grvFooter" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>
                                                     </div>
                                                <hr class=" hrline" />
                                                 <div class="table table-responsive">
                                                <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False"
                                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    OnRowDeleting="gvFeaPrjC_RowDeleting" ShowFooter="True" Width="792px"
                                                    OnRowDataBound="gvFeaPrjC_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnTotalCost" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description of Item">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnfUpdateCost" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateCost_Click">Final Update</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvItemdescc01" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                                    Width="200px"></asp:TextBox>


                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgUnitnumc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvqtyc" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00; -#,##0.00; ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Rate">

                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtgvratec" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="80px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Cost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgveamtc" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estam")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFeAmtc" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phase Common Cost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgvphwam" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "phwam")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFphwam" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="City Common Cost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgvprjwam" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prjwam")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFprjwam" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Budget Cost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvtcost" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFtcost" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="App. Add Cost">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvaaddamtc" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aadam")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFaaddAmtc" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Expected Add Cost">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvexaddamtc" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eadam")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFexaddAmtc" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Save Cost">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvsaveamtc" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "savam")).ToString("#,##0;-#,##0; ") %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFsaveAmtc" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Revised Cost">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgvgvtotalamtc" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFtotalAmtc" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Cost Per Katha">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvcospersft" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costpsft")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFCostpersftc" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
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
                                            <hr class=" hrline" />
                                            <asp:View ID="ViewSalesRevenue" runat="server">

                                                <div class=" form-group">
                                                    <asp:Label ID="lblSaleableAreaa" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Saleable Area (Total):"></asp:Label>
                                                    <asp:TextBox ID="lblPerFlr" runat="server" Visible="False" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                                    <asp:TextBox ID="lblBrate" runat="server" Visible="False" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <hr class=" hrline" />
                                                <div class="table table-responsive">
                                                <asp:GridView ID="gvFeaPrjFCS" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" Width="821px">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodfcs" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnTotalCostFCS" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalCostFCS_Click">Total</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description of Item">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtnfUpdateCostFCS" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnfUpdateCostFCS_Click">Final Update</asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvItemdescsalar" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                                    Width="200px"></asp:TextBox>


                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgUnitnum3" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Land(Bigha)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvlandsizes" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizek")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Katha">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvconktosfts" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contosft")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sft. Per Floor">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvsftperfs" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MGC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvpercntges" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Katha/ Plot">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgvlsizess" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsizes")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No of Plot">
                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtgvstonums" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stonum")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Saleable Khata">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFtssfts" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvtssfts" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcsizes")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Brochure Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvBQTY" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvBDesc" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdesc"))%>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                    </Columns>

                                                    <FooterStyle CssClass="grvFooter" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>
                                                    </div>
                                                <div class=" form-group">
                                                    <asp:Label ID="lblSaleableAreaDistributot" runat="server" Font-Bold="True"
                                                        Font-Size="12px" Text="Saleable Area Distribution:"
                                                        Width="206px"></asp:Label>
                                                    <div class="clearfix"></div>
                                                </div>

                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class=" form-group">
                                                        <asp:Label ID="lblLownertext" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Land Owner Share:"></asp:Label>
                                                        <asp:TextBox ID="lblLownerval" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                                        <asp:Label ID="lblCompanytext" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Company Share:"></asp:Label>

                                                        <asp:TextBox ID="lblCompanyval" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                                        <asp:Label ID="lblProfit" runat="server" CssClass=" smLbl_to" Style="font-size: 11px;" Text="Profit %:"></asp:Label>

                                                        <asp:TextBox ID="lblProfitVal" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                   
                                                </asp:Panel>

                                                <div class="table table-responsive">
                                                    <asp:GridView ID="gvFeaPrj" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        OnRowDeleting="gvFeaPrj_RowDeleting" ShowFooter="True" Width="651px"
                                                        OnRowDataBound="gvFeaPrj_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description of Item">
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnFUpdateSales" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFUpdateSales_Click">Final Update</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtgvItemdescsaldet" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                                        Width="200px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgUnitnum" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvSize" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>


                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFtotalsh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Land Owner">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvlowner" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lowner")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:Label>

                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFlownersh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Company">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvcompany" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "company")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFcompanysh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Purchase From Land Owner">

                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvpurfrmlanowner" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pflowner")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFpfrmlownersh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Adjustment">

                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvadj" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjmnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFadjmnt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Total Company">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvtotalcompany" runat="server" Font-Size="11px"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalcom")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFtcompanysh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Constration Area">

                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvConArea" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iconarea")).ToString("#,##0;-#,##0; ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BEP">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrvBerp" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##; ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgrvFBerp" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>



                                                        </Columns>

                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                    </asp:GridView>
                                                </div>


                                                <div class=" form-group">
                                                    <asp:Label ID="lblSaleRevenue" runat="server" CssClass=" smLbl_to" Text="Sale Revenue:"></asp:Label>
                                                    <asp:Label ID="lblTar" runat="server" CssClass=" smLbl_to" Text="Target Revenue"></asp:Label>


                                                    <asp:TextBox ID="lblTarVal" runat="server" CssClass="smLbl_to " TabIndex="1"></asp:TextBox>
                                                    <div class="clearfix"></div>
                                                </div>

                                                <div class="table table-responsive">
                                                    <asp:GridView ID="gvFeaPrjsalrev" runat="server" AutoGenerateColumns="False"
                                                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        OnRowDeleting="gvFeaPrjsalrev_RowDeleting" ShowFooter="True" Width="651px"
                                                        OnRowDataBound="gvFeaPrjsalrev_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" 
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                            <asp:TemplateField HeaderText="Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvItmCodsalrev" runat="server" Height="16px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnTotalsalrev" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalsalrev_Click">Total</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description of Item">
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnFUpdatesalrev" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFUpdatesalrev_Click">Final Update</asp:LinkButton>
                                                                </FooterTemplate>
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtgvItemdescsalrev" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc"))%>'
                                                                        Width="180px"></asp:TextBox>


                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgUnitnum4" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Floor">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvnum" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrno")) %>'
                                                                        Width="50px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSize" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="30px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtglsizessalrev" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsizes")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                                        Width="50px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFlsisessalrev" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Rate">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Txtgratesalrev" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvamtsalrev" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0; -#,##0; ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtsalrev" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Brochure Rate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgbroratsalrev" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brorat")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Brochure Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrBAmt" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgrFBAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BEP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvBep" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Profit </Br> Addition </Br> in %">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvProfit" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "profit")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min Price">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgMinPrice" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minprice")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min Amount">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgrFMAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrMAmt" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
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




                                            </asp:View>
                                            <hr class=" hrline" />
                                            <asp:View ID="ViewReport" runat="server">
                                                <asp:GridView ID="gvFeaPrjRep" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" Width="785px" OnRowDataBound="gvFeaPrjRep_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Group Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subgrpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "subgrpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>'
                                                                    Width="180px"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description of Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvItemdescRep" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgUnitnumRep" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgSizerep" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgnumrep" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "number")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total SFT / Quantity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgtsizecRep" runat="server" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsize")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate/SFT">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgsalraterep" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFAmtc0" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                    </Columns>

                                                    <FooterStyle CssClass="grvFooter" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>
                                            </asp:View>

                                        </asp:MultiView>
                                    </td>
                                </tr>
                            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

