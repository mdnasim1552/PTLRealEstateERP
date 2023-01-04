<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="Material_Issue.aspx.cs" Inherits="RealERPWEB.F_12_Inv.Material_Issue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".DeleteClick").click(function () {
                if (!confirm("Do you want to delete")) {
                    return false;
                }
            });
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPartSmall row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="pnlMain" runat="server">
                                <div class="form-group">
                                    <div class="col-md-8  pading5px">

                                        <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox" AutoPostBack="true" OnTextChanged="txtCurDate_TextChanged" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label16" runat="server" CssClass=" smLbl_to" Text="No:"></asp:Label>

                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="inputtextbox" style="height:22px"></asp:Label>

                                        <asp:TextBox ID="txtCurNo2" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:Label ID="Label14" runat="server" CssClass="smLbl_to" Text="Ref. No:"></asp:Label>

                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        <asp:Panel ID="pnlxcel" runat="server" style="display:none;" >
                                                    <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Excel :"></asp:Label>
                                                    <div class="uploadFile">
                                                        <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />                                                                
                                                    </div>
                                            <div style="margin-left: auto; margin-right: auto ;text-align: center;">
                                                                <asp:LinkButton CssClass="btn btn-xs btn-success" OnClientClick="return confirm('Do you want to save?');" OnClick="lnkbtn_exlup_Click" ID="lnkbtn_exlup" runat="server" ><span class="fa fa-save"></span></asp:LinkButton>
                                                </div>
                                                </asp:Panel>
                                        

                                    </div>
                                    <div class="col-md-3 pull-right pading5px ">

                                        <asp:Label ID="lblPreViousList" Visible="false" runat="server" CssClass=" lblName lblTxt" Text="Previous:"></asp:Label>

                                        <asp:TextBox ID="txtSrchPrevious" Visible="false" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                        <asp:LinkButton ID="ImgbtnFindPrevious" runat="server" CssClass="btn btn-warning primaryBtn" OnClick="ImgbtnFindPrevious_Click"><span class="glyphicon glyphicon-search asitGlyp"></span> Previous:</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True"
                                            Width="200px" CssClass="ddlPage pull-right">
                                        </asp:DropDownList>



                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4  pading5px ">

                                        <asp:Label ID="Label15" runat="server" CssClass=" lblName lblTxt" Text="Store Name:"></asp:Label>
                                        <asp:DropDownList ID="ddlProject" runat="server"
                                            Width="318px" CssClass="ddlPage chzn-select">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Department"></asp:Label>

                                        <div class="ddlListPart">
                                            <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="inputTxt chzn-select" OnSelectedIndexChanged="ddlDeptCode_SelectedIndexChanged" AutoPostBack="True" Width="200px" TabIndex="3"></asp:DropDownList>
                                        </div>


                                    </div>
                                    <div class="col-md-4 asitCol4">
                                        <asp:Label ID="lblEmployee" runat="server" CssClass=" smLbl_to" Text="List:"></asp:Label>
                                        <asp:DropDownList ID="ddlEmpList" runat="server" AutoPostBack="True" Width="250px" CssClass="ddlPage chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                </div>


                            </asp:Panel>
                            <asp:Panel ID="PanelSub" runat="server" Visible="False" Style="margin: 10px 0px 0px 0px;">
                                <div class="form-group">
                                    <div class="col-md-4  pading5px ">

                                        <asp:Label ID="lblResList" runat="server" CssClass=" lblName lblTxt" Text="Indents:"></asp:Label>

                                        <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" Width="318px" CssClass="ddlPage chzn-select">
                                        </asp:DropDownList>




                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblSpecification" runat="server" CssClass=" smLbl_to" Text="Specification:"></asp:Label>
 

                                        <asp:DropDownList ID="ddlResSpcf" runat="server" Width="330px" CssClass="smDropDown inputTxt chzn-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary checkBox" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                                        <asp:Label ID="lblddlProjectTo" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">
                    <asp:GridView ID="gvIssue" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvIssue_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="501px">
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True"  ControlStyle-CssClass="btn btn-xs btn-danger DeleteClick" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="10px" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMatCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvspcfdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="180px"></asp:Label>
                                      <asp:Label ID="lblgvspcfcod" Visible="false" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 11px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                             <FooterStyle HorizontalAlign="Center" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvempname" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvstkqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock  Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvstkrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                 
                                </FooterTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Issue">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvissueqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFgvissueqty" runat="server" Style="text-align: right"
                                        Width="70px"></asp:Label>
                                </FooterTemplate>
                                  <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                    HorizontalAlign="right" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <%--         <asp:TemplateField HeaderText="Refund">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvrefundqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>


                    </asp:TemplateField>--%>




                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                        Width="100px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvamt" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                    HorizontalAlign="right" VerticalAlign="Middle" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvremarks" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="180px"></asp:TextBox>
                                </ItemTemplate>

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





            <%--<tr>
                                    <td class="style80">
                                        <asp:Label ID="Label15" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Project:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style80">
                                        <asp:TextBox ID="txtsrchProject" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style81">
                                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindProject_Click" 
                                            TabIndex="6" Width="21px" />
                                    </td>
                                    <td class="style82" colspan="9">
                                        <asp:DropDownList ID="ddlProject" runat="server" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            TabIndex="2" Width="355px">
                                        </asp:DropDownList>
                                   
                                        <asp:Label ID="lblddlProject" runat="server" __designer:wfdid="w4" 
                                            BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon" 
                                            style="FONT-SIZE: 12px; TEXT-ALIGN: left" Visible="False" Width="355px"></asp:Label>
                                    </td>
                                    <td class="style84">
                                    </td>
                                    <td class="style85">
                                    </td>
                                    <td class="style86">
                                    </td>
                                    <td class="style82">
                                        </td>
                                    <td class="style82">
                                        </td>
                                    <td class="style82">
                                        </td>
                                    <td class="style82">
                                        </td>
                                    <td class="style82">
                                        </td>
                                    <td class="style82">
                                        </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style17">
                                        <asp:Label ID="Label8" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Date.:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtCurDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="Label16" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="No.:"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:Label ID="lblCurNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: left; background-color: #FFFFFF;" 
                                            Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurNo2" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                            Width="45px">00001</asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="Label14" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Ref. No:" Width="55px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtrefno" runat="server" BorderStyle="None" Width="105px"></asp:TextBox>
                                    </td>
                                    <td class="style88">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                            style="text-align: center" Width="52px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style88">
                                        &nbsp;</td>
                                    <td class="style88">
                                        &nbsp;</td>
                                    <td class="style88">
                                        &nbsp;</td>
                                    <td class="style88">
                                        &nbsp;</td>
                                    <td class="style25">
                                        &nbsp;</td>
                                    <td class="style30">
                                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style31">
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
                                </tr>--%>
            <%--<tr>
                                    <td class="style17">
                                        <asp:Label ID="lblPreViousList" runat="server" CssClass="style16" 
                                            Font-Bold="True" Font-Size="12px" style="TEXT-ALIGN: left" Text="Previous:" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtSrchPrevious" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" TabIndex="5" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style23">
                                        <asp:ImageButton ID="ImgbtnFindPrevious" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPrevious_Click" 
                                            TabIndex="6" Width="21px" />
                                    </td>
                                    <td class="style21" colspan="12">
                                        <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True" 
                                            Font-Size="12px" Style="border-right: midnightblue 1px solid;
                                            border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid;
                                            background-color: #fffbf1" TabIndex="7" Width="355px">
                                        </asp:DropDownList>
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
                                </tr>--%>

            <%--<tr>
                                    <td class="style71">
                                        <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="TEXT-ALIGN: left; color: #FFFFFF;" Text="Materials:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtResSearch" runat="server" BorderStyle="None" 
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style77">
                                        <asp:ImageButton ID="ImgbtnFindRes" runat="server" BorderStyle="None" 
                                            Height="19px" ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindRes_Click" 
                                            TabIndex="16" Width="16px" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" 
                                            Font-Size="12px" onselectedindexchanged="ddlResList_SelectedIndexChanged" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            TabIndex="17" Width="355px">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="lblSpecification" runat="server" Font-Bold="True" 
                                            Font-Size="12px" style="TEXT-ALIGN: left; color: #FFFFFF;" 
                                            Text="Specification:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSrchSpecification" runat="server" BorderStyle="None" 
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImgbtnSpecification" runat="server" BorderStyle="None" 
                                            Height="19px" ImageUrl="~/Image/find_images.jpg" 
                                            onclick="ImgbtnSpecification_Click" TabIndex="16" Width="16px" />
                                    </td>
                                    <td class="style53">
                                        <cc1:ListSearchExtender ID="ListSearchExt1" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlResList">
                                        </cc1:ListSearchExtender>
                                        <asp:DropDownList ID="ddlResSpcf" runat="server" Font-Size="12px" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            TabIndex="19" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style53">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnSelect_Click" 
                                            style="text-align: center" Width="52px">Select</asp:LinkButton>
                                    </td>
                                    <td class="style79">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style71">
                                        <asp:Label ID="lblEmployee" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="TEXT-ALIGN: left; color: #FFFFFF;" Text="Employee List:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtSrchEmployee" runat="server" BorderStyle="None" 
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style77">
                                        <asp:ImageButton ID="ImgbtnFindEmp" runat="server" BorderStyle="None" 
                                            Height="19px" ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindEmp_Click" 
                                            TabIndex="16" Width="16px" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEmpList" runat="server" AutoPostBack="True" 
                                            Font-Size="12px" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            TabIndex="17" Width="355px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlEmpList_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlEmpList">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td width="80px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style79">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

