<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BgdPrjAna02.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdPrjAna02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $("#divGridView able tbody tr").mouseover(function () {

                $(this).addClass("highlightRow");

            }).mouseout(function () { $(this).removeClass('highlightRow'); })
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
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblItem5" runat="server" CssClass="lblTxt lblName" Text="Project/Unit"></asp:Label>
                                        <asp:Label ID="lblItem6" runat="server" CssClass="smLbl_to">:</asp:Label>
                                        <asp:Label ID="lblProjectDesc2" runat="server" CssClass="smLbl_to">To</asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="ddlPage" Width="400"></asp:DropDownList>
                                        <asp:Label ID="lblProjectDesc" runat="server" CssClass="smLbl_to" Visible="False"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk1" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk1_Click" Width="80">Select P/U</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" BackColor="#BBBB99"
                                            BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" RepeatColumns="6" Visible="False"
                                            Width="769px" Font-Size="14px" RepeatDirection="Horizontal">
                                            <asp:ListItem>Floor Selection</asp:ListItem>
                                            <asp:ListItem>Item Selection(All flr)</asp:ListItem>
                                            <asp:ListItem>Item Selction(Ind.flr)</asp:ListItem>
                                            <asp:ListItem>Rate Input</asp:ListItem>
                                            <asp:ListItem>Reports</asp:ListItem>
                                            <asp:ListItem>Special Report</asp:ListItem>
                                        </asp:RadioButtonList>

                                        <asp:CheckBox ID="ChkCopyProject" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Size="12px" OnCheckedChanged="ChkCopyProject_CheckedChanged" 
                                            Text="Copy" Visible="False" Width="60px" />
                                    </div>
                                </div>
                                <asp:Panel ID="PnlCopyProject" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                    BorderWidth="1px" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px">
                                            <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="From Project:"></asp:Label>
                                            <asp:TextBox ID="txtSrcCopyPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnCopyFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnCopyFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlCopyProjectName" runat="server" CssClass="ddlPage" Width="400"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnCopyProject" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnCopyProject_Click" Width="80">Copy Data</asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="View1Floor" runat="server">
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblFPhaseTitle" runat="server" CssClass="  lblName" Text="Floor Selection" Style="float:left !important; color:#000; font-weight:800;" Width="100"></asp:Label>
                                        <asp:CheckBox ID="chkFlrShowSelected" runat="server" AutoPostBack="True" Font-Bold="True" CssClass="checkbox" OnCheckedChanged="chkFlrShowSelected_CheckedChanged" 
                                            Text="Show selected floor(s) only" />
                                        <asp:CheckBoxList ID="cbListFloor" runat="server" CellPadding="2" CellSpacing="8" 
                                            RepeatColumns="7" CssClass="StyleCheckBoxList" Font-Size="12px" BorderColor="#999"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Width="912px"  style="margin-left:5px !important; color:#000 !important">
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
                                </div>
                            </asp:View>
                            <asp:View ID="View2Item" runat="server">
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblTitle1" runat="server" CssClass="lblTxt lblName" Text="Item Selection (All Floor)" Width="200"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName" Text="Description of Items" Width="313px"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:TextBox ID="txtItemSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindItem" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindItem_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="ddlPage" Width="400"></asp:DropDownList>
                                        <asp:LinkButton ID="lbtnSelectItem" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk1_Click" Width="80">Select Item</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="from-group">
                                    <div id="divGridView">
                                        <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            OnPageIndexChanging="gvAnalysis_PageIndexChanging" OnRowCancelingEdit="gvAnalysis_RowCancelingEdit"
                                            OnRowDataBound="gvAnalysis_RowDataBound" OnRowEditing="gvAnalysis_RowEditing"
                                            OnRowUpdating="gvAnalysis_RowUpdating" PageSize="15" Width="76px" ShowFooter="True"
                                            OnRowDeleting="gvAnalysis_RowDeleting">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Work Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvwrkcode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkcode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description of Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")) %>'
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <table style="width: 100%; background-color: #C4D6B1;">
                                                            <tr>
                                                                <td colspan="3" style="text-align: left">
                                                                    <asp:Label ID="lblgvItmDesc_e" runat="server" Font-Bold="True" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")).Trim() %>'
                                                                        Width="350px" BackColor="Maroon" ForeColor="White" Font-Size="14px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:GridView ID="gvgvFloorAna" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                                        Width="200px" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" ShowFooter="True"
                                                                        Height="16px" Style="text-align: left" OnRowDataBound="gvgvFloorAna_RowDataBound">

                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Floor Code" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgvFlrCod" runat="server" Font-Bold="False" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                                                        Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Floor Desc">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvgvFlrDesc" runat="server" CssClass="StyleCheckBoxList" Font-Bold="False"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                                                        Width="80px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                                <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Ref. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgvgvItmRef" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                                        BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem, "itmrefno").ToString() %>'
                                                                                        Width="80px" Font-Size="12px" Style="text-align: left"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:LinkButton ID="lbtngvgvRefresh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                        OnClick="lbtngvgvRefresh_Click">Total</asp:LinkButton>
                                                                                </FooterTemplate>
                                                                                <FooterStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Budgeted Qty.">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgvgvQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                                        BorderWidth="1px" Font-Size="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                                        Width="80px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvgvQtyFooter" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                        Style="text-align: right" Width="85px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <FooterStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                                <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                                                <ItemStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Budgeted Qty.">
                                                                                <FooterTemplate>
                                                                                    <asp:LinkButton ID="lbtngvgvRefresh02" runat="server" Font-Bold="True"
                                                                                        Font-Size="12px" OnClick="lbtngvgvRefresh02_Click">Refresh</asp:LinkButton>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="hlnkgvgvqty02" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                                                        Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                                                                        Font-Underline="false" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.00);")%>'
                                                                                        Width="70px" Target="_blank"></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;&nbsp;
                                                                </td>
                                                                <td style="text-align: right">&nbsp;&nbsp;
                                                                </td>
                                                                <td>&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EditItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budgeted Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                            Width="100px" CssClass="style101"></asp:Label>
                                                    </ItemTemplate>
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
                                </div>

                            </asp:View>
                            <asp:View ID="View3Item" runat="server">
                                <div class="form-group">
                                    <div class="col-md-12  pading5px">
                                        <asp:Label ID="lblTitle1a" runat="server" CssClass="lblTxt lblName" Text="Item Selection (Individual Floor)" Width="400"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Fioor: "></asp:Label>
                                        <asp:DropDownList ID="ddlFloorList" runat="server" CssClass="ddlPage" Width="400"></asp:DropDownList>
                                        <asp:Label ID="lblFloorName" runat="server" Text="Floor Name" CssClass="lblTxt lblName" Visible="False" Width="150px"></asp:Label>
                                        <asp:LinkButton ID="lbtnSelectFloor" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelectFloor_Click" Width="80">Select Floor</asp:LinkButton>
                                         <asp:Label ID="lblProjectLock" runat="server" Visible="False" CssClass="lblTxt lblName"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblItem2" runat="server" CssClass="lblTxt lblName" Text="Description of Items"></asp:Label>
                                        <asp:CheckBox ID="ChkCopy" runat="server" AutoPostBack="True" CssClass="checkbox" OnCheckedChanged="ChkCopy_CheckedChanged" Text="Copy" Visible="False" Width="50px" />
                                        <asp:DropDownList ID="ddlFloorListToCopy" runat="server" CssClass="ddlPage" Visible="False" Width="145px">
                                        </asp:DropDownList>

                                        <asp:LinkButton ID="lbtnCopyData" runat="server" OnClick="lbtnCopyData_Click" CssClass="btn btn-primary  okBtn" Visible="False" Width="67px">Copy Data</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:TextBox ID="txtItemSearch2" runat="server" Visible="false" CssClass="inputTxt inputName "></asp:TextBox>
                                        <asp:ImageButton ID="ImgbtnFindItem2" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                                OnClick="ImgbtnFindItem_Click" Width="16px" Visible="False" />

                                        
                                        <asp:DropDownList ID="ddlItem2" runat="server" Visible="false" CssClass="ddlPage" Width="400"></asp:DropDownList>
                                        <asp:LinkButton ID="lbtnSelectItem2" runat="server" Visible="false" CssClass="btn btn-primary okBtn" OnClick="lbtnSelectItem2_Click" Width="80">Select Item</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:GridView ID="gvAnalysis2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            OnPageIndexChanging="gvAnalysis2_PageIndexChanging" PageSize="15" ShowFooter="True"
                                            Width="16px" Visible="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref. No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvItmRef" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="12px" Style="background-color: Transparent; text-align: left;"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "itmrefno").ToString() %>' Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmDesc" runat="server" Font-Bold="True" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc1")) %>'
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budgeted Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Style="background-color: Transparent; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                            Width="80px" Font-Size="12px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnFinalUpdate2" runat="server" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="Yellow" OnClick="lbtnFinalUpdate2_Click" Style="text-align: center"
                                                            Width="100px">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
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
                                </div>

                            </asp:View>
                            <asp:View ID="View4Resource" runat="server">
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblTitle2" runat="server" CssClass="lblTxt lblName" Text="Resource Rate Input &amp; Report" Width="300"></asp:Label>
                                        <asp:TextBox ID="txtSearchItem" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnSelectFloorRes" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelectFloorRes_Click">Show</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvResInfo_PageIndexChanging" PageSize="20" Width="16px"
                                        ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor">
                                                <FooterTemplate>
                                                    <asp:CheckBox ID="chkProjectLock" runat="server" Font-Size="12px" OnCheckedChanged="chklkrate_CheckedChanged"
                                                        Text="Project Lock" Width="90px" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvfloordes" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Resource">
                                                <FooterTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td class="style173">
                                                                <asp:CheckBox ID="chklkrate" runat="server" AutoPostBack="True" OnCheckedChanged="chklkrate_CheckedChanged"
                                                                    Text="Lock Rate" />
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lbtnUpdateResRate" runat="server" Font-Bold="True" Font-Size="14px"
                                                                    OnClick="lbtnUpdateResRate_Click" Style="text-align: center; height: 17px;" Width="80px">Update Rate</asp:LinkButton>
                                                            </td>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Est.Qty">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnSameValue" runat="server" Font-Bold="True" Font-Size="14px"
                                                        OnClick="lbtnSameValue_Click" Style="text-align: center;" Width="85px">Put Same Val</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="85px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res. Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF" Font-Size="11px"
                                                        BorderStyle="Solid" BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnResTotal" runat="server" Font-Bold="True" Font-Size="14px"
                                                        OnClick="lbtnResTotal_Click" Style="text-align: center;" Width="50px">Total :</asp:LinkButton>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTResAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tresamt")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                </div>
                            </asp:View>
                            <asp:View ID="View5Report" runat="server">
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblTitle4" runat="server" CssClass="lblTxt lblName" Text="Analysis Reports"></asp:Label>

                                        <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged" CssClass="ddlPage" Width="237px">
                                            <asp:ListItem>Resource Basis</asp:ListItem>
                                            <asp:ListItem>Work Basis</asp:ListItem>
                                            <asp:ListItem>Individual Resource Basis</asp:ListItem>
                                            <asp:ListItem>Individual Work Basis</asp:ListItem>
                                            <asp:ListItem>Analysis Sheet</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:CheckBox ID="ChkPunchValues" runat="server" CssClass="checkbox" Text="Including Punch Value" Width="145px" />

                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage" Width="100px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblRptResBreak" runat="server" Text="Res. Group :" Visible="False" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlRptResBreak" runat="server" CssClass="ddlPage" Width="120" Visible="false"></asp:DropDownList>


                                        <asp:CheckBox ID="ChkIgnoreSchRate" runat="server" CssClass="checkbox" Visible="false" Text="Ignore Sch. Rate" Width="145px" />
                                        <asp:CheckBox ID="ChkMKSUnit" runat="server" CssClass="checkbox" Text="Consider MKS Unit"
                                            Visible="False" Width="130px" />


                                        <asp:CheckBox ID="ChkAdditionalCost" runat="server" CssClass="checkbox" Text="Incl. Additional Cost" Width="150px" />
                                        <asp:CheckBox ID="ChkOnSchiNo" runat="server" CssClass="checkbox" Text="Order on Sch. Item"
                                            Visible="False" Width="130px" />

                                        <asp:DropDownList ID="ddlRptMainGroup" runat="server" CssClass="ddlPage" Width="100"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10  pading5px">
                                        <asp:Label ID="lblFloor" runat="server" Text="Floor :" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" CssClass="ddlPage" Width="120"></asp:DropDownList>

                                        <asp:Panel ID="PnlRptItmList" runat="server" Visible="False">
                                            <asp:Label ID="lblItem9" runat="server" CssClass="lblTxt lblName" Text="Item: "></asp:Label>
                                            <asp:TextBox ID="txtRptItemSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnRptFindItem" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnRptFindItem_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlRptItem" runat="server" CssClass="ddlPage" Width="400"></asp:DropDownList>
                                        </asp:Panel>

                                        <asp:Panel ID="PnlRptResList" runat="server" runat="server" Visible="False">
                                            <asp:Label ID="lblItem12" runat="server" CssClass="lblTxt lblName" Text="Res.: "></asp:Label>
                                            <asp:TextBox ID="txtRptResSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnRptFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnRptFindRes_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlRptRes" runat="server" CssClass="ddlPage" Width="400"></asp:DropDownList>
                                        </asp:Panel>
                                        <asp:LinkButton ID="lbtnShowReport" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShowReport_Click" Width="80">Show Report</asp:LinkButton>


                                    </div>
                                </div>
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" Width="847px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        BackColor="#FFCCFF" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Floor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptFlr1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                        Width="120px" Font-Bold="False" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description">
                                                <FooterTemplate>
                                                    <table style="width: 10%; height: 48px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                                    Text="Total Cost:" Width="110px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblConArea" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                                    Text="Construction Area:" Width="110px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCostPsft" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                                    Text="Cost Per SFT:" Width="110px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <FooterTemplate>
                                                    <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bgd. Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
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
                                                                <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblvalConArea" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblvalCostPsft" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
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
                                                                <asp:Label ID="lblgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                                    Width="80px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPer" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
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
                            </asp:View>
                            <asp:View ID="SpReport" runat="server">
                                  <asp:Panel ID="Panel1" runat="server">
                                      <div class="form-group">
                                             <asp:Label ID="lblTitle5" runat="server" CssClass="lblTxt lblName" Text="Special Reports" ></asp:Label>
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"  Text="Floor :"></asp:Label>
                                        
                                            <asp:DropDownList ID="ddlFlrlstspr" runat="server" CssClass="ddlPage" ></asp:DropDownList>
                                           <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Group :"></asp:Label>
                                      <asp:DropDownList ID="ddlRptGroupspr" runat="server"  CssClass="ddlPage">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtnShowSpRpt" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShowSpRpt_Click">Show</asp:LinkButton>
                                      </div>
                                      <div class="form-group">
                                             <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Work: " ></asp:Label>
                                             <asp:DropDownList ID="ddlRptWork" runat="server"   CssClass="ddlPage">
                                                <asp:ListItem Selected="True">All Work</asp:ListItem>
                                                <asp:ListItem>Civil</asp:ListItem>
                                                <asp:ListItem>Sanitary</asp:ListItem>
                                                <asp:ListItem>Electrical</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"  Text="Size :"></asp:Label>
                                         <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"  >
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

                                      <div class=" table  table-responsive">
                                          <asp:GridView ID="gvSpRpt" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                BackColor="#FFCCFF" OnPageIndexChanging="gvSpRpt_PageIndexChanging" ShowFooter="True"
                                Width="847px">
                                <PagerSettings Position="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptFlr2" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptRes2" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptUnit2" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptQty2" runat="server" Font-Bold="true" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bgd. Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptRat2" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptAmt2" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPer0" runat="server" Font-Bold="true" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPer" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                                      </div>
                                  </asp:Panel>
                            </asp:View>


                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

