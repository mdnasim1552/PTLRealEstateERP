<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TASActAnalysis.aspx.cs" Inherits="RealERPWEB.F_07_Ten.TASActAnalysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 141px;
        }

        .auto-style2 {
            width: 94px;
        }
    </style>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <%-- <div class=" form-group">
                                    <asp:Label ID="lblItem5" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project/Unit"></asp:Label>
                                    <asp:TextBox ID="lblProjectDesc2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                </div>--%>
                                <div class="form-group">

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        <asp:DropDownList ID="ddlProject" runat="server" Style="width: 300px" CssClass="chzn-select  ddlPage" TabIndex="3"></asp:DropDownList>

                                        <asp:Label ID="lblProjectDesc" runat="server" Style="width: 300px" Visible="False" CssClass=" inputtextbox"></asp:Label>

                                        <%-- <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click" TabIndex="4">Ok</asp:LinkButton>--%>
                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click" TabIndex="4" Style="margin-left: -86px;">Ok</asp:LinkButton>
                                        <asp:CheckBox ID="chkShorting" runat="server" AutoPostBack="true" Text="Alphabet" />
                                        <%-- OnCheckedChanged="chkShorting_CheckedChanged"--%>
                                    </div>
                                    <div class="col-sm-1 pading5px" style="margin-left: -30px;">
                                        <asp:CheckBox ID="ChkCopyProject" runat="server" AutoPostBack="True"
                                            Text="Copy Budget" CssClass="btn btn-primary primaryBtn chkBoxControl"
                                            Visible="False" OnCheckedChanged="ChkCopyProject_CheckedChanged" />
                                      
                                    </div>
                                    <div class="col-sm-1 pading5px" style="margin-left: 20px;">
                                        <asp:CheckBox ID="ChkCopyTender" runat="server" AutoPostBack="True"
                                            Text="Copy Tender" CssClass="btn btn-primary primaryBtn chkBoxControl"
                                            Visible="False" OnCheckedChanged="ChkCopyTender_CheckedChanged" />
                                       
                                    </div>
                                    <div class="col-md-3 pull-right pading5px">
                                        <asp:Label ID="lblProjectLock" runat="server" Visible="False"></asp:Label>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                    <div class="col-sm-7 pading5px">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" Visible="False" BackColor="#0B88C5" ForeColor="White" AutoPostBack="True" CssClass="btn rbtnList1 margin5px  primaryBtn " OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                                            RepeatColumns="7" RepeatDirection="Horizontal">
                                            <asp:ListItem>Catagory Selection</asp:ListItem>
                                            <asp:ListItem>Item Selection(All Catagory)</asp:ListItem>
                                            <asp:ListItem>Item Selction(Ind.Catagory)</asp:ListItem>
                                            <asp:ListItem>Rate Input</asp:ListItem>
                                            <asp:ListItem>Reports</asp:ListItem>
                                            <asp:ListItem>Special Report</asp:ListItem>
                                            <asp:ListItem> Special Report(02)</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <asp:Panel ID="crDate" runat="server" Visible="false">
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblcreationdate" runat="server" CssClass="lblTxt lblName" Visible="False" Text="Creation Date:"></asp:Label>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inpPixedWidth" Visible="False"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                            <asp:LinkButton ID="lbtnUpdatePCDate" runat="server" CssClass="btn btn-primary primaryBtn" Visible="False" TabIndex="4">Update</asp:LinkButton>
                                            <%--OnClick="lbtnUpdatePCDate_Click"--%>
                                        </div>
                                    </asp:Panel>

                                    <div class=" clearfix"></div>
                                </div>
                            </div>
                            <fieldset class="scheduler-border fieldset_A" id="details" runat="server" visible="False">
                                <div class="form-horizontal">
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddldetails" runat="server" Style="width: 300px" CssClass="chzn-select  ddlPage">
                                            <asp:ListItem Value="Main" Text="Main"> </asp:ListItem>
                                            <asp:ListItem Value="Sub" Text="Sub"></asp:ListItem>
                                            <asp:ListItem Value="Details" Text="Details" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnshow" runat="server" CssClass="btn btn-success primaryBtn" Style="margin-left: -86px;">Show</asp:LinkButton>
                                        <%--OnClick="lbtnshow_OnClick"--%>
                                    </div>

                                </div>

                            </fieldset>
                            <asp:Panel ID="PnlCopyProject" runat="server" Visible="False">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">From Project</asp:Label>

                                        <asp:TextBox ID="txtSrcCopyPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnCopyFindProject" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="ibtnCopyFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                       
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlCopyProjectName" runat="server" CssClass="form-control chzn-select inputTxt" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnCopyProject" runat="server" CssClass="btn btn-primary primaryBtn" TabIndex="4" OnClick="lbtnCopyData_Click">Copy Data P/U</asp:LinkButton>
                                        
                                    </div>
                                </div>

                            </asp:Panel>
                             <asp:Panel ID="PnlCopyTender" runat="server" Visible="False">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">From Tender</asp:Label>

                                        <asp:TextBox ID="txtSrcCopyTen" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnCopyFindTender" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="ibtnCopyFindTender_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlCopyTenderName" runat="server" CssClass="form-control chzn-select inputTxt" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnCopyTender" runat="server" CssClass="btn btn-primary primaryBtn" TabIndex="4" OnClick="lbtnCopyTender_Click">Copy Data P/U</asp:LinkButton>
                                        <%--OnClick="lbtnCopyProject_Click"--%>
                                    </div>


                                </div>

                            </asp:Panel>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1Floor" runat="server">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <asp:Label ID="lblFPhaseTitle" runat="server" CssClass="btn btn-success primaryBtn"
                                        Text="Catagory Selection"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkFlrShowSelected" runat="server" AutoPostBack="True"
                                        CssClass="chkBoxControl primaryBtn"
                                        OnCheckedChanged="chkFlrShowSelected_CheckedChanged"
                                        Text="Show selected Catagory(s) only" />

                                    <asp:LinkButton ID="lbtnShowSelectedFloor" runat="server" CssClass="btn  btn-primary primaryBtn">Show Selected</asp:LinkButton>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="col-md-12">
                                <hr class="hrline" />
                                <asp:CheckBoxList ID="cbListFloor" runat="server"
                                    CellPadding="2" CellSpacing="8" RepeatColumns="7"
                                    CssClass=" chkBoxControl primaryBtn " Font-Size="12px"
                                    Width="912px">
                                    <asp:ListItem>aa</asp:ListItem>
                                    <asp:ListItem>bb</asp:ListItem>
                                    <asp:ListItem>cc</asp:ListItem>
                                    <asp:ListItem>dd</asp:ListItem>
                                    <asp:ListItem>ee</asp:ListItem>
                                    <asp:ListItem>ff</asp:ListItem>
                                    <asp:ListItem>gg</asp:ListItem>
                                    <asp:ListItem>hh</asp:ListItem>
                                    <asp:ListItem>ii</asp:ListItem>
                                    <asp:ListItem>jj</asp:ListItem>
                                    <asp:ListItem>kk</asp:ListItem>
                                    <asp:ListItem>ll</asp:ListItem>
                                    <asp:ListItem>mm</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>


                        </asp:View>
                        <asp:View ID="View2Item" runat="server">
                            <div class="form-group">
                                <div class="col-md-3 pading5px">
                                    <asp:Label ID="lblTitle1" runat="server" CssClass="btn btn-success primaryBtn" 
                                        Text="Item Selection (All Catagory)"></asp:Label>
                                    <asp:HyperLink ID="lnkbtnAnalysis" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="~/F_04_Bgd/BgdStdAna.aspx" Target="_blank"><span class="flaticon-edit26"></span> Analysis </asp:HyperLink>
                                </div>

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label13" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>


                                    <asp:DropDownList ID="ddlpagesizeen" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                        Style="width: 100px;">
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

                                <div class="clearfix"></div>
                            </div>





                            <div class="form-group">

                                <div class="col-md-7 pading5px">
                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Desc. of Items" Style="font-size: 11px;"></asp:Label>

                                    <asp:TextBox ID="txtItemSearch" runat="server" CssClass="inputTxt inpPixedWidth hidden" TabIndex="1"></asp:TextBox>

                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="ImgbtnFindItem" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="ImgbtnFindItem_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>

                                    <asp:DropDownList ID="ddlItem" runat="server" Style="width: 325px" CssClass="chzn-select form-control  inputTxt" TabIndex="3" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <%--<asp:Label ID="ddlterd" runat="server"
                                            Visible="False" CssClass="form-control inputTxt"></asp:Label>--%>
                                </div>

                                <div class="col-md-1 pading5px">
                                    <asp:LinkButton ID="lbtnSelectItem" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectItem_Click" TabIndex="4" Style="margin-left: -155px">Select Item</asp:LinkButton>

                                </div>
                                <div class="col-md-3 pull-right pading5px">
                                    <%--   <asp:Label ID="Label7" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>--%>
                                </div>
                                <div class=" clearfix"></div>
                            </div>

                            <hr class=" hrline" />
                            <div id="divGridView">
                                <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                                    OnRowCancelingEdit="gvAnalysis_RowCancelingEdit"
                                    OnRowDataBound="gvAnalysis_RowDataBound" OnRowEditing="gvAnalysis_RowEditing"
                                    OnRowUpdating="gvAnalysis_RowUpdating" PageSize="15" Width="16px"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item" FooterText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <table style="width: 100%; background-color: #C4D6B1;">
                                                    <tr>
                                                        <td colspan="3" style="text-align: left">
                                                            <asp:Label ID="lblgvItmDesc_e" runat="server" Font-Bold="True"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")).Trim() %>'
                                                                Width="500px" BackColor="Maroon" ForeColor="White" Font-Size="14px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:GridView ID="gvgvFloorAna" runat="server" AutoGenerateColumns="False"
                                                                HorizontalAlign="Center" Width="336px" BorderColor="White"
                                                                BorderStyle="Solid" BorderWidth="1px" ShowFooter="True" Height="16px">
                                                                <RowStyle Font-Size="12px" BorderColor="White" BorderStyle="Solid"
                                                                    BorderWidth="1px" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Catagory Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvgvFlrCod" runat="server" Font-Bold="False"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                                                Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Catagory Desc">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvgvFlrDesc" runat="server" CssClass="StyleCheckBoxList"
                                                                                Font-Bold="False"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                                Width="80px" ForeColor="Black"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sch.Sl.">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgvgvItmSlNo" runat="server" BorderColor="#99CCFF"
                                                                                BorderStyle="Solid" BorderWidth="1px"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmslno")) %>'
                                                                                Width="52px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sch.Item No.">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgvgvItmSChNo" runat="server" BorderColor="#99CCFF"
                                                                                BorderStyle="Solid" BorderWidth="1px"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmschno")) %>'
                                                                                Width="100px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lbtngvgvRefresh" runat="server" Font-Bold="True"
                                                                                Font-Size="12px" OnClick="lbtngvgvRefresh_Click">Total</asp:LinkButton>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sch. Qty.">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgvgvQty" runat="server" BorderColor="#99CCFF"
                                                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                                Width="80px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvgvQtyFooter" runat="server" Font-Bold="True"
                                                                                Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                Width="85px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                            HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sch Rate">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtgvgvRate" runat="server" BorderColor="#99CCFF"
                                                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                                Width="80px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvgvRatFooter" runat="server" Font-Bold="True"
                                                                                Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                Width="85px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                            HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sch Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvgvAmt" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                Width="85px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvgvAmtFooter" runat="server" Font-Bold="True"
                                                                                Font-Size="12px" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                Width="85px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                        <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                            HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" Font-Size="12px"
                                                ForeColor="Blue" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BOQ">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnRefresh" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" OnClick="lbtnRefresh_OnClick">Refresh</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="69px" CssClass="style101"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sch.Rate">
                                            <FooterTemplate>
                                                <asp:HyperLink ID="hlbtnDetails" runat="server" BackColor="#000066"
                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                    ForeColor="White" Style="text-align: center" Target="_blank" Width="65px">Next</asp:HyperLink>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="65px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmount" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvAmountFooter" runat="server" Width="87px" Font-Size="12px"
                                                    ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="View3Item" runat="server">

                            <asp:Label ID="lblTitle1a" runat="server" CssClass=" btn btn-success primaryBtn"> Item Selection (Individual Catagory)</asp:Label>
                            <div class="clearfix"></div>
                            <div class="form-group">

                                <div class="col-md-1 pading5px asitCol1">
                                    <asp:Label ID="lblItem7" runat="server" CssClass="lblTxt lblName"> Catagory</asp:Label>

                                </div>
                                <div class="col-md-4 pading5px asitCol4 ">
                                    <asp:DropDownList ID="ddlFloorList" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblFloorName" runat="server" Visible="False"
                                        CssClass="form-control inputTxt">Catagory Name</asp:Label>
                                </div>

                                <div class="col-md-4 pading5px">
                                    <asp:LinkButton ID="lbtnSelectFloor" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectFloor_Click" TabIndex="4">Select Catagory P/U</asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblItem2" runat="server" CssClass="btn btn-success primaryBtn">Description of Items</asp:Label>
                                <hr class="hrline" />
                                <div class="clearfix"></div>

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                    <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy"
                                        Visible="False" CssClass="btn btn-primary primaryBtn" />
                                </div>
                                <div class="col-md-4 pading5px asitCol4 ">
                                    <asp:DropDownList ID="ddlFloorListToCopy" runat="server" CssClass="form-control inputTxt chzn-select " TabIndex="3">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="Label12" runat="server" Visible="false"
                                        CssClass="form-control inputTxt">Catagory Name</asp:Label>
                                    <asp:LinkButton ID="lbtnCopyData" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnCopyData_Click" TabIndex="4">Copy Data </asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="dislbl" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                    <asp:TextBox ID="txtItemSearch2" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                    <asp:LinkButton ID="ImgbtnFindItem2" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindItem2_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="col-md-3 pading5px asitCol3 ">
                                    <asp:DropDownList ID="ddlItem2" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="3">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-4 pading5px">

                                    <asp:LinkButton ID="lbtnSelectItem2" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectItem2_Click" TabIndex="4" Style="margin-left: -50px;">Select Item </asp:LinkButton>

                                </div>

                                <div class="clearfix"></div>
                            </div>


                       
                            <asp:GridView ID="gvAnalysis2" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False"
                                OnPageIndexChanging="gvAnalysis2_PageIndexChanging" PageSize="15" ShowFooter="True"
                                Width="16px" Visible="False" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle  Font-Size="11px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmcod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sch.Sl.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItmSlNo" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Style="background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmslno")) %>'
                                                Width="52px" Font-Size="12px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sch.Item No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvItmSChNo" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Style="background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmschno")) %>'
                                                Width="100px" Font-Size="12px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFinalUpdate2" runat="server" Font-Bold="True"
                                                Font-Size="14px" ForeColor="Blue" OnClick="lbtnFinalUpdate2_Click"
                                                Style="text-align: center" Width="100px">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" ForeColor="#FFFF66" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sch.Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="12px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sch.Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="12px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal2" runat="server" Font-Bold="True"
                                                Font-Size="14px" ForeColor="Black" CssClass="primarygrdBtn" OnClick="lbtnTotal2_Click"
                                                Style="text-align: center" Width="50px">Total :</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" ForeColor="Yellow" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvAmountFooter" runat="server" Font-Size="12px"
                                                ForeColor="White" Width="87px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
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
                        <asp:View ID="View4Resource" runat="server">

                            <div class=" form-group">
                                <div class="col-md-6pading5px">
                                    <asp:Label ID="lblTitle2" runat="server" CssClass="lblTxt lblName " Width="170">Resource Rate Input &amp; Report</asp:Label>

                                    <asp:TextBox ID="txtSearchItem" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                    <asp:LinkButton ID="lbtnSelectFloorRes" runat="server" OnClick="lbtnSelectFloorRes_Click" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>


                                </div>
                                <div class="clearfix"></div>
                            </div>


                            <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvResInfo_PageIndexChanging"
                                PageSize="20" ShowFooter="True" Width="16px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle  Font-Size="11px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Catagory">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfloordes" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Resource">
                                        <FooterTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style173">&nbsp;</td>
                                                    <td>
                                                        <asp:LinkButton ID="lbtnUpdateResRate" runat="server" Font-Bold="True"
                                                            Font-Size="14px" OnClick="lbtnUpdateResRate_Click"
                                                            Style="text-align: center; height: 17px;" Width="80px">Update Rate</asp:LinkButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Est.Qty">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnSameValue" runat="server" Font-Bold="True"
                                                Font-Size="14px" OnClick="lbtnSameValue_Click" ForeColor="Black" Style="text-align: center;"
                                                Width="85px">Put Same Val</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResQty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res. Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF"
                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnResTotal" runat="server" Font-Bold="True"
                                                Font-Size="14px" OnClick="lbtnResTotal_Click" Style="text-align: center;"
                                                Width="50px">Total :</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTResAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTResAmtFooter" runat="server" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                        <asp:View ID="View5Report" runat="server">
                            <div class="form-group">
                                <div class="col-md-7 pading5px asitCol7">
                                    <asp:Label ID="lblTitle4" runat="server" CssClass="lblTxt lblName " Text="Analysis Reports"></asp:Label>

                                    <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" CssClass=" ddlPage62" Width="180"
                                        OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
                                        <asp:ListItem>Resource Basis</asp:ListItem>
                                        <asp:ListItem>Work Basis</asp:ListItem>
                                        <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                        <asp:ListItem>Individual Work Basis</asp:ListItem>
                                        <asp:ListItem>Analysis Sheet</asp:ListItem>
                                    </asp:DropDownList>





                                    <asp:Label ID="lblRptGroup" runat="server" CssClass=" smLbl_to"
                                        Text="Group :"></asp:Label>

                                    <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblRptMainGroup" runat="server" CssClass="lblTxt lblName " Visible="false"
                                        Text="Main Group :"></asp:Label>

                                    <asp:DropDownList ID="ddlRptMainGroup" runat="server" CssClass="ddlPage" Visible="false">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:Label ID="lblFloor" runat="server" CssClass=" smLbl_to" Text="Cat."></asp:Label>

                                    <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True" CssClass="chzn-select  ddlPage" Width="190px">
                                    </asp:DropDownList>


                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="ddlPage62"  Width="180px">
                                    </asp:DropDownList>--%>
                                </div>
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:LinkButton ID="lbtnShowReport" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowReport_Click">Show Report</asp:LinkButton>
                                </div>
                                <div class=" clearfix"></div>
                            </div>



                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4" style="display: none">
                                    <asp:Label ID="lblRptResBreak" runat="server" CssClass="lblTxt lblName " Visible="false"
                                        Text="Res. Group"></asp:Label>

                                    <asp:DropDownList ID="ddlRptResBreak" runat="server" AutoPostBack="True" CssClass="ddlPage62" Width="180"
                                        Visible="False">
                                        <asp:ListItem>Resource Basis</asp:ListItem>
                                        <asp:ListItem>Work Basis</asp:ListItem>
                                        <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                        <asp:ListItem>Individual Work Basis</asp:ListItem>
                                        <asp:ListItem>Analysis Sheet</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6 pading5px">
                                    <asp:CheckBox ID="ChkPunchValues" runat="server" CssClass="chkBoxControl margin5px" Text="Including Punch Value" Visible="false" />
                                    <asp:CheckBox ID="ChkAdditionalCost" runat="server" Text="Incl. Additional Cost" Visible="false" />

                                    <asp:CheckBox ID="ChkIgnoreSchRate" runat="server" CssClass="chkBoxControl margin5px" Text="Ignore Sch. Rate" Visible="False" />
                                    <asp:CheckBox ID="ChkMKSUnit" runat="server" CssClass="chkBoxControl margin5px"
                                        Text="Consider MKS Unit" Visible="False" />

                                    <asp:CheckBox ID="ChkOnSchiNo" runat="server" Text="Order on Sch. Item" Visible="False" />
                                </div>


                                <div class=" clearfix"></div>
                            </div>

                            <div class="form-group">
                                <%--<div class="col-md-3 pading5px">
                                    <asp:Label ID="lblFloor" runat="server" CssClass="lblTxt lblName "
                                        Text="Floor"></asp:Label>
                               
                                    <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True" CssClass=" ddlPage62" Width="180px">
                                    </asp:DropDownList>
                                </div>--%>

                                <div class="col-md-8 pading5px">
                                    <asp:Panel ID="PnlRptItmList" runat="server"
                                        Visible="False">
                                        <asp:Label ID="lblItem9" runat="server" CssClass="lblTxt lblName"
                                            Text="Item:"></asp:Label>
                                        <asp:TextBox ID="txtRptItemSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnRptFindItem" runat="server" OnClick="ImgbtnRptFindItem_OnClick" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlRptItem" runat="server" CssClass="chzn-select ddlPage" Width="461px">
                                        </asp:DropDownList>



                                    </asp:Panel>
                                    <asp:Panel ID="PnlRptResList" runat="server"
                                        Visible="False">
                                        <asp:Label ID="lblItem12" runat="server" CssClass="lblTxt lblName"
                                            Text="Resource:"></asp:Label>
                                        <asp:TextBox ID="txtRptResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnRptFindRes" runat="server" OnClick="ImgbtnRptFindRes_OnClick" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlRptRes" runat="server" CssClass="chzn-select ddlPage" Width="461px">
                                        </asp:DropDownList>

                                    </asp:Panel>

                                </div>


                                <div class=" clearfix"></div>
                            </div>

                            <div class="row">
                                <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="847px" ShowFooter="True" DataKeyNames="rptdesc1" ViewStateMode="Enabled" AllowSorting="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                    Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catagory" SortExpression="flrdes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Description" SortExpression="rptdesc1">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Total Cost:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Construction Area:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Text="Cost Per SFT:" Width="110px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="300px"></asp:Label>
                                                <asp:Label ID="Label11" runat="server" Font-Bold="False" Style="display: none;" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit" SortExpression="rptunit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" SortExpression="rptqty">
                                            <FooterTemplate>
                                                <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd. Rate" SortExpression="rptrat">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" SortExpression="rptamt">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvalConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblvalCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Percentage">
                                            <FooterTemplate>
                                                <table style="width: 10%; height: 48px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPer" runat="server" Font-Bold="False" Font-Size="12px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <%--<table>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td class="style158" colspan="2"></td>
                                    <td colspan="2"></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblRptResBreak" runat="server" Font-Size="12px"
                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                            Text="Res. Group :" Visible="False" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style158">
                                        <asp:DropDownList ID="ddlRptResBreak" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px"
                                            Style="text-transform: capitalize; margin-left: 0px;" Visible="False"
                                            Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style159">
                                        <asp:CheckBox ID="ChkIgnoreSchRate" runat="server" Font-Bold="True"
                                            Font-Size="12px" Text="Ignore Sch. Rate" Visible="False" Width="110px" />
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td class="style156">
                                        <asp:Label ID="lblRptMainGroup" runat="server" Font-Size="12px"
                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                            Text="Main Group :" Width="72px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptMainGroup" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblFloor" runat="server" Font-Size="14px"
                                            Font-Underline="False" Height="16px" Style="font-weight: 700; text-align: right"
                                            Text="Floor :" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style158">
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" Font-Bold="True"
                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style159" colspan="4"></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style132">&nbsp;</td>
                                    <td colspan="7"></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                        </asp:View>
                        <asp:View ID="SpReport" runat="server">
                            <table style="width: 100%;">

                                <tr>
                                    <td colspan="8">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table class="table table-hover table-condensed">

                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td class="style166">
                                                        <asp:Label ID="lblTitle5" runat="server" CssClass="btn btn-success primaryBtn"
                                                            Text="Special Reports" Width="187px"></asp:Label></td>
                                                    <td class="style171">
                                                        <asp:Label ID="Label1" runat="server" Font-Size="12px"
                                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                            Text="Catagory :" Width="60px" ForeColor="#000"></asp:Label></td>
                                                    <td class="style167">
                                                        <asp:DropDownList ID="ddlFlrlstspr" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="120px">
                                                        </asp:DropDownList></td>
                                                    <td class="style169">
                                                        <asp:Label ID="Label2" runat="server" Font-Size="12px"
                                                            Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                            Text="Group :" Width="80px" ForeColor="#000"></asp:Label></td>
                                                    <td class="style170">

                                                        <asp:DropDownList ID="ddlRptGroupspr" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Height="21px"
                                                            Style="text-transform: capitalize" Width="100px">
                                                            <asp:ListItem>Main</asp:ListItem>
                                                            <asp:ListItem>Sub-1</asp:ListItem>
                                                            <asp:ListItem>Sub-2</asp:ListItem>
                                                            <asp:ListItem>Sub-3</asp:ListItem>
                                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td>
                                                        <asp:LinkButton ID="lbtnShowSpRpt" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowSpRpt_Click">Show</asp:LinkButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td class="style166">&nbsp;</td>
                                                    <td class="style171">
                                                        <asp:Label ID="Label3" runat="server" Font-Size="12px" Font-Underline="False"
                                                            ForeColor="#000" Style="font-weight: 700; text-align: right" Text="Work :"
                                                            Width="60px"></asp:Label>
                                                    </td>
                                                    <td class="style170">
                                                        <asp:DropDownList ID="ddlRptWork" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Height="21px"
                                                            Style="text-transform: capitalize" Width="120px">
                                                            <asp:ListItem Selected="True">All Work</asp:ListItem>
                                                            <asp:ListItem>Civil</asp:ListItem>
                                                            <asp:ListItem>Sanitary</asp:ListItem>
                                                            <asp:ListItem>Electrical</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style168">
                                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="color: #000; text-align: right;" Text=" Page Size:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style167">
                                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="100px">
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
                                                    </td>

                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:GridView ID="gvSpRpt" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" BackColor="#FFCCFF"
                                            OnPageIndexChanging="gvSpRpt_PageIndexChanging" ShowFooter="True" Width="847px">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Catagory">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptFlr2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptRes2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                            Width="300px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptUnit2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptQty2" runat="server" Font-Bold="true" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bgd. Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptRat2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptAmt2" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Percentage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPer0" runat="server" Font-Bold="true" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFPer" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#99CCFF" BorderColor="#660033" BorderStyle="Solid"
                                                BorderWidth="1px" />
                                            <HeaderStyle BackColor="#FFFFCC" BorderColor="#660033" BorderStyle="Solid"
                                                Font-Size="12px" ForeColor="#660033" />
                                            <AlternatingRowStyle BackColor="#CFCFB8" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style162">&nbsp;</td>
                                    <td class="style163">&nbsp;</td>
                                    <td class="style164">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style165">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style162">&nbsp;</td>
                                    <td class="style163">&nbsp;</td>
                                    <td class="style164">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style165">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style162">&nbsp;</td>
                                    <td class="style163">&nbsp;</td>
                                    <td class="style164">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style165">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:View>





                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <%-- <table style="width: 94%;">
        <tr>
            <td class="style137">&nbsp;</td>
            <td class="style12">
                <asp:Label ID="lblTitle" runat="server" BackColor="Blue" Font-Bold="True"
                    ForeColor="Yellow" Style="font-weight: 700; color: #FFFF66; text-align: left"
                    Text="PROJECT/UNIT ANALYSIS INFORMATION INPUT/EDIT" Width="443px"></asp:Label>
            </td>
            <td class="style160">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style139">
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma"
                    Style="font-size: 11px" Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style138">
                <asp:LinkButton ID="lbtnPrintAnaLysis" runat="server" Font-Bold="True"
                    Font-Size="12px" OnClick="lbtnPrintAnaLysis_Click"
                    Style="text-align: center; color: #FFFFFF; height: 15px;" Width="60px"
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td class="style11">&nbsp;</td>
        </tr>
    </table>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 77%;">
                <tr>
                    <td class="style17">&nbsp;</td>
                    <td class="style43">&nbsp;</td>
                    <td class="style21">
                        <asp:Label ID="lblItem5" runat="server" CssClass="newStyle3" Font-Size="16px"
                            Font-Underline="True" Style="font-weight: 700; color: #FFFFFF;"
                            Text="Project/Unit" Width="78px"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:Label ID="lblItem6" runat="server" CssClass="newStyle3" Font-Size="16px"
                            Font-Underline="False" Style="font-weight: 700"
                            Text=":" Width="5px"></asp:Label>
                    </td>
                    <td class="style122" colspan="7">
                        <asp:Label ID="lblProjectDesc2" runat="server" CssClass="newStyle1"
                            Font-Size="16px" Style="font-weight: 700" BackColor="White"
                            ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style119">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style112">&nbsp;</td>
                    <td class="style111">&nbsp;</td>
                </tr>
                <tr>
                    <td class="style17">&nbsp;</td>
                    <td class="style43">&nbsp;</td>
                    <td class="style21">
                        <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="Solid"
                            BorderWidth="1px" Height="18px" Width="78px"></asp:TextBox>
                    </td>
                    <td class="style20">
                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px"
                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindProject_Click"
                            Width="16px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="newStyle1"
                            Font-Bold="True" Font-Size="11px" Height="21px" Width="300px">
                        </asp:DropDownList>
                        <asp:Label ID="lblProjectDesc" runat="server" BackColor="White"
                            CssClass="newStyle1" Font-Bold="True" Font-Size="12px" ForeColor="Blue"
                            Height="18px" Style="font-weight: 700" Visible="False"></asp:Label>
                    </td>
                    <td class="style136">
                        <asp:LinkButton ID="lbtnOk1" runat="server" Font-Bold="True" Font-Size="12px"
                            OnClick="lbtnOk1_Click" Style="text-align: center; color: #FFFFFF; height: 15px;"
                            Width="67px">Select P/U</asp:LinkButton>
                    </td>
                    <td class="style152">
                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                            Font-Size="12px" ForeColor="Maroon" Style="color: #FFFFFF"></asp:Label>
                    </td>
                    <td class="style153">&nbsp;</td>
                    <td class="style121">&nbsp;</td>
                    <td class="style121">&nbsp;</td>
                    <td class="style121">&nbsp;</td>
                    <td class="style121">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style112">&nbsp;</td>
                    <td class="style111">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="11">
                        <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True"
                            BackColor="#BBBB99" BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="True" OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged"
                            RepeatColumns="7" Visible="False" Width="822px" Font-Size="14px"
                            RepeatDirection="Horizontal">
                            <asp:ListItem>Floor Selection</asp:ListItem>
                            <asp:ListItem>Item Selection(All flr)</asp:ListItem>
                            <asp:ListItem>Item Selction(Ind.flr)</asp:ListItem>
                            <asp:ListItem>Rate Input</asp:ListItem>
                            <asp:ListItem>Adjustment</asp:ListItem>
                            <asp:ListItem>Reports</asp:ListItem>
                            <asp:ListItem>Special Reports</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style17">&nbsp;</td>
                    <td class="style43" colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1Floor" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td class="style140">
                                            <asp:Label ID="lblTitle0" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="22px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: left"
                                                Text="Floor Selection"></asp:Label>
                                        </td>
                                        <td class="style142">
                                            <asp:CheckBox ID="chkFlrShowSelected" runat="server" AutoPostBack="True"
                                                Font-Bold="True" Font-Size="16px" ForeColor="White"
                                                OnCheckedChanged="chkFlrShowSelected_CheckedChanged"
                                                Text="Show selected floor(s) only" Style="color: #FFFFFF" />
                                        </td>
                                        <td class="style142">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td colspan="4">
                                            <asp:CheckBoxList ID="cbListFloor" runat="server"
                                                CellPadding="8" CellSpacing="8" RepeatColumns="6"
                                                CssClass="StyleCheckBoxList" Font-Size="16px" BorderColor="#FFCC00"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White">
                                                <asp:ListItem>aa</asp:ListItem>
                                                <asp:ListItem>bb</asp:ListItem>
                                                <asp:ListItem>cc</asp:ListItem>
                                                <asp:ListItem>dd</asp:ListItem>
                                                <asp:ListItem>ee</asp:ListItem>
                                                <asp:ListItem>ff</asp:ListItem>
                                                <asp:ListItem>gg</asp:ListItem>
                                                <asp:ListItem>hh</asp:ListItem>
                                                <asp:ListItem>ii</asp:ListItem>
                                                <asp:ListItem>jj</asp:ListItem>
                                                <asp:ListItem>kk</asp:ListItem>
                                                <asp:ListItem>ll</asp:ListItem>
                                                <asp:ListItem>mm</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td class="style140">&nbsp;</td>
                                        <td class="style142">&nbsp;</td>
                                        <td class="style142">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View2Item" runat="server">
                                <table style="width: 265px; margin-right: 0px; background-color: #999966;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33" colspan="3">
                                            <asp:Label ID="lblTitle1" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="22px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: left"
                                                Text="Item Selection (All Floor)"></asp:Label>
                                        </td>
                                        <td class="style36">&nbsp;</td>
                                        <td class="style107">&nbsp;</td>
                                        <td class="style143">&nbsp;</td>
                                        <td class="style144">&nbsp;</td>
                                        <td class="style145">&nbsp;</td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">&nbsp;</td>
                                        <td class="style34">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblItem" runat="server" CssClass="newStyle3" Font-Size="12px"
                                                Font-Underline="True" Style="font-weight: 700" Text="Description of Items"
                                                Width="313px"></asp:Label>
                                        </td>
                                        <td class="style36">&nbsp;</td>
                                        <td class="style107">&nbsp;</td>
                                        <td class="style143">&nbsp;</td>
                                        <td class="style144">&nbsp;</td>
                                        <td class="style145">&nbsp;</td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">
                                            <asp:TextBox ID="txtItemSearch" runat="server" BorderStyle="Solid"
                                                BorderWidth="1px" Height="18px" Width="66px"></asp:TextBox>
                                        </td>
                                        <td class="style34">
                                            <asp:ImageButton ID="ImgbtnFindItem" runat="server" Height="19px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindItem_Click"
                                                Width="16px" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="newStyle1"
                                                Font-Bold="True" Font-Size="11px" Height="21px" Width="500px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style145">
                                            <asp:LinkButton ID="lbtnSelectItem" runat="server" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtnSelectItem_Click" Style="text-align: center"
                                                Width="67px">Select Item</asp:LinkButton>
                                        </td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33" colspan="12">
                                            <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                                                OnRowCancelingEdit="gvAnalysis_RowCancelingEdit"
                                                OnRowDataBound="gvAnalysis_RowDataBound" OnRowEditing="gvAnalysis_RowEditing"
                                                OnRowUpdating="gvAnalysis_RowUpdating" PageSize="15" Width="16px"
                                                ShowFooter="True">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item" FooterText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <table style="width: 100%; background-color: #C4D6B1;">
                                                                <tr>
                                                                    <td colspan="3" style="text-align: left">
                                                                        <asp:Label ID="lblgvItmDesc_e" runat="server" Font-Bold="True"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")).Trim() %>'
                                                                            Width="500px" BackColor="Maroon" ForeColor="White" Font-Size="14px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:GridView ID="gvgvFloorAna" runat="server" AutoGenerateColumns="False"
                                                                            HorizontalAlign="Center" Width="336px" BorderColor="White"
                                                                            BorderStyle="Solid" BorderWidth="1px" ShowFooter="True" Height="16px">
                                                                            <RowStyle Font-Size="12px" BorderColor="White" BorderStyle="Solid"
                                                                                BorderWidth="1px" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Floor Code" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvgvFlrCod" runat="server" Font-Bold="False"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                                                            Width="30px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Floor Desc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvgvFlrDesc" runat="server" CssClass="StyleCheckBoxList"
                                                                                            Font-Bold="False"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                                            Width="80px" ForeColor="Black"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sch.Sl.">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgvgvItmSlNo" runat="server" BorderColor="#99CCFF"
                                                                                            BorderStyle="Solid" BorderWidth="1px"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmslno")) %>'
                                                                                            Width="52px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sch.Item No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgvgvItmSChNo" runat="server" BorderColor="#99CCFF"
                                                                                            BorderStyle="Solid" BorderWidth="1px"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmschno")) %>'
                                                                                            Width="100px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:LinkButton ID="lbtngvgvRefresh" runat="server" Font-Bold="True"
                                                                                            Font-Size="12px" OnClick="lbtngvgvRefresh_Click">Total</asp:LinkButton>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sch. Qty.">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgvgvQty" runat="server" BorderColor="#99CCFF"
                                                                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                                            Width="80px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblgvgvQtyFooter" runat="server" Font-Bold="True"
                                                                                            Font-Size="12px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                            Width="85px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                                        HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sch Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgvgvRate" runat="server" BorderColor="#99CCFF"
                                                                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="12px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                                            Width="80px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblgvgvRatFooter" runat="server" Font-Bold="True"
                                                                                            Font-Size="12px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                            Width="85px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                                        HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sch Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvgvAmt" runat="server" Font-Bold="True"
                                                                                            Style="text-align: right"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                            Width="85px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblgvgvAmtFooter" runat="server" Font-Bold="True"
                                                                                            Font-Size="12px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                                                                            Width="85px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                    <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                                        HorizontalAlign="Right" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td style="text-align: right">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" Font-Size="12px"
                                                            ForeColor="White" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmUnit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BOQ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvQty" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                Width="69px" CssClass="style101"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sch.Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                Width="65px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAmount" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvAmountFooter" runat="server" Width="87px" Font-Size="12px"
                                                                ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">&nbsp;</td>
                                        <td class="style34">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style36">&nbsp;</td>
                                        <td class="style107">&nbsp;</td>
                                        <td class="style143">&nbsp;</td>
                                        <td class="style144">&nbsp;</td>
                                        <td class="style145">&nbsp;</td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View3Item" runat="server">
                                <table style="width: 265px; margin-right: 0px; background-color: #999966;">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33" colspan="3">
                                            <asp:Label ID="lblTitle1a" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="22px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: left"
                                                Text="Item Selection (Individual Floor)" Width="418px"></asp:Label>
                                        </td>
                                        <td class="style36">
                                            <asp:Label ID="lblItem7" runat="server" Font-Size="14px" Font-Underline="False"
                                                Style="font-weight: 700; text-align: right" Text="Floor :" Width="62px"></asp:Label>
                                        </td>
                                        <td class="style36">
                                            <asp:DropDownList ID="ddlFloorList" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="145px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblFloorName" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="18px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: center; text-transform: capitalize"
                                                Text="Floor Name" Visible="False" Width="150px"></asp:Label>
                                        </td>
                                        <td class="style149">
                                            <asp:LinkButton ID="lbtnSelectFloor" runat="server" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtnSelectFloor_Click" Style="text-align: center"
                                                Width="67px">Select Floor</asp:LinkButton>
                                        </td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">&nbsp;</td>
                                        <td class="style34">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblItem2" runat="server" CssClass="newStyle3" Font-Size="14px"
                                                Font-Underline="True" Style="font-weight: 700" Text="Description of Items"
                                                Width="313px" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True" Font-Bold="True"
                                                Font-Size="12px" OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy"
                                                TextAlign="Left" Visible="False" Width="50px" />
                                        </td>
                                        <td class="style151">
                                            <asp:DropDownList ID="ddlFloorListToCopy" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize"
                                                Visible="False" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style149">
                                            <asp:LinkButton ID="lbtnCopyData" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="19px" OnClick="lbtnCopyData_Click"
                                                Style="text-align: center;" Visible="False" Width="67px">Copy Data</asp:LinkButton>
                                        </td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">
                                            <asp:TextBox ID="txtItemSearch2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Height="18px" Width="66px" Visible="False"></asp:TextBox>
                                        </td>
                                        <td class="style34">
                                            <asp:ImageButton ID="ImgbtnFindItem2" runat="server" Height="19px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindItem_Click"
                                                Width="16px" Visible="False" />
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="ddlItem2" runat="server" CssClass="newStyle1"
                                                Font-Bold="True" Font-Size="11px" Height="21px" Width="540px"
                                                Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style149">
                                            <asp:LinkButton ID="lbtnSelectItem2" runat="server" Font-Bold="True"
                                                Font-Size="12px" Style="text-align: center" Visible="False" Width="67px"
                                                OnClick="lbtnSelectItem2_Click">Select Item</asp:LinkButton>
                                        </td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33" colspan="10">
                                            <asp:GridView ID="gvAnalysis2" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False"
                                                OnPageIndexChanging="gvAnalysis2_PageIndexChanging" PageSize="15" ShowFooter="True"
                                                Width="16px" Visible="False">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmUnit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sch.Sl.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvItmSlNo" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Style="background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmslno")) %>'
                                                                Width="52px" Font-Size="12px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sch.Item No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvItmSChNo" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Style="background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmschno")) %>'
                                                                Width="100px" Font-Size="12px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnFinalUpdate2" runat="server" Font-Bold="True"
                                                                Font-Size="14px" ForeColor="Yellow" OnClick="lbtnFinalUpdate2_Click"
                                                                Style="text-align: center" Width="100px">Final Update</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" ForeColor="#FFFF66" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sch.Qty">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvQty" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="12px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sch.Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRate" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="12px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnTotal2" runat="server" Font-Bold="True"
                                                                Font-Size="14px" ForeColor="Yellow" OnClick="lbtnTotal2_Click"
                                                                Style="text-align: center" Width="50px">Total :</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" ForeColor="Yellow" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAmount" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="75px" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvAmountFooter" runat="server" Font-Size="12px"
                                                                ForeColor="White" Width="87px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style33">&nbsp;</td>
                                        <td class="style34">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style151">&nbsp;</td>
                                        <td class="style149">&nbsp;</td>
                                        <td class="style129">&nbsp;</td>
                                        <td class="style128">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                        <td class="style113">&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View4Resource" runat="server">
                                <table style="width: 62%; background-color: #FFCCCC;">
                                    <tr>
                                        <td class="style134">&nbsp;</td>
                                        <td class="style154">
                                            <asp:Label ID="lblTitle2" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="22px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: left"
                                                Text="Resource Rate Input &amp; Report" Width="303px"></asp:Label>
                                        </td>
                                        <td class="style155">&nbsp;</td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td class="style174">
                                            <asp:TextBox ID="txtSearchItem" runat="server" BorderStyle="None" Width="66px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnSelectFloorRes" runat="server" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtnSelectFloorRes_Click" Style="text-align: center"
                                                Width="50px">Show</asp:LinkButton>
                                        </td>
                                        <td class="style147">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style134">&nbsp;</td>
                                        <td colspan="6">
                                            <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" OnPageIndexChanging="gvResInfo_PageIndexChanging"
                                                PageSize="20" ShowFooter="True" Width="16px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResCod" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Floor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfloordes" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Resource">
                                                        <FooterTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td class="style173">&nbsp;</td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lbtnUpdateResRate" runat="server" Font-Bold="True"
                                                                            Font-Size="14px" OnClick="lbtnUpdateResRate_Click"
                                                                            Style="text-align: center; height: 17px;" Width="80px">Update Rate</asp:LinkButton>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Est.Qty">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnSameValue" runat="server" Font-Bold="True"
                                                                Font-Size="14px" OnClick="lbtnSameValue_Click" Style="text-align: center;"
                                                                Width="85px">Put Same Val</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResQty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="85px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Res. Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnResTotal" runat="server" Font-Bold="True"
                                                                Font-Size="14px" OnClick="lbtnResTotal_Click" Style="text-align: center;"
                                                                Width="50px">Total :</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTResAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvTResAmtFooter" runat="server" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style134">&nbsp;</td>
                                        <td class="style154">&nbsp;</td>
                                        <td class="style155">&nbsp;</td>
                                        <td class="style146">&nbsp;</td>
                                        <td class="style174">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style147">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View5Adjustment" runat="server">
                                <table style="width: 100%; background-color: #CCCCFF;">
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblTitle3" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="22px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: left"
                                                Text="Analysis Adjustment"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textBoxStyleT"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View6Report" runat="server">
                                <table style="width: 100%; background-color: #CCCCFF;">
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td class="style158" colspan="2">
                                            <asp:Label ID="lblTitle4" runat="server" BackColor="Blue" Font-Bold="True"
                                                Font-Size="22px" ForeColor="Yellow"
                                                Style="font-weight: 700; color: #FFFF66; text-align: left"
                                                Text="Analysis Reports" Width="187px"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True"
                                                Font-Bold="True" Font-Size="16px" Height="25px"
                                                OnSelectedIndexChanged="ddlReports_SelectedIndexChanged"
                                                Style="text-transform: capitalize" Width="240px">
                                                <asp:ListItem>Resource Basis</asp:ListItem>
                                                <asp:ListItem>Work Basis</asp:ListItem>
                                                <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                                <asp:ListItem>Individual Work Basis</asp:ListItem>
                                                <asp:ListItem>Analysis Sheet</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkPunchValues" runat="server" Font-Bold="True"
                                                Font-Size="12px" Text="Including Punch Value" Width="145px" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRptGroup" runat="server" Font-Size="12px"
                                                Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                Text="Group :" Width="72px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="100px">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Subs-2</asp:ListItem>
                                                <asp:ListItem>Subs-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblRptResBreak" runat="server" Font-Size="12px"
                                                Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                Text="Res. Group :" Visible="False" Width="70px"></asp:Label>
                                        </td>
                                        <td class="style158">
                                            <asp:DropDownList ID="ddlRptResBreak" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="21px"
                                                Style="text-transform: capitalize; margin-left: 0px;" Visible="False"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style159">
                                            <asp:CheckBox ID="ChkIgnoreSchRate" runat="server" Font-Bold="True"
                                                Font-Size="12px" Text="Ignore Sch. Rate" Visible="False" Width="110px" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkMKSUnit" runat="server" Font-Bold="True" Font-Size="12px"
                                                Text="Consider MKS Unit" Visible="False" Width="130px" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkAdditionalCost" runat="server" Font-Bold="True"
                                                Font-Size="12px" Text="Incl. Additional Cost" Width="150px" />
                                            <asp:CheckBox ID="ChkOnSchiNo" runat="server" Font-Bold="True" Font-Size="12px"
                                                Text="Order on Sch. Item" Visible="False" Width="150px" />
                                        </td>
                                        <td class="style156">
                                            <asp:Label ID="lblRptMainGroup" runat="server" Font-Size="12px"
                                                Font-Underline="False" Style="font-weight: 700; text-align: right"
                                                Text="Main Group :" Width="72px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRptMainGroup" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblItem10" runat="server" Font-Size="14px"
                                                Font-Underline="False" Height="16px" Style="font-weight: 700; text-align: right"
                                                Text="Floor :" Width="70px"></asp:Label>
                                        </td>
                                        <td class="style158">
                                            <asp:DropDownList ID="ddlFloorListRpt" runat="server" Font-Bold="True"
                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style159" colspan="5">

                                            <asp:Panel ID="PnlRptItmList" runat="server" Style="background-color: #999966"
                                                Visible="False">
                                                <table style="width: 54%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblItem9" runat="server" CssClass="newStyle3" Font-Size="14px"
                                                                Font-Underline="False" Style="font-weight: 700; text-align: right;"
                                                                Text="Item :" Width="46px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRptItemSearch" runat="server" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" Width="66px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgbtnRptFindItem" runat="server" Height="19px"
                                                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnRptFindItem_Click"
                                                                Width="16px" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRptItem" runat="server" CssClass="newStyle1"
                                                                Font-Bold="True" Font-Size="11px" Height="21px" Width="450px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="PnlRptResList" runat="server" Style="background-color: #999966"
                                                Visible="False">
                                                <table style="width: 60%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblItem12" runat="server" CssClass="newStyle3" Font-Size="14px"
                                                                Font-Underline="False" Style="font-weight: 700; text-align: right;"
                                                                Text="Res.:" Width="46px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRptResSearch" runat="server" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" Width="66px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgbtnRptFindRes" runat="server" Height="19px"
                                                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnRptFindRes_Click"
                                                                Width="16px" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRptRes" runat="server" CssClass="newStyle1"
                                                                Font-Bold="True" Font-Size="11px" Height="21px" Width="450px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style158">&nbsp;</td>
                                        <td class="style159">&nbsp;</td>
                                        <td class="style157">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style156">&nbsp;</td>
                                        <td>
                                            <asp:LinkButton ID="lbtnShowReport" runat="server" Font-Bold="True"
                                                Font-Size="12px" OnClick="lbtnShowReport_Click"
                                                Style="text-align: center;" Width="80px">Show Report</asp:LinkButton>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style132">&nbsp;</td>
                                        <td colspan="7">
                                            <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False"
                                                BackColor="#FFCCFF" ShowFooter="True" Width="800px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Floor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="true" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                                        <FooterTemplate>
                                                            <table style="width: 10%; height: 48px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Style="text-align: right" Text="Total Cost:" Width="110px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Style="text-align: right" Text="Construction Area:" Width="110px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Style="text-align: right" Text="Cost Per SFT:" Width="110px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <FooterTemplate>
                                                            <table style="width: 10%; height: 48px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True"
                                                                            Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblvalConArea" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblvalCostPsft" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0;(#,##0);-") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle Font-Bold="true" Font-Size="12px" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage">
                                                        <FooterTemplate>
                                                            <table style="width: 10%; height: 48px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Style="text-align: right" Width="80px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPer" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#99CCFF" BorderColor="#660033" BorderStyle="Solid"
                                                    BorderWidth="1px" />
                                                <HeaderStyle BackColor="#FFFFCC" BorderColor="#660033" BorderStyle="Solid"
                                                    Font-Size="12px" ForeColor="#660033" />
                                                <AlternatingRowStyle BackColor="#CFCFB8" />
                                            </asp:GridView>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>

                                </table>
                            </asp:View>
                            <asp:View ID="SpReport" runat="server">
                                <table style="width: 100%;">

                                    <tr>
                                        <td colspan="8">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table style="width: 100%;">

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td class="style137">
                                                            <asp:Label ID="Label1" runat="server" Font-Size="12px" Font-Underline="False"
                                                                ForeColor="White" Style="font-weight: 700; text-align: right" Text="Floor :"
                                                                Width="70px"></asp:Label>
                                                        </td>
                                                        <td class="style161">
                                                            <asp:DropDownList ID="ddlFlrlstspr" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="120px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style171">
                                                            <asp:Label ID="Label3" runat="server" Font-Size="12px" Font-Underline="False"
                                                                ForeColor="White" Style="font-weight: 700; text-align: right" Text="Work :"
                                                                Width="60px"></asp:Label>
                                                        </td>
                                                        <td class="style169">
                                                            <asp:DropDownList ID="ddlRptWork" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Height="21px"
                                                                Style="text-transform: capitalize" Width="120px">
                                                                <asp:ListItem Selected="True">All Work</asp:ListItem>
                                                                <asp:ListItem>Civil</asp:ListItem>
                                                                <asp:ListItem>Sanitary</asp:ListItem>
                                                                <asp:ListItem>Electrical</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style172">

                                                            <asp:Label ID="Label2" runat="server" Font-Size="12px" Font-Underline="False"
                                                                ForeColor="White" Style="font-weight: 700; text-align: right" Text="Group :"
                                                                Width="70px"></asp:Label>
                                                        </td>

                                                        <td class="style173">
                                                            <asp:DropDownList ID="ddlRptGroupspr" runat="server" Font-Bold="True"
                                                                Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="100px">
                                                                <asp:ListItem>Main</asp:ListItem>
                                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnShowSpRpt" runat="server" BackColor="#003366"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="White" OnClick="lbtnShowSpRpt_Click">Show</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td class="style137">
                                                            <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="color: #FFFFFF; text-align: right;" Text=" Page Size:" Width="70px"></asp:Label>
                                                        </td>
                                                        <td class="style161">
                                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                                BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="100px">
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
                                                        </td>
                                                        <td class="style171">&nbsp;</td>
                                                        <td class="style168">&nbsp;</td>
                                                        <td class="style172">&nbsp;</td>

                                                        <td class="style173">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:GridView ID="gvSpRpt" runat="server" AllowPaging="True"
                                                AutoGenerateColumns="False" BackColor="#FFCCFF"
                                                OnPageIndexChanging="gvSpRpt_PageIndexChanging" ShowFooter="True" Width="847px">
                                                <PagerSettings Position="Top" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Floor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptFlr2" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptRes2" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptUnit2" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptQty2" runat="server" Font-Bold="true" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bgd. Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptRat2" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRptAmt2" runat="server" Font-Bold="False" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPer0" runat="server" Font-Bold="true" Font-Size="12px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFPer" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#99CCFF" BorderColor="#660033" BorderStyle="Solid"
                                                    BorderWidth="1px" />
                                                <HeaderStyle BackColor="#FFFFCC" BorderColor="#660033" BorderStyle="Solid"
                                                    Font-Size="12px" ForeColor="#660033" />
                                                <AlternatingRowStyle BackColor="#CFCFB8" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style162">&nbsp;</td>
                                        <td class="style163">&nbsp;</td>
                                        <td class="style164">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style165">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style162">&nbsp;</td>
                                        <td class="style163">&nbsp;</td>
                                        <td class="style164">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style165">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="style162">&nbsp;</td>
                                        <td class="style163">&nbsp;</td>
                                        <td class="style164">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="style165">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                    <td class="style112">&nbsp;</td>
                    <td class="style111">&nbsp;</td>
                </tr>

            </table>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

