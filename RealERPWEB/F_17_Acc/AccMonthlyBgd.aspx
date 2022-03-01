<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccMonthlyBgd.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccMonthlyBgd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
                                    <div class="col-md-12 pading5px">

                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date" Style="display: none;"></asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox" Style="display: none;"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblMonth" runat="server" CssClass="smLbl_to" Text="Month"></asp:Label>

                                        <asp:DropDownList ID="ddlyearmon" runat="server" CssClass="smDropDown inputTxt" Width="120px"
                                            TabIndex="6">
                                        </asp:DropDownList>

                                        <asp:Label ID="lbldepartment" runat="server" CssClass="smLbl_to" Text="Department"></asp:Label>

                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="smDropDown inputTxt" Width="190px"
                                            TabIndex="6">
                                        </asp:DropDownList>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:CheckBox ID="CpyCHeck" AutoPostBack="true" OnCheckedChanged="CpyCHeck_CheckedChanged" runat="server" Text='<span class="lblTxt">Want to Budget Copy?</span>' />

                                        </div>
                                        <div class="col-md-2 " id="CopyTo" runat="server" visible="false">
                                            <asp:Label ID="Label4" runat="server" CssClass="smLbl_to" Style="font-size: 11px;" Text="To Month"></asp:Label>

                                            <asp:DropDownList ID="ddltomonth" runat="server" Width="100" CssClass="inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 " id="datediv" runat="server" visible="false">
                                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtbgddate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtbgddate"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-1" id="Copybtn" runat="server" visible="false">
                                            <asp:LinkButton ID="LbtnCopy" runat="server" CssClass="btn btn-xs btn-success" OnClick="LbtnCopy_Click" OnClientClick="return confirm('Do you agree to copy?')"><span class="glyphicon glyphicon-copy"></span> Copy </asp:LinkButton>
                                        </div>

                                    </div>

                                </div>
                                <%--<div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:LinkButton ID="lbtnPrevBudget" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevBudget_Click">Prev. Budget</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevBgdList" runat="server" CssClass="inputTxt  inpPixedWidth">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Page Size" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="droppagesixze" Visible="False" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg02" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server"></asp:Label>

                                        </div>
                                    </div>
                                </div>--%>
                                <div class="col-md-3 pading5px asitCol3">
                                </div>
                                <%--  <div class="col-md-2 pading5px asitCol2">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                </div>--%>
                                <div class="col-md-3 pading5px">
                                    <div class="msgHandSt">
                                        <asp:Label ID="lblmsg02" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        <%--  <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server"></asp:Label>--%>
                                    </div>
                                </div>
                            </div>

                        </fieldset>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="MainCode" runat="server">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lblacccode1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Accounts Code"></asp:Label>
                                            <div class="col-md-4 pading5px">

                                                <asp:TextBox ID="txtFilter" runat="server" CssClass=" inpPixedWidth inputTxt"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtnAccCode" runat="server" OnClick="ibtnAccCode_Click" CssClass="btn btn-primary srearchBtn" Visible="True"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                </div>


                                            </div>


                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowCommand="dgv2_RowCommand"
                                        OnRowCreated="dgv2_RowCreated" PagerSettings-Position="Bottom" PagerSettings-Visible="false" PagerStyle-HorizontalAlign="Center" PageSize="500" RowStyle-Font-Size="12px" Width="400px"
                                        ShowFooter="True">
                                        <PagerSettings Visible="False" />
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle />
                                                <ItemStyle />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ActCode" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Head of Accounts" Width="320px"></asp:Label>
                                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                    </asp:HyperLink>
                                                </HeaderTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="dgv2ddlPageNo" runat="server" AutoPostBack="True" CssClass="inputTxt pageDropdwn" Style="font-size: 12px; padding: 0 12px;"
                                                        OnSelectedIndexChanged="dgv2ddlPageNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="350px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level" ItemStyle-HorizontalAlign="Center">
                                                <%--  <FooterTemplate>
                                                    <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total :</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                                        Width="50px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Font-Size="14px" ForeColor="#155273" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr.Amount">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvDrAmt" runat="server" Style="background: none; border-style: none; text-align: right;" ReadOnly="True" Width="80px"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDrAmt" runat="server" MaxLength="15" Style="background: none; border-style: none; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr.Amount">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvCrAmt" runat="server" Style="background: none; border-style: none; text-align: right;" ReadOnly="True" Width="80px"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvCrAmt" runat="server" MaxLength="15" Style="background: none; border-style: none; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                                </ItemTemplate>
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

                            <asp:View ID="DetailsCode" runat="server">

                                <asp:Panel ID="Panel3" runat="server">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lblacccode" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Accounts Code:"></asp:Label>
                                            <div class="col-md-6 pading5px">
                                                <asp:TextBox ID="txtActcode" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Resource Code"></asp:Label>                                          <div class="col-md-4 pading5px">

                                                <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inpPixedWidth inputTxt"></asp:TextBox>
                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtnDetailsCode" runat="server" OnClick="ibtnDetailsCode_Click" CssClass="btn btn-primary srearchBtn" Visible="True"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                </div>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page"></asp:Label>
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                                    TabIndex="2" CssClass=" ddlPage">
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
                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkSubmit_Click">Back</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                </asp:Panel>
                                <div class="table-responsive">
                                    <asp:GridView ID="dgv3" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="150px">
                                        <PagerSettings Position="TopAndBottom" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblrescode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Res.Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblResDesc" runat="server" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' Width="350px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblresunit" runat="server" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" OnClick="lnkbtnUpdateRes_Click" Text="Upate" CssClass=" btn  btn-danger  primarygrdBtn"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:TextBox>
                                                </ItemTemplate>



                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="gvlnkFTotal" runat="server" OnClick="gvlnkFTotal_Click" Text="Total" CssClass="btn btn-primary  primarygrdBtn"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None"
                                                        Font-Bold="True" Font-Size="11px" ForeColor="black" ReadOnly="True" Style="text-align: right" Width="80px"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                                FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                        Font-Bold="True" Font-Size="11px" ForeColor="black" Width="80px"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
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

                        </asp:MultiView>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });


        function pageLoaded() {
            try {



                $('#<%=this.dgv2.ClientID%>').Scrollable();
                $('#<%=this.dgv3.ClientID%>').Scrollable();


            }


            catch (e) {

                alert(e);
            }


        }


    </script>



</asp:Content>


