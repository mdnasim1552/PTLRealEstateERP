<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryLostSoldADes.aspx.cs" Inherits="RealERPWEB.F_13_Cen.EntryLostSoldADes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {
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
                                <asp:Panel ID="pnlMain" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-5  pading5px  asitCol10">

                                            <asp:Label ID="Label15" runat="server" CssClass=" lblName lblTxt" Text="Project:"></asp:Label>

                                            <asp:TextBox ID="txtsrchProject" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProject" runat="server" Width="280px" CssClass="chzn-select ddlPage"></asp:DropDownList>

                                            <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>--%>

                                            <asp:Label ID="lblddlProject" runat="server" __designer:wfdid="w4" CssClass="inputtextbox" Visible="False" Width="270px"></asp:Label>
                                        </div>
                                         <div>
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Date.:"></asp:Label>

                                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label16" runat="server" CssClass=" smLbl_to" Text="No.:"></asp:Label>

                                            <asp:Label ID="lblCurNo1" runat="server" CssClass="inputtextbox"></asp:Label>

                                            <asp:TextBox ID="txtCurNo2" runat="server" CssClass="inputtextbox">00001</asp:TextBox>

                                            <asp:Label ID="Label14" runat="server" CssClass="smLbl_to" Text="Ref. No:"></asp:Label>

                                            <asp:TextBox ID="txtrefno" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                       

                                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPreViousList" runat="server" CssClass=" lblName lblTxt" Text="Previous:"></asp:Label>

                                            <asp:TextBox ID="txtSrchPrevious" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindPrevious" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPrevious_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True" Width="280px" CssClass="ddlPage"></asp:DropDownList>

                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelSub" runat="server" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-6  pading5px  asitCol10">

                                            <asp:Label ID="lblResList" runat="server" CssClass=" lblName lblTxt" Text="Materials:"></asp:Label>

                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" Width="280px" CssClass="chzn-select ddlPage"></asp:DropDownList>

                                        </div>
                                        <div class="col-md-5">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass=" lblName lblTxt" Text="Specification:"></asp:Label>

                                            <asp:TextBox ID="txtSrchSpecification" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnSpecification" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnSpecification_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                     <div class="table table-responsive">
                         <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" Width="501px">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
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
                                Width="120px"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lblgvunit" runat="server"
                                Style="font-size: 11px; text-align: center;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" OnClick="lnktotal_Click">Total</asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />

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


                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Lost">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvlqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkupdate" runat="server"  CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sold">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvsqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>


                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Destroyed">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvdqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>


                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Amount">
                        <FooterTemplate>
                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                Width="100px"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblamt" runat="server"
                                Style="font-size: 11px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="100px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000"
                            HorizontalAlign="right" VerticalAlign="Middle" />

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

            <%--<tr>
                                    <td class="style80">
                                        <asp:Label ID="Label15" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Project:" Width="80px"></asp:Label>
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
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="2" Width="355px">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblddlProject" runat="server" __designer:wfdid="w4"
                                            BackColor="#000" Font-Bold="True" Font-Size="14px" ForeColor="Maroon"
                                            Style="font-size: 12px; text-align: left" Visible="False" Width="355px"></asp:Label>
                                    </td>
                                    <td class="style84"></td>
                                    <td class="style85"></td>
                                    <td class="style86"></td>
                                    <td class="style82"></td>
                                    <td class="style82"></td>
                                    <td class="style82"></td>
                                    <td class="style82"></td>
                                    <td class="style82"></td>
                                    <td class="style82"></td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style17">
                                        <asp:Label ID="Label8" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Date.:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style17">
                                        <asp:TextBox ID="txtCurDate" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="Label16" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="No.:"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:Label ID="lblCurNo1" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="border: 1px solid #000000; padding: 1px 4px; text-align: left; background-color: #FFFFFF;"
                                            Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurNo2" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                            Width="45px">00001</asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="Label14" runat="server" CssClass="style16" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left" Text="Ref. No:" Width="55px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtrefno" runat="server" BorderStyle="None" Width="105px"></asp:TextBox>
                                    </td>
                                    <td class="style88">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066"
                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnOk_Click"
                                            Style="text-align: center" Width="52px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style88">&nbsp;</td>
                                    <td class="style88">&nbsp;</td>
                                    <td class="style88">&nbsp;</td>
                                    <td class="style88">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td class="style30">
                                        <asp:Label ID="lblmsg1" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000"></asp:Label>
                                    </td>
                                    <td class="style31">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style17">
                                        <asp:Label ID="lblPreViousList" runat="server" CssClass="style16"
                                            Font-Bold="True" Font-Size="12px" Style="text-align: left" Text="Previous:"
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
                                            Font-Size="12px" Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="7" Width="355px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>--%>

            <%--<tr>
                                    <td class="style71">
                                        <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: left; color: #FFFFFF;" Text="Materials:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtResSearch" runat="server" BorderStyle="None"
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style77">
                                        <asp:ImageButton ID="ImgbtnFindRes" runat="server" BorderStyle="None"
                                            Height="19px" ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindRes_Click"
                                            TabIndex="16" Width="16px" />
                                    </td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True"
                                            Font-Size="12px" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged"
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="17" Width="355px">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="80px">
                                        <asp:Label ID="lblSpecification" runat="server" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: left; color: #FFFFFF;"
                                            Text="Specification:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSrchSpecification" runat="server" BorderStyle="None"
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImgbtnSpecification" runat="server" BorderStyle="None"
                                            Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnSpecification_Click" TabIndex="16" Width="16px" />
                                    </td>
                                    <td class="style53">
                                        <cc1:ListSearchExtender ID="ListSearchExt1" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlResList">
                                        </cc1:ListSearchExtender>
                                        <asp:DropDownList ID="ddlResSpcf" runat="server" Font-Size="12px"
                                            Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                            TabIndex="19" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style53">
                                        <asp:LinkButton ID="lbtnSelect" runat="server" BackColor="#000066"
                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnSelect_Click"
                                            Style="text-align: center" Width="52px">Select</asp:LinkButton>
                                    </td>
                                    <td class="style79">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style53">&nbsp;</td>
                                </tr>--%>

            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

